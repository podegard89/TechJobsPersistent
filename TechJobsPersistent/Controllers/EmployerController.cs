﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private JobDbContext context;

        public EmployerController(JobDbContext dBContext)
        {
            context = dBContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Employer> employers = context.Employers.ToList();
            return View(employers);
        }

        public IActionResult Add()
        {
            AddEmployerViewModel viewModel = new AddEmployerViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel addEmployerViewModel)
        {
            if (ModelState.IsValid)
                {
                    Employer newEmployer = new Employer
                        {
                            Name = addEmployerViewModel.Name,
                            Location = addEmployerViewModel.Location,
                        };
                    context.Employers.Add(newEmployer);
                    context.SaveChanges();
                return Redirect("/Employer/");
            }
            else
            {
                return View("Add", addEmployerViewModel);
            }
            
        }

        public IActionResult About(int id)
        {
            List<Employer> employers = context.Employers.ToList();

            Employer employer = employers.Where(e => e.Id == id).First();

            ViewBag.employer = employer;

            return View();
        }
    }
}
