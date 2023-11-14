using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Telemedicina_TCC.Areas.Identity.Data;
using Telemedicina_TCC.Data;
using Telemedicina_TCC.Models;

namespace Telemedicina_TCC.Areas.Identity.Pages.Triagem
{
    public class IndexModel : PageModel
    {
        private readonly Telemedicina_TCC.Data.ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(Telemedicina_TCC.Data.ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<Triagens> Triagens { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Triagens != null)
            {
                Triagens = await _context.Triagens.ToListAsync();
            }
        }

        public async Task<IActionResult> OnGetCreateTriagem(String[] triagem, string userID)
        {
            triagem = triagem[0].Split(",");
            var user = _userManager.Users.FirstOrDefaultAsync(u => u.Id == userID).Result;
            var triagens = new Triagens();
            triagens.Alergia = triagem[0].ToUpper() == "SIM" ? true : false;
            triagens.DoencaCronica = triagem[0].ToUpper() == "SIM" ? true : false;
            triagens.Diabetes = triagem[0].ToUpper() == "SIM" ? true : false;
            triagens.ProblemaRespiratorio = triagem[0].ToUpper() == "SIM" ? true : false;
            triagens.Created = DateTime.Now;
            triagens.Pressao = "13/8";
            triagens.Peso = 80;
            triagens.Pacient = user;
            triagens.Temperatura = 35;

            var resultTriagem = _context.Triagens.Add(triagens).State;


            if (resultTriagem == EntityState.Added)
            {
                Atendimentos atendimento = new Atendimentos();
                atendimento.StatusAtendimento = EStatusAtendimento.Aguardando;
                atendimento.DataHora = DateTime.Now;
                atendimento.Created = DateTime.Now;
                atendimento.Pacient = user;
                atendimento.Doctor = null;
                atendimento.Resultado = "";
                atendimento.ConnectionID = "";

                _context.Atendimentos.Add(atendimento);

                var result = await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
