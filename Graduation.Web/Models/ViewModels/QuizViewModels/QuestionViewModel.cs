using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation.Web.Models.ViewModels.QuizViewModels
{
    public class QuestionViewModel
    {
         public int Id { get; set; }

        public string Title { get; set; }

        public virtual List<OptionViewModel> Options { get; set; }

        public bool IsLast { get; set; }
    }
}