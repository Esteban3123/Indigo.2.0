

CREATE VIEW [Payments].[PaymentNotesDebitCredit]
AS
	SELECT	P.Id,P.Code,P.NoteDate,
			Case	P.Nature 
					When 1 Then 'Debito' 
					When 2 Then 'Credito' 
			End As Nature,P.Status,
			Case	P.IndicatesBillAdvance
					When 0 Then 'Factura' 
					When 1 Then 'Anticipo' 
					Else 'Otro' 
			End As IndicatesBillAdvance,
			S.Name,S.IdThirdParty,T.Nit,
			CASE	P.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado'
					WHEN 3 THEN 'Anulado'
					ELSE 'Otro' 
			END AS StatusName, 
			--Logica para sacar el valor de la nota de cuentas por pagar.
			Case	when P.Nature = 1 AND P.IndicatesBillAdvance = 0 Then IsNull(D.ValueDebit,0)+IsNull(AD.ValueBilling,0)
					When P.Nature = 1 AND P.IndicatesBillAdvance = 1 Then IsNull(D.ValueDebit,0) + IsNull(AD.ValueAdvance ,0)
					When P.Nature = 2 AND P.IndicatesBillAdvance = 0 Then IsNull(D.ValueCredit,0) + IsNull(AD.ValueBilling ,0)
					When P.Nature = 2 AND P.IndicatesBillAdvance = 1 Then IsNull(D.ValueCredit,0) + IsNull(AD.ValueAdvance ,0)
			Else 0
			End As Value,
			P.CurrencyId,
			c.Abbreviation as CurrencyAbbreviation
	FROM Payments.PaymentNotes AS P WITH (nolock)
	INNER JOIN Common.Supplier As S WITH (nolock) On P.idSupplier = S.Id
	INNER JOIN Common.ThirdParty As T WITH (nolock) On S.IdThirdParty = T.Id
	INNER JOIN 
	(
		Select D.IdPaymentsNote, Sum(Case when D.Nature = 1 Then D.Value Else 0 End) As ValueDebit, Sum(Case when D.Nature = 2 Then D.Value Else 0 End) As ValueCredit
		FROM Payments.PaymentsNoteDetails As D WITH (nolock) 
		Group by D.IdPaymentsNote
	) D On D.IdPaymentsNote = P.Id 
	INNER JOIN 
	(
		Select AD.PaymentNoteId, Sum(AD.AdjusmentValue) As ValueAdvance, Sum(AD.AdjustmentValueShare) As ValueBilling
		FROM Payments.PaymentNotesAccountPayableAdvance As AD WITH (nolock) 
		Group by AD.PaymentNoteid
	) AD On AD.PaymentNoteid = P.Id
	INNER JOIN Common.Currency c WITH(nolock) on P.CurrencyId= c.Id
