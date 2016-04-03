using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Graduation.Web.Entities;
using Graduation.Web.Models;
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
        public static async Task<List<TestListViewModel>> GetTestsAsync(List<TriviaTest> entity)
        {
            var testList = new List<TestListViewModel>();
            await Task.Run(() =>
            {
                for (var i = 0; i < entity.Count(); i++)
                {
                    testList.Add(new TestListViewModel(entity[i]));
                    testList.Add(new TestListViewModel(entity[i]));
                }
            });
            return testList;
        }
    }
}