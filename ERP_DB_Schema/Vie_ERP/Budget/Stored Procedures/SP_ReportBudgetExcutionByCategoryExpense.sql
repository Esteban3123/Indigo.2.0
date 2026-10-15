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