using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossZeros.Models
{
    public class CrossZeroUser:IdentityUser
    {
        public string NickName { get; set; }
    }
}
