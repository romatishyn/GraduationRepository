using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Graduation.Web.Entities
{
    public class TriviaResult
    {
        [Column(Order = 0), Key]
        public string UserId { get; set; }

        public int TestId { get; set; }
        [Column(Order = 1), Key, ForeignKey("TestId")]
        public TriviaTest Test { get; set; }

        //public List<TriviaAnswer> Answers { get; set; }

        //public TriviaResult()
        //{
        //    Answers = new List<TriviaAnswer>();
        //}
    }
}