using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.Model
{
    public class PortfolioDeteriorationByClassificationDTO
    {
        // Estado del registro
        public int Code { get; set; }                // 0 = OK, 1001 = sin clasificación o porcentaje
        public string Message { get; set; }           // Mensaje de error o advertencia (puede ser null)

        // Tipo de libro
        public int? LegalBookId { get; set; }         // null en filas de error
        public int? TypeBook { get; set; }            // 1 = Fiscal, 2 = NIIF, null en error

        // Porcentajes y valores
        public decimal? Percentage { get; set; }       // Porcentaje aplicado según el libro
        public decimal? Value { get; set; }            // Valor del deterioro calculado (Balance * Percentage)
        public decimal? AccumulatedDeterioration { get; set; } // Value + último acumulado en la provisión

        // Identificación de cuenta por cobrar / factura
        public int? AccountReceivableId { get; set; }     // null en error
        public int? AccountReceivableType { get; set; }
        public string InvoiceNumber { get; set; }

        // Fechas relevantes
        public DateTime? AccountReceivableDate { get; set; }
        public DateTime? RadicatedDate { get; set; }
        public DateTime? DocumentDate { get; set; }
        public int? AgesId { get; set; }
        public int? Days { get; set; }                    // null en error
        public string AgesDescription { get; set; }

        // Tercero
        public int ThirdPartyId { get; set; }
        public string ThirdPartyNitName { get; set; }

        // Información financiera
        public string RegimenName { get; set; }
        public decimal? DocumentValue { get; set; }
        public decimal? Balance { get; set; }
        public int? GlosaPortfolioGlosadaId { get; set; }
        public decimal? ValueGlosado { get; set; }
        public decimal? BalanceGlosa { get; set; }
        public decimal? DeteriorationBalance { get; set; }

        // Clasificación de cartera
        public int? PortfolioClassificationId { get; set; } // null en error
        public string PortfolioClassification { get; set; }
    }
}
