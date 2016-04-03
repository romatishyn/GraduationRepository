using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Web.Models.ViewModels.QuizViewModels
{
    public class OptionViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int QuestionId { get; set; }

        public OptionViewModel(TriviaOption option)
        {
            Id = option.Id;
            Title = option.Title;
            QuestionId = option.QuestionId;
        }
    }
}