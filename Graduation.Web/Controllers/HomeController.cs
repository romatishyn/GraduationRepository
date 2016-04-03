using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Graduation.Web.Entities;
using Graduation.Web.Models;
using Graduation.Web.Models.ViewModels.TestCreationModels;
using Graduation.Web.Services;

namespace Graduation.Web.Controllers
{
    public class HomeController : Controller
    {
        private TriviaContext db = new TriviaContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult Index()
        {
            return View("QuizList");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Quiz(int id)
        {
            return View(db.TriviaTests.FindAsync(CancellationToken.None, id));
        }

        public ActionResult QuizList()
        {
            return View();
        }

        public async Task<ActionResult> GetTestsList()
        {
            //var x = TriviaService.GetTestsAsync(this.db.TriviaTests.ToListAsync().Result);
            return Json(await TriviaService.GetTestsAsync(this.db.TriviaTests.ToListAsync().Result), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewTest(TestListViewModel test)
        {
            return null;
        }
    }
}