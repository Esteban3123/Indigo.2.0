-- =============================================
-- Author:		Daniel Eduardo Arévalo Bonilla
-- Create date: 14/10/2020
-- Description:	Store Procedure que Aumenta el Salario de los Empleados
-- =============================================
CREATE PROCEDURE [Payroll].[SP_IncreaseEmployeSalary]
	@GroupId INT,
	@FunctionalUnitId INT,
	@PositionId INT,
	@PercentageIncrease DECIMAL (8,4),
	@Confirm BIT,
	@AproxValue INT,
	@ModificationReasonId INT,
	@UserCode VARCHAR(20),
	@InitialDateNewSalary DATE,
    @WithRetroactive INT,
    @RetroactiveInitialDate DATETIME,
    @PayrollPaidRetroactive INT
AS 
BEGIN
	SET NOCOUNT ON;

	declare @Transaccion bit

	BEGIN TRY

		SET @Transaccion = 0 ;

		DECLARE @ReturnTable table(Id int IDENTITY PRIMARY KEY, StatusField varchar(3), MessageField varchar(max), ContractId INT, InitialContractNumber INT, NitEmployee VARCHAR(100), 
				EmployeeName VARCHAR(300), JobBondingDate DATE, BasicSalary NUMERIC(18,0), ValueIncrease NUMERIC(18,0), NewSalary NUMERIC(18,0), PercentageIncrease DECIMAL(8,4),
				PositionCode VARCHAR(20), PositionName VARCHAR(200), FunctionalUnitCode VARCHAR(20), FunctionalUnitName VARCHAR(100), InitialDate VARCHAR(100),
                                WithRetroactive  INT ,RetroactiveInitialDate  date ,PayrollPaidRetroactive  int , ModificationReasonId int )

		DECLARE @FunctionalUnitCode VARCHAR(20)
		DECLARE @PositionCode VARCHAR(20)

		IF @FunctionalUnitId = 0 BEGIN
			SET @FunctionalUnitCode = '%%'
		END ELSE BEGIN
			SELECT @FunctionalUnitCode = Code FROM Payroll.FunctionalUnit where Id = @FunctionalUnitId
		END

		IF @PositionId = 0 BEGIN
			SET @PositionCode = '%%'
		END ELSE BEGIN
			SELECT @PositionCode = Code FROM Payroll.Position where Id = @PositionId
		END

			INSERT INTO @ReturnTable
			SELECT '001' as StatusField, 'Proceso Realizado Correctamente' as MessageField, C.Id as ContractId, C.InitialContractNumber as InitialContractNumber, TP.Nit as NitEmployee, TP.Name as EmployeeName, C.JobBondingDate as JobBondingDate, C.BasicSalary as BasicSalary,
			CASE @AproxValue
				WHEN 10 THEN CEILING((C.BasicSalary * (@PercentageIncrease / 100)) / 10) * 10
				WHEN 100 THEN CEILING((C.BasicSalary * (@PercentageIncrease / 100)) / 100) * 100
				WHEN 1000 THEN CEILING((C.BasicSalary * (@PercentageIncrease / 100)) / 1000) * 1000
				ELSE (C.BasicSalary * (@PercentageIncrease / 100)) END as ValueIncrease,
			CASE @AproxValue
				WHEN 10 THEN CEILING((C.BasicSalary + (C.BasicSalary * (@PercentageIncrease / 100))) / 10) * 10
				WHEN 100 THEN CEILING((C.BasicSalary + (C.BasicSalary * (@PercentageIncrease / 100))) / 100) * 100
				WHEN 1000 THEN CEILING((C.BasicSalary + (C.BasicSalary * (@PercentageIncrease / 100))) / 1000) * 1000
				ELSE C.BasicSalary + (C.BasicSalary * (@PercentageIncrease / 100)) END as NewSalary,
			@PercentageIncrease as PercentageIncrease, P.Code as PositionCode, P.Name as PositionName, FU.Code as FunctionalUnitCode, FU.Name as FunctionalUnitName,
            @InitialDateNewSalary,
            @WithRetroactive,
            @RetroactiveInitialDate,
            @PayrollPaidRetroactive,
            @ModificationReasonId
			FROM Payroll.Contract C 
			INNER JOIN Payroll.Position P ON P.Id = C.PositionId
			INNER JOIN Payroll.FunctionalUnit FU ON FU.Id = C.FunctionalUnitId
			INNER JOIN Payroll.Employee E ON E.Id = C.EmployeeId
			INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
			WHERE P.Code like @PositionCode AND FU.Code like @FunctionalUnitCode
			AND C.Valid = 1 AND C.[Status] = 1 AND GroupId = @GroupId
			ORDER BY TP.Name

			

		IF (SELECT COUNT (*) FROM @ReturnTable) = 0 BEGIN
			INSERT INTO @ReturnTable
			SELECT '888' as StatusField, 'No existen datos con esos parámetros' as MessageField, NULL as ContractId, NULL as InitialContractNumber, '' as NitEmployee, '' as EmployeeName, [Common].[GETDATE]() as JobBondingDate, 0 as BasicSalary, 0 as ValueIncrease, 0 as NewSalary, 0 as PercentageIncrease, '' as PositionCode, '' as PositionName, '' as FunctionalUnitCode, '' as FunctionalUnitName,  '' as InitialDateNewSalary , '' as  WithRetroactive , '' as RetroactiveInitialDate,  '' as  PayrollPaidRetroactive  ,0 as ModificationReasonId

			SELECT *FROM @ReturnTable

			RETURN
		END
		
		IF @Confirm = 0 BEGIN

			SELECT *FROM @ReturnTable

		END ELSE BEGIN
			
			begin transaction
				SET @Transaccion = 1
			
			-- Inserto en la Tabla de Incremento de Salarios
			INSERT INTO Payroll.IncreaseSalary
			SELECT 1, C.GroupId, C.Id, C.InitialContractNumber, C.EmployeeId, @ModificationReasonId, @PercentageIncrease, RT.BasicSalary,
			RT.NewSalary, C.PositionId, C.FunctionalUnitId, @UserCode, [Common].[GETDATE](), @UserCode, [Common].[GETDATE]()
			FROM @ReturnTable RT INNER JOIN Payroll.Contract C ON C.Id = RT.ContractId

			-- Primero Inactivo Todos los fondos de los Contratos
			UPDATE Payroll.FundContract SET EndingDate = DATEADD(DAY,-1,@InitialDateNewSalary), State = 0, ModificationDate = [Common].[GETDATE](), ModificationUser = @UserCode WHERE ContractId IN (SELECT ContractId from @ReturnTable)

			-- Creo los Otros Si de los Contratos
			
			INSERT INTO Payroll.[Contract] (RowType, InitialContractNumber, ResolutionNumber, ResolutionDate, PosesionDate, CertificateOfficeNumber, EmployeeId, PositionId, FunctionalUnitId,
			ContractTypeId, JobBondingDate, ContractInitialDate, ContractEndingDate, BasicSalary, [Status], PaymentPeriod, PaymentType, TrialPeriod, TrialPeriodTime, TrialPeriodSalaryPercentage,
			ContractCreationDate, CreationUserId, ModificationDate, TypeOfPensionContribution, Valid, Notes, GroupId, BankId, BankAccountNumber, BankAccountType, LiquidationPayroll, ContractModificationReasonId,
			BaseIncome, IncomeDailyBase, HoursDaily, LastLiquidationDate, ModificationUserId, LastModificationDate, Contingency, OrganizationChartPositionId)
			SELECT 2, C.InitialContractNumber, ResolutionNumber, ResolutionDate, PosesionDate, CertificateOfficeNumber, EmployeeId, PositionId, FunctionalUnitId,
			ContractTypeId, C.JobBondingDate, @InitialDateNewSalary, ContractEndingDate, RT.NewSalary, 1, PaymentPeriod, PaymentType, TrialPeriod, TrialPeriodTime, TrialPeriodSalaryPercentage,
			[Common].[GETDATE](), @UserCode,[Common].[GETDATE](), TypeOfPensionContribution, 1, 'AUMENTO DE SALARIO DE MANERA MASIVA', C.GroupId, BankId, BankAccountNumber, BankAccountType, LiquidationPayroll, @ModificationReasonId,
			RT.NewSalary, RT.NewSalary / 8, HoursDaily, NULL, '999', [Common].[GETDATE](), Contingency, OrganizationChartPositionId FROM @ReturnTable RT INNER JOIN Payroll.Contract C ON C.Id = RT.ContractId

			-- Inactivo los Contratos Anteriores
			UPDATE Payroll.[Contract] SET ContractEndingDate = IIF(ContractInitialDate >= @InitialDateNewSalary, ContractInitialDate, DATEADD(DAY,-1,@InitialDateNewSalary)), Valid = 0, [Status] = 4 WHERE ID IN (SELECT ContractId FROM @ReturnTable)

			-- Creo los Nuevos Fondos de los Empleados
			INSERT INTO Payroll.FundContract(FundId, ContractId, FundType, InitialDate, MembershipNumber, VoluntaryContribution, VoluntaryContributionValue, [State], CreationUser, CreationDate)
			SELECT FC.FundId, C.Id, FC.FundType, @InitialDateNewSalary, FC.MembershipNumber, FC.VoluntaryContribution, FC.VoluntaryContributionValue, 1, @UserCode, [Common].[GETDATE]() 
			FROM Payroll.FundContract FC, @ReturnTable RT, Payroll.Contract C
			where FC.ContractId = RT.ContractId AND RT.InitialContractNumber = C.InitialContractNumber AND C.Status = 1 AND C.Valid = 1
			
			-- Actualizo el estado, usuario y fecha de confirmación de la cabecera de Retroactivos 
			UPDATE Payroll.RetroactiveC SET Status = 2, ConfirmationUserId = @UserCode, ConfirmationDate = [Common].[GETDATE]() WHERE IdContract IN (SELECT ContractId FROM @ReturnTable)

			-- Actualizo el estado, usuario y fecha de confirmación de la tabla Incremento de Salarios 
			UPDATE Payroll.IncreaseSalary SET Status = 2, ConfirmationUser = @UserCode, ConfirmationDate = [Common].[GETDATE]() WHERE ContractId IN (SELECT ContractId FROM @ReturnTable)

			INSERT INTO @ReturnTable
			SELECT '002' as StatusField, 'Se realizó la Actualización de Salarios Correctamente' as MessageField, NULL AS ContractId, NULL as InitialContractNumber, '' as NitEmployee, '' as EmployeeName, '' as JobBondingDate, NULL as BasicSalary, NULL as ValueIncrease,NULL as NewSalary, NULL as PercentageIncrease, '' as PositionCode, '' as PositionName, '' as FunctionalUnitCode, '' as FunctionalUnitName , '' as InitialDateNewSalary,'' as  WithRetroactive , '' as RetroactiveInitialDate,  '' as  PayrollPaidRetroactive ,0 as ModificationReasonId

			SELECT *FROM @ReturnTable

			commit transaction
			set @Transaccion = 0 ;

		END

	

	END TRY
	BEGIN CATCH
		IF @Transaccion = 1 begin
			rollback transaction ;
			set @Transaccion = 0 ;
		END
		INSERT INTO @ReturnTable
		SELECT '999' as StatusField, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(5)) as MessageField, NULL as ContractId, NULL as InitialContractNumber, '' as NitEmployee, '' as EmployeeName, [Common].[GETDATE]() as JobBondingDate, 0 as BasicSalary, 0 as ValueIncrease, 0 as NewSalary, 0 as PercentageIncrease,'' as PositionCode, '' as PositionName, '' as FunctionalUnitCode, '' as FunctionalUnitName, '' as InitialDateNewSalary , '' as  WithRetroactive , '' as RetroactiveInitialDate,  '' as  PayrollPaidRetroactive ,0 as ModificationReasonId
		SELECT *FROM @ReturnTable
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que aplica un incremento salarial masivo a los empleados activos de nómina, filtrando por grupo, unidad funcional y cargo. Calcula el nuevo salario sumando el porcentaje de aumento al salario básico actual, con opción de redondeo al múltiplo de 10, 100 o 1.000. En modo de previsualización muestra los empleados afectados con el valor del aumento y el nuevo salario propuesto; al confirmar, registra el incremento en el historial, versiona los contratos laborales creando un nuevo registro con el salario actualizado, inactiva los fondos anteriores y opcionalmente genera el pago retroactivo correspondiente. Toca las entidades de contratos, empleados, cargos, unidades funcionales y terceros (para obtener cédula y nombre del trabajador).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_IncreaseEmployeSalary';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_IncreaseEmployeSalary';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Simula o aplica un incremento salarial masivo a empleados filtrados por grupo, unidad funcional y cargo, generando nuevas versiones de contrato y reemplazando los fondos asociados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_IncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los contratos a afectar deben tener Valid = 1 y Status = 1 y pertenecer al GroupId indicado; Si FunctionalUnitId = 0 o PositionId = 0, no se filtra por dichos criterios (se usa comodín ''%%''); Debe existir al menos un contrato que cumpla los filtros; de lo contrario se retorna estado ''888'' sin aplicar cambios; El parámetro @AproxValue debe ser 10, 100 o 1000 para aplicar redondeo CEILING; cualquier otro valor deja el cálculo sin redondeo', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_IncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se aplican cambios si @Confirm = 0; ese flujo es de simulación; El nuevo salario se calcula como BasicSalary + BasicSalary * (@PercentageIncrease/100), opcionalmente redondeado al alza; Los contratos originales quedan inactivos (Valid=0, Status=4) en el mismo paso en que se crean los contratos versionados (RowType=2); Cada contrato versionado conserva el mismo InitialContractNumber del original, garantizando trazabilidad de la cadena contractual; IncomeDailyBase del nuevo contrato siempre es NewSalary/8; Los fondos anteriores siempre se cierran con EndingDate un día antes de @InitialDateNewSalary y se replican vigentes a partir de esa fecha; ModificationUserId del contrato anterior se fija en ''999'' al inactivarlo', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_IncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'incremento salarial masivo; versionado de contrato laboral (Otro Sí); salario básico y nuevo salario; fondos de empleado (cesantías, pensión, caja); retroactivo de nómina; motivo de modificación contractual; redondeo de salario por aproximación; unidad funcional y cargo; tercero (NIT y nombre del empleado)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_IncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Payroll.IncreaseSalary: Cuando @Confirm = 1, por cada contrato del resultado se inserta un registro de incremento con RowType=1, salario base anterior, nuevo salario, porcentaje y motivo de modificación; [UPDATE] Payroll.FundContract: Cuando @Confirm = 1, todos los fondos de los contratos afectados se inactivan (State=0) y se les asigna EndingDate = @InitialDateNewSalary - 1 día; [INSERT] Payroll.Contract: Cuando @Confirm = 1, se crea un nuevo contrato (RowType=2) por cada contrato afectado con ContractInitialDate = @InitialDateNewSalary, BasicSalary = NewSalary calculado, IncomeDailyBase = NewSalary/8, Status=1, Valid=1 y Notes=''AUMENTO DE SALARIO DE MANERA MASIVA''; [UPDATE] Payroll.Contract: Cuando @Confirm = 1, los contratos originales se inactivan: Valid=0, Status=4 y ContractEndingDate = (ContractInitialDate si es ≥ @InitialDateNewSalary, en caso contrario @InitialDateNewSalary - 1 día); [INSERT] Payroll.FundContract: Cuando @Confirm = 1, por cada fondo previo se crea uno nuevo asociado al nuevo contrato (mismo InitialContractNumber con Status=1 y Valid=1) con InitialDate = @InitialDateNewSalary y State=1; [UPDATE] Payroll.RetroactiveC: Cuando @Confirm = 1, las cabeceras de retroactivo de los contratos afectados pasan a Status=2 y se registra usuario y fecha de confirmación; [UPDATE] Payroll.IncreaseSalary: Cuando @Confirm = 1, los registros recién insertados pasan a Status=2 con usuario y fecha de confirmación; [RETURN_RESULT] @ReturnTable: Devuelve siempre un resultset con StatusField: ''001'' (preview correcto), ''002'' (confirmación aplicada), ''888'' (sin datos) o ''999'' (error capturado con ERROR_MESSAGE y línea)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_IncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @FunctionalUnitId = 0 → Se usa el patrón ''%%'' para no filtrar por unidad funcional else Se obtiene el Code de Payroll.FunctionalUnit por Id para filtrar; si @PositionId = 0 → Se usa el patrón ''%%'' para no filtrar por cargo else Se obtiene el Code de Payroll.Position por Id para filtrar; si @AproxValue IN (10, 100, 1000) → El incremento y nuevo salario se redondean hacia arriba (CEILING) al múltiplo de 10, 100 o 1000 respectivamente else Se usa el valor exacto sin redondeo; si COUNT(@ReturnTable) = 0 tras el SELECT inicial → Se retorna fila con StatusField=''888'' y se termina el procedimiento sin aplicar cambios; si @Confirm = 0 → Solo devuelve la previsualización del cálculo sin modificar datos else Abre transacción y ejecuta toda la cadena de inserciones/actualizaciones; commit al finalizar; si Error capturado en TRY/CATCH con @Transaccion = 1 → Se hace ROLLBACK de la transacción y se retorna fila con StatusField=''999'' y mensaje de error con número de línea', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_IncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.FunctionalUnit; Payroll.Position; Payroll.Contract; Payroll.Employee; Common.ThirdParty; Payroll.FundContract', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_IncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_IncreaseEmployeSalary';
-- GO
