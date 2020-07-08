using System.ComponentModel.DataAnnotations;

namespace Goblin.Landing.Core.Models
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        
        public string SetPasswordToken { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}