using System.ComponentModel.DataAnnotations;
using Goblin.Core.Models;

namespace Goblin.Landing.Core.Models
{
    public class CreateSampleModel : GoblinApiRequestModel
    {
        [Required]
        public string SampleData { get; set; }
    }
}