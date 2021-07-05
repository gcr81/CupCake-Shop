using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CupCakeShop.Helpers
{
    //Interface created to help me get the current user
    public interface ICurrentUserService
    {
        string GetCurrentUserName();
    }
}
