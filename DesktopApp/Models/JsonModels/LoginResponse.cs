using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Models.JsonModels
{
    class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;

    }
}
