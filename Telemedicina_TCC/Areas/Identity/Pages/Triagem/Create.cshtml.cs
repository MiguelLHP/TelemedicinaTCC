using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Telemedicina_TCC.Data;
using Telemedicina_TCC.Models;

namespace Telemedicina_TCC.Areas.Identity.Pages.Triagem
{
    public class CreateModel : PageModel
    {
        private readonly Telemedicina_TCC.Data.ApplicationDBContext _context;

        public CreateModel(Telemedicina_TCC.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Triagens Triagens { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Triagens == null || Triagens == null)
            {
                return Page();
            }

            _context.Triagens.Add(Triagens);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
