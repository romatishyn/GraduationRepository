using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graduation.Web.Entities;
using Graduation.Web.Entities.Repositories;
using Graduation.Web.Models.ViewModels.QuizViewModels;

namespace Graduation.Web.Services
{
    public class TriviaService : ITriviaService
    {
        private IRepository _triviaRepository;

        public TriviaService(IRepository triviaRepository)
        {
            this._triviaRepository = triviaRepository;
        }

        public async Task<QuestionViewModel> GetQuestionAsync(TriviaQuestion entity, bool isLast)
        {
            var question = new QuestionViewModel();
            await Task.Run(() =>
            {
                question.Id = entity.Id;
                question.Title = entity.Title;
                question.IsLast = isLast;
                question.Options = new List<OptionViewModel>();
                foreach (var option in entity.Options)
                {
                    question.Options.Add(new OptionViewModel(option));
                }
            });
            return question;
        }
        public async Task<List<TestViewModel>> GetTestsAsync(List<TriviaTest> entity)
        {
            var testList = new List<TestViewModel>();
            await Task.Run(() =>
            {
                for (var i = 0; i < entity.Count(); i++)
                {
                    testList.Add(new TestViewModel(entity[i]));
                }
            });
            return testList;
        }
        public async Task<TriviaTest> ConvertToTriviaAsync(Models.ViewModels.TestCreationModels.TestViewModel test)
        {
            var triviaTest = new TriviaTest();
            triviaTest.Title = test.Title;
            triviaTest.Questions = new List<TriviaQuestion>();
            await Task.Run(() =>
            {
                foreach (var elem in test.Questions)
                {
                    var question = new TriviaQuestion();
                    question.Title = elem.Title;
                    question.Test = triviaTest;
                    question.Options = new List<TriviaOption>();
                    foreach (var answer in elem.Options)
                    {
                        var option = new TriviaOption(answer);
                        //option.Title = answer.Title;
                        //option.IsCorrect = answer.IsCorrect;
                        option.TriviaQuestion = question;
                        question.Options.Add(option);
                    }
                    triviaTest.Questions.Add(question);
                }
            });
            return triviaTest;
        }
    }
}