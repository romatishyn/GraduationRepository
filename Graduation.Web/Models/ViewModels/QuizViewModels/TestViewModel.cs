using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Graduation.Web.Entities;

namespace Graduation.Web.Models.ViewModels.QuizViewModels
{
    public class TestViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public TestViewModel(TriviaTest test)
        {
            Id = test.Id;
            Title = test.Title;
        }
    }
}