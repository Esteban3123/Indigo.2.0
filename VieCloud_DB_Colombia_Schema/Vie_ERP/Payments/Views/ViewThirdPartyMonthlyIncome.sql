CREATE VIEW [Payments].[ViewThirdPartyMonthlyIncome]
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
			, ap.Status
			, ap.CreationDate
	FROM Payments.AccountPayable ap
	JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable 
	JOIN Payments.AccountPayableDetailConceptLiquidation apdcl ON apdc.Id = apdcl.AccountPayableDetailConceptId
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
			, ap.Status
			, ap.CreationDate
	FROM Payments.AccountPayable ap
	JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable 
	JOIN Payments.AccountPayableDetailConceptLiquidation apdcl ON apdc.Id = apdcl.AccountPayableDetailConceptId
	JOIN Payments.AccountPayableDetailConceptLiquidationAdjusments apdcla ON apdcl.Id = apdcla.LiquidationId

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
			, ap.Status
			, ap.CreationDate
	FROM Payments.PaymentNotes pn
	JOIN Payments.AccountPayable ap ON pn.IdAccountPayable = ap.Id
	JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable 
	JOIN Payments.AccountPayableDetailConceptLiquidation apdcl ON apdc.Id = apdcl.AccountPayableDetailConceptId
	WHERE pn.IndicatesBillAdvance = 2

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
			, ap.Status
			, ap.CreationDate
	FROM Payments.PaymentNotes pn
	JOIN Payments.AccountPayable ap ON pn.IdAccountPayable = ap.Id
	JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable 
	JOIN Payments.AccountPayableDetailConceptLiquidation apdcl ON apdc.Id = apdcl.AccountPayableDetailConceptId
	JOIN Payments.AccountPayableDetailConceptLiquidationAdjusments apdcla ON apdcl.Id = apdcla.LiquidationId
	WHERE pn.IndicatesBillAdvance = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los ingresos mensuales de terceros (contratistas, proveedores de honorarios y servicios) agrupando en un solo resultado cuatro tipos de movimientos: cuentas por pagar originales, ajustes (notas débito/crédito) sobre liquidaciones, notas de reversión y ajustes sobre cuentas por pagar reversadas. Para cada movimiento expone el tercero, el año, el tipo de documento, el código y número de factura, la fecha, los valores de ingreso débito y crédito, la retención en la fuente (artículo 383), los ingresos exentos y las deducciones máximas permitidas. Se utiliza principalmente para el cálculo y reporte de retención en la fuente mensualizada por tercero, facilitando la conciliación de bases gravables y deducciones para efectos tributarios y de nómina de contratistas.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewThirdPartyMonthlyIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewThirdPartyMonthlyIncome';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los ingresos mensuales por tercero combinando cuentas por pagar, sus ajustes de liquidación y las notas de reversión, clasificándolos como débito o crédito para reportes tributarios anuales.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyMonthlyIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen cuentas por pagar con detalle de conceptos y liquidaciones asociadas.; Las notas de reversión deben estar vinculadas a una cuenta por pagar existente.; Los ajustes de liquidación deben referenciar una liquidación válida.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyMonthlyIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El año reportado siempre proviene del DocumentDate de la cuenta por pagar (YEAR(ap.DocumentDate)), incluso para notas de reversión.; Cada fila recibe un Id único generado con NEWID().; Las cuentas por pagar base siempre tienen CreditValue=0 y RetentionValue383 directo; las notas de reversión siempre tienen DebitValue=0 y RetentionValue383 negativo.; El TypeName está hardcoded según el origen: ''Cuenta por pagar'' (1), ''Ajuste'' (2), ''Nota Reversión'' (3), ''Ajuste CXP Reversada'' (2).; Las notas de reversión solo aplican cuando IndicatesBillAdvance=2.; Los ajustes en reversión invierten exactamente la lógica de signos del ajuste original, garantizando neteo contable.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyMonthlyIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Tercero; Retención en la fuente; Ingreso exento; Deducciones y rentas exentas; Nota de reversión; Ajuste contable; Concepto contable; Naturaleza débito/crédito; Ingresos anuales por tercero', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyMonthlyIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payments.AccountPayable: Por cada cuenta por pagar con liquidación se devuelve una fila tipo 1 ''Cuenta por pagar'' con TotalIncome como DebitValue y CreditValue=0.; [RETURN_RESULT] Payments.AccountPayableDetailConceptLiquidationAdjusments: Por cada ajuste se devuelve fila tipo 2 ''Ajuste''; si Nature=1 el valor va a DebitValue, en caso contrario a CreditValue.; [RETURN_RESULT] Payments.PaymentNotes: Cuando pn.IndicatesBillAdvance = 2 se genera fila tipo 3 ''Nota Reversión'' con TotalIncome como CreditValue y los valores de retención/exento/deducción invertidos en signo (multiplicados por -1).; [RETURN_RESULT] Payments.PaymentNotes: Cuando pn.IndicatesBillAdvance = 2 y existen ajustes de liquidación, se genera fila tipo 2 ''Ajuste CXP Reversada'' invirtiendo la asignación débito/crédito respecto al ajuste original (Nature=1 va a CreditValue).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyMonthlyIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si apdcla.Nature = 1 (en flujo de ajuste normal) → TotalIncome se asigna a DebitValue y CreditValue=0; ExemptIncome y MaxDeductionsAndRentExents conservan signo positivo else TotalIncome se asigna a CreditValue y DebitValue=0; ExemptIncome y MaxDeductionsAndRentExents se multiplican por -1; si apdcla.ConceptType = 1 → El Value del ajuste se computa como ExemptIncome (ingreso exento) else El Value del ajuste se computa como MaxDeductionsAndRentExents (deducciones y rentas exentas); si pn.IndicatesBillAdvance = 2 → Se incluyen filas de Nota Reversión y Ajuste CXP Reversada con signos y débito/crédito invertidos respecto al original else Las notas no se incluyen en el reporte; si apdcla.Nature = 1 en flujo de CXP reversada → TotalIncome se asigna a CreditValue (invertido) y los exentos/deducciones se multiplican por -1 else TotalIncome se asigna a DebitValue y exentos/deducciones conservan signo positivo', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyMonthlyIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Payments.AccountPayableDetailConcept; Payments.AccountPayableDetailConceptLiquidation; Payments.AccountPayableDetailConceptLiquidationAdjusments; Payments.PaymentNotes', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyMonthlyIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyMonthlyIncome';
GO
