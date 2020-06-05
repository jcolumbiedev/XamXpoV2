using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Microsoft.AspNetCore.Mvc;
using OrmV2;

namespace BitServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private UnitOfWork UnitOfWork;

        public ContactsController(UnitOfWork uow)
        {
            UnitOfWork = uow;
        }

        [HttpGet]
        public IEnumerable<Contact> Get()
        { var a= UnitOfWork.Query<Contact>();
            return a;
        }
    }
}
