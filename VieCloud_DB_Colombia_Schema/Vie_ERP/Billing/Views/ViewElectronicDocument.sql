CREATE VIEW [Billing].[ViewElectronicDocument]
AS
	with cte_OffcialCurrency as (SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings with(NOLOCK))
	
	SELECT	ed.Id,
			ed.OperatingUnitId,
			ed.CustomerPartyId,
			ed.EntityId,
			ed.EntityName,
			ed.DocumentDate,
			ed.DocumentType,
			ed.Status,
			ed.ShippingDate,
			isnull(ed.Prefix,'') Prefix,
			ed.DocumentNumber,
			ed.CUFE,
			adc.NOMCENATE CenterAttention,
			i.InvoiceNumber InvoiceNumber,
			c.Id CurrencyId,
			c.Abbreviation CurrencyAbbreviation,
			0 AS NoteTypeDetail,
			ed.Retry
	FROM Billing.ElectronicDocument ed WITH(NOLOCK)
	JOIN Billing.Invoice i WITH(NOLOCK) ON ed.EntityId = i.Id
	JOIN cte_OffcialCurrency cte on 1=1
	JOIN Common.Currency c WITH(NOLOCK) on c.Id =ISNULL(i.CurrencyId,cte.OfficialCurrencyId)
	LEFT JOIN dbo.ADINGRESO adi WITH(NOLOCK) ON i.AdmissionNumber = adi.NUMINGRES
	LEFT JOIN dbo.ADCENATEN adc WITH(NOLOCK) ON adi.CODCENATE = adc.CODCENATE
	WHERE ed.EntityName = 'Invoice'
UNION ALL
	SELECT	ed.Id,
			ed.OperatingUnitId,
			ed.CustomerPartyId,
			ed.EntityId,
			ed.EntityName,
			ed.DocumentDate,
			ed.DocumentType,
			ed.Status,
			ed.ShippingDate,
			isnull(ed.Prefix,'') Prefix,
			ed.DocumentNumber,
			ed.CUFE,
			NULL CenterAttention,
			STUFF
			(
				(
					SELECT	', ' + bnd.InvoiceNumber
					FROM Billing.BillingNoteDetail bnd 
					WHERE ed.EntityId = bnd.BillingNoteId
					FOR XML PATH ('')
				), 1, 1, ''
			) InvoiceNumber,
			c.Id CurrencyId,
			c.Abbreviation CurrencyAbbreviation,
			cast(IIF(COALESCE(pn.NoteType,0)=6 or pn.EntityName='Glosas' ,1,0) as BIT) AS NoteTypeDetail,
			ed.Retry
	FROM Billing.ElectronicDocument ed
	LEFT JOIN Billing.BillingNote bn on ed.EntityId = bn.id
	LEFT JOIN Portfolio.PortfolioNote pn on bn.EntityId = pn.id
	LEFT JOIN Billing.Invoice i on i.id = bn.EntityId and bn.EntityName = 'Invoice'
	JOIN cte_OffcialCurrency cte on 1=1
	JOIN Common.Currency c WITH(NOLOCK) on c.Id =COALESCE(pn.CurrencyId,i.CurrencyId,cte.OfficialCurrencyId)
	WHERE ed.EntityName = 'BillingNote'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todos los documentos electrónicos emitidos ante la DIAN, integrando tanto facturas de venta como notas crédito y débito (notas de facturación). Para cada documento electrónico expone su identificador interno, tipo de documento, estado de envío y validación, fecha de emisión, fecha de envío, prefijo, número de documento, CUFE, moneda utilizada (tomando la moneda oficial de la empresa si la factura no tiene una asignada), número(s) de factura relacionada(s) y el nombre del centro de atención del ingreso vinculado. Sirve de base para reportería y consulta de facturación electrónica, permitiendo rastrear el ciclo completo de un documento electrónico desde la factura o nota hasta su respuesta DIAN, con contexto del paciente, sede hospitalaria y tipo de nota (incluyendo glosas).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewElectronicDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewElectronicDocument';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida documentos electrónicos (facturas y notas de facturación) con su número asociado, moneda efectiva, centro de atención y clasificación de notas tipo glosa para reporte unificado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'GeneralLedger.CompanySettings debe contener al menos un registro con OfficialCurrencyId definido (se toma TOP 1 sin ORDER BY).; Common.Currency debe contener el Id resuelto por COALESCE; de lo contrario el documento se excluye por el JOIN.; Para EntityName=''Invoice'', debe existir una fila en Billing.Invoice cuyo Id coincida con ElectronicDocument.EntityId (JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La vista solo expone documentos electrónicos cuyo EntityName sea ''Invoice'' o ''BillingNote''.; Siempre hay un CurrencyId resuelto: si el documento no tiene moneda propia, se cae a la moneda oficial de la compañía.; Para facturas, NoteTypeDetail siempre es 0; la marca de glosa solo aplica a notas de facturación.; El Prefix nunca se devuelve NULL (siempre '''' o el valor).; En la rama BillingNote, el centro de atención no se calcula (siempre NULL).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Documento electrónico (DIAN); CUFE; Factura; Nota de facturación (crédito/débito); Glosa; Centro de atención; Ingreso/admisión del paciente; Moneda oficial de la compañía; Nota de cartera (PortfolioNote)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando ed.EntityName=''Invoice'' se retorna InvoiceNumber tomado de Billing.Invoice y CenterAttention tomado de ADCENATEN vía ADINGRESO (NUMINGRES = AdmissionNumber).; [RETURN_RESULT] N/A: Cuando ed.EntityName=''BillingNote'' se retorna InvoiceNumber como concatenación con coma de todos los Billing.BillingNoteDetail.InvoiceNumber asociados al BillingNote (vía STUFF + FOR XML PATH) y CenterAttention en NULL.; [RETURN_RESULT] N/A: NoteTypeDetail = 1 cuando PortfolioNote.NoteType = 6 o PortfolioNote.EntityName = ''Glosas''; en caso contrario 0 (siempre 0 para facturas).; [RETURN_RESULT] N/A: Prefix se devuelve como cadena vacía cuando es NULL (ISNULL(ed.Prefix,'''')).; [RETURN_RESULT] N/A: CurrencyId/Abbreviation se resuelve por prelación: PortfolioNote.CurrencyId → Invoice.CurrencyId → OfficialCurrencyId de CompanySettings.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ed.EntityName = ''Invoice'' → Resuelve InvoiceNumber desde Billing.Invoice y enriquece con centro de atención del ingreso del paciente (ADINGRESO/ADCENATEN); NoteTypeDetail fijo = 0. else ed.EntityName = ''BillingNote'': agrega InvoiceNumber concatenando facturas afectadas en BillingNoteDetail y evalúa NoteTypeDetail según PortfolioNote.; si COALESCE(pn.NoteType,0)=6 OR pn.EntityName=''Glosas'' → NoteTypeDetail = 1 (nota de tipo glosa). else NoteTypeDetail = 0.; si i.CurrencyId IS NULL (rama Invoice) o pn.CurrencyId e i.CurrencyId NULL (rama BillingNote) → Se utiliza la moneda oficial de la empresa (CompanySettings.OfficialCurrencyId). else Se utiliza la moneda propia del documento.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Billing.ElectronicDocument; Billing.Invoice; Common.Currency; dbo.ADINGRESO; dbo.ADCENATEN; Billing.BillingNoteDetail; Billing.BillingNote; Portfolio.PortfolioNote', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicDocument';
GO
