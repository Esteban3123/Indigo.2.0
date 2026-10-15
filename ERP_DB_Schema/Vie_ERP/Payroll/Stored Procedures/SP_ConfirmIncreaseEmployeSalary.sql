-- =============================================
-- Author:		Andres Steven Rojas 
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
			ISNULL(t.x.value('(ModificationReasonId)[1]', 'INT'), 3)
		FROM @IncreaseEmployeSalary.nodes('/IncreaseEmployeeSalary') t(x);

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
			FunctionalUnitId INT,
			OriginalContractEndingDate DATE
		)

		-- Obtener datos consolidados con indices optimizados
		INSERT INTO @ReturnTable
		(
			ContractId, InitialContractNumber, NitEmployee, EmployeeName, JobBondingDate, 
			BasicSalary, ValueIncrease, NewSalary, PercentageIncrease, PositionCode, 
			PositionName, FunctionalUnitCode, FunctionalUnitName, InitialDate, 
			ModificationReasonId, GroupId, EmployeeId, PositionId, FunctionalUnitId,
			OriginalContractEndingDate
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
			C.FunctionalUnitId,
			C.ContractEndingDate
		FROM @IncreaseSalaryData ISD
		INNER JOIN Common.ThirdParty TP WITH (NOLOCK) ON TP.Nit = ISD.NitEmployee
		INNER JOIN Payroll.Employee E WITH (NOLOCK) ON E.ThirdPartyId = TP.Id
		INNER JOIN Payroll.Contract C WITH (NOLOCK) ON C.EmployeeId = E.Id AND C.Valid = 1 AND C.[Status] = 1
		INNER JOIN Payroll.Position P WITH (NOLOCK) ON P.Id = C.PositionId AND P.Code = ISD.PositionCode
		INNER JOIN Payroll.FunctionalUnit FU WITH (NOLOCK) ON FU.Id = C.FunctionalUnitId AND FU.Code = ISD.FunctionalUnidCode

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
			
			-- 3. Inactivar contratos anteriores (batch update optimizado)
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
			INNER JOIN @ReturnTable RT ON C.InitialContractNumber = RT.InitialContractNumber
			WHERE C.ContractEndingDate >= RT.InitialDate

			-- 4. Crear nuevos contratos (batch insert optimizado)
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
				RT.OriginalContractEndingDate, 
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
			
			-- 5. Crear nuevos fondos (batch insert optimizado)
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