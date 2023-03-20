using BusinessProcess.Data;
using BusinessProcess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace BusinessProcess.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactApiDbContext dbContext;

        public ContactController(ContactApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //get all items
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contact = await dbContext.Contacts.ToListAsync();
            return View(contact);
            //return Ok(await dbContext.Contacts.ToListAsync());
        }
        //get item by id
        /*
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }
        */
        [HttpGet]
        public IActionResult AddContact()
        {
            return View();
        }
        //create item
        [HttpPost]
        public async Task<IActionResult> AddContact(ContactsModel addContactReq)
        {
            var contact = new ContactsModel()
            {
                Id = Guid.NewGuid(),
                Code = addContactReq.Code,
                Firstname = addContactReq.Firstname,
                Surename = addContactReq.Surename,
                Address1 = addContactReq.Address1,
                Address2 = addContactReq.Address2,
                DoB = addContactReq.DoB,
                Status = addContactReq.Status,
                Initials = addContactReq.Initials,
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            TempData["Message"] = "Added successful.";
            return RedirectToAction("AddContact");
        }
        //View item by id
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var contact = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if (contact != null)
            {
                var viewModel = new ContactsModel()
                {
                    Id = contact.Id,
                    Code = contact.Code,
                    Firstname = contact.Firstname,
                    Surename = contact.Surename,
                    Address1 = contact.Address1,
                    Address2 = contact.Address2,
                    DoB = contact.DoB,
                    Status = contact.Status,
                    Initials = contact.Initials,
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }
        //Update item by id
        [HttpPost]
        public async Task<IActionResult> View(ContactsModel model)
        {
            var contact = await dbContext.Contacts.FindAsync(model.Id);
            if(contact != null)
            {
                contact.Code = model.Code;
                contact.Firstname = model.Firstname;
                contact.Surename = model.Surename;
                contact.Address1 = model.Address1;
                contact.Address2 = model.Address2;
                contact.DoB = model.DoB;
                contact.Status = model.Status;
                contact.Initials = model.Initials;

                await dbContext.SaveChangesAsync();
                TempData["Message"] = "Update successful.";
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }
        //delete
        [HttpPost]
        public async Task<IActionResult> Delete(ContactsModel model)
        {
            var contact = await dbContext.Contacts.FindAsync(model.Id);
            if(contact != null)
            {
                dbContext.Contacts.Remove(contact);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
