using PeopleProTraining.Dal.Infrastructure;
using PeopleProTraining.Dal.Interfaces;
using PeopleProTraining.Dal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PeopleProTraining.Controllers
{
    public class EmployeeController : Controller
    {
        private IPeopleProRepo m_repo;
        // private PeopleProRepo peopleProRepo;
        public EmployeeController() : this(new PeopleProRepo()) { }
        public EmployeeController(IPeopleProRepo repo)
        {
            m_repo = repo;
        }
        /*     public EmployeeController(PeopleProRepo peopleProRepo)
             {
                 this.peopleProRepo = peopleProRepo;
             }            */
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<Employee> employees = m_repo.GetAllEmployees();

            if (!employees.Any())
            {
                //If there are no buildings, we should provide the ability to create them?
                return RedirectToAction("Create");
            }

            //paginate buildings, what if there are 10000, do we want the user to scroll through all of that?
            //look up IPagedList<T>

            return View(employees.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        // {
        //      return View();
        //  }
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = m_repo.GetEmployee(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = m_repo.GetEmployee(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(m_repo.GetAllDepartments(), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                // TODO: Add update logic here

                m_repo.UpdateEmployee(employee);

                m_repo.DeleteEmployee(m_repo.GetEmployeeById(id));     //magic line of workingness

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)    
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = m_repo.GetEmployee(id.Value);     
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
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
