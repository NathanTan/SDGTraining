using PeopleProTraining.Dal.Infrastructure;
using PeopleProTraining.Dal.Interfaces;
using PeopleProTraining.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace PeopleProTraining.Controllers
{
    public class DepartmentController : Controller
    {
        private IPeopleProRepo m_repo;
      //  private PeopleProRepo peopleProRepo;
        public DepartmentController() : this(new PeopleProRepo()) { }
        public DepartmentController(IPeopleProRepo repo)
        {
            m_repo = repo;
        }
        // GET: Department
        public ActionResult Index()
        {
            IEnumerable<Department> departments = m_repo.GetAllDepartments();

            if (!departments.Any())
            {
                //If there are no buildings, we should provide the ability to create them?
                return RedirectToAction("Create");
            }

            //paginate buildings, what if there are 10000, do we want the user to scroll through all of that?
            //look up IPagedList<T>
            return View(departments.ToList());
        }
  /*      public DepartmentController(PeopleProRepo peopleProRepo)
        {
            this.peopleProRepo = peopleProRepo;
        }            */


        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {          
            if (!id.HasValue)   
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }  

            Department department = m_repo.GetDepartmentById(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);

        }

        // GET: Department/Create
        public ActionResult Create()
        {
            ViewBag.Departments = new SelectList(m_repo.GetAllDepartments(), "Id", "Name");
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(Department department)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    m_repo.SaveDepartment(department);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Department/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Department department)
        {
            try
            {
                // TODO: Add update logic here
                m_repo.UpdateDepartment(department);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)   
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Department department = m_repo.GetDepartmentById(id.Value);             
            if (department == null)
            {
                return HttpNotFound();
            }

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Department department)
        {
            try
            {
                // TODO: Add delete logic here
                m_repo.DeleteDepartment(department);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
