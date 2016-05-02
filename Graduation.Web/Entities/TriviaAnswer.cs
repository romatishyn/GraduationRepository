using System.ComponentModel.DataAnnotations;
using Graduation.Web.Models;

namespace Graduation.Web.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    public class TriviaAnswer
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("TriviaOption"), Column(Order = 1)]
        public int OptionId { get; set; }

        [ForeignKey("TriviaOption"), Column(Order = 0)]
        public int QuestionId { get; set; }

        [JsonIgnore]
        public virtual TriviaOption TriviaOption { get; set; }
    }
}