using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Graduation.Web.Entities;
using Graduation.Web.Models.ViewModels.QuizViewModels;

namespace Graduation.Web.Services
{
    public interface ITriviaService
    {
        Task<QuestionViewModel> GetQuestionAsync(TriviaQuestion entity, bool isLast);
        Task<List<TestViewModel>> GetTestsAsync(List<TriviaTest> entity);
        Task<TriviaTest> ConvertToTriviaAsync(Models.ViewModels.TestCreationModels.TestViewModel test);
    }
}