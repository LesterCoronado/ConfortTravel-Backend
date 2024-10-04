namespace BackendConfortTravel.DTO
{
    public class PagoPlanillaDto
    {
        public int NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public string Moneda { get; set; }
        public double SalarioBase { get; set; }
        public int IdPlanilla { get; set; }
        public string NombreEmpleado { get; set; }
    }
}
