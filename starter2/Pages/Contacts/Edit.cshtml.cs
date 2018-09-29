using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ContactManager.Authorization;

namespace ContactManager.Pages.Contacts
{
    public class EditModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManager;
        public EditModel(
        ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<ApplicationUser> userManager)
        {
            _context = context; _userManager = userManager; _authorizationService = authorizationService;
        }

        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User,
                Contact, ContactOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Contact).State = EntityState.Modified;

            var isAuthorized = await _authorizationService.AuthorizeAsync(User,
                Contact, ContactOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            if (Contact.Status == ContactStatus.Approved)
            {
                //if the contact is already approved
                // and the user making the edit does not have approval rights
                //change the status to submitted

                var canApprove = await _authorizationService.AuthorizeAsync(User,
                    Contact, ContactOperations.Approve);

                if (!canApprove.Succeeded)
                    Contact.Status = ContactStatus.Submitted;
            }
            else
            {
                //do nothing the status can stay approved
            }
        }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(Contact.ContactId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.ContactId == id);
        }
    }
}
