

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
	JOIN Common.Currency  cu on cu.Id = COALESCE(i.CurrencyId, i1.CurrencyId, cs.OfficialCurrencyId)	
	LEFT JOIN Security.[User] u ON ric.ConfirmUser = U.Id
	LEFT JOIN Security.Person p ON U.IdPerson = p.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reportería para el proceso de radicación de facturas ante entidades pagadoras. Integra el encabezado del radicado (consecutivo, fecha de radicación, fecha de confirmación, NIT y nombre del cliente pagador, usuario confirmador) con el detalle de cada factura presentada al cobro (número de factura, fecha de ingreso, fecha de factura, código y nombre del paciente, valores cobrados a la entidad y al paciente, notas crédito y saldo). Cruza las facturas de Billing con sus grupos de atención y contratos para obtener el número de contrato y el tipo de entidad (EPS Contributivo, EPS Subsidiado, ARL, Medicina Prepagada, Particulares, entre otros), y determina la moneda aplicable según la factura o la configuración oficial de la compañía. Es la fuente principal para reportes de cartera radicada, seguimiento de cobros, glosas y conciliación con aseguradoras y EPS.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewReportRadicateInvoiceDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewReportRadicateInvoiceDocument';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de cabecera y detalle de radicaciones de facturas de cartera, enriqueciéndola con datos del cliente pagador, contrato, tipo de entidad, moneda y usuario que confirmó la radicación, para reportes.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoiceDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen radicaciones (RadicateInvoiceC) asociadas a un Customer válido en Common.Customer.; Cada radicación de cabecera tiene al menos un detalle en RadicateInvoiceD.; Existe configuración global en GeneralLedger.CompanySettings para resolver moneda oficial cuando la factura no tiene moneda asociada.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoiceDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El cliente (Customer) y la radicación de cabecera/detalle son obligatorios (INNER JOIN); sin ellos la fila no aparece.; La moneda mostrada nunca es NULL: si la factura no tiene CurrencyId se sustituye por la moneda oficial de la empresa.; El número de factura del detalle se considera equivalente sea como InvoiceNumber físico o ElectronicInvoiceNumber de Billing.Invoice.; El usuario y persona que confirmaron la radicación son opcionales (LEFT JOIN); pueden quedar vacíos en UserCodeName.; La clasificación de régimen siempre se resuelve a una etiqueta, usando ''Sin Regimen'' como valor por defecto.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoiceDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicación de facturas; Factura electrónica; Cliente pagador (EPS/ARL/IPS/Prepagada); Tipo de régimen/entidad; Contrato y grupo de cuidado; Nota crédito; Saldo de factura; Valor entidad vs. valor paciente; Moneda oficial de la empresa; Usuario confirmador de radicación', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoiceDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.ViewReportRadicateInvoiceDocument: Devuelve una fila por cada detalle de factura radicada (RadicateInvoiceD) cruzado con su cabecera (RadicateInvoiceC), cliente, contrato/grupo de cuidado de la factura, moneda y usuario confirmador.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoiceDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rid.InvoiceNumber coincide con Billing.Invoice.InvoiceNumber (alias i) → Toma ContractNumber, EntityType y CurrencyId del match por número de factura física. else Si no, intenta match por ElectronicInvoiceNumber (alias i1) y usa esos valores; si tampoco hay match, EntityType y ContractNumber quedan NULL.; si ISNULL(i.CurrencyId, cs.OfficialCurrencyId) determina la moneda → Usa la moneda de la factura si existe; en caso contrario usa la moneda oficial de la empresa (CompanySettings.OfficialCurrencyId).; si EntityType IN (1..12, 99) → Mapea a etiqueta legible: 1=EPS Contributivo, 2=EPS Subsidiado, 3=ET Vinculados Municipios, 4=ET Vinculados Departamentos, 5=ARL, 6=Medicina Prepagada, 7=IPS Privada, 8=IPS Pública, 9=Régimen Especial, 10=Accidentes de tránsito, 11=Fosyga, 12=Otros, 99=Particulares. else Cualquier otro valor o NULL se etiqueta como ''Sin Regimen''.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoiceDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Contract.CareGroup; Contract.Contract; Common.Customer; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; GeneralLedger.CompanySettings; Common.Currency; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoiceDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoiceDocument';
GO
