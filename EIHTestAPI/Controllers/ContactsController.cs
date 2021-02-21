using EIHTest.Helpers;
using EIHTest.Interfaces;
using EIHTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIHTest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {

        private IContactService _contactService;
        private readonly AppSettings _appSettings;

        public ContactsController(IContactService contactService, IOptions<AppSettings> appSettings)
        {
            _contactService = contactService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("contacts")]
        public IActionResult GetAll()
        {
            var contacts = _contactService.GetAll();
            return Ok(contacts);
        }


        [HttpGet("contactsByEmail")]
        public IActionResult GetContactByEmail(string email)
        {
            var contacts = _contactService.GetByEmail(email);
            return Ok(contacts);
        }

        [HttpGet("contactsById")]
        public IActionResult GetContactById([FromQuery]string id)
        {
            var contacts = _contactService.GetById(id);
            return Ok(contacts);
        }

        [HttpGet("contactsByStatus")]
        public IActionResult GetAllByStatus(int status)
        {
            var contacts = _contactService.GetAllContactsByState(status);
            return Ok(contacts);
        }


        [HttpPost("Add")]
        public IActionResult AddContact([FromBody] Contact contact)
        {
            var contacts = _contactService.AddContact(contact);
            if(!contacts)
            {
                return Problem("Resource is already exists!", "", 409);
            }
            return Ok(contacts);
        }

        [HttpPost("Update")]
        public IActionResult UpdateContact([FromBody] Contact contact)
        {
            var contacts = _contactService.UpdateContact(contact);
            return Ok(contacts);
        }

        [HttpPost("Delete")]
        public IActionResult DeleteContact([FromBody] Contact contact)
        {
            var contacts = _contactService.DeleteContact(contact);
            return Ok(contacts);
        }
    }
}
