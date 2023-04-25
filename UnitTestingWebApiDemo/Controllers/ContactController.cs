using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using UnitTestingWebApiDemo.Model;

namespace UnitTestingWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        public AppDbContext _context { get; set; }

        public ContactController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetContact()
        {
            return Ok(_context.Contacts.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetContactById(int id) 
        {
            var result= _context.Contacts.Where(x => x.Id == id).Select(contact => new Contact()
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Address = contact.Address,

            }).FirstOrDefault();
            _context.SaveChanges();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddContact(AddContact addcontact) 
        {
            var contact = new Contact();
            {
                contact.Name = addcontact.Name;
                contact.Email = addcontact.Email;   
                contact.Phone = addcontact.Phone;
                contact.Address = addcontact.Address;
            };
            _context.Contacts.AddAsync(contact);
            _context.SaveChanges();
            return Ok(contact);
        }

        [HttpPut("update-by-id/{id}")]
        public IActionResult UpdateContactById(int id,UpdateContact updateContact) 
        { 
            var contact=_context.Contacts.FirstOrDefault(x => x.Id == id);
            if(contact != null)
            {
                contact.Name = updateContact.Name;
                contact.Email = updateContact.Email;
                contact.Phone = updateContact.Phone;
                contact.Address = updateContact.Address;
                _context.SaveChanges();
                return Ok(contact);
            }
            return NotFound();
        }

        [HttpDelete("delete-by-id/{id}")]
        public IActionResult DeleteContacts(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(x => x.Id == id);
            if (contact != null)
            {
                _context.Remove(contact);
                _context.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

        
    }
}
