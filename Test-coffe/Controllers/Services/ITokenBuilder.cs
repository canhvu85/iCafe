using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Test_coffe.Models;

namespace Test_coffe.Controllers.Services
{
    public interface ITokenBuilder
    {
        string BuildToken(Users users);

        bool isExpiredToken();
        //bool isExpiredToken(string remember_token);
    }
}
