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
    public class EmployeesTestController : Controller
    {
        private IPeopleProRepo m_repo;
        //private PeopleProRepo peopleProRepo;
        public EmployeesTestController() : this(new PeopleProRepo()) { }
      
        public EmployeesTestController(IPeopleProRepo repo)
        {
            m_repo = repo;
        }

     /*   public EmployeesTestController(PeopleProRepo peopleProRepo)
        {

            this.peopleProRepo = peopleProRepo;
        }           */


        // GET: EmployeesTest
        public ActionResult Index()
        {

            //var cheating = new PeopleProRepo();
            IEnumerable<Employee> employees = m_repo.GetAllEmployees();

         //   var employees = m_repo.GetAllEmployees();
            return View(employees.ToList());
        }

        // GET: EmployeesTest/Details/5
        public ActionResult Details(int? id)
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

        // GET: EmployeesTest/Create
        public ActionResult Create()
        {
          //  var m_repo = new EmployeesTestController(new PeopleProRepo());
            ViewBag.DepartmentId = new SelectList(m_repo.GetAllDepartments(), "Id", "Name");
            return View();
        }

        // POST: EmployeesTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,Email,PhoneNumber,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                m_repo.UpdateEmployee(employee);
                m_repo.SaveEmployee(employee);

                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(m_repo.GetAllDepartments(), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: EmployeesTest/Edit/5
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

        // POST: EmployeesTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,Email,PhoneNumber,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                m_repo.UpdateEmployee(employee);
                m_repo.SaveEmployee(employee);
               // db.Entry(employee).State = EntityState.Modified;
               // db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(m_repo.GetAllDepartments(), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: EmployeesTest/Delete/5
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

        // POST: EmployeesTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
   //         Employee employee = db.Employees.Find(id);
            Employee employee = m_repo.GetEmployee(id);
            m_repo.DeleteEmployee(employee);
            m_repo.SaveEmployee(employee);
     //       db.Employees.Remove(employee);
        //    db.SaveChanges();
            return RedirectToAction("Index");
        }

      /*  protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                .Dispose();
            }

            base.Dispose(disposing);
        }*/
    }
}
