using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
//using OfficeOpenXml;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Identity;

namespace ErezAPI.Data.Controllers
{
    [Produces("application/json")]
    [Route("api/RegistryList")]
    public class RegistryListController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private UserManager<ActivityUser> _userManager;
        private AgentsActionsContext _repository;
        private IRepository _repo;

        public RegistryListController(IHostingEnvironment hostingEnvironment, UserManager<ActivityUser> userManager, AgentsActionsContext repository,
            IRepository repo)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _repository = repository;
            _repo = repo;
        }

        [HttpPost]
        [Route("Import")]
        public async Task<ActionResult> OnPostImportAsync()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            List<LoginViewModel> UsersList = new List<LoginViewModel>();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    string[] arr = new string[2];

                    //string emailCol = null;
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        arr[j] = cell.ToString();


                    }
                    UsersList.Add(new LoginViewModel { UserName = arr[0], Email = arr[0], Password = arr[1] });
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                arr[j] = row.GetCell(j).ToString();
                        }
                        UsersList.Add(new LoginViewModel { UserName = arr[0], Email = arr[0], Password = arr[1] });

                    }
                }
            }

            ActivityUser user;
            IdentityResult result;
            foreach (LoginViewModel i in UsersList)
            {
                user = new ActivityUser() { UserName = i.Email, Email = i.Email };

                result = await _userManager.CreateAsync(user, i.Password);
            }
            return Ok();

        }


        [HttpPost]
        [Route("AppendToDB")]
        public async Task<ActionResult> AppendDataToDB()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        for (int i = 0; i < hssfwb.NumberOfSheets; i++)
                        {
                            if (!(hssfwb.GetSheetAt(i) == null) && !hssfwb.IsSheetHidden(i))
                            {
                                sheet = hssfwb.GetSheetAt(i); //get first sheet from workbook  
                                await ActivityAdd(sheet);
                            }
                        }
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }


                    return Ok();

                }
            }
            return BadRequest();
        }


        private async Task ActivityAdd(ISheet iSheet)
        {
            IRow headerRow = iSheet.GetRow(1); //Get Header Row
            int cellCount = headerRow.LastCellNum;
            string[] arr = new string[cellCount];
            List<Activity> A= new List<Activity>();
            int ActType=0;

            switch (iSheet.SheetName)
            {
                case "EnrollmentActivity":
                    ActType = 0;
                    break;
                case "SalesActivity":
                    ActType = 1;
                    break;
                default:
                    break;
            }
            var fns = new Func<int, string, int?,double?,double?,DateTime?,object,object,object, Activity>[] 
            {
                (AgentId,AgentName,Place,PercOfTarget,Points,SalesDate,EnrollmentTarget,NewCustomers,ExistingCustomers) =>
                {return new EnrollmentActivity {AgentId=AgentId,AgentName=AgentName,Place=Place,PercOfTarget=PercOfTarget,Points=Points,SalesDate=SalesDate,EnrollmentTarget=(int?)EnrollmentTarget,NewCustomers=(int?)NewCustomers,ExistingCustomers=(int)ExistingCustomers };}
                ,
                //(AgentId,AgentName,Place,PercOfTarget,Points,SalesDate,SalesTarget,Sales,Nothing) =>
                //{return new SalesActivity{AgentId=AgentId,AgentName=AgentName,Place=Place,PercOfTarget=PercOfTarget,Points=Points,SalesDate=SalesDate,SalesTarget=(int?)SalesTarget,Sales=(int?)Sales};}
            };

            var saveFun = new Action<object>[]
            {
                (obj) => {
                    List<Activity> TempActivityList = (List<Activity>)obj;
                    List<EnrollmentActivity> EA = new List<EnrollmentActivity>();
                            for (int i = 0; i < TempActivityList.Count; i++)
                    {
                        EA.Add((EnrollmentActivity)TempActivityList[i]);
                    }
                    _repo.AppendEnrollmentActivities(EA);
                        },
                (obj) => {
                    List<Activity> TempActivityList = (List<Activity>)obj;
                    //List<SalesActivity> SA = new List<SalesActivity>();
                            for (int i = 0; i < TempActivityList.Count; i++)
                    {
                        //SA.Add((SalesActivity)TempActivityList[i]);
                    }
                    //_repo.AppendSalesActivities(SA);
                        }
            };


            for (int i = (iSheet.FirstRowNum + 1); i <= iSheet.LastRowNum; i++) //Read Excel File
            {
                IRow row = iSheet.GetRow(i);
                if (row == null) continue;
                if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        arr[j] = row.GetCell(j).ToString();
                }
                A.Add(fns[ActType](
                    Int32.Parse(arr[0]),
                    arr[1],
                    Int32.Parse(arr[2]),
                    Double.Parse(arr[3]),
                    ((String.IsNullOrEmpty(arr[4])) ? (double?)null : Double.Parse(arr[4])),
                    DateTime.Parse(arr[5]),
                    Int32.Parse(arr[6]),
                    Int32.Parse(arr[7]),
                    (cellCount < 9) ? (int?)null : ((String.IsNullOrEmpty(arr[8])) ? (int?)null : Int32.Parse(arr[8]))
                    ));
            }
            saveFun[ActType](A);
            await _repo.SaveAllAsync();
        }
    }
}