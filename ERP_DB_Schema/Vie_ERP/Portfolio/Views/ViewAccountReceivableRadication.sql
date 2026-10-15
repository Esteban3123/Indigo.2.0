CREATE VIEW [Portfolio].[ViewAccountReceivableRadication]
AS
	SELECT	CAST(rid.Id AS VARCHAR(20)) Id, 
			ar.Id AccountReceivableId, 
			ri.RadicatedConsecutive, 
			ri.ConfirmDate RadicatedDate
	FROM Portfolio.AccountReceivable ar
	JOIN Portfolio.RadicateInvoiceD rid ON ar.InvoiceNumber = rid.InvoiceNumber
	JOIN Portfolio.RadicateInvoiceC ri ON rid.RadicateInvoiceCId = ri.Id
	WHERE rid.Devolution = 0