using DotNetSurfer_Backend.Core.Models;

namespace DotNetSurfer.Core.TokenGenerators
{
    public interface ITokenGenerator
    {
        string GetToken(User user);
    }
}
