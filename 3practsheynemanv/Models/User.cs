using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3practsheynemanv.Models
{
    internal class User
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string RoleName { get; set; }
    }
}
