using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskItemStatus Status { get; set; }
        public int ProjectId { get; set; } 
        public int CreatedByUserId { get; set; }
        public DateTime Description_Date { get; set; }
        public DateTime Last_ChangeDate { get; set; }
    }
}
