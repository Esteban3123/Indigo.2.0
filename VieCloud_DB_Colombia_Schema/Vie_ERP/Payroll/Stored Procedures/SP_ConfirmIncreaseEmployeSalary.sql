-- =============================================
-- Author:		Andrés Steven Rojas 
-- Create date: 12/11/2023
-- Modified:    29/01/2026 - Optimizacion de rendimiento
-- Description:	Store Procedure que Confirma el Aumento de Salario de los Empleados
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ConfirmIncreaseEmployeSalary]
	@IncreaseEmployeSalary XML,
	@UserCode VARCHAR(20)
AS 
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @StatusField VARCHAR(3),
			@MessageField VARCHAR(MAX)
	
	-- Tabla temporal final con fechas ya convertidas
	DECLARE @IncreaseSalaryData TABLE
	(
		Id INT,
		BasicSalary DECIMAL(18, 2),
		ContractId INT,
		FunctionalUnidCode VARCHAR(20),
		InitialContractNumber VARCHAR(50),
		JobBondingDate DATETIME,
		NewSalary DECIMAL(18, 2),
		NitEmployee VARCHAR(20),
		PercentageIncrease DECIMAL(8, 4),
		PositionCode VARCHAR(20),
		ValueIncrease DECIMAL(18, 2),
		InitialDate DATE,
		ModificationReasonId INT
	)

	BEGIN TRY
		-- Extraer y convertir fechas directamente del XML de forma optimizada
		INSERT INTO @IncreaseSalaryData
		(
			Id, BasicSalary, ContractId, FunctionalUnidCode, InitialContractNumber,
			JobBondingDate, NewSalary, NitEmployee, PercentageIncrease, PositionCode,
			ValueIncrease, InitialDate, ModificationReasonId
		)
		SELECT
			t.x.value('(Id)[1]', 'INT'),
			t.x.value('(BasicSalary)[1]', 'DECIMAL(18, 2)'),
			t.x.value('(ContractId)[1]', 'INT'),
			t.x.value('(FunctionalUnidCode)[1]', 'VARCHAR(20)'),
			t.x.value('(InitialContractNumber)[1]', 'VARCHAR(50)'),
			-- JobBondingDate: Optimizado - solo dos intentos
			CASE 
				WHEN CHARINDEX('/', t.x.value('(JobBondingDate)[1]', 'VARCHAR(20)')) > 0 
				THEN CONVERT(DATETIME, t.x.value('(JobBondingDate)[1]', 'VARCHAR(20)'), 103)  -- dd/MM/yyyy
				ELSE CONVERT(DATETIME, t.x.value('(JobBondingDate)[1]', 'VARCHAR(20)'), 120)  -- yyyy-MM-dd
			END,
			t.x.value('(NewSalary)[1]', 'DECIMAL(18, 2)'),
			t.x.value('(NitEmployee)[1]', 'VARCHAR(20)'),
			t.x.value('(PercentageIncrease)[1]', 'DECIMAL(8, 4)'),
			t.x.value('(PositionCode)[1]', 'VARCHAR(20)'),
			t.x.value('(ValueIncrease)[1]', 'DECIMAL(18, 2)'),
			-- InitialDate: Optimizado - solo dos intentos
			CASE 
				WHEN CHARINDEX('/', t.x.value('(InitialDate)[1]', 'VARCHAR(20)')) > 0 
				THEN CONVERT(DATE, t.x.value('(InitialDate)[1]', 'VARCHAR(20)'), 103)  -- dd/MM/yyyy
				ELSE CONVERT(DATE, t.x.value('(InitialDate)[1]', 'VARCHAR(20)'), 120)  -- yyyy-MM-dd
			END,
			ISNULL(t.x.value('(ModificationReasonId)[1]', 'INT'), 3) -- Default 3 si viene NULL
		FROM @IncreaseEmployeSalary.nodes('/IncreaseEmployeeSalary') t(x)

		-- Validacion rapida de fechas
		IF EXISTS (SELECT 1 FROM @IncreaseSalaryData WHERE InitialDate IS NULL OR JobBondingDate IS NULL)
		BEGIN
			SET @StatusField = '003'
			SET @MessageField = 'Error: Fechas invalidas en el XML. Verifique el formato (dd/MM/yyyy o yyyy-MM-dd)'
			SELECT @StatusField StatusField, @MessageField MessageField
			RETURN
		END

		-- Validar y corregir ModificationReasonId
		UPDATE @IncreaseSalaryData
		SET ModificationReasonId = 3
		WHERE ModificationReasonId = 0 OR ModificationReasonId IS NULL

		-- Recalcular ValueIncrease y PercentageIncrease
		UPDATE @IncreaseSalaryData
		SET ValueIncrease = (NewSalary - BasicSalary),
			PercentageIncrease = ((NewSalary - BasicSalary) / NULLIF(BasicSalary, 0)) * 100
		WHERE ValueIncrease <> (NewSalary - BasicSalary)

		-- Tabla temporal con datos consolidados
		DECLARE @ReturnTable TABLE
		(
			Id INT IDENTITY PRIMARY KEY, 
			ContractId INT, 
			InitialContractNumber INT, 
			NitEmployee VARCHAR(100), 
			EmployeeName VARCHAR(300), 
			JobBondingDate DATE, 
			BasicSalary NUMERIC(18,0), 
			ValueIncrease NUMERIC(18,0), 
			NewSalary NUMERIC(18,0), 
			PercentageIncrease DECIMAL(8,4),
			PositionCode VARCHAR(20), 
			PositionName VARCHAR(200), 
			FunctionalUnitCode VARCHAR(20), 
			FunctionalUnitName VARCHAR(100), 
			InitialDate DATE, 
			ModificationReasonId INT,
			GroupId INT,
			EmployeeId INT,
			PositionId INT,
			FunctionalUnitId INT
		)

		-- Obtener datos consolidados con indices optimizados
		INSERT INTO @ReturnTable
		(
			ContractId, InitialContractNumber, NitEmployee, EmployeeName, JobBondingDate, 
			BasicSalary, ValueIncrease, NewSalary, PercentageIncrease, PositionCode, 
			PositionName, FunctionalUnitCode, FunctionalUnitName, InitialDate, 
			ModificationReasonId, GroupId, EmployeeId, PositionId, FunctionalUnitId
		)
		SELECT 
			C.Id,
			C.InitialContractNumber,
			TP.Nit,
			TP.Name,
			C.JobBondingDate,
			C.BasicSalary,
			ISD.ValueIncrease,
			ISD.NewSalary,
			ISD.PercentageIncrease,
			P.Code,
			P.Name,
			FU.Code,
			FU.Name,
			ISD.InitialDate,
			ISD.ModificationReasonId,
			C.GroupId,
			C.EmployeeId,
			C.PositionId,
			C.FunctionalUnitId
		FROM @IncreaseSalaryData ISD
		INNER JOIN Common.ThirdParty TP ON TP.Nit = ISD.NitEmployee
		INNER JOIN Payroll.Employee E ON E.ThirdPartyId = TP.Id
		INNER JOIN Payroll.Contract C ON C.EmployeeId = E.Id AND C.Valid = 1 AND C.[Status] = 1
		INNER JOIN Payroll.Position P ON P.Id = C.PositionId AND P.Code = ISD.PositionCode
		INNER JOIN Payroll.FunctionalUnit FU ON FU.Id = C.FunctionalUnitId AND FU.Code = ISD.FunctionalUnidCode

		-- Validar que hay registros
		IF NOT EXISTS (SELECT 1 FROM @ReturnTable)
		BEGIN
			SET @StatusField = '001'
			SET @MessageField = 'No se encontraron registros validos para procesar'
			SELECT @StatusField StatusField, @MessageField MessageField
			RETURN
		END

		-- TRANSACCION PRINCIPAL
		BEGIN TRANSACTION

			DECLARE @CurrentDate DATETIME = [Common].[GETDATE]()

			-- 1. Insertar en IncreaseSalary
			INSERT INTO Payroll.IncreaseSalary
			(
				[Status], GroupId, ContractId, ContractInitialNumber, EmployeeId, 
				ModificationReasonId, PercentageIncrease, BasicSalary, NewSalary, 
				PositionId, FunctionalUnitId, CreationUser, CreationDate, 
				ConfirmationUser, ConfirmationDate
			)
			SELECT 
				1, 
				RT.GroupId, 
				RT.ContractId, 
				RT.InitialContractNumber, 
				RT.EmployeeId, 
				RT.ModificationReasonId, 
				RT.PercentageIncrease, 
				RT.BasicSalary,
				RT.NewSalary, 
				RT.PositionId, 
				RT.FunctionalUnitId, 
				@UserCode, 
				@CurrentDate, 
				@UserCode, 
				@CurrentDate
			FROM @ReturnTable RT

			-- 2. Inactivar fondos (batch update optimizado)
			UPDATE FC
			SET FC.EndingDate = DATEADD(DAY, -1, RT.InitialDate), 
				FC.State = 0, 
				FC.ModificationDate = @CurrentDate, 
				FC.ModificationUser = @UserCode 
			FROM Payroll.FundContract FC
			INNER JOIN @ReturnTable RT ON FC.ContractId = RT.ContractId
			WHERE FC.[State] = 1

			-- 3. Crear nuevos contratos (batch insert optimizado)
			INSERT INTO Payroll.[Contract] 
			(
				RowType, InitialContractNumber, ResolutionNumber, ResolutionDate, PosesionDate, 
				CertificateOfficeNumber, EmployeeId, PositionId, FunctionalUnitId, ContractTypeId, 
				JobBondingDate, ContractInitialDate, ContractEndingDate, BasicSalary, [Status], 
				PaymentPeriod, PaymentType, TrialPeriod, TrialPeriodTime, TrialPeriodSalaryPercentage,
				ContractCreationDate, CreationUserId, ModificationDate, TypeOfPensionContribution, 
				Valid, Notes, GroupId, BankId, BankAccountNumber, BankAccountType, LiquidationPayroll, 
				ContractModificationReasonId, BaseIncome, IncomeDailyBase, HoursDaily, LastLiquidationDate, 
				ModificationUserId, LastModificationDate, Contingency, OrganizationChartPositionId
			)
			SELECT 
				2,
				C.InitialContractNumber,
				C.ResolutionNumber,
				C.ResolutionDate,
				C.PosesionDate,
				C.CertificateOfficeNumber,
				C.EmployeeId,
				C.PositionId,
				C.FunctionalUnitId,
				C.ContractTypeId,
				C.JobBondingDate,
				RT.InitialDate,
				C.ContractEndingDate,
				RT.NewSalary,
				1,
				C.PaymentPeriod,
				C.PaymentType,
				C.TrialPeriod,
				C.TrialPeriodTime,
				C.TrialPeriodSalaryPercentage,
				@CurrentDate,
				@UserCode,
				@CurrentDate,
				C.TypeOfPensionContribution,
				1,
				'AUMENTO DE SALARIO DE MANERA MASIVA',
				C.GroupId,
				C.BankId,
				C.BankAccountNumber,
				C.BankAccountType,
				C.LiquidationPayroll,
				RT.ModificationReasonId,
				RT.NewSalary,
				RT.NewSalary / 8,
				C.HoursDaily,
				NULL,
				@UserCode,
				@CurrentDate,
				C.Contingency,
				C.OrganizationChartPositionId
			FROM @ReturnTable RT 
			INNER JOIN Payroll.Contract C ON C.Id = RT.ContractId

			-- 4. Inactivar contratos anteriores (batch update optimizado)
			UPDATE C
			SET C.ContractEndingDate = CASE 
					WHEN C.ContractInitialDate >= RT.InitialDate THEN C.ContractInitialDate 
					ELSE DATEADD(DAY, -1, RT.InitialDate) 
				END, 
				C.Valid = 0, 
				C.[Status] = 4,
				C.ModificationDate = @CurrentDate,
				C.ModificationUserId = @UserCode
			FROM Payroll.[Contract] C
			INNER JOIN @ReturnTable RT ON C.Id = RT.ContractId

			-- 5.  Crear nuevos fondos (batch insert optimizado)
			INSERT INTO Payroll.FundContract
			(
				FundId, ContractId, FundType, InitialDate, MembershipNumber, 
				VoluntaryContribution, VoluntaryContributionValue, [State], 
				CreationUser, CreationDate
			)
			SELECT 
				FC.FundId, CNuevo.Id, FC.FundType, RT.InitialDate, FC.MembershipNumber, 
				FC.VoluntaryContribution, FC.VoluntaryContributionValue, 1, @UserCode, @CurrentDate
			FROM Payroll.FundContract FC
			INNER JOIN @ReturnTable RT ON FC.ContractId = RT.ContractId
			INNER JOIN Payroll.Contract CNuevo ON CNuevo.InitialContractNumber = RT.InitialContractNumber 
				AND CNuevo.[Status] = 1 
				AND CNuevo.Valid = 1
				AND CNuevo.ContractCreationDate = @CurrentDate

			-- 6. Actualizar retroactivos (solo si existen)
			IF EXISTS (SELECT 1 FROM Payroll.RetroactiveC WHERE IdContract IN (SELECT ContractId FROM @ReturnTable) AND Status <> 2)
			BEGIN
				UPDATE Payroll.RetroactiveC 
				SET Status = 2, ConfirmationUserId = @UserCode, ConfirmationDate = @CurrentDate 
				WHERE IdContract IN (SELECT ContractId FROM @ReturnTable) AND Status <> 2
			END

			-- 7. Confirmar registros en IncreaseSalary
			UPDATE Payroll.IncreaseSalary 
			SET Status = 2, ConfirmationUser = @UserCode, ConfirmationDate = @CurrentDate 
			WHERE ContractId IN (SELECT ContractId FROM @ReturnTable) 
				AND CreationDate = @CurrentDate
				AND Status <> 2

		COMMIT TRANSACTION

		SET @StatusField = '002'
		SET @MessageField = 'Se realizo la Actualizacion de Salarios Correctamente' 
		SELECT @StatusField StatusField, @MessageField MessageField

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		SET @StatusField = '999'
		SET @MessageField = ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5))
		SELECT @StatusField StatusField, @MessageField MessageField
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma y registra definitivamente el aumento de salario de uno o varios empleados en el módulo de nómina. Recibe un XML con los datos del incremento (salario actual, nuevo salario, porcentaje de aumento, valor del aumento, cargo, unidad funcional y fecha de vigencia), los valida, recalcula los valores si hay inconsistencias y los persiste en las tablas de contrato y aumento salarial. Es el paso final del flujo de ajuste salarial: consolida la información del empleado, su contrato vigente, el cargo y la unidad funcional, y devuelve un resumen con el nombre del empleado, el cargo, la unidad funcional y los montos del incremento confirmado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmIncreaseEmployeSalary';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmIncreaseEmployeSalary';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma de forma masiva el incremento salarial de empleados: registra el aumento, cierra el contrato y los fondos vigentes, crea un nuevo contrato (Otro Sí) con el nuevo salario y replica los fondos hacia el contrato nuevo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmIncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe tener la estructura /IncreaseEmployeeSalary con los nodos esperados (Id, BasicSalary, ContractId, NitEmployee, NewSalary, InitialDate, etc.).; Cada NitEmployee del XML debe existir en Common.ThirdParty y estar asociado a un Payroll.Employee.; El empleado debe tener un contrato vigente en Payroll.Contract con Valid=1 y Status=1.; El PositionCode y FunctionalUnidCode del XML deben coincidir con los del contrato vigente (Payroll.Position y Payroll.FunctionalUnit).; Debe existir al menos un registro consolidado tras los JOIN; de lo contrario se aborta sin transacción.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmIncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las escrituras (incremento, cierre de contrato/fondos, nuevo contrato, nuevos fondos, confirmación de retroactivos e incremento) ocurren dentro de la misma transacción TT1: o todo o nada.; El contrato nuevo y los fondos nuevos siempre quedan con Status=1/State=1 y Valid=1; el contrato anterior queda con Valid=0 y Status=4.; CreationDate, ModificationDate y ConfirmationDate de los registros creados/actualizados en esta ejecución comparten el mismo @CurrentDate obtenido de Common.GETDATE().; El registro en IncreaseSalary se inserta con Status=1 y posteriormente se actualiza a Status=2 dentro de la misma transacción, garantizando que al finalizar quede confirmado.; El nuevo contrato hereda del anterior la mayoría de atributos (ResolutionNumber, banco, tipo de pago, etc.) y solo cambia salario, fechas, usuarios, motivo y RowType=2.; BaseIncome se iguala a NewSalary y IncomeDailyBase se calcula como NewSalary/8.; Solo se procesan contratos cuyo PositionCode y FunctionalUnidCode coinciden con los enviados en el XML (validación implícita por JOIN).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmIncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Payroll.IncreaseSalary: Por cada empleado consolidado se inserta un registro de incremento con Status=1, CreationUser/ConfirmationUser=@UserCode y fechas = Common.GETDATE().; [UPDATE] Payroll.FundContract: Para los contratos afectados, los fondos con State=1 se inactivan: State=0 y EndingDate = InitialDate - 1 día.; [INSERT] Payroll.Contract: Se crea un nuevo contrato (RowType=2, Status=1, Valid=1) clonando el vigente, con BasicSalary y BaseIncome = NewSalary, IncomeDailyBase = NewSalary/8, ContractInitialDate = InitialDate del XML, Notes=''AUMENTO DE SALARIO DE MANERA MASIVA'' y ContractModificationReasonId = ModificationReasonId.; [UPDATE] Payroll.Contract: El contrato anterior se inactiva: Valid=0, Status=4 y ContractEndingDate = (InitialDate-1) salvo si ContractInitialDate >= InitialDate, en cuyo caso ContractEndingDate = ContractInitialDate.; [INSERT] Payroll.FundContract: Se replican los fondos del contrato anterior hacia el contrato nuevo recién creado (identificado por InitialContractNumber, Status=1, Valid=1 y ContractCreationDate=@CurrentDate), con InitialDate = InitialDate del XML y State=1.; [UPDATE] Payroll.RetroactiveC: Los retroactivos cuyos IdContract están en los contratos procesados y Status<>2 se marcan como confirmados: Status=2, ConfirmationUserId=@UserCode, ConfirmationDate=@CurrentDate.; [UPDATE] Payroll.IncreaseSalary: Los registros recién insertados (CreationDate=@CurrentDate) se confirman: Status=2, ConfirmationUser=@UserCode, ConfirmationDate=@CurrentDate.; [RETURN_RESULT] @ReturnTable: Devuelve StatusField=''002'' y mensaje de éxito al confirmar; ''001'' si no hay registros válidos; ''999'' con ERROR_MESSAGE y línea en caso de excepción (con ROLLBACK de la transacción TT1).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmIncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ModificationReasonId del XML es 0 o NULL → Se fuerza ModificationReasonId = 3 (motivo por defecto).; si ValueIncrease del XML <> (NewSalary - BasicSalary) → Se recalcula ValueIncrease = NewSalary - BasicSalary y PercentageIncrease = ((NewSalary - BasicSalary)/BasicSalary)*100, protegiendo contra división por cero con NULLIF.; si No existen registros en @ReturnTable tras el cruce con ThirdParty/Employee/Contract/Position/FunctionalUnit → Retorna StatusField=''001'' y mensaje ''No se encontraron registros válidos para procesar'' sin abrir transacción. else Procede con la transacción TT1 que ejecuta los 7 pasos de inserción/actualización.; si ContractInitialDate del contrato anterior >= InitialDate del XML → ContractEndingDate del contrato anterior se fija a ContractInitialDate (mismo día). else ContractEndingDate = InitialDate - 1 día.; si Ocurre una excepción dentro del TRY → Si @@TRANCOUNT>0 se hace ROLLBACK de TT1 y se retorna StatusField=''999'' con ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmIncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmIncreaseEmployeSalary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmIncreaseEmployeSalary';
-- GO
