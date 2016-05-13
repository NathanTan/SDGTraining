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
        public ActionResult Index(string sortOrder, string CurrentSort, int? page)
        {
            IEnumerable<Building> buildings = m_repo.GetAllBuildings();

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

            buildings.OrderBy(b => b.Id).OrderBy(b => b.Name);

            sortOrder = string.IsNullOrEmpty(sortOrder) ? "Id" : sortOrder;
            return View("Index");
        }
  /*      public ActionResult Index()
        {
            return View();
        }*/

        // GET: Building/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Building/Create
        public ActionResult Create()
        {
            ViewBag.Buildings = new SelectList(m_repo.GetAllBuildings(), "Id", "Name");
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Building/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Building building)
        {
            try
            {
                // TODO: Add update logic here
                m_repo.UpdateBuilding(building);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Building/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
