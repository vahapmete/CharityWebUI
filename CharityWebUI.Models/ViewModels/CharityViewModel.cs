using System;
using System.Collections.Generic;
using System.Text;
using CharityWebUI.Models.DbModels;

namespace CharityWebUI.Models.ViewModels
{
    public class CharityViewModel
    {
        public IEnumerable<Charity> Charities { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
    }
}
