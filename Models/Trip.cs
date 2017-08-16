using System.ComponentModel.DataAnnotations;
using System;
 
namespace travel.Models
{
    public class Trip 
    {
        [Key]
        public int Id {get; set;}
        public string Title {get; set;}
        public string Description {get; set;}
        public DateTime LeaveDate {get; set;}
        public DateTime ReturnDate {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
    }

        public class TripVal 
    {
        [Required(ErrorMessage = "Required Field!")]
        public string Title {get; set;}

        [Required(ErrorMessage = "Required Field!")]
        public string Description {get; set;}

        [Required(ErrorMessage = "Required Field!")]
        [DataType(DataType.DateTime)]
        public DateTime LeaveDate {get; set;}

        [Required(ErrorMessage = "Required Field!")]
        [DataType(DataType.DateTime)]
        public DateTime ReturnDate {get; set;}

        public int UserId {get; set;}
    }
}