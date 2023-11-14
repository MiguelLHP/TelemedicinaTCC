using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Telemedicina_TCC.Areas.Identity.Data;
using Telemedicina_TCC.Data;
using Telemedicina_TCC.Models;

namespace Telemedicina_TCC.Views.Atendimento
{
    public class IndexModel : PageModel
    {
        private readonly Telemedicina_TCC.Data.ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(Telemedicina_TCC.Data.ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Atendimentos> Atendimentos { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var claimType = _userManager.GetClaimsAsync(user).Result.FirstOrDefault().Type;
            if (_context.Atendimentos != null)
            {
                if (claimType == "ADMINCODE" || claimType == "CRM")
                    Atendimentos = await _context.Atendimentos.Include(x => x.Pacient).Where(x => x.StatusAtendimento == EStatusAtendimento.Aguardando).ToListAsync();
                    //Atendimentos = await _context.Atendimentos.Include(x => x.Pacient).ToListAsync();

                if (claimType == "CPF")
                    Atendimentos = await _context.Atendimentos.Where(x => x.Pacient.Id == user.Id).ToListAsync();
            }
        }
    }
}
