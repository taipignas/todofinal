using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoFinal.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        [StringLength(90, MinimumLength = 3)]
        public string Description { get; set; }
        [EnumDataType(typeof(Priority))]
        public Priority Priority { get; set; }
        public DateTime DueDate { get; set; }
        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }
        [StringLength(450)]
        public string ToDoUserId { get; set; }
    }
}
