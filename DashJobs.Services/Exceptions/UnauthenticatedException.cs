using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashJobs.Services.Exceptions
{
    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException(string message = "Usuário não autenticado") : base(message) 
        {
                
        }
    }
}
