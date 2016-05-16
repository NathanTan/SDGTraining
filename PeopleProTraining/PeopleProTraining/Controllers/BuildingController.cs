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
    public class BuildingController : Controller
    {
        private IPeopleProRepo m_repo;
   //     private PeopleProRepo peopleProRepo;
        public BuildingController() : this(new PeopleProRepo()) { }
        public BuildingController(IPeopleProRepo repo)
        {
            m_repo = repo;
        }
        // GET: Building
    /*    public BuildingController(PeopleProRepo peopleProRepo)
        {
            this.peopleProRepo = peopleProRepo;
        }          */

        /* public ActionResult Index(string sortOrder, string CurrentSort, int? page)
         {}*/
        public ActionResult Index()
        {
            IEnumerable<Building> buildings = m_repo.GetAllBuildings();

            if (!buildings.Any())
            {
                //If there are no buildings, we should provide the ability to create them?
                return RedirectToAction("Create");
            }

            //paginate buildings, what if there are 10000, do we want the user to scroll through all of that?
            //look up IPagedList<T>

            return View(buildings.ToList());
        }
  /*      public ActionResult Index()
        {
            return View();
        }*/

        // GET: Building/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);       
            }

            Building building = m_repo.GetBuildingById(id.Value);
            if (building == null)
            {
                return HttpNotFound();
            }

            return View(building);

        }

        // GET: Building/Create
        public ActionResult Create()
        {
            ViewBag.Buildings = new SelectList(m_repo.GetAllBuildings(), "Id", "Name");
            return View("Index");
        }

        // POST: Building/Create
        [HttpPost]
        public ActionResult Create(Building building)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    m_repo.SaveBuilding(building);

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Building/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = m_repo.GetBuildingById(id.Value);
            if (building == null)
            {
                return HttpNotFound();
            }
//ViewBag.DepartmentId = new SelectList(m_repo.GetAllDepartments(), "Id", "Name", employee.DepartmentId);
            return View(building);
        }

        // POST: Building/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Building building)
        {
            try
            {
                // TODO: Add update logic here
                m_repo.UpdateBuilding(building);
                //add building
                //delete building
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Building/Delete/5
        public ActionResult Delete(int? id)     
        {     
            if (id == null)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);    
            }    

            Building building = m_repo.GetBuildingById(id.Value);

            if (building == null)
            {
                return HttpNotFound();    
            }

            return View(building);
        }

        // POST: Building/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Building building)
        {
            try
            {
                // TODO: Add delete logic here

                    m_repo.DeleteBuilding(building);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
