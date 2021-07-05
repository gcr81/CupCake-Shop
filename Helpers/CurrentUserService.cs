using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// Oligert Crroj
/// Creted on :7/1/2021 10:24 Am
/// Last changes made: 7/2/2021 2:02 pm
/// </summary>

namespace CupCakeShop.Helpers
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        //Using httpContextAccessor to get the user email
        public string GetCurrentUserName()
        {   
            return httpContextAccessor.HttpContext.User.Identity.Name;
         
        }
    }
}
