-- =============================================
-- Author:		Andrés Steven Rojas 
-- Create date: 12/11/2023
-- Description:	Store Procedure que Confirma el Aumento de Salario de los Empleados
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ConfirmIncreaseEmployeSalary]
	@IncreaseEmployeSalary Xml,
	@UserCode VARCHAR(20),
	@PercentageIncrease DECIMAL (8,4), 
	@ModificationReasonId INT,
	@InitialDateNewSalary DATE
AS 
BEGIN
SET NOCOUNT ON;
	
	declare @StatusField as varchar(3),
			@MessageField as varchar(max)
	
	--CODIGO PARA EXTRAER VARIABLES DEL XML
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
        PercentageIncrease DECIMAL(5, 2),
        PositionCode VARCHAR(20),
        ValueIncrease DECIMAL(18, 2)
    )

	BEGIN TRY

    -- Insertar datos del XML en la tabla temporal
    INSERT INTO @IncreaseSalaryData
    (
        Id,
        BasicSalary,
        ContractId,
        FunctionalUnidCode,
        InitialContractNumber,
        JobBondingDate,
        NewSalary,
        NitEmployee,
        PercentageIncrease,
        PositionCode,
        ValueIncrease
    )
    SELECT
        t.x.value('(Id)[1]', 'INT'),
        t.x.value('(BasicSalary)[1]', 'DECIMAL(18, 2)'),
        t.x.value('(ContractId)[1]', 'INT'),
        t.x.value('(FunctionalUnidCode)[1]', 'VARCHAR(20)'),
        t.x.value('(InitialContractNumber)[1]', 'VARCHAR(50)'),
        TRY_PARSE( t.x.value('(JobBondingDate)[1]', 'VARCHAR(MAX)') AS DATETIME),
        t.x.value('(NewSalary)[1]', 'DECIMAL(18, 2)'),
        t.x.value('(NitEmployee)[1]', 'VARCHAR(20)'),
        t.x.value('(PercentageIncrease)[1]', 'DECIMAL(5, 2)'),
        t.x.value('(PositionCode)[1]', 'VARCHAR(20)'),
        t.x.value('(ValueIncrease)[1]', 'DECIMAL(18, 2)')
    FROM @IncreaseEmployeSalary.nodes('/IncreaseEmployeeSalary') t(x)
	--

	DECLARE @Transaccion AS BIT
	SET @Transaccion = 0 ;

		DECLARE @ReturnTable table(Id int IDENTITY PRIMARY KEY, ContractId INT, InitialContractNumber INT, NitEmployee VARCHAR(100), 
				EmployeeName VARCHAR(300), JobBondingDate DATE, BasicSalary NUMERIC(18,0), ValueIncrease NUMERIC(18,0), NewSalary NUMERIC(18,0), PercentageIncrease DECIMAL(8,4),
				PositionCode VARCHAR(20), PositionName VARCHAR(200), FunctionalUnitCode VARCHAR(20), FunctionalUnitName VARCHAR(100))

		DECLARE @FunctionalUnitCode VARCHAR(20)
		DECLARE @PositionCode VARCHAR(20)

		-- Validando el ModificationReason 
		IF @ModificationReasonId = 0 OR	@ModificationReasonId IS NULL
		BEGIN 
			SELECT @ModificationReasonId = 3 --Si viene vacío se pone por defecto Cambio de Salario
		END
		--

			-- Validando si se modificaron valores desde la rejilla

			-- Se inicia el Cursor
			DECLARE @Id INT

			BEGIN TRAN t123
			BEGIN TRY
 
				DECLARE intera1 CURSOR SCROLL

					FOR SELECT Id FROM @IncreaseSalaryData WHERE ValueIncrease <> (NewSalary - BasicSalary)

				OPEN intera1

					fetch next from intera1 into @Id
					while (@@FETCH_STATUS = 0)
					begin
						--logica
						
						UPDATE @IncreaseSalaryData
						SET ValueIncrease = (NewSalary - BasicSalary) 
						WHERE Id = @Id

						UPDATE @IncreaseSalaryData
						SET PercentageIncrease = (ValueIncrease / BasicSalary) * 100
						WHERE Id = @Id

						fetch next from intera1 into @Id
					end			
				CLOSE intera1
				DEALLOCATE intera1
	
	
 
			COMMIT TRAN t123
 
			END TRY
			BEGIN CATCH
				ROLLBACK TRAN t123
				set @StatusField = '999'
				set @MessageField = ERROR_MESSAGE()

				SELECT @StatusField,@MessageField
			END CATCH
			-- Fin Cursor


			INSERT INTO @ReturnTable
			SELECT C.Id as ContractId, C.InitialContractNumber as InitialContractNumber, TP.Nit as NitEmployee, TP.Name as EmployeeName, C.JobBondingDate as JobBondingDate, C.BasicSalary as BasicSalary,
			ISD.ValueIncrease as ValueIncrease, ISD.NewSalary as NewSalary,
			ISD.PercentageIncrease as PercentageIncrease, P.Code as PositionCode, P.Name as PositionName, FU.Code as FunctionalUnitCode, FU.Name as FunctionalUnitName
			FROM Payroll.Contract C 
			INNER JOIN Payroll.Position P ON P.Id = C.PositionId
			INNER JOIN Payroll.FunctionalUnit FU ON FU.Id = C.FunctionalUnitId
			INNER JOIN Payroll.Employee E ON E.Id = C.EmployeeId
			INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
			INNER JOIN @IncreaseSalaryData ISD ON ISD.FunctionalUnidCode = FU.Code
			WHERE P.Code = ISD.PositionCode AND TP.Nit = ISD.NitEmployee
			AND C.Valid = 1 AND C.[Status] = 1 
			ORDER BY TP.Name


			-- ACTUALIZACIÓN DE TABLAS IMPLICADAS
			BEGIN TRANSACTION TT1
				SET @Transaccion = 1

				-- Inserto en la Tabla de Incremento de Salarios
				INSERT INTO Payroll.IncreaseSalary
				SELECT 1, C.GroupId, C.Id, C.InitialContractNumber, C.EmployeeId, @ModificationReasonId, RT.PercentageIncrease, RT.BasicSalary,
				RT.NewSalary, C.PositionId, C.FunctionalUnitId, @UserCode, [Common].[GETDATE](), @UserCode, [Common].[GETDATE]()
				FROM @ReturnTable RT INNER JOIN Payroll.Contract C ON C.Id = RT.ContractId

				-- Primero Inactivo Todos los fondos de los Contratos
				UPDATE Payroll.FundContract SET EndingDate = DATEADD(DAY,-1,@InitialDateNewSalary), State = 0, ModificationDate = [Common].[GETDATE](), ModificationUser = @UserCode 
				WHERE ContractId IN (SELECT ContractId from @ReturnTable)

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
				UPDATE Payroll.[Contract] SET ContractEndingDate = IIF(ContractInitialDate >= @InitialDateNewSalary, ContractInitialDate, DATEADD(DAY,-1,@InitialDateNewSalary)), Valid = 0, [Status] = 4 
				WHERE ID IN (SELECT ContractId FROM @ReturnTable)

				-- Creo los Nuevos Fondos de los Empleados
				INSERT INTO Payroll.FundContract(FundId, ContractId, FundType, InitialDate, MembershipNumber, VoluntaryContribution, VoluntaryContributionValue, [State], CreationUser, CreationDate)
				SELECT FC.FundId, C.Id, FC.FundType, @InitialDateNewSalary, FC.MembershipNumber, FC.VoluntaryContribution, FC.VoluntaryContributionValue, 1, @UserCode, [Common].[GETDATE]() 
				FROM Payroll.FundContract FC, @ReturnTable RT, Payroll.Contract C
				where FC.ContractId = RT.ContractId AND RT.InitialContractNumber = C.InitialContractNumber AND C.Status = 1 AND C.Valid = 1

				-- Actualizo el estado, usuario y fecha de confirmación de la cabecera de Retroactivos 
				UPDATE Payroll.RetroactiveC SET Status = 2, ConfirmationUserId = @UserCode, ConfirmationDate = [Common].[GETDATE]() WHERE IdContract IN (SELECT ContractId FROM @ReturnTable)

				-- Actualizo el estado, usuario y fecha de confirmación de la tabla Incremento de Salarios 
				UPDATE Payroll.IncreaseSalary SET Status = 2, ConfirmationUser = @UserCode, ConfirmationDate = [Common].[GETDATE]() WHERE ContractId IN (SELECT ContractId FROM @ReturnTable)
				

			COMMIT TRANSACTION TT1
				SET @Transaccion = 0

			
				   
			-- DATOS A RETORNAR DESPUES DE OPERACIONES
			set @StatusField= '002'
			set @MessageField= 'Se realizó la Actualización de Salarios Correctamente' 
			
			--SELECT * FROM @ReturnTable --BORRAR
			SELECT @StatusField StatusField,@MessageField MessageField

			RETURN

	END TRY
	BEGIN CATCH

		IF @Transaccion = 1 begin
			ROLLBACK TRANSACTION TT1;
			set @Transaccion = 0 ;
		END

		set @StatusField= '999'
		set @MessageField = ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(5))

		SELECT @StatusField StatusField,@MessageField MessageField

	END CATCH

END