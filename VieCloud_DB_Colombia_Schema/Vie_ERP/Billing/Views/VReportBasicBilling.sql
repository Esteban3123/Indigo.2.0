

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
			ELSE ''
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de facturación básica que consolida en una sola consulta toda la información relevante de cada factura emitida: encabezado de la factura (número, fecha, estado, tipo de documento, observaciones), datos del cliente o tercero pagador (tipo y número de identificación, nombre, dirección, teléfono), valores cobrados (total cuenta por cobrar, descuentos, ventas a terceros), información del usuario que facturó, condiciones de pago (contado o crédito, plazo en días, método de pago como efectivo, cheque o tarjeta) y estado de la factura electrónica ante la DIAN (CUFE, QR, fecha de validación y estado del documento electrónico). Integra las tablas de facturación básica, facturas, cuentas por cobrar de cartera, terceros, usuarios del sistema y documentos electrónicos para proveer una fuente unificada orientada a reportería y consulta operativa del proceso de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportBasicBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportBasicBilling';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida datos básicos de facturación junto con información del cliente, usuario emisor, medios de pago y estado de la factura electrónica ante la DIAN para impresión o exportación de facturas.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Billing.BasicBilling vinculado a una Billing.Invoice; La factura debe tener una cuenta por cobrar asociada en Portfolio.AccountReceivable (JOIN obligatorio); El tercero de la factura debe existir en Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el documento electrónico más reciente por factura (TOP 1 ORDER BY ValidationDate DESC) y solo de tipo EntityName=''Invoice''; Para calcular el total pagado se excluyen los métodos de pago con AccountReceivableType IN (4, 6); Cuando hay múltiples métodos de pago, se toma el MIN(PaymentMethodTypes) como tipo representativo; La dirección y teléfono del tercero se obtienen tomando solo el primer registro (TOP 1) y el teléfono únicamente del tipo IdPhoneType=1; El nombre completo del usuario emisor se trunca a los primeros 15 caracteres; Las facturas sin documento electrónico, cuenta por usuario o métodos de pago se incluyen igual (LEFT/OUTER APPLY) con valores nulos en esos campos', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Facturación básica; Cuenta por cobrar; Tercero/Cliente; Factura electrónica DIAN; CUFE; QR de factura; Medio de pago (Contado/Crédito); Método de pago (Efectivo/Cheque/Tarjeta/Consignación); Plazo de pago (Term); Estado documento electrónico; Usuario facturador', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.VReportBasicBilling: Devuelve una fila por combinación BasicBilling-Invoice-AccountReceivable; si una factura tiene múltiples cuentas por cobrar, se duplica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si i.InvoiceExpirationDate IS NULL → Term = 0 else Term = DATEDIFF(DAY, InvoiceDate, InvoiceExpirationDate) (días de plazo de la factura); si i.InvoiceValue = ISNULL(vpm.Value, 0) (valor de la factura igual al total pagado) → PaymentMeans = ''Contado'' else PaymentMeans = ''Crédito''; si vpm.PaymentMethodTypes (1=Efectivo, 2=Cheque, 3=Tarjeta Crédito, 4=Consignación bancaria) → Mapea al texto correspondiente else '''' (vacío, no válido para DIAN) para cualquier otro valor o NULL; si ed.Status (0=Erronea, 1=Registrada, 2=Enviada, 3=Valida, 4=Invalida, 88=Envio en Proceso, 99=Validación en Proceso) → Mapea al texto del estado DIAN correspondiente else ''Procesando...'' para cualquier otro valor', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BasicBilling; Billing.Invoice; Portfolio.AccountReceivable; Common.ThirdParty; Security.UserInt; Security.PersonInt; Billing.ElectronicDocument; Billing.ViewPaymentMethods; Common.Address; Common.Phone', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBilling';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportBasicBilling';
GO
