
CREATE VIEW [Portfolio].[VReportListRadicatedInvoice]
AS
	SELECT 
		ri.Id AS RadicatedId,
		ri.RadicatedConsecutive AS RadicatedConsecutive,
		ri.RadicatedDate AS RadicatedDate,
		ri.DocumentDate AS DocumentDate,
		ri.ConfirmDate AS ConfirmDate,
		ri.State AS Status,

		c.Id AS IdCustomer,
		c.Nit AS NitCustomer,
		c.Name AS NameCustomer,
		
		rid.InvoiceNumber AS InvoiceNumber,
		rid.InvoiceDate AS InvoiceDate,
		rid.PatientCode AS PatientCode,
		rid.PatientName AS PatientName,
		rid.InvoiceValueEntity AS InvoiceValueEntity,
		rid.CreditNoteValue AS CreditNoteValue,
		rid.BalanceInvoice AS BalanceInvoice,
		rid.ContractCode AS ContractCode,
		rid.Devolution,
		rid.ConceptDevolution,
		
		cg.Code AS CodeCareGroup,
		cg.Name AS NameCareGroup,		
		cg.EntityType AS Regimen,
		pr.RegimenName
		
	FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
	JOIN Common.Customer c WITH (NOLOCK) ON ri.CustomerId = c.Id
	LEFT JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId	
	LEFT JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType NOT IN (4, 6)
	LEFT JOIN GeneralLedger.MainAccounts mar WITH (NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId 
	LEFT JOIN Portfolio.GetRegimes() pr ON mar.Number = pr.AccountNumber
	LEFT JOIN Billing.Invoice i WITH (NOLOCK) ON ar.InvoiceId = i.Id
	LEFT JOIN [Contract].[CareGroup] cg WITH (NOLOCK) ON cg.Id = i.CareGroupId