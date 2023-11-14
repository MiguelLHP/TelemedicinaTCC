using System.ComponentModel.DataAnnotations;
using Telemedicina_TCC.Areas.Identity.Data;

namespace Telemedicina_TCC.Models
{
    public class Atendimentos
    {
        public Atendimentos()
        {
            ID = Guid.NewGuid();
        }

        [Key]
        public Guid ID { get; set; }
        public string Resultado { get; set; } = string.Empty;
        [Display(Name = "Status Atendimento")]
        public EStatusAtendimento StatusAtendimento { get; set; }
        public String ConnectionID { get; set; }
        public DateTime DataHora { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public ApplicationUser Pacient { get; set; }
        public ApplicationUser? Doctor { get; set; }
    }
}
