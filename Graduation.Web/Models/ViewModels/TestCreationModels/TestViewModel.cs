using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Web.Models.ViewModels.TestCreationModels
{
    public class TestViewModel
    {
        public string Title { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}