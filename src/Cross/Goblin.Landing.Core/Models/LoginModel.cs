using System.ComponentModel.DataAnnotations;

namespace Goblin.Landing.Core.Models
{
    public class LoginModel
    {
        /// <summary>
        ///     Redirect URI
        /// </summary>
        public string Continue { get; set; }
        
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}