
-- =============================================
-- Author:		Cristhian Salazar
-- Create date: 2022-02-08
-- Description:	Procedimiento para el reporte de ejecucion presupuestal de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportExpenseExecution_Co]
	@validityId as integer,
	@month as integer
AS
BEGIN	
	SET NOCOUNT ON
		
	
	DECLARE @TableExecution TABLE
	(
		BudgetId integer,
		Code VARCHAR(40),
		Name VARCHAR(MAX),
		ValidityType tinyint,
		BudgetSection varchar(5),
		Sector varchar(5),
		CPCCode varchar(20),
		CPCName varchar(300),
		FinancialSourceCode varchar(20),
		FinancialSourceName varchar(300),
		FundSituation char(1),
		PublicPolicyCode varchar(20),
		PublicPolicyName varchar(300),
		NitThirdParty varchar(20),
		NameThirdParty varchar(300),
		CommitmentValue DECIMAL(18,2) DEFAULT (0),
		ObligationValue DECIMAL(18,2) DEFAULT (0),
		PaymentValue DECIMAL(18,2) DEFAULT (0)
	)

	BEGIN TRY
		

		/********************************************  OBTENCION DE DATOS ********************************************/

		-- insertamos en la tabla temporal los datos del presupuesto inicial
		INSERT INTO @TableExecution 
		(
			BudgetId, Code, Name, ValidityType, BudgetSection, Sector, CPCCode,
			CPCName, FinancialSourceCode, FinancialSourceName, FundSituation, PublicPolicyCode,
			PublicPolicyName, NitThirdParty, NameThirdParty, CommitmentValue, ObligationValue, PaymentValue
		)
		SELECT
			b.Id,
			ccpet.Code,
			ccpet.Name,
			c.Validity,
			'2.6' as Seccion, -- por definir
			'19' as Sector, -- por definir
			cpc.Code,
			cpc.Name,
			fs.Code,
			fs.Name,
			c.FundSituation,
			pp.Code,
			pp.Name,
			t.Nit,
			t.Name,
			ava.TotalCommitment,
			ava.TotalObligation,
			ava.TotalPaymentOrder
		FROM Budget.Budget b 
		INNER JOIN Budget.Category c ON c.Id = b.CategoryId
		INNER JOIN Budget.CCPET ccpet on ccpet.Id = c.CCPETCodeId
		INNER JOIN Budget.FinancialSource fs ON fs.Id = c.FinancialSourceId 
		INNER JOIN Budget.RevenueType rt ON rt.Id = b.RevenueTypeId 
		INNER JOIN Budget.BudgetHeader bh ON bh.Id = b.BudgetHeaderId
		inner JOIN (
			SELECT ad.BudgetId, ad.CPCCodeId, cd.ThirdPartyId,
				ISNULL(cd.TotalCommitment,0) as TotalCommitment,
				ISNULL(od.TotalObligation,0) as TotalObligation, 
				ISNULL(pod.TotalPaymentOrder, 0) as TotalPaymentOrder
			FROM (Budget.[Availability] a
				inner join Budget.AvailabilityDetail ad 
				on ad.AvailabilityId = a.Id and a.Status = 2 and a.BudgetaryValidityId = @validityId
				and month(a.DocumentDate) <= @month
				)
			left join (
				select cd.AvailabilityDetailId,cd.Id, iif(t.ContributionType = 2, t.Id, null) as ThirdPartyId, SUM(cd.TotalCommitment) as TotalCommitment
				from Budget.CommitmentDetail cd
				inner join Budget.Commitment c 
				inner join Common.ThirdParty t on t.Id = c.ThirdPartyId
				on c.Id = cd.CommitmentId and c.Status = 2 and c.BudgetaryValidityId = @validityId
				and month(c.DocumentDate) <= @month
				GROUP BY cd.AvailabilityDetailId, cd.Id, t.ContributionType, t.Id
				)cd on cd.AvailabilityDetailId = ad.Id
			left join (
				select Min(od.Id) as Id, od.CommitmentDetailId, SUM(od.TotalObligation) as TotalObligation
				from Budget.ObligationDetail od 
				inner join Budget.Obligation o 
				on o.Id = od.ObligationId and o.Status = 2 and o.BudgetaryValidityId = @validityId
				and month(o.DocumentDate) <= @month
				GROUP BY od.CommitmentDetailId
				)od on od.CommitmentDetailId = cd.Id
			left join (
			select pod.ObligationDetailId, SUM(pod.TotalPaymentOrder) as TotalPaymentOrder
			from Budget.PaymentOrderDetail pod 
				inner join Budget.PaymentOrder po 
				on po.Id = pod.PaymentOrderId and po.Status = 2 and po.BudgetaryValidityId = 9
				and month(po.DocumentDate) <= @month
				GROUP BY pod.ObligationDetailId
				) pod on pod.ObligationDetailId = od.Id
		) ava on ava.BudgetId = b.Id
		LEFT JOIN Common.ThirdParty t on t.Id = ava.ThirdPartyId
		LEFT JOIN Budget.PublicPolicy pp on pp.Id = c.PublicPolicyId
		left join Budget.CPCCatalog cpc on cpc.Id = ava.CPCCodeId
		WHERE bh.BudgetaryValidityId = @validityId
		AND c.ItemType = 2 

		SELECT	
		Code,
		Name,
		ValidityType,
		BudgetSection,
		Sector,
		CPCCode,
		CPCName,
		FinancialSourceCode,
		FinancialSourceName,
		FundSituation,
		PublicPolicyCode,
		PublicPolicyName,
		NitThirdParty,
		NameThirdParty,
		SUM(CommitmentValue) CommitmentValue,
		SUM(ObligationValue) ObligationValue,
		SUM(PaymentValue) PaymentValue
		FROM @TableExecution
		GROUP BY Code, Name, ValidityType, BudgetSection, Sector, CPCCode, CPCName,
		FinancialSourceCode, FinancialSourceName, FundSituation, PublicPolicyCode,
		PublicPolicyName, NitThirdParty, NameThirdParty
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END