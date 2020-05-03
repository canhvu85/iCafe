using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Services
{
    public interface ILogin
    {
        dynamic GetUser(Users users);

        void Register(Users users);
    }
}
