
namespace DotNetSurfer_Backend.Core.Models
{
    public class SignIn
    {
        public string Auth_Token { get; set; }

        public User User { get; set; }
    }
}
