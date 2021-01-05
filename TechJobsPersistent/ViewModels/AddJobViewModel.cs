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
        public List<Skill> SelectSkill { get; set; }
        public string[] CheckedBoxes { get; set; }

        public AddJobViewModel()
        {
        }
        public AddJobViewModel(List<Employer> employers, List<Skill> skills)
        {
            SelectListItem = employers;
            SelectSkill = skills;
        }
    }
}