UNION ALL
	SELECT	P.Id,P.Code,
			P.NoteDate,
			Case	P.Nature
					When 1 Then 'Debito'
					When 2 Then 'Credito' 
			End As Nature,
			P.Status,
			'Reversion CxP' As IndicatesBillAdvance,
			S.Name,S.IdThirdParty,
			T.Nit,
			CASE P.Status 
				WHEN 1 THEN 'Registrado'
				WHEN 2 THEN 'Confirmado'
				WHEN 3 THEN 'Anulado'
				ELSE 'Otro'
			END AS StatusName, 
			--Logica para sacar el valor de la nota de cuentas por pagar.
			IsNull(ap.InvoiceValue,0) As Value,
			P.CurrencyId,
			c.Abbreviation as CurrencyAbbreviation
	FROM Payments.PaymentNotes AS P WITH (nolock)
	INNER JOIN Common.Supplier As S WITH (nolock) On P.idSupplier = S.Id
	INNER JOIN Common.ThirdParty As T WITH (nolock) On S.IdThirdParty = T.Id
	LEFT JOIN Payments.AccountPayable ap WITH (NOLOCK) ON p.IdAccountPayable = ap.Id
	INNER JOIN Common.Currency c WITH(nolock) on P.CurrencyId= c.Id
	WHERE P.IndicatesBillAdvance = 2

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todas las notas débito y crédito registradas en el módulo de pagos a proveedores. Integra la información de cada nota (código, fecha, naturaleza débito/crédito, estado legible, tipo de documento asociado —factura, anticipo o reversión de cuenta por pagar—) con los datos del proveedor y su NIT, el valor calculado de la nota sumando los conceptos contables del detalle y los ajustes sobre cuentas por pagar o anticipos, y la moneda correspondiente. Sirve para reportería y consulta operativa de ajustes financieros a proveedores, permitiendo identificar notas en estado registrado, confirmado o anulado, así como distinguir si el ajuste aplica sobre una factura, un anticipo o una reversión de cuenta por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'PaymentNotesDebitCredit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'PaymentNotesDebitCredit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola consulta las notas débito/crédito de pagos a proveedores con sus valores totales, mostrando tanto las aplicadas a facturas/anticipos como las de reversión de cuentas por pagar.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'PaymentNotesDebitCredit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Toda nota de pago debe tener un proveedor (Supplier) y un tercero (ThirdParty) asociados; Toda nota de pago debe tener una moneda (Currency) válida; Para la rama de reversión de CxP la nota debe estar vinculada a una cuenta por pagar existente', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'PaymentNotesDebitCredit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La naturaleza solo se reconoce con dos valores válidos: 1=Débito y 2=Crédito; Los estados de la nota solo reconocen tres valores válidos: 1=Registrado, 2=Confirmado y 3=Anulado; El valor de la nota en la rama principal siempre se compone de la suma del detalle (débito o crédito según naturaleza) más el ajuste de la cuenta por pagar (factura o anticipo); En la rama de reversión de CxP el valor reportado siempre proviene del valor de la factura/cuenta por pagar asociada, no del detalle de la nota; Solo se incluyen notas con proveedor, tercero y moneda existentes (INNER JOIN); El detalle se agrega siempre por nota, separando totales de débito y crédito por la naturaleza de cada línea de detalle', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'PaymentNotesDebitCredit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nota débito; Nota crédito; Proveedor; Tercero; NIT; Factura; Anticipo; Cuenta por pagar; Reversión de cuentas por pagar; Moneda; Ajuste de pago', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'PaymentNotesDebitCredit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payments.PaymentNotesDebitCredit: Devuelve por cada nota su naturaleza traducida (1=Débito, 2=Crédito), estado traducido (1=Registrado, 2=Confirmado, 3=Anulado, otro=Otro) y tipo de aplicación (0=Factura, 1=Anticipo, otro=Otro, o ''Reversion CxP'' en la segunda rama); [RETURN_RESULT] Payments.PaymentNotesDebitCredit: Cuando Nature=1 (Débito) e IndicatesBillAdvance=0 (Factura), Value = suma de valores débito del detalle + ValueBilling (AdjustmentValueShare) del avance; [RETURN_RESULT] Payments.PaymentNotesDebitCredit: Cuando Nature=1 (Débito) e IndicatesBillAdvance=1 (Anticipo), Value = suma débito del detalle + ValueAdvance (AdjusmentValue); [RETURN_RESULT] Payments.PaymentNotesDebitCredit: Cuando Nature=2 (Crédito) e IndicatesBillAdvance=0 (Factura), Value = suma crédito del detalle + ValueBilling; [RETURN_RESULT] Payments.PaymentNotesDebitCredit: Cuando Nature=2 (Crédito) e IndicatesBillAdvance=1 (Anticipo), Value = suma crédito del detalle + ValueAdvance; [RETURN_RESULT] Payments.PaymentNotesDebitCredit: Para combinaciones no contempladas de Nature/IndicatesBillAdvance el Value resultante es 0; [RETURN_RESULT] Payments.PaymentNotesDebitCredit: Para la rama ''Reversion CxP'' (UNION ALL) el Value se toma directamente del InvoiceValue de la cuenta por pagar relacionada', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'PaymentNotesDebitCredit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si P.Nature = 1 → Se etiqueta como ''Debito'' else Si Nature=2 se etiqueta como ''Credito''; si P.IndicatesBillAdvance = 0 → Se etiqueta como ''Factura'' y el valor agrega ValueBilling else Si =1 ''Anticipo'' agrega ValueAdvance; otro valor produce ''Otro'' con Value=0; si P.Status IN (1,2,3) → Traduce a ''Registrado''/''Confirmado''/''Anulado'' else Cualquier otro estado se rotula como ''Otro''; si Rama UNION ALL con JOIN a Payments.AccountPayable → Clasifica la nota como ''Reversion CxP'' y toma el valor desde la cuenta por pagar (InvoiceValue)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'PaymentNotesDebitCredit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.PaymentNotes; Common.Supplier; Common.ThirdParty; Payments.PaymentsNoteDetails; Payments.PaymentNotesAccountPayableAdvance; Common.Currency; Payments.AccountPayable', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'PaymentNotesDebitCredit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'PaymentNotesDebitCredit';
GO
