using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Graduation.Web.Entities;
using Graduation.Web.Models;
using Graduation.Web.Models.ViewModels.QuizViewModels;
using Graduation.Web.Services;

namespace Graduation.Web.Controllers
{
    [Authorize]
    public class TrivialController : ApiController
    {
        private static Dictionary<string, List<TriviaResult>> _resultsDictionary = new Dictionary<string, List<TriviaResult>>();
        public class TriviaController : ApiController
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

            // GET api/Trivia
            [ResponseType(typeof(TriviaQuestion))]
            public async Task<IHttpActionResult> Get()
            {
                var userId = User.Identity.Name;

                var nextQuestion = await this.NextQuestionAsync(userId);

                if (nextQuestion == null)
                {
                    return this.NotFound();
                }

                return this.Ok(nextQuestion);
            }

            private async Task<QuestionViewModel> NextQuestionAsync(string userId)
            {
                var lastQuestionId = await this.db.TriviaAnswers
                    .Where(a => a.UserId == userId)
                    .GroupBy(a => a.QuestionId)
                    .Select(g => new { QuestionId = g.Key, Count = g.Count() })
                    .OrderByDescending(q => new { q.Count, QuestionId = q.QuestionId })
                    .Select(q => q.QuestionId)
                    .FirstOrDefaultAsync();

                var questionsCount = await this.db.TriviaQuestions.CountAsync();

                var nextQuestionId = (lastQuestionId % questionsCount) + 1;

                var isLast = (nextQuestionId == questionsCount);

                return await TriviaService.GetQuestionAsync(
                        this.db.TriviaQuestions.FindAsync(CancellationToken.None, nextQuestionId).Result, isLast);
                //return await this.db.TriviaQuestions.FindAsync(CancellationToken.None, nextQuestionId);
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

                await Task.Run(() =>
                {
                    if (!_resultsDictionary.ContainsKey(User.Identity.Name.ToString()))
                    {
                        _resultsDictionary.Add(User.Identity.Name.ToString(), new List<TriviaResult>());
                    }
                    _resultsDictionary[User.Identity.Name.ToString()].Add(new TriviaResult { QuestionId = answer.QuestionId, IsCorrect = isCorrect });
                });
                return this.Ok<bool>(isCorrect);
            }
            private async Task<bool> StoreAsync(TriviaAnswer answer)
            {
                this.db.TriviaAnswers.Add(answer);

                await this.db.SaveChangesAsync();
                var selectedOption = await this.db.TriviaOptions.FirstOrDefaultAsync(
                    o => o.Id == answer.OptionId
                    &&
                    o.QuestionId == answer.QuestionId);

                return selectedOption.IsCorrect;
            }

        }
    }
}
