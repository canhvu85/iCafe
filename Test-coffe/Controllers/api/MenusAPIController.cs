using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_coffe.Controllers.Services;
using Test_coffe.Models;

namespace Test_coffe.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusAPIController : ControllerBase
    {
        private readonly IMenu _menuRepository;
        private dynamic result;


        public MenusAPIController(IMenu menuRepositor)
        {
            _menuRepository = menuRepositor;
        }

        // GET: api/MenusAPI
        [HttpGet]
        public ActionResult GetMenu()
        {
            result = _menuRepository.GetMenu();
            return Ok(result);
        }
    }
}
