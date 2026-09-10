CREATE VIEW [Payments].[ViewThirdPartyRetention]
AS
	SELECT	NEWID() Id
			, ap.IdThirdParty ThirdPartyId
			, YEAR(ap.DocumentDate) [Year]
			, 1 [Type]
			, 0 ConceptType
			, 'Cuenta por pagar' [TypeName]
			, ap.Code
			, ap.BillNumber
			, ap.BillDate DocumentDate
			, apdcl.TotalIncome DebitValue
			, 0 CreditValue
			, apdcl.RetentionValue383
			, apdcl.ExemptIncome ExemptIncome
			, apdcl.MaxDeductionsAndRentExents MaxDeductionsAndRentExents
			, ap.CreationDate
	FROM Payments.AccountPayable ap
	JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable 
	JOIN Payments.AccountPayableDetailConceptLiquidation apdcl ON apdc.Id = apdcl.AccountPayableDetailConceptId
	WHERE ap.Status = 2
	GROUP BY  ap.IdThirdParty 
			, ap.DocumentDate
			, ap.Code
			, ap.BillNumber
			, ap.BillDate 
			, apdcl.TotalIncome 
			, apdcl.RetentionValue383
			, apdcl.ExemptIncome 
			, apdcl.MaxDeductionsAndRentExents 
			, ap.Status
			, ap.CreationDate

	UNION ALL

	SELECT	NEWID() Id
			, ap.IdThirdParty ThirdPartyId
			, YEAR(ap.DocumentDate) [Year]
			, 2 [Type]
			, apdcla.ConceptType
			, 'Ajuste' [TypeName]
			, ap.Code
			, ap.BillNumber
			, ap.BillDate DocumentDate
			, IIF(apdcla.Nature = 1, apdcla.TotalIncome, 0) DebitValue
			, IIF(apdcla.Nature = 1, 0, apdcla.TotalIncome) CreditValue
			, 0 RetentionValue383
			, IIF(apdcla.ConceptType = 1, apdcla.Value, 0)*IIF(apdcla.Nature = 1, 1, -1) ExemptIncome
			, IIF(apdcla.ConceptType = 1, 0, apdcla.Value)*IIF(apdcla.Nature = 1, 1, -1) MaxDeductionsAndRentExents
			, ap.CreationDate
	FROM Payments.AccountPayable ap
	JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable 
	JOIN Payments.AccountPayableDetailConceptLiquidation apdcl ON apdc.Id = apdcl.AccountPayableDetailConceptId
	JOIN Payments.AccountPayableDetailConceptLiquidationAdjusments apdcla ON apdcl.Id = apdcla.LiquidationId
	WHERE ap.Status = 2

	UNION ALL

	SELECT	NEWID() Id
			, ap.IdThirdParty ThirdPartyId
			, YEAR(ap.DocumentDate) [Year]
			, 3 [Type]
			, 0 ConceptType
			, 'Nota Reversión' [TypeName]
			, pn.Code
			, ap.BillNumber
			, pn.NoteDate DocumentDate
			, 0 DebitValue
			, apdcl.TotalIncome CreditValue
			, (apdcl.RetentionValue383 * -1) RetentionValue383
			, apdcl.ExemptIncome * -1 ExemptIncome
			, apdcl.MaxDeductionsAndRentExents * -1 MaxDeductionsAndRentExents
			, ap.CreationDate
	FROM Payments.PaymentNotes pn
	JOIN Payments.AccountPayable ap ON pn.IdAccountPayable = ap.Id
	JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable 
	JOIN Payments.AccountPayableDetailConceptLiquidation apdcl ON apdc.Id = apdcl.AccountPayableDetailConceptId
	WHERE pn.IndicatesBillAdvance = 2 AND pn.Status = 2

	UNION ALL

	SELECT	NEWID() Id
			, ap.IdThirdParty ThirdPartyId
			, YEAR(ap.DocumentDate) [Year]
			, 2 [Type]
			, apdcla.ConceptType
			, 'Ajuste CXP Reversada' [TypeName]
			, pn.Code
			, ap.BillNumber
			, pn.NoteDate DocumentDate
			, IIF(apdcla.Nature = 1, 0, apdcla.TotalIncome) DebitValue
			, IIF(apdcla.Nature = 1, apdcla.TotalIncome, 0) CreditValue
			, 0 RetentionValue383
			, IIF(apdcla.ConceptType = 1, apdcla.Value, 0)*IIF(apdcla.Nature = 1, -1, 1) ExemptIncome
			, IIF(apdcla.ConceptType = 1, 0, apdcla.Value)*IIF(apdcla.Nature = 1, -1, 1) MaxDeductionsAndRentExents
			, ap.CreationDate
	FROM Payments.PaymentNotes pn
	JOIN Payments.AccountPayable ap ON pn.IdAccountPayable = ap.Id
	JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable 
	JOIN Payments.AccountPayableDetailConceptLiquidation apdcl ON apdc.Id = apdcl.AccountPayableDetailConceptId
	JOIN Payments.AccountPayableDetailConceptLiquidationAdjusments apdcla ON apdcl.Id = apdcla.LiquidationId
	WHERE pn.IndicatesBillAdvance = 2 AND pn.Status = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todos los movimientos de retención en la fuente asociados a terceros (proveedores, contratistas) a partir de las cuentas por pagar confirmadas (estado 2). Integra cuatro tipos de registros: cuentas por pagar originales con su liquidación de retención, ajustes sobre esas liquidaciones (notas débito/crédito), notas de reversión de cuentas por pagar, y ajustes sobre liquidaciones revertidas. Para cada movimiento expone el tercero, el año fiscal, el tipo de documento, el código y número de factura, la fecha, los valores débito y crédito, el valor de retención artículo 383, los ingresos exentos y las deducciones máximas permitidas. Sirve como base para reportes de retención en la fuente a terceros, certificados de ingresos y retenciones, y conciliación de obligaciones tributarias de pagos a proveedores o contratistas.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewThirdPartyRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewThirdPartyRetention';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista los movimientos de retención en la fuente por tercero y año, integrando cuentas por pagar aprobadas, sus ajustes de liquidación y las notas de reversión con sus correspondientes ajustes invertidos.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de relación íntegra entre AccountPayable, AccountPayableDetailConcept y AccountPayableDetailConceptLiquidation; Para reversiones: existencia de PaymentNotes vinculadas a la cuenta por pagar mediante IdAccountPayable; Los ajustes requieren registros en AccountPayableDetailConceptLiquidationAdjusments asociados a la liquidación', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan cuentas por pagar con Status = 2; Solo se consideran notas de pago con Status = 2 e IndicatesBillAdvance = 2 (notas que reversan factura); El año de reporte se determina siempre por el DocumentDate de la cuenta por pagar, incluso en notas de reversión; La RetentionValue383 solo se reporta en movimientos tipo ''Cuenta por pagar'' (positiva) y ''Nota Reversión'' (negada); en ajustes siempre es 0; En notas de reversión, los valores de retención, exentos y deducciones se invierten (multiplicados por -1) frente a la cuenta original; Cada fila genera un Id único vía NEWID(), por lo que la vista no es estable entre consultas; La clasificación débito/crédito de los ajustes depende exclusivamente de la naturaleza del ajuste (Nature) y se invierte cuando el documento es una reversión', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Tercero; Retención en la fuente; Ingresos exentos; Deducciones y rentas exentas; Ajuste contable; Nota de reversión; Naturaleza débito/crédito; Concepto contable; Factura', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando ap.Status = 2 → emite fila Type=1 ''Cuenta por pagar'' con TotalIncome como DebitValue y RetentionValue383/ExemptIncome/MaxDeductionsAndRentExents tal cual; [RETURN_RESULT] resultset: Cuando ap.Status = 2 y existe ajuste de liquidación → emite fila Type=2 ''Ajuste'' donde Nature=1 envía TotalIncome a Débito y Nature≠1 a Crédito; ConceptType=1 mapea Value a ExemptIncome y ConceptType≠1 a MaxDeductionsAndRentExents, conservando signo según Nature; [RETURN_RESULT] resultset: Cuando pn.IndicatesBillAdvance = 2 y pn.Status = 2 → emite fila Type=3 ''Nota Reversión'' con TotalIncome como CreditValue y RetentionValue383, ExemptIncome y MaxDeductionsAndRentExents multiplicados por -1; [RETURN_RESULT] resultset: Cuando pn.IndicatesBillAdvance = 2 y pn.Status = 2 y existe ajuste → emite fila Type=2 ''Ajuste CXP Reversada'' invirtiendo el lado débito/crédito respecto al ajuste original y negando los valores cuando Nature=1', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status de cuenta por pagar = 2 (aprobada/contabilizada) → Se incluyen sus conceptos y liquidaciones como movimientos tipo ''Cuenta por pagar'' (Type=1) y sus ajustes como ''Ajuste'' (Type=2); si Nota de pago con IndicatesBillAdvance = 2 y Status = 2 → Se incluyen registros de ''Nota Reversión'' (Type=3) con valores invertidos al crédito y retenciones/exentos multiplicados por -1, y sus ''Ajuste CXP Reversada'' con signos invertidos; si Naturaleza del ajuste (apdcla.Nature) = 1 → En ''Ajuste'' el TotalIncome se ubica como Débito y los valores conservan signo (+); en ''Ajuste CXP Reversada'' se ubica como Crédito y los valores se invierten (-) else En ''Ajuste'' el TotalIncome se ubica como Crédito y los valores se invierten (-); en ''Ajuste CXP Reversada'' se ubica como Débito y los valores conservan signo (+); si ConceptType del ajuste = 1 → El Value del ajuste se imputa a ExemptIncome (ingresos exentos) else El Value del ajuste se imputa a MaxDeductionsAndRentExents (deducciones y rentas exentas)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Payments.AccountPayableDetailConcept; Payments.AccountPayableDetailConceptLiquidation; Payments.AccountPayableDetailConceptLiquidationAdjusments; Payments.PaymentNotes', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetention';
GO
