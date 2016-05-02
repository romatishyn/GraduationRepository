using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Graduation.Web.Entities;
using Graduation.Web.Entities.Repositories;
using Graduation.Web.Models.ViewModels.TestCreationModels;
using Graduation.Web.Services;

namespace Graduation.Web.Controllers
{
    public class HomeController : Controller
    {
        //private TriviaContext db = new TriviaContext();

        private IRepository _triviaRepository;

        public HomeController()
        {
            this._triviaRepository = new GenericRepository(new TriviaContext());
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
            //if (_triviaRepository.Get<TriviaTest>().FindAsync(CancellationToken.None, id)) ;
            if (_triviaRepository.Get<TriviaTest>().Any(x => x.Id == id))
            {
                ViewBag.TestId = id;
                return View();
            }
            return View("Error");
        }

        public ActionResult QuizList()
        {
            return View(_triviaRepository.Get<TriviaTest>().ToList());
        }

        public async Task<ActionResult> GetTestsList()
        {
            return Json(await TriviaService.GetTestsAsync(this._triviaRepository.Get<TriviaTest>().ToListAsync().Result), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddNewTest(TestViewModel test)
        {
            //TriviaDatabaseInitializer
            if (test != null)
            {
                TriviaContext context = new TriviaContext();
                await Task.Run(() =>
                {
                    var triviaTest = TriviaService.ConvertToTriviaAsync(test).Result;
                    _triviaRepository.Add(triviaTest);
                    _triviaRepository.Commit();
                });
            }
            return null;
        }
    }
}