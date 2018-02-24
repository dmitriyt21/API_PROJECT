using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ErezAPI
{
    public class PrivateMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }

        [Required]
        [ForeignKey("AgentId")]
        public int AgentId { get; set; }

        [MaxLength (200)]
        public string Message { get; set; }

        public bool IsSeen { get; set; } = false;

        public DateTime Date { get; set; } = DateTime.UtcNow.Date;

        public string Issuer { get; set; }

    }
}
