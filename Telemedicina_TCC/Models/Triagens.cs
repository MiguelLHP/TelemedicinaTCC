using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telemedicina_TCC.Areas.Identity.Data;

namespace Telemedicina_TCC.Models
{
    public class Triagens
    {
        public Triagens()
        {
            ID = new Guid();
        }

        [Key]
        public Guid ID { get; set; }
        public bool? Alergia { get; set; }
        public bool? DoencaCronica { get; set; }
        public bool Diabetes { get; set; }
        public string Pressao { get; set; }
        public bool ProblemaRespiratorio { get; set; }
        public int Peso { get; set; }
        public int Temperatura { get; set; }
        public DateTime Created { get; set; }
        public ApplicationUser Pacient { get; set; }
    }
}
