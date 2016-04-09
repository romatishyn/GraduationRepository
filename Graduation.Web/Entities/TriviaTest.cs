using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Graduation.Web.Models;
using Graduation.Web.Models.ViewModels.TestCreationModels;

namespace Graduation.Web.Entities
{
    public class TriviaTest
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public virtual List<TriviaQuestion> Questions { get; set; }
    }
}