namespace Transferencias.App.DTOs
{
    public class TransferenciaRequestDTO
    {
        public string cuilOriginante { get; set; }
        public string cuilDestinatario { get; set; }
        public string cbuOrigen { get; set; }
        public string cbuDestino { get; set; }
        public double importe { get; set; }
        public string concepto { get; set; }
        public string descripcion { get; set; }
    }
}