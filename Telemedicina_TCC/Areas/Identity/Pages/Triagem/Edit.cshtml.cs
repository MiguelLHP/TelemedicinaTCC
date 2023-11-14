using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Telemedicina_TCC.Data;
using Telemedicina_TCC.Models;

namespace Telemedicina_TCC.Areas.Identity.Pages.Triagem
{
    public class EditModel : PageModel
    {
        private readonly Telemedicina_TCC.Data.ApplicationDBContext _context;

        public EditModel(Telemedicina_TCC.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Triagens Triagens { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Triagens == null)
            {
                return NotFound();
            }

            var triagens =  await _context.Triagens.FirstOrDefaultAsync(m => m.ID == id);
            if (triagens == null)
            {
                return NotFound();
            }
            Triagens = triagens;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Triagens).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TriagensExists(Triagens.ID))
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

        private bool TriagensExists(Guid id)
        {
          return (_context.Triagens?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
