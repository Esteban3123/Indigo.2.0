#region "Usings"
using Domain.Entities;
using System;
#endregion

namespace Domain.Treasury.Model
{
    public class PendingItemsToReconciled
    {
        /// <summary>
        /// Define el origen de la partida pendiente por conciliar
        /// 1. Libro de Bancos / 2. Extracto Bancario
        /// </summary>
        public byte Origin { get; set; }

        public string OriginName
        {
            get
            {
                if (Origin == 1)
                {
                    return "Libro de bancos";
                }
                else
                {
                    return "Extracto bancario";
                }
            }
        }

        public DateTime DocumentDate { get; set; }

        public int DocumentType { get; set; }

        public string DocumentTypeName
        {
            get
            {
                switch (DocumentType)
                {
                    case 1:
                        return "Recibo de caja";
                    case 2:
                        return "Comprobante de egreso";
                    case 3:
                        return "Notas";
                    default:
                        return "Consignaciones";
                }
            }
        }

        public byte ReconciledStatus { get; set; }

        public string ReconciledStatusName
        {
            get
            {
                switch (ReconciledStatus)
                {
                    case 1:
                        return "Partidas pendientes";
                    case 2:
                        return "Descartado";
                    default:
                        return "Estado no especificado";
                }
            }
        }

        public byte Nature { get; set; }

        public decimal Value { get; set; }

        public BankReconciliationAutomaticExtractDetail ExtractDetail { get; set; }

        public BankReconciliationAutomaticDetail DocumentDetail { get; set; }

        public bool Checked { get; set; }
    }
}
