using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ContactManager.Authorization;

namespace ContactManager.Pages.Contacts
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManager;
        public DeleteModel(
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

            Contact = await _context.Contact.SingleOrDefaultAsync(m => m.ContactId == id);

            var isAuthorized = await _authorizationService.AuthorizeAsync(User,
              Contact, ContactOperations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            if (Contact == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contact = await _context.Contact.FindAsync(id);

            var isAuthorized = await _authorizationService.AuthorizeAsync(User,
              Contact, ContactOperations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            if (Contact != null)
            {
                _context.Contact.Remove(Contact);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}