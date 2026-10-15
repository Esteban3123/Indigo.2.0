



CREATE VIEW [Billing].[VReportBasicBilling]
as

	select	
		i.Id InvoiceId
		,bb.Id BasicBillingId
		,i.InvoiceNumber
		,i.InvoiceDate
		,i.Observation
		,i.DocumentType
		--,(i.TotalInvoice + i.ThirdPartyDiscountValue) as SubTotalInvoice
		,i.ThirdPartyDiscountValue
		,ar.Value TotalAccountReceivable
		,i.ThirdPartySalesValue
		,i.Status
		     
		------USER
		,i.InvoicedUser
		,u.UserCode
		, SUBSTRING(PU.FirstName + ' ' + PU.FirstLastName, 1 , 15) as FullNameUser

		--CLIENT
		,i.ThirdPartyId
		,tp.PersonType as IdentificationType ---ACOMODAR
		,tp.Nit as Identification
		,tp.DigitVerification
		,tp.Name ThirdPartyFullName
		, (select top 1 Addresss from Common.[Address] (nolock) Ad where Ad.IdPerson = tp.PersonId) as ThirdPartyAddress
		, (select top 1 Phone from Common.Phone (nolock) Ph where Ph.IdPerson = tp.PersonId and ph.IdPhoneType = 1) as ThirdPartyPhone
		
		------------ DATOS FACTURA ELECTRONICA ------------
		, IIF(i.InvoiceExpirationDate IS NULL, 0, DATEDIFF(DAY, i.InvoiceDate, i.InvoiceExpirationDate)) as Term
		, IIF
		(
			i.InvoiceValue = ISNULL(vpm.[Value], 0),
			'Contado',
			'Crédito'
		) as PaymentMeans
		, CASE vpm.PaymentMethodTypes
			WHEN 1 THEN 'Efectivo'
			WHEN 2 THEN 'Cheque'
			WHEN 3 THEN 'Tarjeta Crédito'
			WHEN 4 THEN 'Consiganción bancaria'
			ELSE 'Acuerdo mutuo'
		END PaymentMethod
		, i.CUFE
		, i.QR
		, CASE ed.Status
			WHEN 0 THEN 'Erronea'
			WHEN 1 THEN 'Registrada'
			WHEN 2 THEN 'Enviada'
			WHEN 3 THEN 'Valida'
			WHEN 4 THEN 'Invalida'
			WHEN 88 THEN 'Envio en Proceso'
			WHEN 99 THEN 'Validación en Proceso'
			ELSE 'Procesando...'
		END StatusDIAN
		, ed.ValidationDate

		--INVOICEDETAAIL
		--Product O SERVICIO CODIGO
		--ARTICULOS
		--IVA
		--DESCUENTO
		--CONCEPTOS DE FACTURACION
		--TIPO PRODUCTO O SERVICIO O ACTIVO 
		--CANTIDAD
		--TotalSalesPrice
		--ThirdPartySalesPrice
from Billing.BasicBilling bb
join Billing.Invoice i on i.Id = bb.InvoiceId
join Portfolio.AccountReceivable ar on ar.InvoiceId = i.Id
join Common.ThirdParty tp on i.ThirdPartyId = tp.Id
LEFT JOIN Security.[UserInt] u ON u.UserCode = i.InvoicedUser 
LEFT JOIN Security.PersonInt pu ON U.IdPerson = PU.Id 
OUTER APPLY
(
	SELECT TOP 1 EntityId, ed.EntityName, ValidationDate, [Status]
	FROM Billing.ElectronicDocument ed WITH (NOLOCK)
	WHERE ed.EntityName = 'Invoice' AND ed.EntityId = i.Id
	ORDER BY ValidationDate DESC
) ed
LEFT JOIN 
(
	SELECT InvoiceId, SUM([Value]) [Value], MIN(PaymentMethodTypes) PaymentMethodTypes
	FROM Billing.ViewPaymentMethods WITH (NOLOCK)
	WHERE AccountReceivableType NOT IN (4, 6)
	GROUP BY InvoiceId
) vpm ON i.Id = vpm.InvoiceId