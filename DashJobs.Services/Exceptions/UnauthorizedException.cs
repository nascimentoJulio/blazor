using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashJobs.Services.Exceptions
{

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message = "Usuário não autorizado") : base(message) { }

    }
}
