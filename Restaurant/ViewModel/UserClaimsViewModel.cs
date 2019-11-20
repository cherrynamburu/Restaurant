using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewModel
{
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            Claims = new List<UserClaim>();
        }

        public string UserId { get; set; }

        public IList<UserClaim> Claims { get; set; }
    }
}
