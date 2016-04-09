using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graduation.Web.Entities;
using Graduation.Web.Models.ViewModels.QuizViewModels;

namespace Graduation.Web.Services
{
    public static class TriviaService
    {
        public static async Task<QuestionViewModel> GetQuestionAsync(TriviaQuestion entity, bool isLast)
        {
            var question = new QuestionViewModel();
            await Task.Run(() =>
            {
                question.Id = entity.Id;
                question.Title = entity.Title;
                question.IsLast = isLast;
                question.Options = new List<OptionViewModel>();
                for (var i = 0; i < entity.Options.Count; i++)
                {
                    question.Options.Add(new OptionViewModel(entity.Options[i]));
                }

            });
            return question;
        }
        public static async Task<List<TestViewModel>> GetTestsAsync(List<TriviaTest> entity)
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
        public static async Task<TriviaTest> ConvertToTriviaAsync(Models.ViewModels.TestCreationModels.TestViewModel test)
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
                        var option = new TriviaOption();
                        option.Title = answer.Title;
                        option.IsCorrect = answer.IsCorrect;
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