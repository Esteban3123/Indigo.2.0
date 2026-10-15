



CREATE VIEW [Portfolio].[ViewReportRadicateInvoiceDocument]
AS
	WITH Invoice_cte AS (
						SELECT 
							i.InvoiceNumber,
							i.ElectronicInvoiceNumber,
							i.CurrencyId,
							cg.Id CareGroupId,
							cg.EntityType,
							co.Id ContractId,
							co.ContractNumber
						FROM Billing.Invoice i
						LEFT JOIN [Contract].[CareGroup] cg ON i.CareGroupId = cg.Id
						LEFT JOIN [Contract].[Contract] co ON cg.ContractId = co.Id
)

SELECT 
		rid.Id radicateInoviceDId, 
		ric.Id radicateInvoiceCId, 
		----------  CABECERA ----------
		ric.RadicatedConsecutive, 		
		ric.RadicatedDate,
		ric.ConfirmDate,
		ric.Comment radicateInvoiceComment, 
		c.Nit customerNit, 
		c.Name customerName,
		c.EPSCode, 
		CONCAT(u.UserCode, ' - ', p.Fullname) UserCodeName,
		----------- DETALLE -----------		
		rid.InvoiceNumber, 
		rid.IngressDate, 
		rid.InvoiceDate, 
		rid.ContractCode, 
		ISNULL(i.ContractNumber, i1.ContractNumber) ContractNumber,
		rid.PatientCode,
		rid.PatientName, 
		rid.InvoiceValueEntity, 
		rid.InvoiceValuePacient, 
		rid.CreditNoteValue, 
		rid.BalanceInvoice, 
		rid.[State] radicateInvoiceStatus, 
		ISNULL(i.EntityType, i1.EntityType) EntityType,
		CASE ISNULL(i.EntityType, i1.EntityType)
			WHEN 1 THEN 'EPS Contributivo'
			WHEN 2 THEN 'EPS Subsidiado' 
			WHEN 3 THEN 'ET Vinculados Municipios' 
			WHEN 4 THEN 'ET Vinculados Departamentos' 
			WHEN 5 THEN 'ARL Riesgos Laborales'
			WHEN 6 THEN 'MP Medicina Prepagada' 
			WHEN 7 THEN 'IPS Privada' 
			WHEN 8 THEN 'IPS Publica' 
			WHEN 9 THEN 'Regimen Especial' 
			WHEN 10 THEN 'Accidentes de transito'
			WHEN 11 THEN 'Fosyga' 
			WHEN 12 THEN 'Otros' 
			WHEN 99 THEN 'Particulares' 
			ELSE 'Sin Regimen' 
		END entityTypeName,
		cu.id AS CurrencyId,
		cu.Abbreviation as	CurrencyAbbreviation
	FROM Common.Customer c
	JOIN Portfolio.RadicateInvoiceC ric ON c.Id = ric.CustomerId
	JOIN Portfolio.RadicateInvoiceD rid ON ric.Id = rid.RadicateInvoiceCId
	JOIN GeneralLedger.CompanySettings cs on 1=1
	LEFT JOIN Invoice_cte i ON rid.InvoiceNumber = i.InvoiceNumber
	LEFT JOIN Invoice_cte i1 ON rid.invoiceNumber = i1.ElectronicInvoiceNumber
	JOIN Common.Currency  cu on cu.Id = ISNULL(i.CurrencyId,cs.OfficialCurrencyId)
	LEFT JOIN Security.[User] u ON ric.ConfirmUser = U.Id
	LEFT JOIN Security.Person p ON U.IdPerson = p.Id