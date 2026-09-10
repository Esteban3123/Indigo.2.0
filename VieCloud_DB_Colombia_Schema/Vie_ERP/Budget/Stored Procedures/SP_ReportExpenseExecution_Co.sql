
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de ejecución presupuestal de gastos para una vigencia y mes determinados. Consolida, por rubro presupuestal (CCPET), fuente de financiación, clasificación CPC, situación de fondos, política pública y tercero, los valores acumulados de compromisos, obligaciones y órdenes de pago aprobados hasta el mes indicado. Integra las tablas de presupuesto, categorías, disponibilidades, compromisos, obligaciones y órdenes de pago del módulo Budget, permitiendo a las áreas financieras y de presupuesto hacer seguimiento al gasto ejecutado frente a lo apropiado en cada vigencia presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportExpenseExecution_Co';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportExpenseExecution_Co';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de ejecución presupuestal de gastos para una vigencia y mes de corte, agregando compromisos, obligaciones y pagos por rubro, fuente, política pública, CPC y tercero.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseExecution_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La vigencia presupuestal recibida debe existir y corresponder a documentos en BudgetHeader, Availability, Commitment, Obligation y PaymentOrder.; Solo se consideran documentos con Status = 2 (estado aprobado/activo) en Availability, Commitment, Obligation y PaymentOrder.; Las categorías presupuestales deben tener ItemType = 2 (rubros de gasto).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseExecution_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los valores de compromiso, obligación y pago se acumulan acumulativamente desde el inicio de la vigencia hasta el mes de corte (month(DocumentDate) <= @month).; Solo documentos con Status = 2 contribuyen a los totales ejecutados.; BudgetSection se fija siempre como ''2.6'' y Sector como ''19'' (valores constantes ''por definir'' en el código).; Las órdenes de pago se filtran con BudgetaryValidityId = 9 fijo (constante hardcodeada, no toma @validityId).; Si no existen compromisos/obligaciones/pagos asociados, los totales se asumen como 0 vía ISNULL.; El tercero solo se asocia a la línea cuando su ContributionType = 2.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseExecution_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ejecución presupuestal de gastos; Vigencia presupuestal; Compromiso presupuestal; Obligación presupuestal; Orden de pago; Disponibilidad presupuestal; Rubro CCPET; Fuente de financiación; Situación de fondos; Política pública; Clasificación CPC; Tercero; Tipo de contribución; Sección presupuestal; Sector presupuestal', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseExecution_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] CLIENT: Devuelve el resultado agregado (SUM de CommitmentValue, ObligationValue y PaymentValue) agrupado por rubro CCPET, fuente financiera, situación de fondos, política pública, CPC y tercero.; [RETURN_RESULT] CLIENT: En caso de error capturado por CATCH, retorna una fila con CodeResult=''999'' y MessageResult con el mensaje y línea del error.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseExecution_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Budget.Category.ItemType = 2 y BudgetHeader.BudgetaryValidityId = @validityId → Se incluyen únicamente las líneas presupuestales de gasto de la vigencia solicitada en el reporte.; si ThirdParty.ContributionType = 2 (en el subquery de compromisos) → Se asigna el ThirdPartyId al detalle; en otros tipos de contribución el tercero queda como NULL. else ThirdPartyId queda NULL y no se enlaza tercero al compromiso.; si Status = 2 y month(DocumentDate) <= @month en Availability/Commitment/Obligation/PaymentOrder → El documento se acumula en el reporte; en caso contrario se excluye.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseExecution_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Budget; Budget.Category; Budget.CCPET; Budget.FinancialSource; Budget.RevenueType; Budget.BudgetHeader; Budget.Availability; Budget.AvailabilityDetail; Budget.CommitmentDetail; Budget.Commitment; Common.ThirdParty; Budget.ObligationDetail; Budget.Obligation; Budget.PaymentOrderDetail; Budget.PaymentOrder; Budget.PublicPolicy; Budget.CPCCatalog', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseExecution_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseExecution_Co';
-- GO
