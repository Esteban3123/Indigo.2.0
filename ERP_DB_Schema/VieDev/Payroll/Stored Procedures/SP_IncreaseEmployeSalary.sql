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
	@InitialDateNewSalary DATE
AS 
BEGIN
	SET NOCOUNT ON;

	declare @Transaccion bit

	BEGIN TRY

		SET @Transaccion = 0 ;

		DECLARE @ReturnTable table(Id int IDENTITY PRIMARY KEY, StatusField varchar(3), MessageField varchar(max), ContractId INT, InitialContractNumber INT, NitEmployee VARCHAR(100), 
				EmployeeName VARCHAR(300), JobBondingDate DATE, BasicSalary NUMERIC(18,0), ValueIncrease NUMERIC(18,0), NewSalary NUMERIC(18,0), PercentageIncrease DECIMAL(8,4),
				PositionCode VARCHAR(20), PositionName VARCHAR(200), FunctionalUnitCode VARCHAR(20), FunctionalUnitName VARCHAR(100))

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
			@PercentageIncrease as PercentageIncrease, P.Code as PositionCode, P.Name as PositionName, FU.Code as FunctionalUnitCode, FU.Name as FunctionalUnitName
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
			SELECT '888' as StatusField, 'No existen datos con esos parámetros' as MessageField, NULL as ContractId, NULL as InitialContractNumber, '' as NitEmployee, '' as EmployeeName, [Common].[GETDATE]() as JobBondingDate, 0 as BasicSalary, 0 as ValueIncrease, 0 as NewSalary, 0 as PercentageIncrease, '' as PositionCode, '' as PositionName, '' as FunctionalUnitCode, '' as FunctionalUnitName

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
			ContractTypeId, C.JobBondingDate, IIF(C.ContractInitialDate >= @InitialDateNewSalary,C.ContractInitialDate,@InitialDateNewSalary), ContractEndingDate, RT.NewSalary, 1, PaymentPeriod, PaymentType, TrialPeriod, TrialPeriodTime, TrialPeriodSalaryPercentage,
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
			SELECT '002' as StatusField, 'Se realizó la Actualización de Salarios Correctamente' as MessageField, NULL AS ContractId, NULL as InitialContractNumber, '' as NitEmployee, '' as EmployeeName, '' as JobBondingDate, NULL as BasicSalary, NULL as ValueIncrease,NULL as NewSalary, NULL as PercentageIncrease, '' as PositionCode, '' as PositionName, '' as FunctionalUnitCode, '' as FunctionalUnitName

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
		SELECT '999' as StatusField, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(5)) as MessageField, NULL as ContractId, NULL as InitialContractNumber, '' as NitEmployee, '' as EmployeeName, [Common].[GETDATE]() as JobBondingDate, 0 as BasicSalary, 0 as ValueIncrease, 0 as NewSalary, 0 as PercentageIncrease,'' as PositionCode, '' as PositionName, '' as FunctionalUnitCode, '' as FunctionalUnitName
		SELECT *FROM @ReturnTable
	END CATCH
END