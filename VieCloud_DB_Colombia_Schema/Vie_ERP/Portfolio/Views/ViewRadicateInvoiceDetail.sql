
CREATE VIEW [Portfolio].[ViewRadicateInvoiceDetail]
AS
select 
	rd.id as Row,
	ic.Code + ' - ' + ic.name as CategoryName,
	c.Id CurrencyId,
	c.Name CurrencyName,
	c.Abbreviation CurrencyAbbreviation,
	rd.* ,
	iep.CUV,
	rc.RadicatedConsecutive,
	CONCAT(cus.Nit,' - ',cus.Name) NitName,
	rc.DocumentDate,
	CASE rc.State
		WHEN 1 THEN 'Sin Confirmar'
		WHEN 2 THEN 'Confirmado'
		ELSE 'Anulado'
	END [HeaderState],
	CASE iep.StatusRIPS
		WHEN 1 THEN 'Sin validar'
		WHEN 2 THEN 'En proceso de validación'
		WHEN 3 THEN 'Validado'
		ELSE '' END AS  [RIPSStatusName],
	er.CosmoDBId as CosmoDBId

from Portfolio.RadicateInvoiceC rc  with (nolock) 
join Common.Customer cus WITH(NOLOCK) on rc.CustomerId = cus.Id
join Portfolio.RadicateInvoiceD rd  with (nolock) on rc.Id = rd.RadicateInvoiceCId 
join Portfolio.AccountReceivable ar with (nolock) on rd.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType in (1,2)
join Billing.InvoiceCategories ic with (nolock) on ar.InvoiceCategoryId = ic.id
left join Billing.Invoice i WITH (NOLOCK) on rd.InvoiceNumber = i.InvoiceNumber
left join Billing.ElectronicsProperties iep WITH(NOLOCK) ON i.Id = iep.EntityId and iep.EntityName ='Invoice'
left JOIN Billing.ElectronicsRIPS er on er.ElectronicsPropertiesId = iep.Id
left join Common.Currency c WITH (NOLOCK) on ISNULL(i.CurrencyId, ar.CurrencyId) = c.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del detalle de facturas radicadas ante entidades pagadoras (EPS, aseguradoras, empresas). Combina el encabezado del radicado de cartera con cada factura presentada al cobro, incluyendo la categoría de facturación (hospitalización, urgencias, ambulatorio, etc.), la moneda utilizada, el NIT y nombre del cliente o pagador, la fecha del documento y el estado del radicado (Sin Confirmar, Confirmado, Anulado). Además incorpora información de facturación electrónica: el Código Único de Validación (CUV), el estado de validación de los RIPS (Sin validar, En proceso, Validado) y el identificador del documento en CosmoDB. Sirve como fuente principal para reportes de cartera, seguimiento de cobros radicados, control de glosas y trazabilidad del proceso de facturación electrónica con RIPS.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewRadicateInvoiceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewRadicateInvoiceDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida el detalle de facturas radicadas en cartera con datos del encabezado de radicación, cliente, categoría, moneda, estado de validación RIPS y CUV asociado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe existir como cuenta por cobrar (Portfolio.AccountReceivable) con AccountReceivableType en (1,2); Debe existir el encabezado de radicación (RadicateInvoiceC) y su detalle (RadicateInvoiceD) relacionados; La categoría de la factura debe estar registrada en Billing.InvoiceCategories', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen radicados cuya cuenta por cobrar es de tipo 1 o 2; El vínculo con propiedades electrónicas y RIPS es opcional (LEFT JOIN); si no existen, los campos CUV, RIPSStatusName y CosmoDBId quedan vacíos/nulos; La factura en Billing.Invoice es opcional: el detalle de radicación puede existir sin factura en el módulo de Billing; Las propiedades electrónicas consideradas son exclusivamente las de entidad ''Invoice''', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicación de facturas; Cartera / Cuentas por cobrar; Cliente pagador (EPS/aseguradora/empresa); Categoría de factura; Factura de venta; RIPS (Registros Individuales de Prestación de Servicios de Salud); CUV (Código Único de Validación); Facturación electrónica; Estado de radicación (Sin confirmar/Confirmado/Anulado); Moneda', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.ViewRadicateInvoiceDetail: Devuelve únicamente facturas radicadas cuya cuenta por cobrar tiene AccountReceivableType IN (1,2); descarta otros tipos de cuentas por cobrar; [RETURN_RESULT] Portfolio.ViewRadicateInvoiceDetail: Traduce rc.State a etiquetas: 1=''Sin Confirmar'', 2=''Confirmado'', cualquier otro valor=''Anulado''; [RETURN_RESULT] Portfolio.ViewRadicateInvoiceDetail: Traduce iep.StatusRIPS a etiquetas: 1=''Sin validar'', 2=''En proceso de validación'', 3=''Validado'', otro valor=cadena vacía; [RETURN_RESULT] Portfolio.ViewRadicateInvoiceDetail: Compone NitName concatenando NIT y nombre del cliente en formato ''Nit - Name''; [RETURN_RESULT] Portfolio.ViewRadicateInvoiceDetail: Compone CategoryName concatenando código y nombre de la categoría de factura en formato ''Code - name''; [RETURN_RESULT] Portfolio.ViewRadicateInvoiceDetail: La moneda se determina priorizando i.CurrencyId de la factura; si es nulo, usa ar.CurrencyId de la cuenta por cobrar (ISNULL); [RETURN_RESULT] Portfolio.ViewRadicateInvoiceDetail: Las propiedades electrónicas se vinculan solo cuando iep.EntityName=''Invoice'' y iep.EntityId coincide con i.Id', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rc.State = 1 → HeaderState = ''Sin Confirmar'' else Si =2 → ''Confirmado''; cualquier otro valor → ''Anulado''; si iep.StatusRIPS = 1/2/3 → RIPSStatusName = ''Sin validar'' / ''En proceso de validación'' / ''Validado'' respectivamente else Cadena vacía; si i.CurrencyId IS NOT NULL → Usa la moneda de la factura else Usa la moneda de la cuenta por cobrar (ar.CurrencyId)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Common.Customer; Portfolio.AccountReceivable; Billing.InvoiceCategories; Billing.Invoice; Billing.ElectronicsProperties; Billing.ElectronicsRIPS; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateInvoiceDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewRadicateInvoiceDetail';
GO
