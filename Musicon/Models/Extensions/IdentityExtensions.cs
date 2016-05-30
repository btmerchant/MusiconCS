using System.Security.Claims;
using System.Security.Principal;

namespace Musicon.Models.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserFirstname(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FirstName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}