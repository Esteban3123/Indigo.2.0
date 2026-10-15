



CREATE VIEW [Billing].[ViewUpdateJournalVoucherToInvoice]
AS
	select 
			i.Id as InvoiceId,
			i.InvoiceNumber,
			jv.Id AS JournalVoucherId
	from Billing.Invoice AS i WITH(NOLOCK)
	JOIN GeneralLedger.JournalVouchers AS jv WITH(NOLOCK) ON i.Id =jv.EntityId and jv.EntityName ='Invoice'
	JOIN GeneralLedger.LegalBook AS lb WITH(NOLOCK) on jv.LegalBookId =lb.Id
	WHERE i.JournalVoucherId =0 and lb.OfficialBook =1 and NOT jv.Detail LIKE 'Anulación%'