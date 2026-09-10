-- =============================================
-- Author:		Daniel Eduardo Arévalo Bonilla
-- Create date: 15/07/2016
-- Description:	Actualiza las Provisiones de Nómina por Tipo de Provision para Nóminas CONFIRMADAS

-- Fecha Modificación: 21/07/2016
-- Description: Se agregan las Opciones 6. SENA, 7. ICBF, 8. Caja de Compensacion
-- =============================================
CREATE PROCEDURE [Payroll].[UpdateProvision]
	@IdGroup int, -- Id Grupo
	@PayrollDate date, -- Fecha de Nomina
	@ProvisionType int, --(1. Primas, 2. Cesantias, 3. Intereses de Cesantias, 4. Vacaciones, 5. ARL, 6. SENA, 7. ICBF, 8. Caja de Compensacion)
	@Status bit, -- (0. No Confirmar, 1. Confirmar)
	@NitEmployee varchar(20) -- Cedula del Empleado, Vacío si se desea que cargue todos los Empleados del Grupo
AS 
BEGIN
	SET NOCOUNT ON;

	declare @Transaccion bit
	declare @Cantidad int

	BEGIN TRY

		set @Transaccion = 0 ;

		IF @NitEmployee = '' BEGIN
			SET @NitEmployee = '%%'
		END

		IF @ProvisionType > 8 BEGIN
			SELECT 'TIPO DE PROVISION ERRÓNEA' AS Mensaje, '999' AS CodigoMensaje
			return 9999
		END

		SELECT @Cantidad =  COUNT(*) from Payroll.Employee E, Common.ThirdParty TP
		WHERE TP.Id = E.ThirdPartyId and TP.Nit LIKE @NitEmployee

		IF @Cantidad = 0 BEGIN
			SELECT 'El Empleado con Cédula ' + @NitEmployee + ' NO EXISTE' AS Mensaje, '999' AS CodigoMensaje
			return 9999
		END

		IF @NitEmployee <> '%%' BEGIN
			SELECT @Cantidad = COUNT(*) FROM 
				Payroll.Liquidation L, Common.ThirdParty TP, Payroll.Employee E
			WHERE 
				PayrollDateLiquidated = @PayrollDate 
				AND TP.Id = E.ThirdPartyId 
				AND L.EmployeeId = E.Id
				AND RegisterStatus = 'C' AND L.GroupId = @IdGroup and TP.Nit = @NitEmployee

				IF @Cantidad = 0 BEGIN
					SELECT 'Con la Cédula digitada ' + @NitEmployee + ' y el Id del Grupo Ingresado, y para la Fecha Digitada ' + CONVERT(VARCHAR(10), @PayrollDate, 103) + ' NO EXISTEN DATOS DE ESE EMPLEADO' AS Mensaje, '999' AS CodigoMensaje
					return 9999
				END				
		END

		SELECT @Cantidad = COUNT(*) FROM Payroll.Liquidation L, Payroll.Employee E, Common.ThirdParty TP
		WHERE L.PayrollDateLiquidated = @PayrollDate AND L.RegisterStatus = 'C' AND E.Id = L.EmployeeId AND E.ThirdPartyId = TP.Id AND TP.Nit like @NitEmployee

		if @Cantidad = 0 BEGIN
			SELECT 'Con la Cédula digitada ' + @NitEmployee + ' y el Id del Grupo Ingresado, y para la Fecha Digitada ' + CONVERT(VARCHAR(10), @PayrollDate, 103) + ' NO EXISTEN DATOS DE ESE EMPLEADO' AS Mensaje, '999' AS CodigoMensaje
			return 9999
		END

			-- NO CONFIRMADA
			DECLARE @ProvisionReturn Table (IdLiquidation int, IdEmployee int, NitEmployee varchar(20), NameEmployee varchar(100), OldIBCProvision numeric(18,0), NewIBCProvision numeric(18,0), OlderProvisionValue numeric(18,0), NewProvisionValue numeric(18,0))
			
			INSERT INTO @ProvisionReturn
			SELECT L.Id, E.Id, TP.Nit, TP.Name, 0, 0, 0, 0 FROM Payroll.Liquidation L, Payroll.Employee E, Common.ThirdParty TP
			WHERE L.EmployeeId = E.Id and TP.Id = E.ThirdPartyId AND L.RegisterStatus = 'C' AND L.PayrollDateLiquidated = @PayrollDate AND TP.Nit like @NitEmployee and L.GroupId = @IdGroup

			DECLARE @IdLiquidation int
			DECLARE @IdEmployee int
			DECLARE @UnemploymentAccumulated numeric(18,0)
			DECLARE @ProvisionIncentive numeric(18,0)
			DECLARE @ProvisionVacation numeric(18,0)
			DECLARE @ProvisionInterestsUnemployment numeric(18,0)
			DECLARE @IBCVacation numeric(18,0)
			DECLARE @IBCIncentivePayment numeric(18,0)
			DECLARE @IBCUnemployment numeric(18,0)
			DECLARE @IBCSENA numeric(18,0)
			DECLARE @IBCICBF numeric(18,0)
			DECLARE @IBCCompensationFund numeric(18,0)
			DECLARE @ValueSENA numeric(18,0)
			DECLARE @ValueICBF numeric(18,0)
			DECLARE @ValueCompensationFund numeric(18,0)
			
			declare C_Fondos cursor for	
						
			-- FONDOS
			SELECT Id, EmployeeId, UnemploymentAccumulated, ProvisionIncentive, ProvisionVacation, ProvisionInterestsUnemployment, IBCVacation, IBCIncentivePayment, IBCUnemployment, IBCSENA , IBCICBF, IBCCompensationFund
			FROM Payroll.Liquidation 
			WHERE PayrollDateLiquidated = @PayrollDate AND RegisterStatus = 'C' AND GroupId = @IdGroup
		
			begin transaction
				SET @Transaccion = 1

			open C_Fondos
				fetch next from C_Fondos into @IdLiquidation, @IdEmployee, @UnemploymentAccumulated, @ProvisionIncentive, @ProvisionVacation, @ProvisionInterestsUnemployment, @IBCVacation, @IBCIncentivePayment, @IBCUnemployment, @IBCSENA, @IBCICBF, @IBCCompensationFund 
				while @@FETCH_STATUS = 0 begin

					DECLARE @NewIBC as numeric(18,0) = 0
					DECLARE @NewProvision as Numeric(18,0) = 0
					
					IF @ProvisionType = 1 BEGIN
						--PRIMAS

						SELECT @NewIBC = SUM(LD.ConceptTotalValue) 
						FROM Payroll.Liquidation L, Payroll.LiquidationDetail LD, Payroll.Concept CONC
						WHERE L.Id = LD.PayrollId AND LD.ConceptId = CONC.Id AND L.Id = @IdLiquidation AND CONC.AffectIBCIncentivePayment = 1

						SET @NewProvision = @NewIBC * (1.00/12.00)

						-- Actualizo la Tabla Temporal para devolver
						UPDATE @ProvisionReturn 
							SET OldIBCProvision = @IBCIncentivePayment, OlderProvisionValue = @ProvisionIncentive, NewIBCProvision = @NewIBC, NewProvisionValue = @NewProvision
						WHERE IdEmployee = @IdEmployee

					END ELSE IF  @ProvisionType = 2 BEGIN
						-- CESANTIAS

						SELECT @NewIBC = SUM(LD.ConceptTotalValue) 
						FROM Payroll.Liquidation L, Payroll.LiquidationDetail LD, Payroll.Concept CONC
						WHERE L.Id = LD.PayrollId AND LD.ConceptId = CONC.Id AND L.Id = @IdLiquidation AND CONC.AffectIBCSeverance = 1

						SET @NewProvision = @NewIBC * (1.00/12.00)

						--PRINT '@NewProvision' + CONVERT(VARCHAR(20),@NewProvision)

						-- Actualizo la Tabla Temporal para devolver
						UPDATE @ProvisionReturn 
							SET OldIBCProvision = @IBCUnemployment, OlderProvisionValue = @UnemploymentAccumulated, NewIBCProvision = @NewIBC, NewProvisionValue = @NewProvision
						WHERE IdEmployee = @IdEmployee

					END ELSE IF  @ProvisionType = 3 BEGIN
						-- INTERESES DE CESANTIAS

						SELECT @NewIBC = SUM(LD.ConceptTotalValue) 
						FROM Payroll.Liquidation L, Payroll.LiquidationDetail LD, Payroll.Concept CONC
						WHERE L.Id = LD.PayrollId AND LD.ConceptId = CONC.Id AND L.Id = @IdLiquidation AND CONC.AffectIBCSeverance = 1

						SET @NewProvision = @NewIBC * 0.01

						-- Actualizo la Tabla Temporal para devolver
						UPDATE @ProvisionReturn 
							SET OldIBCProvision = @IBCUnemployment, OlderProvisionValue = @ProvisionInterestsUnemployment, NewIBCProvision = @NewIBC, NewProvisionValue = @NewProvision
						WHERE IdEmployee = @IdEmployee

					END ELSE IF  @ProvisionType = 4 BEGIN
						-- VACACIONES

						SELECT @NewIBC = SUM(LD.ConceptTotalValue) 
						FROM Payroll.Liquidation L, Payroll.LiquidationDetail LD, Payroll.Concept CONC
						WHERE L.Id = LD.PayrollId AND LD.ConceptId = CONC.Id AND L.Id = @IdLiquidation AND CONC.AffectIBCVacation = 1

						SET @NewProvision = @NewIBC * 0.0417

							-- Actualizo la Tabla Temporal para devolver
						UPDATE @ProvisionReturn 
							SET OldIBCProvision = @IBCVacation, OlderProvisionValue = @ProvisionVacation, NewIBCProvision = @NewIBC, NewProvisionValue = @NewProvision
						WHERE IdEmployee = @IdEmployee

					END ELSE IF  @ProvisionType = 5 BEGIN
						-- ARL
						DECLARE @EmployeePercentage as Decimal (8,4) = 0
						SELECT @NewIBC = SUM(LD.ConceptTotalValue) 
						FROM Payroll.Liquidation L, Payroll.LiquidationDetail LD, Payroll.Concept CONC
						WHERE L.Id = LD.PayrollId AND LD.ConceptId = CONC.Id AND L.Id = @IdLiquidation AND CONC.AffectIBCARP = 1

						SELECT @EmployeePercentage = ProfessionalRiskPercentage FROM Payroll.Employee where ID = @IdEmployee

						SET @NewProvision = @NewIBC * (@EmployeePercentage / 100)

							-- Actualizo la Tabla Temporal para devolver
						UPDATE @ProvisionReturn 
							SET OldIBCProvision = @IBCVacation, OlderProvisionValue = @ProvisionVacation, NewIBCProvision = @NewIBC, NewProvisionValue = @NewProvision
						WHERE IdEmployee = @IdEmployee
					
					END ELSE IF  @ProvisionType = 6 BEGIN
						-- 21/07/2016
						-- SENA

						DECLARE @SENAPercentage as numeric(6,3) = 0

						SELECT @SENAPercentage = PP.SenaContributionPercentage 
						FROM Payroll.[Group] G, Payroll.PayrollParameter PP 
						WHERE G.PayrollParameterId = PP.Id and G.Id = @IdGroup

						SELECT @NewIBC = SUM(LD.ConceptTotalValue) 
						FROM Payroll.Liquidation L, Payroll.LiquidationDetail LD, Payroll.Concept CONC
						WHERE L.Id = LD.PayrollId AND LD.ConceptId = CONC.Id AND L.Id = @IdLiquidation AND CONC.AffectIBCSENA = 1

						SET @NewProvision = @NewIBC * (@SENAPercentage / 100)

							-- Actualizo la Tabla Temporal para devolver
						UPDATE @ProvisionReturn 
							SET OldIBCProvision = @IBCSENA, OlderProvisionValue = @ValueSENA, NewIBCProvision = @NewIBC, NewProvisionValue = @NewProvision
						WHERE IdEmployee = @IdEmployee
					END ELSE IF  @ProvisionType = 7 BEGIN
						-- 21/07/2016
						-- ICBF
						DECLARE @ICBFPercentage as numeric(6,3) = 0

						SELECT @ICBFPercentage = PP.ICBFContributionPercentage 
						FROM Payroll.[Group] G, Payroll.PayrollParameter PP 
						WHERE G.PayrollParameterId = PP.Id and G.Id = @IdGroup

						SELECT @NewIBC = SUM(LD.ConceptTotalValue) 
						FROM Payroll.Liquidation L, Payroll.LiquidationDetail LD, Payroll.Concept CONC
						WHERE L.Id = LD.PayrollId AND LD.ConceptId = CONC.Id AND L.Id = @IdLiquidation AND CONC.AffectIBCICBF = 1

						SET @NewProvision = @NewIBC * (@ICBFPercentage / 100)

							-- Actualizo la Tabla Temporal para devolver
						UPDATE @ProvisionReturn 
							SET OldIBCProvision = @IBCICBF, OlderProvisionValue = @ValueICBF, NewIBCProvision = @NewIBC, NewProvisionValue = @NewProvision
						WHERE IdEmployee = @IdEmployee

					END ELSE IF  @ProvisionType = 8 BEGIN
						-- 21/07/2016
						-- Caja de Compensacion
						DECLARE @CajaCompensacionPercentage as numeric(6,3) = 0

						SELECT @CajaCompensacionPercentage = PP.CompensationFundContributionPercentage 
						FROM Payroll.[Group] G, Payroll.PayrollParameter PP 
						WHERE G.PayrollParameterId = PP.Id and G.Id = @IdGroup

						SELECT @NewIBC = SUM(LD.ConceptTotalValue) 
						FROM Payroll.Liquidation L, Payroll.LiquidationDetail LD, Payroll.Concept CONC
						WHERE L.Id = LD.PayrollId AND LD.ConceptId = CONC.Id AND L.Id = @IdLiquidation AND CONC.AffectIBCCompensationFund = 1

						SET @NewProvision = @NewIBC * (@CajaCompensacionPercentage / 100)

							-- Actualizo la Tabla Temporal para devolver
						UPDATE @ProvisionReturn 
							SET OldIBCProvision = @IBCCompensationFund, OlderProvisionValue = @ValueCompensationFund, NewIBCProvision = @NewIBC, NewProvisionValue = @NewProvision
						WHERE IdEmployee = @IdEmployee
					END
							
					DECLARE @IdLiquidationDetail int = 0

					IF @Status = 1 BEGIN
						-- ESTADO CONFIRMADO, SE ACTUALIZA LA TABLA DE LIQUIDACION
	
							IF @ProvisionType = 1 BEGIN
								-- PRIMAS
								UPDATE Payroll.Liquidation SET ProvisionIncentive = @NewProvision, IBCIncentivePayment = @NewIBC WHERE Id = @IdLiquidation
							END ELSE IF  @ProvisionType = 2 BEGIN
								-- CESANTIAS
								UPDATE Payroll.Liquidation SET UnemploymentAccumulated = @NewProvision, IBCUnemployment = @NewIBC, IBCUnemploymentNoSanctions = @NewIBC WHERE Id = @IdLiquidation

							END ELSE IF  @ProvisionType = 3 BEGIN
								-- INTERESES DE CESANTIAS
								UPDATE Payroll.Liquidation SET ProvisionInterestsUnemployment = @NewProvision, IBCUnemployment = @NewIBC, IBCUnemploymentNoSanctions = @NewIBC WHERE Id = @IdLiquidation

							END ELSE IF  @ProvisionType = 4 BEGIN
								-- VACACIONES
								UPDATE Payroll.Liquidation SET ProvisionVacation = @NewProvision, IBCVacation = @NewIBC  WHERE Id = @IdLiquidation

							END ELSE IF  @ProvisionType = 5 BEGIN
								UPDATE Payroll.Liquidation SET IBCOccupationalRisks = @NewIBC, OccupationalRisksContributionValue = @NewProvision WHERE Id = @IdLiquidation

							END ELSE IF @ProvisionType = 6 BEGIN
								-- 21/07/2016
								-- SENA
								
								SELECT @IdLiquidationDetail = Id FROM Payroll.LiquidationDetail where PayrollId = @IdLiquidation and ConceptClass = '035' and ConceptType = 3

								IF @IdLiquidationDetail > 0 BEGIN	
									UPDATE Payroll.Liquidation SET IBCSENA  = @NewIBC, SenaContributionValue = @NewProvision, ParafiscalContribution = (FamilyCompensationFundContributionValue + ICBFContributionValue + @NewProvision) WHERE Id = @IdLiquidation
									UPDATE Payroll.LiquidationDetail SET ConceptTotalValue = @NewProvision, AccruedValue = @NewProvision where Id = @IdLiquidationDetail
								END ELSE BEGIN
									SELECT 'Con la Cédula digitada ' + @NitEmployee + ' No tiene, en el Detalle de Liquidación, el Concepto SENA' AS Mensaje, '999' AS CodigoMensaje
									return 9999
								END
							END ELSE IF @ProvisionType = 7 BEGIN
								-- 21/07/2016
								-- ICBF
								SELECT @IdLiquidationDetail = Id FROM Payroll.LiquidationDetail where PayrollId = @IdLiquidation and ConceptClass = '037' and ConceptType = 3

								IF @IdLiquidationDetail > 0 BEGIN
									UPDATE Payroll.Liquidation SET IBCICBF  = @NewIBC, ICBFContributionValue = @NewProvision, ParafiscalContribution = (FamilyCompensationFundContributionValue + SenaContributionValue + @NewProvision) WHERE Id = @IdLiquidation
									UPDATE Payroll.LiquidationDetail SET ConceptTotalValue = @NewProvision, AccruedValue = @NewProvision where Id = @IdLiquidationDetail
								END ELSE BEGIN
									SELECT 'Con la Cédula digitada ' + @NitEmployee + ' No tiene, en el Detalle de Liquidación, el Concepto ICBF' AS Mensaje, '999' AS CodigoMensaje
									return 9999
								END
							END ELSE IF @ProvisionType = 8 BEGIN
								-- 21/07/2016
								-- Caja de Compensación
								SELECT @IdLiquidationDetail = Id FROM Payroll.LiquidationDetail where PayrollId = @IdLiquidation and ConceptClass = '036' and ConceptType = 3

								IF @IdLiquidationDetail > 0 BEGIN
									UPDATE Payroll.Liquidation SET IBCCompensationFund  = @NewIBC, FamilyCompensationFundContributionValue = @NewProvision, ParafiscalContribution = (ICBFContributionValue + SenaContributionValue + @NewProvision) WHERE Id = @IdLiquidation
									UPDATE Payroll.LiquidationDetail SET ConceptTotalValue = @NewProvision, AccruedValue = @NewProvision where Id = @IdLiquidationDetail
								END ELSE BEGIN
									SELECT 'Con la Cédula digitada ' + @NitEmployee + ' No tiene, en el Detalle de Liquidación, el Concepto CAJA DE COMPENSACION' AS Mensaje, '999' AS CodigoMensaje
									return 9999
								END
							END
					END

				fetch next from C_Fondos into @IdLiquidation, @IdEmployee, @UnemploymentAccumulated, @ProvisionIncentive, @ProvisionVacation, @ProvisionInterestsUnemployment, @IBCVacation, @IBCIncentivePayment, @IBCUnemployment, @IBCSENA, @IBCICBF, @IBCCompensationFund 
				end -- fin while de C_Fondos
			close C_Fondos 
			deallocate C_Fondos
			
			commit transaction
			set @Transaccion = 0 ;

			IF @Status = 1 BEGIN
				SELECT 'SE REALIZÓ LA ACTUALIZACIÓN CORRECTAMENTE' AS Mensaje, '000' AS CodigoMensaje
			END

			IF @Status = 0 BEGIN
				SELECT *FROM @ProvisionReturn
			END

	END TRY
	BEGIN CATCH
		
		IF @Transaccion = 1 begin
			rollback transaction ;
			set @Transaccion = 0 ;
		END
		SELECT ERROR_MESSAGE() AS Mensaje, '999' AS CodigoMensaje
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que actualiza las provisiones laborales de empleados confirmados para un grupo y fecha de nómina específicos. Permite recalcular y registrar ocho tipos de provisiones: primas de servicios, cesantías, intereses sobre cesantías, vacaciones, ARL, SENA, ICBF y caja de compensación familiar. Opera sobre las liquidaciones confirmadas (RegisterStatus = ''C'') de la tabla Payroll.Liquidation, cruzando con el maestro de empleados (Payroll.Employee) y terceros (Common.ThirdParty) para identificar al trabajador por cédula (NitEmployee). Puede ejecutarse para un empleado específico (filtrado por NIT/cédula) o para todos los empleados del grupo, y retorna un detalle comparativo entre los valores anteriores y los nuevos valores calculados de IBC y provisión por empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'UpdateProvision';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'UpdateProvision';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@ProvisionType debe estar entre 1 y 8; Si se especifica @NitEmployee (no vacío), debe existir un empleado en Payroll.Employee con ese Nit en Common.ThirdParty; Debe existir al menos una liquidación confirmada (RegisterStatus=''C'') para el grupo, fecha y empleado indicados; Para tipos 6, 7 y 8 con @Status=1, la liquidación debe tener un registro en Payroll.LiquidationDetail con ConceptType=3 y ConceptClass=''035'' (SENA), ''037'' (ICBF) o ''036'' (Caja de Compensación); El grupo debe estar asociado a un PayrollParameter con los porcentajes de SENA/ICBF/Caja configurados (para tipos 6, 7, 8)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'UpdateProvision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan liquidaciones con RegisterStatus=''C'' (CONFIRMADAS) para la fecha y grupo indicados; Si @Status=0, nunca se persisten cambios: solo se devuelven valores antiguos vs nuevos en una tabla temporal; Las actualizaciones se realizan dentro de una transacción explícita; ante error se hace rollback si la transacción está activa; El factor de provisión es fijo: 1/12 para primas y cesantías, 0.01 para intereses de cesantías, 0.0417 para vacaciones; ARL usa el porcentaje de riesgo profesional del empleado (ProfessionalRiskPercentage), mientras SENA/ICBF/Caja usan porcentajes definidos en PayrollParameter del grupo; Para tipos 6, 7 y 8, ParafiscalContribution se recalcula como la suma de las tres contribuciones parafiscales (FamilyCompensationFund + ICBF + SENA) reemplazando la del tipo actualizado; Para SENA/ICBF/Caja, la actualización exige que exista un LiquidationDetail con ConceptType=3 y ConceptClass específica (''035'' SENA, ''037'' ICBF, ''036'' Caja); ProvisionType solo admite valores 1..8', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'UpdateProvision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Provisión de nómina; Primas; Cesantías; Intereses de cesantías; Vacaciones; ARL (Riesgos Profesionales); SENA; ICBF; Caja de Compensación; Aportes parafiscales; IBC (Ingreso Base de Cotización); Liquidación de nómina confirmada; Grupo de nómina; Parámetros de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'UpdateProvision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ProvisionType > 8 → Devuelve mensaje ''TIPO DE PROVISION ERRÓNEA'' con código ''999'' y retorna 9999; si @NitEmployee = '''' → Se reemplaza por ''%%'' para procesar todos los empleados del grupo; si No existen empleados con la cédula indicada en Payroll.Employee/Common.ThirdParty → Devuelve mensaje ''NO EXISTE'' con código ''999'' y retorna 9999; si @NitEmployee <> ''%%'' y no hay liquidaciones confirmadas (RegisterStatus=''C'') para esa cédula, grupo y fecha → Devuelve mensaje ''NO EXISTEN DATOS DE ESE EMPLEADO'' con código ''999'' y retorna 9999; si @ProvisionType = 1 (Primas) → Calcula NewIBC = SUM(LD.ConceptTotalValue) sobre conceptos con AffectIBCIncentivePayment=1 y NewProvision = NewIBC * (1/12); si @ProvisionType = 2 (Cesantías) → NewIBC = SUM(ConceptTotalValue) de conceptos con AffectIBCSeverance=1; NewProvision = NewIBC * (1/12); si @ProvisionType = 3 (Intereses de Cesantías) → NewIBC = SUM(ConceptTotalValue) con AffectIBCSeverance=1; NewProvision = NewIBC * 0.01; si @ProvisionType = 4 (Vacaciones) → NewIBC = SUM(ConceptTotalValue) con AffectIBCVacation=1; NewProvision = NewIBC * 0.0417; si @ProvisionType = 5 (ARL) → NewIBC = SUM(ConceptTotalValue) con AffectIBCARP=1; NewProvision = NewIBC * (Employee.ProfessionalRiskPercentage/100); si @ProvisionType = 6 (SENA) → Toma SenaContributionPercentage de PayrollParameter del Group; NewIBC = SUM con AffectIBCSENA=1; NewProvision = NewIBC * (porcentaje/100); si @ProvisionType = 7 (ICBF) → Toma ICBFContributionPercentage de PayrollParameter del Group; NewIBC = SUM con AffectIBCICBF=1; NewProvision = NewIBC * (porcentaje/100); si @ProvisionType = 8 (Caja de Compensación) → Toma CompensationFundContributionPercentage de PayrollParameter del Group; NewIBC = SUM con AffectIBCCompensationFund=1; NewProvision = NewIBC * (porcentaje/100); si @Status = 1 (Confirmar) → Aplica UPDATE sobre Payroll.Liquidation (y Payroll.LiquidationDetail para tipos 6,7,8) según el tipo de provisión else Solo retorna en una tabla los valores antiguos vs nuevos calculados (sin persistir cambios); si @Status=1 y @ProvisionType ∈ {6,7,8} y no existe el detalle del concepto correspondiente (ConceptClass ''035''/''037''/''036'' con ConceptType=3) → Devuelve mensaje ''No tiene, en el Detalle de Liquidación, el Concepto SENA/ICBF/CAJA DE COMPENSACION'' con código ''999'' y retorna 9999', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'UpdateProvision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Employee; Common.ThirdParty; Payroll.Liquidation; Payroll.LiquidationDetail; Payroll.Concept; Payroll.Group; Payroll.PayrollParameter', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'UpdateProvision';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'UpdateProvision';
-- GO
