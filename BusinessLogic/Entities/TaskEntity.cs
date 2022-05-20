using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeadLine { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
