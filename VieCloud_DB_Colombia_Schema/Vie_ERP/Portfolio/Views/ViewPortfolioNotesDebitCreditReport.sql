

CREATE VIEW [Portfolio].[ViewPortfolioNotesDebitCreditReport]
AS
	SELECT  pn.Id,
			pn.Code,
			pn.NoteDate,
			pn.CustomerId,
			pn.Observations,
			pn.Nature,
			pn.NoteType,
			pn.PortfolioAdvanceId,
			pn.OperatingUnitId,
			pn.Status,
			pn.CreationUser,
			pn.CreationDate,
			pn.ModificationUser,
			pn.ModificationDate,
			pn.ConfirmationUser,
			pn.ConfirmationDate,
			pn.PortfolioTransferId,
			pn.AnnulmentUser,
			pn.AnnulmentDate,
			STRING_AGG(CAST(bn.CUDE AS VARCHAR(MAX)), ', ') AS CUDE,
			STRING_AGG(CAST(bn.QR AS VARCHAR(MAX)), ', ') AS QR,
			STRING_AGG(CAST(bn.Code AS VARCHAR(MAX)), ', ') AS Consecutive,
			min(ed.ValidationDate) ValidationDate,
			max(ed.Status) ElectronicDocumentStatus,
			min(ed.ShippingDate) ShippingDate,
			pn.CurrencyId,
			i.CodeAbbreviation CurrencyAbbreviation,
			i.CurrencyName
	from Portfolio.PortfolioNote pn WITH(NOLOCK)
	JOIN Common.Currency c WITH(NOLOCK) ON pn.CurrencyId=c.Id
	JOIN Common.ISO4217 i WITH(NOLOCK) ON c.ISO4217Id=i.Id
	LEFT JOIN Billing.BillingNote bn on bn.EntityId = pn.Id and bn.EntityName = 'PortfolioNote'
	LEFT JOIN Billing.ElectronicDocument ed on ed.EntityId = bn.Id and ed.EntityName='BillingNote'
	GROUP by 
			pn.Id,
			pn.Code,
			pn.NoteDate,
			pn.CustomerId,
			pn.Observations,
			pn.Nature,
			pn.NoteType,
			pn.PortfolioAdvanceId,
			pn.OperatingUnitId,
			pn.Status,
			pn.CreationUser,
			pn.CreationDate,
			pn.ModificationUser,
			pn.ModificationDate,
			pn.ConfirmationUser,
			pn.ConfirmationDate,
			pn.PortfolioTransferId,
			pn.AnnulmentUser,
			pn.AnnulmentDate,
			pn.CurrencyId,
			i.CodeAbbreviation,
			i.CurrencyName
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista para el reporte de notas débito y crédito de cartera. Integra las notas de cartera (acuerdos de pago, compromisos de cobro) con sus respectivas notas de facturación electrónica emitidas ante la DIAN, consolidando por cada nota de cartera los códigos CUDE, QR y consecutivos de las notas de facturación asociadas, así como el estado y fechas de validación y envío del documento electrónico. Incluye también la moneda de la nota con su abreviatura y nombre oficial según el estándar ISO 4217. Sirve para reportería de gestión de cartera, seguimiento de notas crédito y débito, y trazabilidad de la facturación electrónica vinculada a compromisos de cobro o ajustes de deuda de clientes o pagadores.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewPortfolioNotesDebitCreditReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewPortfolioNotesDebitCreditReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida cada nota de cartera con su moneda y, cuando existen, agrega los identificadores de las notas de facturación (CUDE, QR, consecutivos) y el estado/fechas del documento electrónico, para reportes de notas débito/crédito.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNotesDebitCreditReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La nota de cartera debe tener un CurrencyId existente en Common.Currency con un ISO4217Id válido; de lo contrario no se incluye en el resultado.; Las relaciones con Billing.BillingNote y Billing.ElectronicDocument son opcionales (LEFT JOIN); pueden ser nulas si la nota aún no está facturada o emitida electrónicamente.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNotesDebitCreditReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada nota de cartera debe tener una moneda válida en Common.Currency vinculada a un registro ISO4217 (JOINs internos obligatorios).; Solo se asocian notas de facturación cuyo EntityName=''PortfolioNote'' y cuyo EntityId coincida con el Id de la nota de cartera.; Solo se consideran documentos electrónicos cuyo EntityName=''BillingNote'' y EntityId coincida con la nota de facturación asociada.; Cuando una nota de cartera tiene múltiples notas de facturación, sus CUDE, QR y consecutivos se concatenan con coma como separador.; Por cada nota de cartera se reporta la primera fecha de validación y de envío (MIN) y el estado más alto (MAX) del documento electrónico asociado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNotesDebitCreditReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nota de cartera; Nota crédito/débito; Documento electrónico (DIAN); CUDE; QR de factura; Moneda ISO 4217; Cliente; Unidad operativa', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNotesDebitCreditReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.PortfolioNote: Devuelve una fila por cada nota de cartera (PortfolioNote) agrupada, con CUDE/QR/Consecutive concatenados vía STRING_AGG cuando hay varias BillingNote asociadas.; [RETURN_RESULT] Common.ISO4217: Incluye CodeAbbreviation y CurrencyName desde ISO4217 a través del JOIN Currency→ISO4217 sobre CurrencyId de la nota.; [RETURN_RESULT] Billing.ElectronicDocument: Agrega MIN(ValidationDate), MIN(ShippingDate) y MAX(Status) del ElectronicDocument vinculado a la BillingNote (EntityName=''BillingNote'') asociada a la nota de cartera (EntityName=''PortfolioNote'').', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNotesDebitCreditReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioNote; Common.Currency; Common.ISO4217; Billing.BillingNote; Billing.ElectronicDocument', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNotesDebitCreditReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioNotesDebitCreditReport';
GO
