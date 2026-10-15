-- ==================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 18/07/2016
-- Description:	Procedimiento que se encarga de obtener los valores para exportarlos a excel
-- ==================================================================================================
CREATE PROCEDURE [Cost].[SP_ExportExcelCostDistributionManPower] 
	@Year INT,
	@Month INT
AS
BEGIN
	SET NOCOUNT ON;

	/******************************************************** VARIABLES ******************************************************/

	DECLARE @CostDistributionManpower AS TABLE
	(
		Id INT IDENTITY(1,1),
		EntityId INT, ManpowerType TINYINT, ManpowerTypeName VARCHAR(500),
		GroupCode VARCHAR(20), GroupName VARCHAR(500),
		ThirdPartyNit VARCHAR(20), ThirdPartyName VARCHAR(500),
		PositionCode VARCHAR(20), PositionName VARCHAR(500),		
		--------------------------------------
		ProductionCenterCode VARCHAR(20), ProductionCenterName VARCHAR(500),
		HoursQuantity INT DEFAULT(0),
		TotalAccrued DECIMAL(20,4) DEFAULT(0),
		TotalProvision DECIMAL(20,4) DEFAULT(0),
		TotalEmployerContribution DECIMAL(20,4) DEFAULT(0),
		TotalParafiscal DECIMAL(20,4) DEFAULT(0),
		Distribuited BIT DEFAULT(0)
	)

	/**************************************************** CARGUE DETALLES ****************************************************/

	INSERT @CostDistributionManpower 
	(
		EntityId, ManpowerType,	ManpowerTypeName,
		GroupCode, GroupName,
		ThirdPartyNit, ThirdPartyName,
		PositionCode, PositionName,
		ProductionCenterCode, ProductionCenterName,
		HoursQuantity,
		TotalAccrued, TotalProvision, TotalEmployerContribution, TotalParafiscal,
		Distribuited
	)
	SELECT
		cdm.EntityId, cdm.ManpowerType, IIF(cdm.ManpowerType = 1, 'Empleado', 'Contratista'),
		g.Code, g.Name,
		tp.Nit, tp.Name,
		p.Code, p.Name,
		cpc.Code, cpc.Name,
		cdmd.HoursQuantity, 
		cdmd.TotalAccrued, cdmd.TotalProvision, cdmd.TotalEmployerContribution, cdmd.TotalParafiscal,
		1
	FROM Cost.CostDistributionManpower cdm	
	JOIN Common.ThirdParty tp ON cdm.ThirdPartyId = tp.Id
	JOIN Payroll.Position p ON cdm.PositionId = p.Id
	JOIN Cost.CostDistributionManpowerDetail cdmd ON cdm.Id = cdmd.DistributionManpowerId
	JOIN Cost.CostProductionCenter cpc ON cdmd.ProductionCenterId = cpc.Id
	LEFT JOIN Payroll.[Group] g ON cdm.GroupId = g.Id
	WHERE cdm.Year = @Year AND cdm.Month = @Month

	INSERT @CostDistributionManpower 
	(
		EntityId, ManpowerType, ManpowerTypeName,
		GroupCode, GroupName,
		ThirdPartyNit, ThirdPartyName,
		PositionCode, PositionName,
		ProductionCenterCode, ProductionCenterName,
		HoursQuantity,
		TotalAccrued, TotalProvision, TotalEmployerContribution, TotalParafiscal
	)
	SELECT 
		l.Id, 1, 'Empleado',
		g.Code, g.Name,
		tp.Nit, tp.Name,
		p.Code, p.Name,
		ld.ProductionCenterCode, ld.ProductionCenterName,
		ISNULL(sd.Hours, CAST(l.DaysWorked AS INT) * c.HoursDaily), 
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
			cpc.Code ProductionCenterCode, cpc.Name ProductionCenterName,
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
		GROUP BY ld.PayrollId, cpccc.ProductionCenterId, cpc.Code, cpc.Name
	) ld ON l.Id = ld.PayrollId
	LEFT JOIN
	(
		SELECT sd.EmployeeId, SUM(sd.TotalNumberHours) Hours
		FROM Payroll.ScheduleDetail sd
		WHERE sd.State = 1 AND YEAR(sd.DateDetail) = @Year AND MONTH(sd.DateDetail) = @Month
		GROUP BY sd.EmployeeId
		HAVING SUM(sd.TotalNumberHours) > 0
	) sd ON l.EmployeeId = sd.EmployeeId
	LEFT JOIN @CostDistributionManpower cdm ON l.Id = cdm.EntityId AND 1 = cdm.ManpowerType
	WHERE l.RegisterStatus = 'C'
		AND YEAR(l.PayrollDateLiquidated) = @Year AND MONTH(l.PayrollDateLiquidated) = @Month
		AND cdm.Id IS NULL

	/****************************************************** ASIGNACIONES *****************************************************/

	INSERT @CostDistributionManpower 
	(
		EntityId, ManpowerType, ManpowerTypeName,
		ThirdPartyNit, ThirdPartyName, 
		PositionCode, PositionName,
		ProductionCenterCode, ProductionCenterName,
		HoursQuantity, TotalAccrued
	)
	SELECT
		ap.Id, 2, 'Contratista',
		tp.Nit, tp.Name,
		p.Code, p.Name,
		apdc.ProductionCenterCode, apdc.ProductionCenterName,
		ap.Hours, ISNULL(apdc.TotalAccrued, ap.InvoiceValue)
	FROM Payments.AccountPayable ap
	JOIN Common.ThirdParty tp ON ap.IdThirdParty = tp.Id
	JOIN Payroll.Position p ON ap.PositionId = p.Id
	JOIN
	(
		SELECT
			apdc.IdAccountPayable,
			cpccc.ProductionCenterId,
			cpc.Code ProductionCenterCode, cpc.Name ProductionCenterName,
			SUM(apdc.Value * IIF(apdc.Nature = 1, 1, -1)) TotalAccrued
		FROM Cost.CostProductionCenter cpc
		JOIN Cost.CostProductionCenterCostCenter cpccc ON cpc.Id = cpccc.ProductionCenterId
		JOIN Cost.CostProductionCenterHomologation cpch ON cpc.Id = cpch.ProductionCenterId
		JOIN Payments.AccountPayableDetailConcept apdc ON cpccc.CostCenterId = apdc.IdCostCenter AND cpch.AccountOriginId = apdc.IdAccount
		GROUP BY apdc.IdAccountPayable, cpccc.ProductionCenterId, cpc.Code, cpc.Name
	) apdc ON ap.Id = apdc.IdAccountPayable
	LEFT JOIN @CostDistributionManpower cdm ON ap.Id = cdm.EntityId AND 2 = cdm.ManpowerType
	WHERE ap.Status = 2 
		AND YEAR(ap.DocumentDate) = @Year AND MONTH(ap.DocumentDate) = @Month
		AND cdm.Id IS NULL

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
		WHERE cdm.Distribuited = 0
			AND cdm.ManpowerType > @DetailManpowerType
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
			WHERE cdm.Distribuited = 0
				AND cdm.ManpowerType = @DetailManpowerType
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
		ManpowerType, EntityId, ManpowerTypeName,
		GroupCode, GroupName,
		ThirdPartyNit, ThirdPartyName,
		PositionCode, PositionName,
		ProductionCenterCode, ProductionCenterName,
		HoursQuantity,
		TotalAccrued,
		TotalProvision,
		TotalEmployerContribution,
		TotalParafiscal,
		Distribuited
	FROM @CostDistributionManpower
	ORDER BY ThirdPartyNit, ProductionCenterCode
END