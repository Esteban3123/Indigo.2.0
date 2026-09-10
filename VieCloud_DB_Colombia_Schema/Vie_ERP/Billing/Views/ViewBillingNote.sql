CREATE VIEW [Billing].[ViewBillingNote]
AS
WITH Cte_ElectronicDocument AS (
	SELECT
		ROW_NUMBER() OVER (PARTITION BY ed.EntityName, ed.EntityId ORDER BY ed.Id DESC) AS RN,
		ed.EntityId,
		ed.Status,
		ed.CUFE,
		ed.ValidationDate
	FROM Billing.ElectronicDocument ed
	WHERE ed.EntityName = 'BillingNote'
)

-------

SELECT	bn.Id,
		IIF(bn.Nature = 1, 'NOTA DÉBITO', 'NOTA CRÉDITO') NoteTypeName,
		CASE WHEN bn.Nature = 2 AND bnd_counted.BillingNoteId IS NOT NULL THEN 'Contado' ELSE 'Crédito' END PaymentMethod,
		ISNULL(
			CASE vpm.PaymentMethodTypes
				WHEN 1 THEN 'Efectivo'
				WHEN 2 THEN 'Cheque'
				WHEN 3 THEN CASE vpm.CardType
					WHEN 1 THEN 'Tarjeta Débito'
					WHEN 2 THEN 'Tarjeta Crédito'
					ELSE ''
				END
				WHEN 4 THEN 'Debito Automatico'
				ELSE NULL
			END,
			'ZZZ'
		) PaymentMeans,
		bn.Code,
		bn.NoteDate,
		bn.Observations,
		------------------------------- CLIENTE -------------------------------
		CONCAT(tp.Nit, '-', tp.DigitVerification) CustomerPartyNit,
		tp.Name CustomerPartyName,
		a.Addresss CustomerAddress,
		c.Name CustomerCityName,
		ph.Phone CustomerPhone,
		-----------------------  DATOS NOTA ELECTRONICA -----------------------
		ISNULL(bn.CUDE, ed.CUFE) CUDE,
		ISNULL(bn.QR, CONCAT('https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey=', ISNULL(bn.CUDE, ed.CUFE))) QR,
		CASE ed.Status
			WHEN 0 THEN 'Erronea'
			WHEN 1 THEN 'Registrada'
			WHEN 2 THEN 'Enviada'
			WHEN 3 THEN 'Valida'
			WHEN 4 THEN 'Invalida'
			WHEN 88 THEN 'Envio en Proceso'
			WHEN 99 THEN 'Validación en Proceso'
			ELSE 'Procesando...'
		END StatusDIAN,
		ed.ValidationDate,
		pn.NoteType,
		pn.EntityName PortfolioNoteEntityName
