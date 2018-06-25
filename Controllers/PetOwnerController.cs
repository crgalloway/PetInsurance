using System;
using ProblemD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace ProblemD
{
    public class PetOwnerController : Controller
    {
        private ProblemDContext _context;
        public PetOwnerController(ProblemDContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}