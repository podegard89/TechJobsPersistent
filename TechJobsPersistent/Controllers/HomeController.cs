using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;
        private List<Skill> skills;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
            skills = context.Skills.ToList();
        }
        
        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Employer> employers = context.Employers.ToList();
            //List<Skill> skills = context.Skills.ToList();
            AddJobViewModel viewModel = new AddJobViewModel(employers, skills);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ProcessAddJobForm(AddJobViewModel viewModel, string[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Job newJob = new Job(viewModel.Name)
                {
                    Employer = context.Employers.Find(viewModel.EmployerId)
                };
                foreach (String skill in selectedSkills)
                {
                    JobSkill newJobSkill = new JobSkill
                    {
                        Skill = context.Skills.Find(int.Parse(skill)),
                        Job = newJob
                    };
                    context.JobSkills.Add(newJobSkill);
                }
                context.Jobs.Add(newJob);
                context.SaveChanges();
                return Redirect("Index");
            }
            ViewBag.checkedBoxes = selectedSkills;
            viewModel = new AddJobViewModel(context.Employers.ToList(), skills);
            return View("AddJob", viewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
