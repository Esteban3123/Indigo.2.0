
CREATE VIEW [Cost].[ViewCostReportDistributionDirectCost]
AS
	SELECT	cddc.Id,
			cddc.Code,
			cddc.Year,
			cddc.Month,
			CONCAT(cge.Code, ' - ', cge.Name) GeneralExpenseCodeName,
			CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
			cddc.Observation,
			ISNULL(ap.BillNumber, cddc.BillNumber) BillNumber,
			ISNULL(ap.BillDate, cddc.BillDate) BillDate,
			ISNULL(ap.ServicePeriodDate, cddc.ServicePeriodDate) ServicePeriodDate,
			cddc.Value,
			CASE cddc.Status
				WHEN 1 THEN 'Registrado'
				WHEN 2 THEN 'Confirmado'
				WHEN 3 THEN 'Anulado'
			END StatusName,
			cddc.CreationUser,
			c.Abbreviation CurrencyAbbreviation ,
			c.Id CurrencyId,
			iso.CurrencyName
	FROM Cost.CostDistributionDirectCost cddc  WITH(NOLOCK)
	JOIN Cost.CostGeneralExpense cge WITH(NOLOCK) ON cddc.GeneralExpenseId = cge.Id
	LEFT JOIN Payments.AccountPayable ap WITH(NOLOCK) ON cddc.AccountPayableId = ap.Id
	LEFT JOIN Common.ThirdParty tp WITH(NOLOCK) ON ISNULL(ap.IdThirdParty, cddc.ThirdPartyId) = tp.Id
	JOIN Common.Currency c WITH(NOLOCK) ON c.Id= cddc.CurrencyId
	JOIN Common.ISO4217 iso WITH(NOLOCK) on c.ISO4217Id=iso.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte para la distribución de costos directos. Consolida cada registro de distribución de costo directo con el gasto general al que pertenece (código y nombre), el tercero o proveedor involucrado (NIT y nombre), la factura asociada (número y fecha, tomando el dato de la cuenta por pagar si existe, o del registro directo en caso contrario), el período de servicio, el valor, la moneda (con abreviatura y nombre ISO 4217) y el estado del registro (Registrado, Confirmado o Anulado). Se utiliza para reportería contable y de costos, permitiendo analizar los costos directos distribuidos por proveedor, gasto general y período (año y mes).', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostReportDistributionDirectCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostReportDistributionDirectCost';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los costos directos de distribución con sus datos de gasto general, tercero, cuenta por pagar y moneda, presentando el estado en forma legible.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada costo directo debe tener un GeneralExpenseId válido en Cost.CostGeneralExpense; Cada costo directo debe tener una moneda válida (CurrencyId) asociada a un registro ISO4217', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los datos de factura (número, fecha y periodo de servicio) se priorizan desde la cuenta por pagar cuando existe; si no, se usan los del costo directo; El tercero mostrado proviene preferentemente de la cuenta por pagar y solo en su ausencia del costo directo; Solo se incluyen costos directos con gasto general y moneda válidos (JOIN obligatorio); El estado siempre se presenta en texto en español (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Costo directo; Distribución de costos; Gasto general; Cuenta por pagar; Tercero/Proveedor (NIT); Factura; Periodo de servicio; Moneda/Divisa (ISO 4217); Estado del costo (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un registro por cada costo directo con descripciones concatenadas de gasto general (Code - Name), tercero (Nit - Name), datos de factura priorizando los de la cuenta por pagar y nombre de estado traducido', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status = 1 → Se reporta como ''Registrado''; si Status = 2 → Se reporta como ''Confirmado''; si Status = 3 → Se reporta como ''Anulado''; si Existe AccountPayable asociada (AccountPayableId no nulo) → Se usan BillNumber, BillDate y ServicePeriodDate de la cuenta por pagar else Se usan los valores propios del costo directo; si Existe AccountPayable con IdThirdParty → Se utiliza el tercero de la cuenta por pagar else Se utiliza el ThirdPartyId del costo directo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionDirectCost; Cost.CostGeneralExpense; Payments.AccountPayable; Common.ThirdParty; Common.Currency; Common.ISO4217', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostReportDistributionDirectCost';
GO
