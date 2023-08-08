namespace CXPApp.Models
{
    public class WebServiceResponse
    {
        public List<AsientoContable> AsientoContable { get; set; }
    }

    public class AsientoContable
    {
        public int Id { get; set; }
        public string descripcion { get; set; }
        public string tipoMovimiento { get; set; }
        public string fechaRegistro { get; set; }
        public decimal monto { get; set; }
        public string estado { get; set; }
    }
}
