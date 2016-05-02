using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Graduation.Web.Entities;
using Graduation.Web.Entities.Repositories;
using Graduation.Web.Models;
using Graduation.Web.Models.ViewModels.QuizViewModels;
using Graduation.Web.Services;

namespace Graduation.Web.Controllers
{
    [Authorize]
    public class TriviaController : ApiController
    {
        //private Dictionary<string, List<TriviaAnswer>> _resultsDictionary = new Dictionary<string, List<TriviaAnswer>>();
        private TriviaTest _currentTest;
        public bool Tested = false;

        private readonly IRepository _triviaRepository;

        public TriviaController()
        {
            this._triviaRepository = new GenericRepository(new TriviaContext());
        }

        // GET api/Trivia
        [ResponseType(typeof(TriviaQuestion))]
        public async Task<IHttpActionResult> Get(int id, int questionId = 0)
        {
            //   if (!Tested)
            //   {
            if (_triviaRepository.Get<TriviaTest>().Any(x => x.Id == id))
            {
                _currentTest = _triviaRepository.Get<TriviaTest>().First(x => x.Id == id);
                //             Tested = true;
            }
            //     }
            var isLast = (questionId == _currentTest.Questions.Count() - 1);
            var nextQuestion = new QuestionViewModel();
            await Task.Run(() =>
            {
                nextQuestion = TriviaService.GetQuestionAsync(
                    _currentTest.Questions[questionId], isLast).Result;
            });


            if (nextQuestion == null)
            {
                return this.NotFound();
            }

            return this.Ok(nextQuestion);
        }

        // POST api/Trivia
        [ResponseType(typeof(TriviaAnswer))]
        public async Task<IHttpActionResult> Post(TriviaAnswer answer)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            answer.UserId = User.Identity.Name;

            var isCorrect = await this.StoreAsync(answer);

            //await Task.Run(() =>
            //{
            //    if (!_resultsDictionary.ContainsKey(User.Identity.Name))
            //    {
            //        _resultsDictionary.Add(User.Identity.ToString(), new List<Result>());
            //    }
            //    _resultsDictionary[User.Identity.Name].Add(new Result { QuestionId = answer.QuestionId, IsCorrect = isCorrect });
            //});
            return this.Ok<bool>(isCorrect);
        }

        private async Task<bool> StoreAsync(TriviaAnswer answer)
        {
            var selectedOption = new TriviaOption();
            await Task.Run(() =>
            {
                this._triviaRepository.Get<TriviaAnswer>().Add(answer);
                this._triviaRepository.Commit();

                selectedOption = this._triviaRepository.Get<TriviaOption>().FirstOrDefaultAsync(
                      o => o.Id == answer.OptionId && o.QuestionId == answer.QuestionId).Result;
            });
            return selectedOption.IsCorrect;
        }

    }
}
