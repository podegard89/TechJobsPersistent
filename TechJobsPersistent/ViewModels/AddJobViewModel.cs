using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        public int EmployerId { get; set; }
        public List<Employer> SelectListItem { get; set; }

        public AddJobViewModel(List<Employer> employers)
        {
            SelectListItem = employers;
        }
    }
}
