using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Graduation.Web.Entities
{
    public class TriviaContext : DbContext
    {
        public TriviaContext()
            : base("name=DefaultConnection")
        {
        }
        public DbSet<TriviaTest> TriviaTests { get; set; }
        public DbSet<TriviaQuestion> TriviaQuestions { get; set; }

        public DbSet<TriviaOption> TriviaOptions { get; set; }

        public DbSet<TriviaAnswer> TriviaAnswers { get; set; }
    }


}