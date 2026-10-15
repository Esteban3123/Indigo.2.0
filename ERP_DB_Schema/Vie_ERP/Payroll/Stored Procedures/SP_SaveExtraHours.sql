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