using Microsoft.AspNetCore.Identity;

namespace FP_NZWALKS.Repositories
{
    public interface ITokenRepository
    {
        public string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
