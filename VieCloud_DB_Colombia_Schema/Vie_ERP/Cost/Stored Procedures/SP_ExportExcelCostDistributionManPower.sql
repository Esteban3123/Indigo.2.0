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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de distribución de costos de mano de obra para exportación a Excel, correspondiente a un año y mes específicos. Consolida en una tabla temporal los costos de empleados y contratistas, cruzando información de liquidaciones de nómina, cargos, terceros (empleados y proveedores externos), centros de producción y detalles de horas trabajadas, valores causados, provisiones, aportes patronales y parafiscales. Integra dos fuentes principales: las distribuciones ya registradas en Cost.CostDistributionManpower y las liquidaciones de nómina del período (Payroll.Liquidation), completando con cuentas por pagar de contratistas (Payments.AccountPayable) cuando aplica. Es utilizado por el módulo de costos para el análisis y control del gasto de personal por centro de producción en un período contable determinado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ExportExcelCostDistributionManPower';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ExportExcelCostDistributionManPower';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la distribución de costos de mano de obra (empleados y contratistas) de un mes/año dado, completando información faltante desde nómina y cuentas por pagar, redistribuye las horas proporcionalmente al costo total y devuelve el resultado para exportar a Excel.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionManPower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir homologaciones de centros de producción (CostProductionCenterHomologation) y relaciones centro de producción-centro de costo (CostProductionCenterCostCenter) activas; Los centros de producción considerados deben tener Status = 1; Las liquidaciones de nómina a incluir deben estar en estado ''C'' (cerradas/confirmadas); Las cuentas por pagar a incluir deben tener Status = 2; Los detalles de horario (ScheduleDetail) deben tener State = 1 para ser sumados', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionManPower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los registros precargados desde Cost.CostDistributionManpower siempre quedan marcados como Distribuited=1 y no participan en la redistribución de horas; La redistribución de horas conserva el total original de horas de la entidad (se compensa la diferencia por redondeo en el primer Id); Solo se procesan datos de un único par año/mes por ejecución; ManpowerType=1 corresponde siempre a Empleado y ManpowerType=2 a Contratista; No se incluyen liquidaciones ni cuentas por pagar ya distribuidas previamente, evitando duplicados; La clasificación de conceptos (devengado, aporte patronal, parafiscal, provisión) depende de Payroll.GetConceptClassByType', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionManPower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Mano de obra (empleados y contratistas); Distribución de costos por centro de producción; Liquidación de nómina; Devengados; Provisiones laborales; Aportes patronales; Parafiscales; Cuentas por pagar a contratistas; Centros de costo y centros de producción; Homologación contable de cuentas; Horas trabajadas / horarios; Grupos de nómina', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionManPower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @CostDistributionManpower (tabla temporal): Inserta los registros ya existentes en Cost.CostDistributionManpower del año/mes solicitados marcándolos con Distribuited = 1 y ManpowerTypeName ''Empleado'' si ManpowerType=1, sino ''Contratista''; [INSERT] @CostDistributionManpower (tabla temporal): Para cada Payroll.Liquidation con RegisterStatus=''C'' del año/mes y que aún no tenga distribución previa (cdm.Id IS NULL), inserta un registro tipo ''Empleado'' (ManpowerType=1) con totales agregados desde CostDistribution clasificados por tipo de concepto (1=Devengado, 3=Aporte Patronal, 4=Parafiscal, 5=Provisión) y horas tomadas de ScheduleDetail o calculadas como DaysWorked * HoursDaily; [INSERT] @CostDistributionManpower (tabla temporal): Para cada Payments.AccountPayable con Status=2 del año/mes que no tenga distribución previa, inserta un registro tipo ''Contratista'' (ManpowerType=2) con TotalAccrued = SUM(Value * (+1 si Nature=1 sino -1)) o, si es nulo, ap.InvoiceValue; [UPDATE] @CostDistributionManpower (tabla temporal): Distribuye HoursQuantity proporcionalmente al peso (TotalAccrued+TotalProvision+TotalEmployerContribution+TotalParafiscal)/TotalDistribuited * Horas originales, redondeado, para entidades con más de un centro de producción; [UPDATE] @CostDistributionManpower (tabla temporal): Ajusta diferencias por redondeo de horas: al primer registro (MIN(Id)) de cada entidad le resta (Horas_originales - SUMA_horas_redistribuidas) para conservar el total exacto; [RETURN_RESULT] RESULTSET: Retorna el contenido consolidado de @CostDistributionManpower ordenado por ThirdPartyNit y ProductionCenterCode', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionManPower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ManpowerType = 1 → Etiqueta el registro como ''Empleado'' else Etiqueta el registro como ''Contratista''; si Existe registro en ScheduleDetail con State=1 para el empleado en el mes/año → Usa SUM(TotalNumberHours) como HoursQuantity else Usa DaysWorked * HoursDaily del contrato como HoursQuantity; si AccountPayableDetailConcept.Nature = 1 → Suma el valor con signo positivo (devengado) else Suma el valor con signo negativo (deducción); si La entidad tiene más de un centro de producción asociado (COUNT(*)>1) y Distribuited=0 → Aplica la redistribución proporcional de horas else No redistribuye; si Ya existe un registro previo en CostDistributionManpower para la entidad y tipo (cdm.Id IS NOT NULL) → No vuelve a generar el registro desde Liquidation/AccountPayable else Genera el registro desde la fuente correspondiente', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionManPower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payroll.GetConceptClassByType', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionManPower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionManpower; Cost.CostDistributionManpowerDetail; Cost.CostProductionCenter; Cost.CostProductionCenterCostCenter; Cost.CostProductionCenterHomologation; Common.ThirdParty; Payroll.Position; Payroll.Liquidation; Payroll.Employee; Payroll.Contract; Payroll.Group; Payroll.CostDistribution; Payroll.LiquidationDetail; Payroll.ScheduleDetail; Payments.AccountPayable; Payments.AccountPayableDetailConcept', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionManPower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelCostDistributionManPower';
-- GO
