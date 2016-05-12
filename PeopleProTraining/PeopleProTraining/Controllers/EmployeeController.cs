using PeopleProTraining.Dal.Infrastructure;
using PeopleProTraining.Dal.Interfaces;
using PeopleProTraining.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeopleProTraining.Controllers
{
    public class EmployeeController : Controller
    {
        private IPeopleProRepo m_repo;
        private PeopleProRepo peopleProRepo;
        public EmployeeController() : this(new PeopleProRepo()) { }
        public EmployeeController(IPeopleProRepo repo)
        {
            m_repo = repo;
        }
        // GET: Employee
        public ActionResult Index(string sortOrder, string CurrentSort, int? page)
        {
            IEnumerable<Employee> buildings = m_repo.GetAllEmployees();

            if (!buildings.Any())
            {
                //If there are no buildings, we should provide the ability to create them?
                return RedirectToAction("Create");
            }

            //paginate buildings, what if there are 10000, do we want the user to scroll through all of that?
            //look up IPagedList<T>
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            buildings.OrderBy(b => b.Id).OrderBy(b => b.FirstName);

            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Id" : sortOrder;
            return View("Index");
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.Employees = new SelectList(m_repo.GetAllEmployees(), "Id", "FirstName");
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    m_repo.SaveEmployee(employee);

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                // TODO: Add update logic here
                m_repo.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                // TODO: Add delete logic here
                m_repo.DeleteEmployee(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
