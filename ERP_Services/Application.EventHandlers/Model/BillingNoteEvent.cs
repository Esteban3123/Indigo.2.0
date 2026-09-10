using System;
using System.Collections.Generic;

namespace Application.EventHandlers.Model
{
    public class BillingNoteEvent
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string EntityName { get; set; }
        public string CodeNotePortfolio { get; set; }
        public int ThirdPartyId { get; set; }
        public string Observations { get; set; }
        public byte Nature { get; set; }
        public int OperatingUnitId { get; set; }
        public List<BillingNoteDetailEvent> billingNoteDetailEvent { get; set; }
    }

    public class BillingNoteDetailEvent
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string CUFE { get; set; }
        public DateTime DocumentDate { get; set; }
        public decimal AdjusmentValue { get; set; }
        public decimal BillingValue { get; set; }
        public decimal DiscountValue { get; set; }
    }
}
