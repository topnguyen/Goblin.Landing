using System.ComponentModel.DataAnnotations;
using Goblin.Core.Models;

namespace Goblin.Landing.Core.Models
{
    public class CreateSampleModel : GoblinApiSubmitRequestModel
    {
        [Required]
        public string SampleData { get; set; }
    }
}