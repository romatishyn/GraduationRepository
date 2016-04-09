using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Web.Models.ViewModels.TestCreationModels
{
    public class QuestionViewModel
    {
        public string Title { get; set; }

        public List<OptionViewModel> Options { get; set; }
    }
}