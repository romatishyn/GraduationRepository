using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Graduation.Web.Entities;
using Graduation.Web.Entities.Repositories;
using Graduation.Web.Models.ViewModels.TestCreationModels;
using Graduation.Web.Services;
using Newtonsoft.Json;

namespace Graduation.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _triviaRepository;
        private readonly ITriviaService _triviaService;
        public HomeController(IRepository triviaRepository, ITriviaService triviaService)
        {
            _triviaRepository = triviaRepository;
            _triviaService = triviaService;
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

        public ActionResult Quiz(int? id)
        {
            if (id.HasValue && _triviaRepository.Get<TriviaTest>().Any(x => x.Id == id.Value))
            {
                ViewBag.TestId = id.Value;
                return View();
            }
            return View("Error");
        }

        public ActionResult UpdateQuiz(TriviaTest testToUpdate)
        {
            if (testToUpdate != null && _triviaRepository.Get<TriviaTest>().Any(x => x.Id == testToUpdate.Id))
            {
                if (ModelState.IsValid)
                {
                    foreach (var question in testToUpdate.Questions)
                    {
                        foreach (var option in question.Options)
                        {
                            _triviaRepository.Update(option);
                        }
                        _triviaRepository.Update(question);
                    }
                    _triviaRepository.Update(testToUpdate);
                    _triviaRepository.Commit();
                    return Json(new { Success = true, Message = "Success" });
                }
            }
            return Json(new { Success = false, Message = "Error" });
        }

        public ActionResult Edit(int? id)
        {
            if (id.HasValue && _triviaRepository.Get<TriviaTest>().Any(x => x.Id == id.Value))
            {
                ViewBag.TestId = id.Value;
                return View(_triviaRepository.Get<TriviaTest>().FindAsync(id).Result);
            }
            return View("Error");
        }

        public string EditQuiz(int? id)
        {
            if (id.HasValue && _triviaRepository.Get<TriviaTest>().Any(x => x.Id == id.Value))
            {
                //return Json(_triviaRepository.Get<TriviaTest>().FindAsync(id).Result);
                return JsonConvert.SerializeObject(_triviaRepository.Get<TriviaTest>().FindAsync(id).Result);
            }
            else return string.Empty;
        }

        public ActionResult QuizList()
        {
            return View();
        }

        public async Task<ActionResult> GetTestsList()
        {
            return Json(await _triviaService.GetTestsAsync(this._triviaRepository.Get<TriviaTest>().ToListAsync().Result), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddNewTest(TestViewModel test)
        {
            //TriviaDatabaseInitializer
            if (test != null)
            {
                TriviaContext context = new TriviaContext();
                await Task.Run(() =>
                {
                    var triviaTest = _triviaService.ConvertToTriviaAsync(test).Result;
                    _triviaRepository.Add(triviaTest);
                    _triviaRepository.Commit();
                });
            }
            return null;
        }
    }
}