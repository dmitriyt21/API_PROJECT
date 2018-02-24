using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErezAPI
{
    public class CreateUserModel
    {
        
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int AgentId { get; set; }

        [Required]
        public string AgentName { get; set; }

        public string Role { get; set; }

    }
}
