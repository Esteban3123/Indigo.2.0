-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date:	2019-09-09
-- Description:	Obtiene los empleados y contratistas a distribuid
-- =============================================
CREATE PROCEDURE [Cost].[SP_GetDistributionManpower]
	@Year INT,
	@Month INT,
	@ManpowerType TINYINT,
	@EntityId INT
AS
BEGIN
	SET NOCOUNT ON;

	/******************************************************** VARIABLES ******************************************************/

	DECLARE @CostDistributionManpower AS TABLE
	(
		Id INT IDENTITY(1,1),
		EntityId INT, ManpowerType TINYINT,
		GroupId INT, GroupCodeName VARCHAR(500),
		EmployeeId INT, ThirdPartyId INT, ThirdPartyNitName VARCHAR(500),
		PositionId INT, PositionCodeName VARCHAR(500),
		--------------------------------------
		ProductionCenterId INT, ProductionCenterCodeName VARCHAR(500),
		HoursQuantity INT DEFAULT(0),
		TotalAccrued DECIMAL(20,4) DEFAULT(0),
		TotalProvision DECIMAL(20,4) DEFAULT(0),
		TotalEmployerContribution DECIMAL(20,4) DEFAULT(0),
		TotalParafiscal DECIMAL(20,4) DEFAULT(0)
	)

	/**************************************************** CARGUE DETALLES ****************************************************/

	INSERT @CostDistributionManpower 
	(
		EntityId, ManpowerType,
		GroupId, GroupCodeName,
		EmployeeId, ThirdPartyId, ThirdPartyNitName, 
		PositionId, PositionCodeName,
		ProductionCenterId, ProductionCenterCodeName,
		HoursQuantity, 
		TotalAccrued, TotalProvision, TotalEmployerContribution, TotalParafiscal
	)
	SELECT 
		l.Id, 1,
		l.GroupId, CONCAT(g.Code, ' - ', g.Name),
		l.EmployeeId, e.ThirdPartyId, CONCAT(tp.Nit, ' - ', tp.Name),
		c.PositionId, CONCAT(p.Code, ' - ', p.Name),
		ld.ProductionCenterId, ld.ProductionCenterCodeName,
		ISNULL(sd.Hours, l.DaysWorked * c.HoursDaily), 
		ld.TotalAccrued, ld.TotalProvision, ld.TotalEmployerContribution, ld.TotalParafiscal
	FROM Payroll.Liquidation l
	JOIN Payroll.[Group] g ON l.GroupId = g.Id
	JOIN Payroll.Employee e ON l.EmployeeId = e.Id
	JOIN Common.ThirdParty tp ON e.ThirdPartyId = tp.Id	
	JOIN Payroll.Contract c ON l.ContractId = c.Id
	JOIN Payroll.Position p ON c.PositionId = p.Id
	JOIN
	(
		SELECT
			ld.PayrollId,
			cpccc.ProductionCenterId,
			CONCAT(cpc.Code, ' - ', cpc.Name) ProductionCenterCodeName,
			cd.CostCenterId,
			SUM(IIF(c1.ConceptClass IS NOT NULL, (cd.DebitValue - cd.CreditValue), 0)) TotalAccrued,			
			SUM(IIF(c3.ConceptClass IS NOT NULL, (cd.DebitValue - cd.CreditValue), 0)) TotalEmployerContribution,
			SUM(IIF(c4.ConceptClass IS NOT NULL, (cd.DebitValue - cd.CreditValue), 0)) TotalParafiscal,
			SUM(IIF(c5.ConceptClass IS NOT NULL, (cd.DebitValue - cd.CreditValue), 0)) TotalProvision
		FROM Cost.CostProductionCenter cpc
		JOIN Cost.CostProductionCenterCostCenter cpccc ON cpc.Id = cpccc.ProductionCenterId
		JOIN Cost.CostProductionCenterHomologation cpch ON cpc.Id = cpch.ProductionCenterId
		JOIN Payroll.CostDistribution cd ON cpccc.CostCenterId = cd.CostCenterId AND cpch.AccountOriginId = cd.MainAccountId
		JOIN Payroll.LiquidationDetail ld ON cd.LiquidationDetailId = ld.Id
		LEFT JOIN Payroll.GetConceptClassByType(1) c1 ON ld.ConceptClass = c1.ConceptClass
		LEFT JOIN Payroll.GetConceptClassByType(3) c3 ON ld.ConceptClass = c3.ConceptClass
		LEFT JOIN Payroll.GetConceptClassByType(4) c4 ON ld.ConceptClass = c4.ConceptClass
		LEFT JOIN Payroll.GetConceptClassByType(5) c5 ON ld.ConceptClass = c5.ConceptClass
		WHERE cpc.Status = 1
		GROUP BY ld.PayrollId, cpccc.ProductionCenterId, cpc.Code, cpc.Name, cd.CostCenterId
	) ld ON l.Id = ld.PayrollId
	LEFT JOIN
	(
		SELECT	sd.EmployeeId, 
				--fu.CostCenterId, 
				SUM(sd.TotalNumberHours) Hours
		FROM Payroll.ScheduleDetail sd
		JOIN Payroll.FunctionalUnit fu ON sd.ScheduleFunctionalUnitId = fu.Id
		WHERE YEAR(sd.DateDetail) = @Year AND MONTH(sd.DateDetail) = @Month --AND sd.State = 1
		GROUP BY sd.EmployeeId--, fu.CostCenterId
		HAVING SUM(sd.TotalNumberHours) > 0
	) sd ON l.EmployeeId = sd.EmployeeId --AND ld.CostCenterId = sd.CostCenterId
	WHERE l.RegisterStatus = 'C'
		AND YEAR(l.PayrollDateLiquidated) = @Year AND MONTH(l.PayrollDateLiquidated) = @Month
		AND 1 = ISNULL(@ManpowerType, 1)
		AND l.Id = ISNULL(@EntityId, l.Id)

	/****************************************************** ASIGNACIONES *****************************************************/

	INSERT @CostDistributionManpower 
	(
		EntityId, ManpowerType,
		ThirdPartyId, ThirdPartyNitName, 
		PositionId, PositionCodeName,
		ProductionCenterId, ProductionCenterCodeName,
		HoursQuantity, TotalAccrued
	)
	SELECT
		ap.Id, 2,
		ap.IdThirdParty, CONCAT(tp.Nit, ' - ', tp.Name),
		ap.PositionId, CONCAT(p.Code, ' - ', p.Name),
		apdc.ProductionCenterId, apdc.ProductionCenterCodeName,
		ap.Hours, ISNULL(apdc.TotalAccrued, ap.InvoiceValue)
	FROM Payments.AccountPayable ap
	JOIN Common.ThirdParty tp ON ap.IdThirdParty = tp.Id
	JOIN Payroll.Position p ON ap.PositionId = p.Id
	JOIN
	(
		SELECT
			apdc.IdAccountPayable,
			cpccc.ProductionCenterId,
			CONCAT(cpc.Code, ' - ', cpc.Name) ProductionCenterCodeName,
			SUM(apdc.Value * IIF(apdc.Nature = 1, 1, -1)) TotalAccrued
		FROM Cost.CostProductionCenter cpc
		JOIN Cost.CostProductionCenterCostCenter cpccc ON cpc.Id = cpccc.ProductionCenterId
		JOIN Cost.CostProductionCenterHomologation cpch ON cpc.Id = cpch.ProductionCenterId
		JOIN Payments.AccountPayableDetailConcept apdc ON cpccc.CostCenterId = apdc.IdCostCenter AND cpch.AccountOriginId = apdc.IdAccount
		GROUP BY apdc.IdAccountPayable, cpccc.ProductionCenterId, cpc.Code, cpc.Name
	) apdc ON ap.Id = apdc.IdAccountPayable
	WHERE ap.Status = 2 
		AND YEAR(ap.DocumentDate) = @Year AND MONTH(ap.DocumentDate) = @Month
		AND 2 = ISNULL(@ManpowerType, 2)
		AND ap.Id = ISNULL(@EntityId, ap.Id)

	/**************************************************** DISTRIBUIR HORAS ***************************************************/

	DECLARE @DetailManpowerTypeRows INT = 1,
			@DetailManpowerType INT = 0,
			---------------------------------------
			@DetailRows INT,
			@DetailId INT = 0,
			---------------------------------------			
			@Hours INT,
			@TotalDistribuited DECIMAL(20,4)

	WHILE @DetailManpowerTypeRows > 0
	BEGIN
		SELECT TOP 1
			@DetailManpowerType = cdm.ManpowerType,
			---------------------------------------
			@DetailRows = 1,
			@DetailId = 0
		FROM @CostDistributionManpower cdm
		WHERE cdm.ManpowerType > @DetailManpowerType
		ORDER BY cdm.ManpowerType

		SET @DetailManpowerTypeRows = @@ROWCOUNT
		IF @DetailManpowerTypeRows = 0 
		BEGIN
			BREAK
		END

		-----------------------------------------------------------------------------------------------------------------------

		WHILE @DetailRows > 0
		BEGIN
			SELECT TOP 1
				@DetailId = cdm.EntityId,
				@Hours = cdm.HoursQuantity
			FROM @CostDistributionManpower cdm
			WHERE cdm.ManpowerType = @DetailManpowerType
				AND cdm.EntityId > @DetailId
			GROUP BY cdm.EntityId, cdm.HoursQuantity
			HAVING COUNT(*) > 1
			ORDER BY cdm.EntityId

			SET @DetailRows = @@ROWCOUNT
			IF @DetailRows = 0 
			BEGIN
				BREAK
			END

			-------------------------------------------------------------------------------------------------------------------

			SELECT @TotalDistribuited = SUM(cdm.TotalAccrued + cdm.TotalProvision + cdm.TotalEmployerContribution + cdm.TotalParafiscal)
			FROM @CostDistributionManpower cdm
			WHERE cdm.ManpowerType = @DetailManpowerType AND cdm.EntityId = @DetailId

			-------------------------------------------------------------------------------------------------------------------

			UPDATE cdm
				SET cdm.HoursQuantity = ROUND((cdm.TotalAccrued + cdm.TotalProvision + cdm.TotalEmployerContribution + cdm.TotalParafiscal) / @TotalDistribuited * @Hours, 0)
			FROM @CostDistributionManpower cdm
			WHERE cdm.ManpowerType = @DetailManpowerType AND cdm.EntityId = @DetailId

			UPDATE cdm
				SET cdm.HoursQuantity = cdm.HoursQuantity - (@Hours - cdmd.Hours)
			FROM @CostDistributionManpower cdm
			JOIN
			(
				SELECT MIN(cdm.Id) Id, SUM(cdm.HoursQuantity) Hours
				FROM @CostDistributionManpower cdm
				WHERE cdm.ManpowerType = @DetailManpowerType AND cdm.EntityId = @DetailId
				GROUP BY cdm.ManpowerType, cdm.EntityId
			) cdmd ON cdm.Id = cdmd.Id
		END
	END

	/******************************************************* RESULTADO *******************************************************/

    SELECT
		ManpowerType, EntityId,
		GroupId, GroupCodeName,
		EmployeeId, ThirdPartyId, ThirdPartyNitName,
		PositionId, PositionCodeName,
		ProductionCenterId, ProductionCenterCodeName,
		SUM(HoursQuantity) HoursQuantity,
		SUM(TotalAccrued) TotalAccrued,
		SUM(TotalProvision) TotalProvision,
		SUM(TotalEmployerContribution) TotalEmployerContribution,
		SUM(TotalParafiscal) TotalParafiscal
	FROM @CostDistributionManpower
	GROUP BY ManpowerType, EntityId,
		GroupId, GroupCodeName,
		EmployeeId, ThirdPartyId, ThirdPartyNitName,
		PositionId, PositionCodeName,
		ProductionCenterId, ProductionCenterCodeName
END