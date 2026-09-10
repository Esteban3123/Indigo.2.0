-- =============================================
-- Author:		Juan Bermudez
-- Create date: 21/01/2016
-- Description:	Procedimiento para el reporte de ejecucion presupuestal por Rubro de Gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportBudgetExcutionByCategoryExpense]
	@ValidityId as int,
	@BudgetId as int
AS
BEGIN
	SET NOCOUNT ON
		
	SELECT	d.Code DependecyCode,
			d.Name DependencyName, 

			c.Code CategoryCode,
			c.Name CategoryName,
			fs.Code FinancialSourceCode,
			fs.Name FinancialSourceName,
			rt.Code revenueTypeCode,
			rt.Name revenueTypeName,

			a.Code availabilityCode, 
			a.ExpirationDate AvailabilyDateExpired, 
			ad.TotalAvailability availabilityValue, 			
			
			tp.Nit SupplierNit, 
			tp.Name SupplierName, 

			ISNULL(co.Code, '') CommitmentCode, 			
			ISNULL(co.Document, '') ContractCode, 
			ISNULL(Cd.TotalCommitment, 0) CommitmentValue, 

			ISNULL(o.Code, '') ObligationCode, 
			ISNULL(od.TotalObligation, 0) ObligationValue,

			ISNULL(po.Code, '') PaymentOrderCode, 
			po.DocumentDate PaymentOrderDate, 
			ISNULL(pod.TotalPaymentOrder, 0) PaymentOrderValue
	FROM Budget.Dependency d
	JOIN Budget.Availability a ON d.Id = a.DependencyId
	JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId 
	JOIN Budget.Budget b ON ad.BudgetId = b.Id
	JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id
	JOIN Budget.Category c ON b.CategoryId = c.Id
	JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
	JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
	LEFT JOIN Budget.CommitmentDetail cd ON ad.Id = cd.AvailabilityDetailId
	LEFT JOIN Budget.Commitment co ON cd.CommitmentId = co.Id
	LEFT JOIN Common.ThirdParty tp ON co.ThirdPartyId = tp.Id
	LEFT JOIN Budget.ObligationDetail od ON cd.Id = od.CommitmentDetailId
	LEFT JOIN Budget.Obligation o ON od.ObligationId = o.Id
	LEFT JOIN Budget.PaymentOrderDetail pod ON od.Id = pod.ObligationDetailId
	LEFT JOIN Budget.PaymentOrder po ON pod.PaymentOrderId = po.Id
	WHERE bh.BudgetaryValidityId = @ValidityId AND b.Id = @BudgetId
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de ejecución presupuestal por rubro de gastos: dado una vigencia presupuestal y un presupuesto específico, consolida en una sola consulta toda la cadena de gasto desde la disponibilidad (CDP) hasta el pago, pasando por el compromiso y la obligación. Para cada combinación de dependencia, categoría de gasto, fuente de financiación y tipo de ingreso, muestra el valor disponible, el proveedor o contratista asociado (NIT y nombre), el compromiso con su contrato, la obligación y la orden de pago con su fecha y valor. Se usa para auditar y reportar cuánto se ha comprometido, obligado y pagado frente al presupuesto aprobado en cada rubro de gasto de la institución.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBudgetExcutionByCategoryExpense';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBudgetExcutionByCategoryExpense';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte tabular de ejecución presupuestal por rubro de gasto, mostrando la trazabilidad desde la disponibilidad (CDP) hasta el compromiso, la obligación y la orden de pago, para una vigencia y línea de presupuesto dadas.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionByCategoryExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un presupuesto (Budget.Budget) cuyo Id coincida con @BudgetId y cuyo BudgetHeader tenga BudgetaryValidityId igual a @ValidityId.; Debe existir al menos una Availability con su AvailabilityDetail vinculada a la línea presupuestal indicada para que el reporte retorne filas.; La línea presupuestal debe tener Category, FinancialSource (vía Category) y RevenueType referenciadas válidamente.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionByCategoryExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reporte siempre se acota a una única línea de presupuesto (b.Id = @BudgetId) dentro de una vigencia presupuestal específica (bh.BudgetaryValidityId = @ValidityId).; La trazabilidad de ejecución se construye en cadena: AvailabilityDetail → CommitmentDetail → ObligationDetail → PaymentOrderDetail; los eslabones posteriores a la disponibilidad son opcionales (LEFT JOIN), permitiendo mostrar disponibilidades aún no comprometidas/obligadas/pagadas.; Cuando no existe compromiso, obligación u orden de pago asociada, los códigos se devuelven como cadena vacía y los valores como 0 (vía ISNULL), nunca NULL.; La disponibilidad y su detalle son obligatorios en el resultado (INNER JOIN): solo se reportan rubros que tengan al menos un registro en Availability y AvailabilityDetail.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionByCategoryExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ejecución presupuestal; Rubro de gasto; Vigencia presupuestal; Disponibilidad presupuestal (CDP); Compromiso; Obligación; Orden de pago; Fuente de financiación; Tipo de ingreso; Dependencia; Tercero/Proveedor; Contrato', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionByCategoryExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.Budget: Devuelve un resultset con datos de dependencia, categoría, fuente financiera, tipo de ingreso, disponibilidad, tercero/proveedor, compromiso/contrato, obligación y orden de pago, filtrado por bh.BudgetaryValidityId = @ValidityId AND b.Id = @BudgetId.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionByCategoryExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Dependency; Budget.Availability; Budget.AvailabilityDetail; Budget.Budget; Budget.BudgetHeader; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.CommitmentDetail; Budget.Commitment; Common.ThirdParty; Budget.ObligationDetail; Budget.Obligation; Budget.PaymentOrderDetail; Budget.PaymentOrder', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionByCategoryExpense';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExcutionByCategoryExpense';
-- GO
