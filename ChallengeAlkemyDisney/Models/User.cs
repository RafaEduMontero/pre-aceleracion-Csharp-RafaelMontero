using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAlkemyDisney.Models
{
    public class User : IdentityUser
    {
        public bool IsActive { get; set; }
    }
}
