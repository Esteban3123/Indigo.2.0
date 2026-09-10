using System.Data;

namespace Domain.Inventory.POCO
{
    public class AllSurgicalPackageProcessWrapper
    {
        public int ProgramationId { get; set; }
        public DataTable Components { get; set; }
        public DataUnilogMovementModel DataUnilogMovement { get; set; }
        public int UserId { get; set; }
    }

    public class DataUnilogMovementModel
    {
        public string Atendimento { get; set; }
        public string CentroCustoOrigem { get; set; }
        public string CentroCustoDestino { get; set; }
        public string Codigo { get; set; }
        public string CodigoDocumento { get; set; }
        public int? CodigoItemDocumento { get; set; }
        public string CodigoEtiqueta { get; set; }
        public string CodigoEAN { get; set; }
        public string DataMovimentacao { get; set; }
        public string EmpresaOrigem { get; set; }
        public string EmpresaDestino { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public int TipoDocumento { get; set; }
        public int TipoMovimentacao { get; set; }
        public string Usuario { get; set; }
        public string CodigoProgramacao { get; set; }
        public string Proveedor { get; set; }
        public string Programa { get; set; }
        public bool Nutricao { get; set; }
    }
}
