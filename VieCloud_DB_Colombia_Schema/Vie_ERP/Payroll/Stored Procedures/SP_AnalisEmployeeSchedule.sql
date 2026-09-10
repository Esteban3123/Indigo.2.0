-- =============================================
-- Author:		Daniel Eduardo Arévalo Bonilla
-- Create date: 2020-08-26
-- Description:	
-- =============================================

CREATE PROCEDURE [Payroll].[SP_AnalisEmployeeSchedule] 
	@InitialDate DATE,
	@EndDate DATE,
	@IdFunctionalUnitParam INT,
	@IdPositionParam INT,
	@IdEmployeeParam INT
WITH RECOMPILE
AS
BEGIN
	
		DECLARE @TablaTemp table
		( Warning TINYINT NOT NULL,
			IdEmployee INT NOT NULL,
			IdContract INT NOT NULL,
			IdFunctionalUnit INT NOT NULL,
			IdCostCenter INT NOT NULL,
			InitialDate DATE NULL,
			InitialTime TIME NULL,
			EndDate DATE NULL,
			EndTime TIME NULL,
			Observation VARCHAR(100),
			ExtraTime BIT NULL,
			HoursExtraTime INT NULL,
			InitialDateExtraTime DATE NULL,
			InitialHourExtraTime TIME NULL,
			EndDateExtraTime DATE NULL,
			EndTimeExtraTime TIME NULL
			); 

		DECLARE @DelayMinutes TINYINT = 0

		SELECT @DelayMinutes = ISNULL(IngressDelayMinutes, 0) FROM Payroll.PayrollSettings 

		DECLARE @IdScheduleDetail INT
		DECLARE @Letter VARCHAR(3)
		DECLARE @IdEmployee int
		DECLARE @DateDetail DATE
		DECLARE @IdContract INT
		DECLARE @IdFunctionalUnit INT
		DECLARE @IdCostCenter INT
		DECLARE @CodeFunctionalUnit VARCHAR(20)
		DECLARE @CodePosition VARCHAR(20)

		IF @IdFunctionalUnitParam = 0 BEGIN
			SET @CodeFunctionalUnit = '%%'
		END ELSE BEGIN
			SELECT @CodeFunctionalUnit = Code FROM Payroll.FunctionalUnit WHERE Id = @IdFunctionalUnitParam
		END

		IF @IdPositionParam = 0 BEGIN
			SET @CodePosition = '%%'
		END ELSE BEGIN
			SELECT @CodePosition = Code FROM Payroll.Position WHERE Id = @IdPositionParam
		END

		declare C_Horario cursor for	

		SELECT SD.EmployeeId, Letter, SD.Id, DateDetail, ScheduleFunctionalUnitId, CC.Id, SD.ContractId
		FROM Payroll.ScheduleDetail SD, Payroll.CostCenter CC, Payroll.FunctionalUnit FU, Payroll.[Contract] C, Payroll.Position P
		WHERE DateDetail BETWEEN @InitialDate and  @EndDate AND SD.ScheduleFunctionalUnitId = FU.Id and FU.CostCenterId = CC.Id 
		AND FU.Code like @CodeFunctionalUnit AND C.Id = SD.ContractId AND C.PositionId = P.Id AND P.Code like @CodePosition
			
		open C_Horario
			fetch next from C_Horario into @IdEmployee, @Letter, @IdScheduleDetail, @DateDetail, @IdFunctionalUnit, @IdCostCenter, @IdContract
			while @@FETCH_STATUS = 0 begin
					
				DECLARE @InitialTimeScheduleDetail TIME
				DECLARE @EndTimeScheduleDetail TIME

				DECLARE @InitialTimeScheduleEmployee TIME
				DECLARE @EndTimeScheduleEmployee TIME
				DECLARE @MaxTimeInitialDateScheduleEmployee TIME

				IF @Letter = 'I' AND (SELECT COUNT(*) FROM Payroll.EmployeeScheduleDetail ESD, Payroll.EmployeeScheduleC EC WHERE EC.Id = ESD.IdEmployeeScheduleC and EC.[Status] = 2 AND EmployeeId = @IdEmployee AND InitialDate = @DateDetail) > 0 BEGIN
					INSERT INTO @TablaTemp
					VALUES(2,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, NULL, @DateDetail, NULL, 'EL EMPLEADO MARCÓ EL SISTEMA DE HUELLAS PERO ESTÁ INCAPACITADO ESTE DÍA', 0, 0, NULL, NULL, NULL, NULL)
				END

				IF @Letter = 'V' AND (SELECT COUNT(*) FROM Payroll.EmployeeScheduleDetail ESD, Payroll.EmployeeScheduleC EC WHERE EC.Id = ESD.IdEmployeeScheduleC and EC.[Status] = 2 AND EmployeeId = @IdEmployee AND InitialDate = @DateDetail) > 0 BEGIN
					INSERT INTO @TablaTemp
					VALUES(2,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, NULL, @DateDetail, NULL, 'EL EMPLEADO MARCÓ EL SISTEMA DE HUELLAS PERO ESTÁ EN VACACIONES ESTE DÍA', 0, 0, NULL, NULL, NULL, NULL)
				END

				IF @Letter = 'S' AND (SELECT COUNT(*) FROM Payroll.EmployeeScheduleDetail ESD, Payroll.EmployeeScheduleC EC WHERE EC.Id = ESD.IdEmployeeScheduleC and EC.[Status] = 2 AND EmployeeId = @IdEmployee AND InitialDate = @DateDetail) > 0 BEGIN
					INSERT INTO @TablaTemp
					VALUES(2,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, NULL, @DateDetail, NULL, 'EL EMPLEADO MARCÓ EL SISTEMA DE HUELLAS PERO TIENE UNA SANCIÓN ESTE DÍA', 0, 0, NULL, NULL, NULL, NULL)
				END

				IF @Letter = 'L' AND (SELECT COUNT(*) FROM Payroll.EmployeeScheduleDetail ESD, Payroll.EmployeeScheduleC EC WHERE EC.Id = ESD.IdEmployeeScheduleC and EC.[Status] = 2 AND EmployeeId = @IdEmployee AND InitialDate = @DateDetail) > 0 BEGIN
					INSERT INTO @TablaTemp
					VALUES(2,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, NULL, @DateDetail, NULL, 'EL EMPLEADO MARCÓ EL SISTEMA DE HUELLAS PERO TIENE UNA LICENCIA ESTE DÍA', 0, 0, NULL, NULL, NULL, NULL)
				END

				IF @Letter <> 'N' BEGIN
					IF (SELECT COUNT (*) FROM Payroll.ScheduleDetailHour WHERE ScheduleDetailId = @IdScheduleDetail) = 1 BEGIN
 						SELECT @InitialTimeScheduleDetail = DateTimeInitial, @EndTimeScheduleDetail = DateTimeEnding FROM Payroll.ScheduleDetailHour WHERE ScheduleDetailId = @IdScheduleDetail

						SELECT @InitialTimeScheduleEmployee = MIN(InitialHourDate), @EndTimeScheduleEmployee = MAX(EndHourDate) 
						FROM Payroll.EmployeeScheduleDetail ESD, Payroll.EmployeeScheduleC EC WHERE EC.Id = ESD.IdEmployeeScheduleC AND EC.[Status] = 2
						AND InitialDate = @DateDetail and EmployeeId = @IdEmployee
						
						SET @MaxTimeInitialDateScheduleEmployee = DATEADD(MINUTE, @DelayMinutes, @InitialTimeScheduleEmployee)

						IF @InitialTimeScheduleEmployee < @InitialTimeScheduleDetail BEGIN
							INSERT INTO @TablaTemp
							VALUES(0,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado ingresó antes de su horario de Inicio', 0, 0, NULL, NULL, NULL, NULL)
						END ELSE IF @MaxTimeInitialDateScheduleEmployee > @InitialTimeScheduleDetail BEGIN
							INSERT INTO @TablaTemp
							VALUES(2,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @MaxTimeInitialDateScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado ingresó más tarde de la hora del Turno', 0, 0, NULL, NULL, NULL, NULL)
						END ELSE IF @InitialTimeScheduleDetail >= @InitialTimeScheduleEmployee AND @InitialTimeScheduleEmployee <= @MaxTimeInitialDateScheduleEmployee BEGIN
							INSERT INTO @TablaTemp
							VALUES(0,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado ingresó a tiempo', 0, 0, NULL, NULL, NULL, NULL)
						END

						IF @EndTimeScheduleEmployee > @EndTimeScheduleDetail BEGIN
							INSERT INTO @TablaTemp
							VALUES(1,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado salió después de la hora de de salida del turno', 1, DATEDIFF(HOUR,@EndTimeScheduleDetail,@EndTimeScheduleEmployee), @DateDetail, @EndTimeScheduleDetail, @DateDetail, @EndTimeScheduleEmployee)
						END ELSE IF @EndTimeScheduleEmployee < @EndTimeScheduleDetail BEGIN
							INSERT INTO @TablaTemp
							VALUES(2,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El empleado salió antes de tiempo', 0, 0, NULL, NULL, NULL, NULL)
						END
					END
				END

				IF @Letter = 'N' BEGIN
					SELECT @InitialTimeScheduleEmployee = MIN(InitialHourDate)
					FROM Payroll.EmployeeScheduleDetail ESD, Payroll.EmployeeScheduleC EC WHERE ESD.IdEmployeeScheduleC = EC.ID AND EC.[Status] = 2
					AND InitialDate = @DateDetail and EmployeeId = @IdEmployee

					SELECT @EndTimeScheduleEmployee = MAX(EndHourDate) 
					FROM Payroll.EmployeeScheduleDetail ESD, Payroll.EmployeeScheduleC EC WHERE EC.Id = ESD.IdEmployeeScheduleC AND EC.[Status] = 2
					AND EndDate = dateadd(DAY,1,@DateDetail) and EmployeeId = @IdEmployee

					-- Horario Nocturno
					SELECT @InitialTimeScheduleDetail = MIN(DateTimeInitial)
					FROM Payroll.ScheduleDetailHour WHERE ScheduleDetailId = @IdScheduleDetail and NextDay = 0

					SELECT @EndTimeScheduleDetail = MAX(DateTimeEnding)
					FROM Payroll.ScheduleDetailHour WHERE ScheduleDetailId = @IdScheduleDetail and NextDay = 1

					SET @MaxTimeInitialDateScheduleEmployee = DATEADD(MINUTE, @DelayMinutes, @InitialTimeScheduleDetail)

					IF @InitialTimeScheduleEmployee < @InitialTimeScheduleDetail BEGIN
						INSERT INTO @TablaTemp
						VALUES(0,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado ingresó antes de su horario de Inicio', 0, 0, NULL, NULL, NULL, NULL)
					END ELSE IF @InitialTimeScheduleEmployee > @MaxTimeInitialDateScheduleEmployee BEGIN
						INSERT INTO @TablaTemp
						VALUES(2,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @MaxTimeInitialDateScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado ingresó más tarde de la hora del Turno', 0, 0, NULL, NULL, NULL, NULL)
					END ELSE IF @InitialTimeScheduleDetail >= @InitialTimeScheduleEmployee AND @InitialTimeScheduleEmployee <= @MaxTimeInitialDateScheduleEmployee BEGIN
						INSERT INTO @TablaTemp
						VALUES(0,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado ingresó a tiempo', 0, 0, NULL, NULL, NULL, NULL)
					END

					IF @EndTimeScheduleEmployee > @EndTimeScheduleDetail BEGIN
						INSERT INTO @TablaTemp
						VALUES(1,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado salió después de la hora de de salida del turno', 1, DATEDIFF(HOUR,@EndTimeScheduleDetail,@EndTimeScheduleEmployee), @DateDetail, @EndTimeScheduleDetail, @DateDetail, @EndTimeScheduleEmployee)
					END ELSE IF @EndTimeScheduleEmployee < @EndTimeScheduleDetail BEGIN
						INSERT INTO @TablaTemp
						VALUES(2,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El empleado salió antes de tiempo', 0, 0, NULL, NULL, NULL, NULL)
					END

				END

				IF (SELECT COUNT (*) FROM Payroll.ScheduleDetailHour WHERE ScheduleDetailId = @IdScheduleDetail) > 1 AND @Letter <> 'N' BEGIN
					IF(SELECT COUNT(DateTimeInitial) FROM Payroll.ScheduleDetailHour where ScheduleDetailId = @IdScheduleDetail and DateTimeInitial = ANY (SELECT DateTimeEnding FROM Payroll.ScheduleDetailHour where ScheduleDetailId = @IdScheduleDetail)) > 0 BEGIN
						-- Es un mismo horario de corrido
						SELECT @InitialTimeScheduleDetail = MIN(DateTimeInitial)
						FROM Payroll.ScheduleDetailHour WHERE ScheduleDetailId = @IdScheduleDetail

						SELECT @EndTimeScheduleDetail = MAX(DateTimeEnding)
						FROM Payroll.ScheduleDetailHour WHERE ScheduleDetailId = @IdScheduleDetail
							
						SELECT @InitialTimeScheduleEmployee = MIN(InitialHourDate), @EndTimeScheduleEmployee = MAX(EndHourDate) 
						FROM Payroll.EmployeeScheduleDetail ESD, Payroll.EmployeeScheduleC EC WHERE EC.Id = ESD.IdEmployeeScheduleC AND EC.[Status] = 2
						AND InitialDate = @DateDetail and EmployeeId = @IdEmployee

						SET @MaxTimeInitialDateScheduleEmployee = DATEADD(MINUTE, @DelayMinutes, @InitialTimeScheduleDetail)

						IF @InitialTimeScheduleDetail > @MaxTimeInitialDateScheduleEmployee BEGIN
							INSERT INTO @TablaTemp
							VALUES(2,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @MaxTimeInitialDateScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado ingresó más tarde de la hora del Turno', 0, 0, NULL, NULL, NULL, NULL)
						END ELSE IF @InitialTimeScheduleEmployee < @InitialTimeScheduleDetail BEGIN
							INSERT INTO @TablaTemp
							VALUES(0,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado ingresó antes de su horario de Inicio', 0, 0, NULL, NULL, NULL, NULL)
						END

						IF @EndTimeScheduleEmployee > @EndTimeScheduleDetail BEGIN
							INSERT INTO @TablaTemp
							VALUES(1,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado salió después de la hora de de salida del turno', 1, DATEDIFF(HOUR,@EndTimeScheduleDetail,@EndTimeScheduleEmployee), @DateDetail, @EndTimeScheduleDetail, @DateDetail, @EndTimeScheduleEmployee)
						END ELSE IF @EndTimeScheduleEmployee < @EndTimeScheduleDetail BEGIN
							INSERT INTO @TablaTemp
							VALUES(2,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El empleado salió antes de tiempo', 0, 0, NULL, NULL, NULL, NULL)
						END

					END ELSE BEGIN
						-- Tiene Horarios separados
						DECLARE @CountSchedule int = 1
						DECLARE @CountEmployeeScheduleDetail int = 0
						DECLARE @CountEmployeeSheduleDetailHour int = 0

						SELECT @CountEmployeeSheduleDetailHour = COUNT(*) FROM Payroll.ScheduleDetailHour where ScheduleDetailId = @IdScheduleDetail
						SELECT @CountEmployeeScheduleDetail = COUNT(*) FROM Payroll.EmployeeScheduleDetail ESD, Payroll.EmployeeScheduleC EC WHERE EC.Id = ESD.IdEmployeeScheduleC AND EC.[Status] = 2 AND EmployeeId = @IdEmployee and InitialDate = @DateDetail

						DECLARE @TablaTempHourScheduleDetailHour table
						( Id INT NOT NULL IDENTITY(1,1),
							DateDetail DATE NULL,
							InitialTime TIME NULL,
							EndTime TIME NULL
						); 

						INSERT INTO @TablaTempHourScheduleDetailHour (DateDetail, InitialTime, EndTime)
						SELECT @DateDetail, DateTimeInitial, DateTimeEnding FROM Payroll.ScheduleDetailHour where ScheduleDetailId = @IdScheduleDetail order by DateTimeInitial
							
						IF @CountEmployeeSheduleDetailHour = @CountEmployeeScheduleDetail BEGIN
							-- Escenario donde se han registrado el mismo numero de registros en ambas tablas

							WHILE @CountSchedule <= @CountEmployeeSheduleDetailHour BEGIN
									
								SELECT @InitialTimeScheduleDetail = InitialTime
								FROM @TablaTempHourScheduleDetailHour WHERE Id = @CountSchedule

								SELECT @EndTimeScheduleDetail = EndTime
								FROM @TablaTempHourScheduleDetailHour WHERE Id = @CountSchedule

								SELECT @InitialTimeScheduleEmployee = MIN(InitialHourDate), @EndTimeScheduleEmployee = MAX(EndHourDate) 
								FROM Payroll.EmployeeScheduleDetail ESD, Payroll.EmployeeScheduleC EC WHERE EC.Id = ESD.IdEmployeeScheduleC AND EC.[Status] = 2
								AND InitialDate = @DateDetail and EmployeeId = @IdEmployee and InitialHourDate >= @InitialTimeScheduleDetail and InitialHourDate <= @InitialTimeScheduleDetail

								SET @MaxTimeInitialDateScheduleEmployee = DATEADD(MINUTE, @DelayMinutes, @InitialTimeScheduleEmployee)

								IF @MaxTimeInitialDateScheduleEmployee > @InitialTimeScheduleDetail BEGIN
									INSERT INTO @TablaTemp
									VALUES(2,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @MaxTimeInitialDateScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado ingresó más tarde de la hora del Turno', 0, 0, NULL, NULL, NULL, NULL)
								END ELSE IF @InitialTimeScheduleEmployee < @InitialTimeScheduleDetail BEGIN
									INSERT INTO @TablaTemp
									VALUES(0,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado ingresó antes de su horario de Inicio', 0, 0, NULL, NULL, NULL, NULL)
								END ELSE IF @InitialTimeScheduleDetail >= @InitialTimeScheduleEmployee AND @InitialTimeScheduleEmployee <= @MaxTimeInitialDateScheduleEmployee BEGIN
									INSERT INTO @TablaTemp
									VALUES(0,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado ingresó a tiempo', 0, 0, NULL, NULL, NULL, NULL)
								END

								IF @EndTimeScheduleEmployee > @EndTimeScheduleDetail BEGIN
									INSERT INTO @TablaTemp
									VALUES(1,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El Empleado salió después de la hora de de salida del turno', 1, DATEDIFF(HOUR,@EndTimeScheduleDetail,@EndTimeScheduleEmployee), @DateDetail, @EndTimeScheduleDetail, @DateDetail, @EndTimeScheduleEmployee)
								END ELSE IF @EndTimeScheduleEmployee < @EndTimeScheduleDetail BEGIN
									INSERT INTO @TablaTemp
									VALUES(2,@IdEmployee, @IdContract, @IdFunctionalUnit, @IdCostCenter, @DateDetail, @InitialTimeScheduleEmployee, @DateDetail, @EndTimeScheduleEmployee, 'El empleado salió antes de tiempo', 0, 0, NULL, NULL, NULL, NULL)
								END

								SET @CountSchedule = @CountSchedule + 1
							END

						END
	
					END
				END

			fetch next from C_Horario into @IdEmployee, @Letter, @IdScheduleDetail, @DateDetail, @IdFunctionalUnit, @IdCostCenter, @IdContract
			end -- fin while de C_Fondos
		close C_Horario 
		deallocate C_Horario

		select tp.Nit AS Cedula, tp.Name as Empleado, CC.Code as CodeCostCenter, CC.Name as NameCostCenter,
		FU.Code as CodeFunctionalUnit, FU.Name as NameFunctionalUnit, tt. *
		from @TablaTemp tt, Payroll.Employee E, Common.ThirdParty TP, Payroll.FunctionalUnit FU, Payroll.CostCenter CC
		WHERE tt.IdEmployee = E.Id AND E.ThirdPartyId = TP.Id AND TT.IdFunctionalUnit = FU.Id and TT.IdCostCenter = CC.Id
		order by tp.Name

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Analiza el cumplimiento del horario programado de los empleados comparando el turno asignado en el cronograma con las marcaciones reales del sistema biométrico de huellas, para un rango de fechas, una unidad funcional y un cargo específicos. Detecta y clasifica irregularidades como llegadas tardías, salidas anticipadas, marcaciones en días de incapacidad, vacaciones, sanciones o licencias, y posibles horas extra, generando observaciones y alertas (warnings) por cada novedad encontrada. Consume la configuración de tolerancia de minutos de ingreso tardío desde PayrollSettings, filtra los detalles de turno por unidad funcional y cargo usando FunctionalUnit y Position, y recorre día a día el detalle del cronograma (ScheduleDetail y ScheduleDetailHour) cruzándolo con las marcaciones aprobadas del empleado (EmployeeScheduleDetail). Es utilizado para control de asistencia, auditoría de nómina y soporte a la liquidación de recargos o descuentos por novedades de horario.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_AnalisEmployeeSchedule';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_AnalisEmployeeSchedule';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Audita la asistencia comparando las marcaciones biométricas del empleado contra el turno programado en el rango de fechas, generando advertencias por llegadas tarde, salidas anticipadas, horas extra e inconsistencias con incapacidades, vacaciones, sanciones o licencias.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_AnalisEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas @InitialDate y @EndDate deben acotar un rango válido para Payroll.ScheduleDetail.DateDetail; Si @IdFunctionalUnitParam <> 0 debe existir el registro en Payroll.FunctionalUnit; si @IdPositionParam <> 0 debe existir en Payroll.Position; Debe existir registro en Payroll.PayrollSettings para obtener los minutos de tolerancia de ingreso; Las marcaciones del empleado deben estar en EmployeeScheduleC con Status = 2 para ser consideradas', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_AnalisEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran marcaciones de empleados cuyo EmployeeScheduleC.Status = 2 (estado válido/aprobado de la programación de marcaciones); La tolerancia de llegada se toma de Payroll.PayrollSettings.IngressDelayMinutes (0 si es NULL); Las horas extra se calculan únicamente cuando la salida supera la hora fin del turno y se expresan en horas enteras (DATEDIFF HOUR); El warning 2 corresponde a anomalías (tarde, salida anticipada, marcación inconsistente con incapacidad/vacaciones/sanción/licencia); 1 a horas extra; 0 a registro normal/temprano/a tiempo; La letra ''N'' identifica turnos nocturnos que cruzan al día siguiente (ScheduleDetailHour.NextDay = 1); El procedimiento solo lee datos y devuelve un resultado; no persiste cambios en tablas físicas', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_AnalisEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Incapacidad; Vacaciones; Sanción disciplinaria; Licencia; Turno nocturno; Marcación biométrica (huellas); Horas extra; Tolerancia de ingreso; Centro de costo; Unidad funcional; Cargo; Contrato laboral', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_AnalisEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @IdFunctionalUnitParam = 0 → Se usa comodín ''%%'' para no filtrar por unidad funcional else Se obtiene el Code de Payroll.FunctionalUnit y se filtra el cursor por FU.Code LIKE ese código; si @IdPositionParam = 0 → Se usa comodín ''%%'' para no filtrar por cargo else Se obtiene el Code de Payroll.Position y se filtra por P.Code LIKE ese código; si @Letter = ''I'' y existe marcación de huellas (EmployeeScheduleC.Status=2) en la fecha → Inserta advertencia Warning=2: empleado marcó huellas estando incapacitado; si @Letter = ''V'' y existe marcación de huellas activa en la fecha → Inserta advertencia Warning=2: empleado marcó huellas estando en vacaciones; si @Letter = ''S'' y existe marcación de huellas activa en la fecha → Inserta advertencia Warning=2: empleado marcó huellas teniendo una sanción; si @Letter = ''L'' y existe marcación de huellas activa en la fecha → Inserta advertencia Warning=2: empleado marcó huellas teniendo una licencia; si @Letter <> ''N'' y ScheduleDetailHour para el turno tiene exactamente 1 registro → Compara hora marcada vs turno único: ingreso temprano (Warning=0), tarde (Warning=2 con tolerancia @DelayMinutes) o a tiempo (Warning=0); y salida tardía (Warning=1, calcula horas extra) o salida anticipada (Warning=2); si @Letter = ''N'' (turno nocturno) → Toma InitialHourDate del @DateDetail y EndHourDate del día siguiente; usa ScheduleDetailHour con NextDay=0 para inicio y NextDay=1 para fin; aplica las mismas reglas de comparación de ingreso/salida; si ScheduleDetailHour para el turno tiene >1 registros y @Letter <> ''N'' y existe encadenamiento (DateTimeInitial = DateTimeEnding de otro tramo) → Trata el turno como uno corrido: usa MIN(DateTimeInitial) y MAX(DateTimeEnding) y aplica reglas de comparación; si ScheduleDetailHour >1 registros, sin encadenamiento, y conteo de tramos = conteo de marcaciones del empleado → Itera tramo por tramo evaluando ingreso y salida contra cada intervalo programado; si @EndTimeScheduleEmployee > @EndTimeScheduleDetail → Registra Warning=1 con ExtraTime=1 y HoursExtraTime = DATEDIFF(HOUR, fin turno, fin marcación)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_AnalisEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.PayrollSettings; Payroll.FunctionalUnit; Payroll.Position; Payroll.ScheduleDetail; Payroll.CostCenter; Payroll.Contract; Payroll.EmployeeScheduleDetail; Payroll.EmployeeScheduleC; Payroll.ScheduleDetailHour; Payroll.Employee; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_AnalisEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_AnalisEmployeeSchedule';
-- GO