FROM Common.ThirdParty tp WITH (NOLOCK)
JOIN Billing.BillingNote bn WITH (NOLOCK) ON tp.Id = bn.CustomerPartyId
LEFT JOIN Portfolio.PortfolioNote pn WITH(NOLOCK) on pn.Id = bn.EntityId and bn.EntityName ='PortfolioNote'
-------------------------------------------------------------------------------
LEFT JOIN
(
	SELECT IdPerson, MIN(Id) Id
	FROM Common.Address WITH (NOLOCK)
	GROUP BY IdPerson
) am ON tp.PersonId = am.IdPerson
LEFT JOIN Common.Address a WITH (NOLOCK) ON am.Id = a.Id
LEFT JOIN Common.City c WITH (NOLOCK) ON a.CityId = c.Id
-------------------------------------------------------------------------------
LEFT JOIN
(
	SELECT IdPerson, MIN(Id) Id
	FROM Common.Phone WITH (NOLOCK)
	GROUP BY IdPerson
) pm ON tp.PersonId = pm.IdPerson
LEFT JOIN Common.Phone ph WITH (NOLOCK) ON pm.Id = ph.Id
-------------------------------------------------------------------------------
LEFT JOIN
(
	SELECT DISTINCT BillingNoteId
	FROM Billing.BillingNoteDetail WITH (NOLOCK)
	WHERE ConceptId IN (2, 3)
) bnd_counted ON bn.Id = bnd_counted.BillingNoteId
-------------------------------------------------------------------------------
LEFT JOIN
(
	SELECT BillingNoteId, MIN(InvoiceId) InvoiceId
	FROM Billing.BillingNoteDetail WITH (NOLOCK)
	GROUP BY BillingNoteId
) bnd_inv ON bn.Id = bnd_inv.BillingNoteId
LEFT JOIN
(
	SELECT ic.InvoiceId, MIN(bb.InvoiceId) CopayInvoiceId
	FROM Billing.InvoiceCopay ic WITH (NOLOCK)
	JOIN Billing.BasicBilling bb WITH (NOLOCK) ON ic.BasicBillingId = bb.Id
	GROUP BY ic.InvoiceId
) copay ON bnd_inv.InvoiceId = copay.InvoiceId
OUTER APPLY
(
	SELECT TOP (1)
		v.InvoiceId,
		v.PaymentMethodTypes,
		v.CardType
	FROM Billing.ViewPaymentMethods v
	WHERE v.InvoiceId = COALESCE(copay.CopayInvoiceId, bnd_inv.InvoiceId)
	ORDER BY v.DocumentDate ASC
) vpm
-------------------------------------------------------------------------------
LEFT JOIN Cte_ElectronicDocument ed WITH (NOLOCK) ON bn.Id = ed.EntityId AND ed.RN = 1
GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el encabezado de las notas de facturación (notas crédito y notas débito) emitidas a clientes o pagadores, integrando los datos del tercero cliente (NIT, nombre, dirección y ciudad), la información del documento electrónico validado ante la DIAN (CUDE/CUFE, código QR, estado de validación y fecha) y, cuando aplica, el vínculo con la nota de cartera asociada. Sirve para reportería y consulta operativa de notas de facturación electrónica, permitiendo conocer en una sola consulta el estado DIAN de cada nota crédito o débito junto con los datos del cliente y su trazabilidad en cartera.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewBillingNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewBillingNote';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de notas crédito/débito de facturación con datos del cliente, su dirección principal y el estado del documento electrónico ante la DIAN para reportes y consultas.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe registro en Billing.BillingNote vinculado a un tercero (Common.ThirdParty) por CustomerPartyId.; Para obtener datos electrónicos DIAN debe existir registro en Billing.ElectronicDocument con EntityName=''BillingNote''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Para cada nota se considera únicamente el documento electrónico más reciente (RN=1 ordenado por ed.Id DESC particionado por EntityName/EntityId).; Solo se consideran documentos electrónicos cuyo EntityName=''BillingNote''.; La dirección del cliente expuesta corresponde a la de menor Id registrada para la persona (MIN(Id) por IdPerson).; El NIT del cliente se expone concatenando Nit y DigitVerification con guion (''Nit-DV'').; Si no hay CUDE propio en la nota, el QR siempre apunta al portal DIAN (catalogo-vpfe.dian.gov.co) con el CUFE del documento electrónico.; El medio de pago se resuelve sobre la factura de copago (InvoiceCopay→BasicBilling) si existe, igual que SP_GetPaymentMethodsByInvoiceId; en caso contrario se usa la factura original (MIN(InvoiceId) de BillingNoteDetail).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nota crédito; Nota débito; Documento electrónico DIAN; CUFE; CUDE; QR de validación DIAN; Cliente/tercero; Nota de cartera (PortfolioNote); Dirección y ciudad del cliente; Estado de validación DIAN', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewBillingNote: Devuelve una fila por cada Billing.BillingNote con su tercero cliente, dirección mínima, ciudad y último documento electrónico asociado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si bn.Nature = 1 → NoteTypeName se etiqueta como ''NOTA DEBITO'' else NoteTypeName se etiqueta como ''NOTA CREDITO''; si bn.CUDE es NULL → Se usa ed.CUFE como CUDE y se construye QR con la URL https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey=<CUFE> else Se usa el CUDE/QR propio de la nota; si ed.Status ∈ {0,1,2,3,4,88,99} → Se traduce a etiqueta legible: 0=Erronea, 1=Registrada, 2=Enviada, 3=Valida, 4=Invalida, 88=Envio en Proceso, 99=Validación en Proceso else Se etiqueta como ''Procesando...''; si bn.EntityName = ''PortfolioNote'' → Se enlaza con Portfolio.PortfolioNote para exponer NoteType y EntityName de la nota de cartera origen else Los campos de PortfolioNote quedan en NULL; si EXISTS en Billing.InvoiceCopay para la factura de la nota → El medio de pago (PaymentMeans) se resuelve desde los pagos de la factura del copago (COALESCE(copay.CopayInvoiceId, bnd_inv.InvoiceId)), alineando el comportamiento con SP_GetPaymentMethodsByInvoiceId else Se usa la factura original de BillingNoteDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ElectronicDocument; Billing.BillingNote; Billing.BillingNoteDetail; Billing.InvoiceCopay; Billing.BasicBilling; Billing.ViewPaymentMethods; Common.ThirdParty; Portfolio.PortfolioNote; Common.Address; Common.City; Common.Phone', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNote';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingNote';
GO
