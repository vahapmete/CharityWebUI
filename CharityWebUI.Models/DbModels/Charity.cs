using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CharityWebUI.Models.DbModels
{
    public class Charity
    {
        [Key]
        public int  Id { get; set; }

        [Required]
        public string  Name { get; set; }

        [Required]
        public string   Email { get; set; }

        [Required]
        public string About { get; set; }

        [Required]
        public string  City { get; set; }

        [Required]
        public string  Address { get; set; }

        public int  AdminId { get; set; }
        
    }
}
