using System.Security.Claims;
using AFF_BE.Core.Constants;

namespace AFF_BE.Api.Extension
{
    public static class IdentityExtension
    {
        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);

            return (claim != null) ? claim.Value : string.Empty;
        }

        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = ((ClaimsIdentity)claimsPrincipal.Identity).Claims.SingleOrDefault(x => x.Type == ClaimTypeConstant.Id);

            if (claim == null)
            {
                return 0;
            }
            return int.Parse(claim.Value);
        }
        
        public static int GetUserIds(this string hii)
        {
            return 0;
        }
        
    }
}
