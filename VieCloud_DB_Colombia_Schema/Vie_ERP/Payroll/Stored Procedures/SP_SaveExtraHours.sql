-- =============================================
-- Author:		Daniel Eduardo Arévalo Bonilla
-- Create date: 2020-11-29
-- Description:	
-- =============================================

CREATE PROCEDURE [Payroll].[SP_SaveExtraHours]
@XmlObject XML
WITH RECOMPILE
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables para obtener la cabecera

	DECLARE @AnalisisEmployeeSchedule TABLE
	(
		Cedula VARCHAR(30), Empleado VARCHAR(200), IdEmployee INT, IdContract INT, IdFunctionalUnit INT, IdCostCenter INT, InitialDate DATE,
		InitialTime TIME, EndDate DATE, EndTime TIME, ExtraTime bit, HoursExtraTime INT, InitialDateExtraTime DATE, InitialHourExtraTime TIME,
		EndDateExtraTime DATE, EndTimeExtraTime TIME
	)

	Begin try
		insert into @AnalisisEmployeeSchedule
		select 
			t.x.value('Cedula[1]','VARCHAR(30)') AS Cedula,
			t.x.value('Empleado[1]','VARCHAR(200)') as Empleado,
			t.x.value('IdEmployee[1]','INT') as IdEmployee,
			t.x.value('IdContract[1]','INT') as IdContract,
			t.x.value('IdFunctionalUnit[1]','INT') as IdFunctionalUnit,
			t.x.value('IdCostCenter[1]','INT') as IdCostCenter,
			t.x.value('InitialDate[1]','date') as InitialDate,
			t.x.value('InitialTime[1]','TIME') as InitialTime,
			t.x.value('EndDate[1]','DATE') as EndDate,
			t.x.value('EndTime[1]','TIME') as EndTime,
			t.x.value('Extratime[1]','bit') as Extratime,
			t.x.value('HoursExtraTime[1]','INT') as HoursExtraTime,
			t.x.value('InitialDateExtraTime[1]','DATETIME') as InitialDateExtraTime,
			t.x.value('InitialHourExtraTime[1]','TIME') as InitialHourExtraTime,
			t.x.value('EndDateExtraTime[1]','DATETIME') as EndDateExtraTime,
			t.x.value('EndTimeExtraTime[1]','TIME') as EndTimeExtraTime
		from @XmlObject.nodes('/AnalisisSchedule') t(x)

		

		DECLARE @IdEmployee INT
		DECLARE @IdContract INT
		DECLARE @IdFunctionalUnit INT
		DECLARE @IdCostCenter INT
		DECLARE @HoursExtraTime INT
		DECLARE @InitialDateExtraTime DATE
		DECLARE @InitialHourExtraTime TIME
		DECLARE @EndDateExtraTime DATE
		DECLARE @EndTimeExtraTime TIME

		declare C_Horario cursor for	

		SELECT IdEmployee, IdContract, IdFunctionalUnit, IdCostCenter, HoursExtraTime, InitialDateExtraTime, InitialHourExtraTime, EndDateExtraTime, EndTimeExtraTime from @AnalisisEmployeeSchedule

		open C_Horario
			fetch next from C_Horario into @IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @HoursExtraTime, @InitialDateExtraTime, @InitialHourExtraTime, @EndDateExtraTime, @EndTimeExtraTime
			while @@FETCH_STATUS = 0 begin
					
				DECLARE @IdGroup INT
				DECLARE @IdCompany INT
				DECLARE @IdBranchOffice INT
				DECLARE @Letter VARCHAR(1)
				DECLARE @IdScheduleDetail INT
				DECLARE @IdScheduleDetailHour INT
				DECLARE @InitialDateTimeHourExtraTime DATETIME
				DECLARE @EndDateTimeHourExtraTime DATETIME

				DECLARE @IdPayrollParameter INT
				DECLARE @InitialTimeOrdinaryDay TIME
				DECLARE @EndTimeOrdinaryDay TIME

				SET @InitialDateTimeHourExtraTime = CONVERT(DATETIME, @InitialDateExtraTime) + CONVERT(DATETIME, @InitialHourExtraTime)
				SET @EndDateTimeHourExtraTime = CONVERT(DATETIME, @EndDateExtraTime) + CONVERT(DATETIME, @EndTimeExtraTime)

				SELECT @IdGroup = GroupId, @IdBranchOffice = FU.BranchOfficeId 
				FROM Payroll.Contract C, Payroll.FunctionalUnit FU
				WHERE C.Id = @IdContract AND FU.Id = C.FunctionalUnitId

				SELECT @IdPayrollParameter = PayrollParameterId FROM Payroll.[Group] WHERE Id = @IdGroup

				SELECT @InitialTimeOrdinaryDay = InitialTimeOrdinaryDay, @EndTimeOrdinaryDay = EndTimeOrdinaryDay FROM Payroll.PayrollParameter WHERE Id = @IdPayrollParameter

				SELECT @IdCompany = CompanyId FROM Payroll.[Group]

				IF @InitialHourExtraTime >= '19:00:00' AND @InitialHourExtraTime <= '06:00:00'  BEGIN
					SET @Letter = 'N'
				END

				IF @InitialHourExtraTime >= '06:00:00' AND @InitialHourExtraTime <= '13:00:00'  BEGIN
					SET @Letter = 'M'
				END

				IF @InitialHourExtraTime >= '13:00:00' AND @InitialHourExtraTime <= '19:00:00'  BEGIN
					SET @Letter = 'T'
				END

				IF (SELECT COUNT(*) FROM Payroll.ScheduleDetail where EmployeeId = @IdEmployee AND DateDetail = @InitialDateExtraTime) = 0 BEGIN
					-- Es porque no hay nada en el cuadro de turnos ese día y debo ingresar desde cero

					--Inserto en la tabla de Detalle de Cuadro de Turnos
					INSERT INTO Payroll.ScheduleDetail(GroupId, EmployeeId, ContractId, CompanyId, BranchOfficeId, FunctionalUnitId, CenterCostId, ScheduleFunctionalUnitId, Letter, DateDetail, TotalNumberHours, [Status], [State])
					VALUES(@IdGroup, @IdEmployee, @IdContract, @IdCompany, @IdBranchOffice, @IdFunctionalUnit, @IdCostCenter, @IdFunctionalUnit, @Letter, @InitialDateExtraTime, @HoursExtraTime, 0, 0)

					set @IdScheduleDetail = SCOPE_IDENTITY()

					
				END ELSE BEGIN
					-- Es porque ya hay turno ese día en el cuadro de turnos, y debo agregar los detalles
					SELECT @IdScheduleDetail = Id FROM Payroll.ScheduleDetail where EmployeeId = @IdEmployee AND DateDetail = @InitialDateExtraTime

					PRINT 'InitialDateTimeHourExtraTime' + convert(varchar(20),@InitialDateTimeHourExtraTime)
					PRINT 'EndDateTimeHourExtraTime' + convert(varchar(20),@EndDateTimeHourExtraTime)

					IF(SELECT COUNT(*) FROM Payroll.ScheduleDetailHour where ScheduleDetailId = @IdScheduleDetail and DateTimeInitial = @InitialDateTimeHourExtraTime and DateTimeEnding = @EndDateTimeHourExtraTime) = 0 BEGIN
						INSERT INTO Payroll.ScheduleDetailHour(ScheduleDetailId, DateTimeInitial, DateTimeEnding, TotalNumberHours, NextDay, [Event], Approved, AppliedLiquidationConcept, [State], EventLastMonth, PaidEventLastMonth)
						SELECT @IdScheduleDetail, DatetimeInitial, DatetimeEnd, TotalNumberHours,NextDay, [Event], Approved, AppliedLiquidationConcept, 0, 0, NULL FROM [Payroll].[fnScheduleDetailHour](@InitialDateTimeHourExtraTime, @EndDateTimeHourExtraTime, @IdGroup, @HoursExtraTime)

						SET @IdScheduleDetailHour = SCOPE_IDENTITY()

						INSERT INTO Payroll.ScheduleDetailConcept(ScheduleDetailHourId, ConceptType, ConceptId)
						VALUES(@IdScheduleDetailHour, 0, 8307)
					END

				END

			fetch next from C_Horario into @IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @HoursExtraTime, @InitialDateExtraTime, @InitialHourExtraTime, @EndDateExtraTime, @EndTimeExtraTime
			end -- fin while de C_Fondos
		close C_Horario 
		deallocate C_Horario

	SELECT '001' as CodeMessage, 'Se ha guardado correctamente las Horas Extras en el Cuadro de Turnos' as Message

	END TRY
	BEGIN CATCH
		SELECT '999' AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(100)) AS Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra horas extras de empleados a partir de un XML con el análisis de horario. Recibe datos de cada empleado (cédula, contrato, unidad funcional, centro de costo, fechas y horas de la jornada extraordinaria) y los persiste en el cuadro de turnos de nómina: si el empleado no tiene turno registrado ese día en ScheduleDetail, crea el registro desde cero; si ya existe un turno, agrega el detalle de horas extras en ScheduleDetailHour y sus conceptos asociados (ScheduleDetailConcept). Clasifica automáticamente la jornada extra como nocturna (N), mañana (M) o tarde (T) según la hora de inicio, y consulta los parámetros del grupo de nómina y el contrato vigente para determinar el grupo, la sucursal y los tiempos ordinarios de referencia. Se utiliza en el proceso de liquidación de nómina para registrar y valorar el trabajo en tiempo extraordinario del personal.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveExtraHours';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveExtraHours';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra horas extras laboradas por empleados en el cuadro de turnos, creando o complementando el detalle diario y sus conceptos de liquidación a partir de un XML de entrada.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveExtraHours';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /AnalisisSchedule con los campos de empleado, contrato, unidad funcional, centro de costo y rangos de fecha/hora extra.; Debe existir el contrato (Payroll.Contract) y la unidad funcional (Payroll.FunctionalUnit) referenciados para resolver Group y BranchOffice.; Debe existir el grupo de nómina (Payroll.Group) con su PayrollParameter asociado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveExtraHours';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada nuevo ScheduleDetailHour creado por este flujo siempre genera un ScheduleDetailConcept con ConceptId=8307 y ConceptType=0 (concepto fijo de horas extras).; Los nuevos ScheduleDetail se crean siempre con Status=0 y State=0 (estado inicial no aprobado).; No se duplica un ScheduleDetailHour con el mismo rango DateTimeInitial/DateTimeEnding sobre el mismo ScheduleDetail.; La letra de jornada (N/M/T) se determina exclusivamente por la hora de inicio de la extra, no por la hora de fin.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveExtraHours';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'horas extras; cuadro de turnos; jornada nocturna/mañana/tarde; grupo de nómina; parámetros de nómina; contrato laboral; unidad funcional; centro de costo; concepto de liquidación', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveExtraHours';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Payroll.ScheduleDetail: Cuando no existe registro en ScheduleDetail para el EmployeeId y DateDetail = @InitialDateExtraTime, se crea el detalle de turno desde cero con Status=0, State=0 y la letra de jornada (N/M/T) calculada según la hora de inicio.; [INSERT] Payroll.ScheduleDetailHour: Cuando ya existe ScheduleDetail para ese día y no hay un ScheduleDetailHour con el mismo DateTimeInitial y DateTimeEnding, se inserta el detalle horario obtenido de la función Payroll.fnScheduleDetailHour con State=0 y EventLastMonth=0.; [INSERT] Payroll.ScheduleDetailConcept: Tras insertar un nuevo ScheduleDetailHour se registra automáticamente un concepto con ConceptType=0 y ConceptId=8307 asociado al SCOPE_IDENTITY() del detalle horario.; [RETURN_RESULT] (resultset): Al finalizar exitosamente devuelve CodeMessage=''001'' con mensaje de confirmación; ante excepción devuelve CodeMessage=''999'' con ERROR_MESSAGE() y línea del error.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveExtraHours';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @InitialHourExtraTime entre 19:00 y 06:00 → Clasifica la jornada como nocturna (Letter=''N''); si @InitialHourExtraTime entre 06:00 y 13:00 → Clasifica la jornada como mañana (Letter=''M''); si @InitialHourExtraTime entre 13:00 y 19:00 → Clasifica la jornada como tarde (Letter=''T''); si No existe ScheduleDetail para el empleado en @InitialDateExtraTime → Inserta nuevo ScheduleDetail con TotalNumberHours=@HoursExtraTime else Recupera el IdScheduleDetail existente y, si no hay solapamiento exacto en ScheduleDetailHour, inserta el detalle horario y su ScheduleDetailConcept (ConceptId=8307)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveExtraHours';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payroll.fnScheduleDetailHour', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveExtraHours';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Contract; Payroll.FunctionalUnit; Payroll.Group; Payroll.PayrollParameter; Payroll.ScheduleDetail; Payroll.ScheduleDetailHour; Payroll.fnScheduleDetailHour', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveExtraHours';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveExtraHours';
-- GO
