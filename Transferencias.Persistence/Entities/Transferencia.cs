using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transferencias.Persistence.Entities
{
    public class Transferencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string resultado { get; set; }
        public string cuilOriginante { get; set; }
        public string cuilDestinatario { get; set; }
        public string cbuOrigen { get; set; }
        public string cbuDestino { get; set; }
        public double importe { get; set; }
        public string concepto { get; set; }
        public string descripcion { get; set; }
    }
}
