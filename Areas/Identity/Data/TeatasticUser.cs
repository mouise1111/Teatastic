using Microsoft.AspNetCore.Identity;

namespace Teatastic.Areas.Identity.Data;

// Add profile data for application users by adding properties to the TeatasticUser class
public class TeatasticUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

