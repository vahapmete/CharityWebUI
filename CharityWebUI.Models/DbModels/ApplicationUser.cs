using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CharityWebUI.Models.DbModels
{
    public class ApplicationUser : IdentityUser

    {
        [Required]
        public string NameSurname { get; set; }

        [Required]
        public int? CharityId { get; set; }

        [ForeignKey("CharityId")]
        public Charity Charity { get; set; }

        [NotMapped]
        public string Role { get; set; }

    }
}
