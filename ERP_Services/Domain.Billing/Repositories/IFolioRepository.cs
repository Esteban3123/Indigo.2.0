using Domain.Billing.POCO;
using System;

namespace Domain.Billing.Repositories
{
    public interface IFolioRepository
    {
        Folio GetFolio(long folioId);

        AnnullateFolio GetAnnullateFolio(long invoiceId);

        /// <summary>
        /// metodo que obtiene informacion de la prefactura
        /// </summary>
        /// <param name="folioId"></param>
        /// <returns></returns>
        InvoicePartialMasterAccount GetVReportInvoicePartial(long folioId);
    }
}
