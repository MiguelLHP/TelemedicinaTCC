using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Telemedicina_TCC.Areas.Identity.Data;
using Telemedicina_TCC.Data;
using Telemedicina_TCC.Models;

namespace Telemedicina_TCC.Areas.Identity.Pages.Atendimento
{
    public class ChatAtendimentoModel : PageModel
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatAtendimentoModel(ApplicationDBContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public string idAtendimento { get; set; }
        public string idUser { get; set; }
        public Triagens Triagem { get; set; } = default!;
        public Atendimentos Atendimento { get; set; } = default!;
        public async Task OnGetAsync()
        {
            idAtendimento = Request.Query["id"];
            idUser = Request.Query["user"];

            if(idUser != null)
            {
                var atendimento = _dbContext.Atendimentos.Where(x => x.ID == new Guid(idAtendimento)).FirstOrDefault();
                atendimento.StatusAtendimento = EStatusAtendimento.EmAtendimento;
                atendimento.Doctor = _userManager.Users.FirstOrDefault(x => x.Id == idUser);

                _dbContext.Atendimentos.Update(atendimento);
                await _dbContext.SaveChangesAsync();
            }
            

            Atendimento = _dbContext.Atendimentos.Where(x => x.ID == new Guid(idAtendimento)).Include(x => x.Pacient).Include(x => x.Doctor).FirstOrDefault();
            Triagem = _dbContext.Triagens.Where(x => x.Pacient.Id == Atendimento.Pacient.Id &&
                x.Created.ToString().Contains(Atendimento.Created.ToString("yyyy-MM-dd HH:mm"))).FirstOrDefault();
        }

        public async Task OnPostChangeAtendimentoStatus(string idAtendimento, string userID)
        {
            
        }

        public async Task<IActionResult> OnPostCloseAtendimento(string  idAtendimento, string resultado, string? user)
        {
            if(user == "paciente")
                return LocalRedirect("~/Identity/Atendimento/Index");

            var atendimento = _dbContext.Atendimentos.Where(x => x.ID == new Guid(idAtendimento)).FirstOrDefault();
            atendimento.Resultado = resultado;
            atendimento.StatusAtendimento = EStatusAtendimento.Atendido;

            _dbContext.Atendimentos.Update(atendimento);
            await _dbContext.SaveChangesAsync();

            return LocalRedirect("~/Identity/Atendimento/Index");
        }
    }
}
