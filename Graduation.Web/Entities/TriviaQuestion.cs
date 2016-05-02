using Newtonsoft.Json;

namespace Graduation.Web.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Graduation.Web.Models.ViewModels.TestCreationModels;
    public class TriviaQuestion
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual List<TriviaOption> Options { get; set; }

        [JsonIgnore]
        public virtual TriviaTest Test { get; set; }
        public TriviaQuestion()
        {
        }

        public TriviaQuestion(QuestionViewModel question)
        {
            Title = question.Title;
            Options = new List<TriviaOption>();
            foreach (var option in question.Options)
            {
                Options.Add(new TriviaOption(option));
            }
        }
    }
}