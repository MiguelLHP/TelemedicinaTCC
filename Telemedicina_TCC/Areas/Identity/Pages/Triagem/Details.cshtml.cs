using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Telemedicina_TCC.Data;
using Telemedicina_TCC.Models;

namespace Telemedicina_TCC.Areas.Identity.Pages.Triagem
{
    public class DetailsModel : PageModel
    {
        private readonly Telemedicina_TCC.Data.ApplicationDBContext _context;

        public DetailsModel(Telemedicina_TCC.Data.ApplicationDBContext context)
        {
            _context = context;
        }

      public Triagens Triagens { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Triagens == null)
            {
                return NotFound();
            }

            var triagens = await _context.Triagens.FirstOrDefaultAsync(m => m.ID == id);
            if (triagens == null)
            {
                return NotFound();
            }
            else 
            {
                Triagens = triagens;
            }
            return Page();
        }
    }
}
