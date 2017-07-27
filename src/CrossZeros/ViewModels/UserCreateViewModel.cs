using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrossZeros.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(1024, MinimumLength = 5)]
        public string NickName { get; set; }

        [Required]
        [StringLength(1024, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
