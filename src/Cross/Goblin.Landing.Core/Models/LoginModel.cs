using System.ComponentModel.DataAnnotations;

namespace Goblin.Landing.Core.Models
{
    public class LoginModel
    {
        /// <summary>
        ///     Redirect URI
        /// </summary>
        public string Continue { get; set; }
        
        public string Email { get; set; }

        /// <summary>
        ///     Hint - pre-enter Password
        /// </summary>
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}