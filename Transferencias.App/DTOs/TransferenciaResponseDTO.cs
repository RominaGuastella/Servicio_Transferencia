namespace Transferencias.App.DTOs
{
    public class TransferenciaResponseDTO
    {
        public Int32 id { get; set; }
        public string resultado { get; set; }
        public double importe { get; set; }
        public string cuentaOrigen { get; set; }
        public string cuentaDestino { get; set; }
    }
}