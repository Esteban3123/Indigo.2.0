CREATE VIEW [Payments].[ViewThirdPartyRetentionAccumulated]
AS
SELECT	CONCAT(ap.ThirdPartyId, '-', YEAR(ap.DocumentDate)) Id
		, ap.ThirdPartyId
		, YEAR(ap.DocumentDate) [Year]
		, SUM(ap.ExemptIncome) ExemptIncome
		, SUM(ap.MaxDeductionsAndRentExents) MaxDeductionsAndRentExents
FROM [Payments].[ViewThirdPartyRetention] ap
GROUP BY ap.ThirdPartyId, YEAR(ap.DocumentDate)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que acumula por año fiscal los ingresos no gravados y el tope de deducciones y rentas exentas de cada tercero (proveedor, contratista o beneficiario de pago), agrupando los registros de retenciones individuales de la vista ViewThirdPartyRetention. Sirve para calcular el comportamiento tributario anual de un tercero, determinando si ha superado los límites de exención de renta o deducciones aplicables para efectos de retención en la fuente. Es la base para reportes de retenciones acumuladas por tercero y período, útil en conciliaciones fiscales y cierre contable.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewThirdPartyRetentionAccumulated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewThirdPartyRetentionAccumulated';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por tercero y año los ingresos exentos y el tope de deducciones y rentas exentas, a partir de las retenciones de terceros.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetentionAccumulated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador resultante es único por tercero y año (CONCAT ThirdPartyId-Año).; Cada fila representa un único par (tercero, año) gracias al GROUP BY.; Los acumulados se calculan por año calendario de DocumentDate.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetentionAccumulated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención a terceros; Ingresos exentos; Deducciones y rentas exentas; Acumulado anual por tercero', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetentionAccumulated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payments.ViewThirdPartyRetention: Agrupa por ThirdPartyId y YEAR(DocumentDate), retornando SUM(ExemptIncome) y SUM(MaxDeductionsAndRentExents).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetentionAccumulated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.ViewThirdPartyRetention', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetentionAccumulated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewThirdPartyRetentionAccumulated';
GO
