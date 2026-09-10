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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que obtiene y consolida la distribución de mano de obra (empleados de nómina y contratistas externos) por centro de producción para un año y mes determinados, con el fin de asignar los costos laborales —devengados, provisiones, aportes patronales y parafiscales— a los centros de producción correspondientes. Combina información de liquidaciones de nómina (Payroll.Liquidation), contratos, cargos, grupos de nómina y terceros con los centros de producción y centros de costo definidos en el módulo de costos (Cost), y adicionalmente incorpora cuentas por pagar a contratistas (Payments.AccountPayable). Se utiliza en el proceso de costeo para distribuir la carga de personal entre las unidades productivas de la organización, discriminando entre mano de obra directa de nómina (tipo 1) y mano de obra contratada/externa (tipo 2).', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_GetDistributionManpower';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_GetDistributionManpower';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la mano de obra (empleados de nómina y contratistas) con sus valores devengados, provisiones, aportes patronales y parafiscales distribuidos por centro de producción para un periodo, repartiendo proporcionalmente las horas trabajadas según el peso económico.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere que existan centros de producción activos (Status=1) homologados con cuentas y centros de costo; Las liquidaciones de nómina del periodo deben estar cerradas (RegisterStatus=''C''); Las cuentas por pagar deben tener Status=2 para ser consideradas como contratistas; Los conceptos de nómina deben estar clasificados vía Payroll.GetConceptClassByType en tipos 1, 3, 4, 5; Si se filtra por EntityId debe corresponder al tipo de mano de obra solicitado', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera liquidaciones con RegisterStatus = ''C'' (cerradas/confirmadas); Solo considera cuentas por pagar con Status = 2; Solo considera centros de producción con Status = 1 (activos); El periodo se filtra por año/mes de PayrollDateLiquidated (nómina) o DocumentDate (CxP); La redistribución de horas preserva el total de horas original ajustando la diferencia en la fila de menor Id; Los empleados se identifican como ManpowerType=1 y los contratistas como ManpowerType=2; Los valores económicos se ponderan por (Débito - Crédito) según la naturaleza contable; El resultado final agrega por entidad, grupo, empleado/tercero, cargo y centro de producción', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Mano de obra (empleados y contratistas); Distribución de costos de nómina; Centros de producción y centros de costo; Devengado, provisión, aportes patronales, parafiscales; Liquidación de nómina; Cuentas por pagar a contratistas; Homologación de cuentas contables; Cronograma/turnos (ScheduleDetail); Conceptos de nómina por clase', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @CostDistributionManpower: Cuando ManpowerType=1 y la liquidación está cerrada en el periodo, inserta una fila por combinación empleado/centro de producción con sus valores económicos clasificados por tipo de concepto; [INSERT] @CostDistributionManpower: Cuando ManpowerType=2 y la cuenta por pagar tiene Status=2 en el periodo, inserta una fila por contratista/centro de producción con TotalAccrued = suma de Value firmada por Nature, o InvoiceValue si no hay distribución; [UPDATE] @CostDistributionManpower: Cuando una EntityId tiene múltiples filas con misma HoursQuantity, recalcula HoursQuantity proporcional al peso económico: ROUND((Accrued+Provision+EmployerContribution+Parafiscal)/Total * HorasOriginales, 0); [UPDATE] @CostDistributionManpower: Después de redistribuir, ajusta la fila de menor Id por entidad restando la diferencia entre las horas originales y la suma redondeada, garantizando que el total de horas se conserve; [RETURN_RESULT] @CostDistributionManpower: Devuelve el conjunto agregado por ManpowerType, EntityId, grupo, empleado/tercero, cargo y centro de producción con sumas de horas y valores económicos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ManpowerType = 1 (empleados de nómina) → Carga detalles desde Payroll.Liquidation con sus devengados, provisiones, aportes patronales y parafiscales por centro de producción else Si ManpowerType = 2, carga desde Payments.AccountPayable (contratistas) usando AccountPayableDetailConcept; si Existen registros en ScheduleDetail con horas > 0 para el empleado en el año/mes → Usa la suma de TotalNumberHours del cronograma como horas trabajadas else Usa DaysWorked * HoursDaily del contrato como horas; si Para una misma EntityId existen múltiples filas con misma HoursQuantity (registros duplicados por distribución) → Redistribuye las horas proporcionalmente al peso económico (Accrued+Provision+EmployerContribution+Parafiscal) sobre el total, ajustando la fila de menor Id para conservar el total de horas original; si ConceptClass de LiquidationDetail coincide con tipo 1/3/4/5 vía Payroll.GetConceptClassByType → Clasifica el (DebitValue-CreditValue) en TotalAccrued / TotalEmployerContribution / TotalParafiscal / TotalProvision respectivamente; si Nature = 1 en AccountPayableDetailConcept → Suma el Value como positivo en TotalAccrued; en caso contrario lo resta', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.Group; Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.Position; Cost.CostProductionCenter; Cost.CostProductionCenterCostCenter; Cost.CostProductionCenterHomologation; Payroll.CostDistribution; Payroll.LiquidationDetail; Payroll.GetConceptClassByType; Payroll.ScheduleDetail; Payroll.FunctionalUnit; Payments.AccountPayable; Payments.AccountPayableDetailConcept', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionManpower';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GetDistributionManpower';
-- GO
