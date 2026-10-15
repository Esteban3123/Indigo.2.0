-- =============================================
-- Author:		Iván Ospina
-- Create date: 2019-12-18
-- Description:	
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SaveMassiveContract]
	@pXMLObj XML,
	@pCodeUser VARCHAR(20),
	@pIdUser INT
AS
BEGIN
	set nocount on;
	declare @trancount int;
    set @trancount = @@trancount;
	
	-- Variables locales para resultado
	DECLARE @CodeResult INT = 0;
	DECLARE @MessageResult VARCHAR(MAX) = '';


	BEGIN TRY
	-- Modificar
	if @trancount = 0
	BEGIN TRAN [tran1]

		DECLARE @commonPerson TABLE(
			[CommonPersonPersonID] INT,
			[CommonThirdPartyNit] VARCHAR(20)
		)

		DECLARE @commonThirdParty TABLE(
			[CommonThirdPartyThirdPartyID] INT,
			[CommonThirdPartyNit] VARCHAR(20)
		)

		DECLARE @payrollEmployee TABLE(
			[PayrollEmployeeEmployeeID] INT,
			[CommonThirdPartyThirdPartyID] INT
		)

		DECLARE @payrollContract TABLE(
			[PayrollContractContractID] INT,
			[PayrollEmployeeEmployeeID] INT
		)


		DECLARE @dataFile AS TABLE(
			[CommonThirdPartyNit] VARCHAR(20)
			,[CommonPersonIdentificationType] VARCHAR(5)
			,[CommonIDCityName] VARCHAR(100)
			,[CommonPersonIdentificationExpeditionDate] DATE
			,[CommonPersonMilitaryCardId] varchar(100)
			,[CommonPersonMilitaryCardNumber] VARCHAR(15)
			,[CommonPersonFirstName] VARCHAR(50)
			,[CommonPersonSecondName] VARCHAR(50)
			,[CommonPersonFirstLastName] VARCHAR(50)
			,[CommonPersonSecondLastName] VARCHAR(50)
			,[CommonPersonBirthDate] DATE
			,[CommonBirthCityName] VARCHAR(100)
			,[CommonPersonGender] varchar(100)
			,[CommonPersonBloodGroup] VARCHAR(2)
			,[CommonPersonRH] CHAR(1)
			,[CommonPersonMaritalStatus] varchar(100)
			,[PayrollProfessionalRiskCode] VARCHAR(20)
			,[PayrollEmployeePensionary] BIT
			,[PayrollEmployeeTypeCode] VARCHAR(20)
			,[PayrollCostCenterCode] VARCHAR(20)
			,[PayrollWorkCenterCode] CHAR(20)
			,[PayrollPositionCode] VARCHAR(20)
			,[PayrollFunctionalUnitCode] VARCHAR(20)
			,[PayrollContractTypeCode] VARCHAR(20)
			,[PayrollContractContractInitialDate] DATE
			,[PayrollContractContractEndingDate] DATE
			,[PayrollContractBasicSalary] NUMERIC(18, 0)
			,[PayrollContractPaymentPeriod] TINYINT
			,[PayrollContractPaymentType] TINYINT
			,[PayrollContractTrialPeriod] BIT
			,[PayrollContractTrialPeriodTime] INT
			,[PayrollContractTypeOfPensionContribution] TINYINT
			,[PayrollGroupCode] VARCHAR(20)
			,[PayrollBankCode] VARCHAR(20)
			,[PayrollContractBankAccountNumber] VARCHAR(20)
			,[PayrollContractBankAccountType] INT
			,[PayrollContractHoursDaily] TINYINT
			,[PayrollContractContingency] VARCHAR(100)
			,[HealthCode] VARCHAR(20)
			,[PensionCode] VARCHAR(20)
			,[UnemploymentCode] VARCHAR(20)
			,[OccupationalAccidentInsuranceCode] VARCHAR(20)
			,[FamilyWelfareCode] VARCHAR(20)
			,[EsCambioFechaVinculación] BIT
		)

		

		;WITH cteXML AS(
			SELECT
				t.x.value('CommonThirdPartyNit[1]', 'VARCHAR(100)') AS [CommonThirdPartyNit]
				,t.x.value('CommonPersonIdentificationType[1]', 'VARCHAR(100)') AS [CommonPersonIdentificationType]
				,t.x.value('CommonIDCityName[1]', 'VARCHAR(100)') AS [CommonIDCityName]
				,t.x.value('CommonPersonIdentificationExpeditionDate[1]', 'VARCHAR(100)') AS [CommonPersonIdentificationExpeditionDate]
				,t.x.value('CommonPersonMilitaryCardId[1]', 'VARCHAR(100)') AS [CommonPersonMilitaryCardId]
				,t.x.value('CommonPersonMilitaryCardNumber[1]', 'VARCHAR(100)') AS [CommonPersonMilitaryCardNumber]
				,t.x.value('CommonPersonFirstName[1]', 'VARCHAR(100)') AS [CommonPersonFirstName]
				,t.x.value('CommonPersonSecondName[1]', 'VARCHAR(100)') AS [CommonPersonSecondName]
				,t.x.value('CommonPersonFirstLastName[1]', 'VARCHAR(100)') AS [CommonPersonFirstLastName]
				,t.x.value('CommonPersonSecondLastName[1]', 'VARCHAR(100)') AS [CommonPersonSecondLastName]
				,t.x.value('CommonPersonBirthDate[1]', 'VARCHAR(100)') AS [CommonPersonBirthDate]
				,t.x.value('CommonBirthCityName[1]', 'VARCHAR(100)') AS [CommonBirthCityName]
				,t.x.value('CommonPersonGender[1]', 'VARCHAR(100)') AS [CommonPersonGender]
				,t.x.value('CommonPersonBloodGroup[1]', 'VARCHAR(100)') AS [CommonPersonBloodGroup]
				,t.x.value('CommonPersonMaritalStatus[1]', 'VARCHAR(100)') AS [CommonPersonMaritalStatus]
				,t.x.value('PayrollProfessionalRiskCode[1]', 'VARCHAR(100)') AS [PayrollProfessionalRiskCode]
				,t.x.value('PayrollEmployeePensionary[1]', 'VARCHAR(100)') AS [PayrollEmployeePensionary]
				,t.x.value('PayrollEmployeeTypeCode[1]', 'VARCHAR(100)') AS [PayrollEmployeeTypeCode]
				,t.x.value('PayrollCostCenterCode[1]', 'VARCHAR(100)') AS [PayrollCostCenterCode]
				,t.x.value('PayrollWorkCenterCode[1]', 'VARCHAR(100)') AS [PayrollWorkCenterCode]				
				,t.x.value('PayrollPositionCode[1]', 'VARCHAR(100)') AS [PayrollPositionCode]
				,t.x.value('PayrollFunctionalUnitCode[1]', 'VARCHAR(100)') AS [PayrollFunctionalUnitCode]
				,t.x.value('PayrollContractTypeCode[1]', 'VARCHAR(100)') AS [PayrollContractTypeCode]
				,t.x.value('PayrollContractContractInitialDate[1]', 'VARCHAR(100)') AS [PayrollContractContractInitialDate]
				,t.x.value('PayrollContractContractEndingDate[1]', 'VARCHAR(100)') AS [PayrollContractContractEndingDate]
				,t.x.value('PayrollContractBasicSalary[1]', 'VARCHAR(100)') AS [PayrollContractBasicSalary]
				,t.x.value('PayrollContractPaymentPeriod[1]', 'VARCHAR(100)') AS [PayrollContractPaymentPeriod]
				,t.x.value('PayrollContractPaymentType[1]', 'VARCHAR(100)') AS [PayrollContractPaymentType]
				,t.x.value('PayrollContractTrialPeriod[1]', 'VARCHAR(100)') AS [PayrollContractTrialPeriod]
				,t.x.value('PayrollContractTrialPeriodTime[1]', 'VARCHAR(100)') AS [PayrollContractTrialPeriodTime]
				,t.x.value('PayrollContractTypeOfPensionContribution[1]', 'VARCHAR(100)') AS [PayrollContractTypeOfPensionContribution]
				,t.x.value('PayrollGroupCode[1]', 'VARCHAR(100)') AS [PayrollGroupCode]
				,t.x.value('PayrollBankCode[1]', 'VARCHAR(100)') AS [PayrollBankCode]
				,t.x.value('PayrollContractBankAccountNumber[1]', 'VARCHAR(100)') AS [PayrollContractBankAccountNumber]
				,t.x.value('PayrollContractBankAccountType[1]', 'VARCHAR(100)') AS [PayrollContractBankAccountType]
				,t.x.value('PayrollContractHoursDaily[1]', 'VARCHAR(100)') AS [PayrollContractHoursDaily]
				,t.x.value('PayrollContractContingency[1]', 'VARCHAR(100)') AS [PayrollContractContingency]
				,t.x.value('HealthCode[1]', 'VARCHAR(100)') AS [HealthCode]
				,t.x.value('PensionCode[1]', 'VARCHAR(100)') AS [PensionCode]
				,t.x.value('UnemploymentCode[1]', 'VARCHAR(100)') AS [UnemploymentCode]
				,t.x.value('OccupationalAccidentInsuranceCode[1]', 'VARCHAR(100)') AS [OccupationalAccidentInsuranceCode]
				,t.x.value('FamilyWelfareCode[1]', 'VARCHAR(100)') AS [FamilyWelfareCode]
			FROM @pXMLObj.nodes('/Data/Row') t(x)
		), cteDF AS(
			SELECT
				[CommonThirdPartyNit]
				,[CommonPersonIdentificationType]
				,[CommonIDCityName]
				,CONVERT(DATE, [CommonPersonIdentificationExpeditionDate], 103) AS [CommonPersonIdentificationExpeditionDate]
				,[CommonPersonMilitaryCardId]
				,[CommonPersonMilitaryCardNumber]
				,[CommonPersonFirstName]
				,[CommonPersonSecondName]
				,[CommonPersonFirstLastName]
				,[CommonPersonSecondLastName]
				,CONVERT(DATE, [CommonPersonBirthDate], 103) AS [CommonPersonBirthDate]
				,[CommonBirthCityName]
				,[CommonPersonGender]
				,CASE LEN(REPLACE([CommonPersonBloodGroup], ' ', ''))
						WHEN  3 THEN SUBSTRING([CommonPersonBloodGroup],0,3)
						ELSE SUBSTRING([CommonPersonBloodGroup],0,2) END AS [CommonPersonBloodGroup]
				,CASE LEN(REPLACE([CommonPersonBloodGroup], ' ', ''))
						WHEN  3 THEN SUBSTRING([CommonPersonBloodGroup],3,4)
						ELSE SUBSTRING([CommonPersonBloodGroup],2,3) END AS [CommonPersonRH]
				,[CommonPersonMaritalStatus]
				,[PayrollProfessionalRiskCode]
				,CONVERT(BIT, [PayrollEmployeePensionary]) AS [PayrollEmployeePensionary]
				,[PayrollEmployeeTypeCode]
				,[PayrollCostCenterCode]
				,[PayrollWorkCenterCode]
				,[PayrollPositionCode]
				,[PayrollFunctionalUnitCode]
				,[PayrollContractTypeCode]
				,CONVERT(DATE, [PayrollContractContractInitialDate], 103) AS [PayrollContractContractInitialDate]
				,CONVERT(DATE, [PayrollContractContractEndingDate], 103) AS [PayrollContractContractEndingDate]
				,CONVERT(NUMERIC(18, 0), COALESCE(NULLIF([PayrollContractBasicSalary], ''), 0)) AS [PayrollContractBasicSalary]
				,CONVERT(TINYINT, [PayrollContractPaymentPeriod]) AS [PayrollContractPaymentPeriod]
				,CONVERT(TINYINT, [PayrollContractPaymentType]) AS [PayrollContractPaymentType]
				,CONVERT(BIT, [PayrollContractTrialPeriod]) AS [PayrollContractTrialPeriod]
				,CONVERT(INT, [PayrollContractTrialPeriodTime]) AS [PayrollContractTrialPeriodTime]
				,CONVERT(TINYINT, [PayrollContractTypeOfPensionContribution]) AS [PayrollContractTypeOfPensionContribution]
				,[PayrollGroupCode]
				,[PayrollBankCode]
				,[PayrollContractBankAccountNumber]
				,CONVERT(INT, [PayrollContractBankAccountType]) AS [PayrollContractBankAccountType]
				,CONVERT(TINYINT, [PayrollContractHoursDaily]) AS [PayrollContractHoursDaily]
				,[PayrollContractContingency]
				,[HealthCode]
				,[PensionCode]
				,[UnemploymentCode]
				,[OccupationalAccidentInsuranceCode]
				,[FamilyWelfareCode]
				,ISNULL([contrato].[EsCambioFechaVinculación], 0) AS [EsCambioFechaVinculación]
			FROM cteXML
			OUTER APPLY(
				SELECT
					CASE
						WHEN CONVERT(DATE, cteXML.[PayrollContractContractInitialDate], 103) = cntrc.ContractInitialDate
						THEN 0
						ELSE 1
					END AS [EsCambioFechaVinculación]
				FROM Payroll.[Contract] cntrc
				JOIN Payroll.Employee emply ON emply.Id = cntrc.EmployeeId
				JOIN Common.ThirdParty tPrty ON tPrty.Id = emply.ThirdPartyId
				OUTER APPLY(
					SELECT COUNT(*) AS Quantity
					FROM Payroll.Liquidation lq
					WHERE lq.EmployeeId = emply.Id
				) liquidation
				WHERE tPrty.[Nit] = cteXML.CommonThirdPartyNit
					AND cntrc.[Status] = 1
					AND cntrc.Valid = 1
					AND liquidation.Quantity = 0
			) [contrato]		
		)

		

		INSERT INTO @dataFile(
			[CommonThirdPartyNit]
			,[CommonPersonIdentificationType]
			,[CommonIDCityName]
			,[CommonPersonIdentificationExpeditionDate]
			,[CommonPersonMilitaryCardId]
			,[CommonPersonMilitaryCardNumber]
			,[CommonPersonFirstName]
			,[CommonPersonSecondName]
			,[CommonPersonFirstLastName]
			,[CommonPersonSecondLastName]
			,[CommonPersonBirthDate]
			,[CommonBirthCityName]
			,[CommonPersonGender]
			,[CommonPersonBloodGroup]
			,[CommonPersonRH]
			,[CommonPersonMaritalStatus]
			,[PayrollProfessionalRiskCode]
			,[PayrollEmployeePensionary]
			,[PayrollEmployeeTypeCode]
			,[PayrollCostCenterCode]
			,[PayrollWorkCenterCode]
			,[PayrollPositionCode]
			,[PayrollFunctionalUnitCode]
			,[PayrollContractTypeCode]
			,[PayrollContractContractInitialDate]
			,[PayrollContractContractEndingDate]
			,[PayrollContractBasicSalary]
			,[PayrollContractPaymentPeriod]
			,[PayrollContractPaymentType]
			,[PayrollContractTrialPeriod]
			,[PayrollContractTrialPeriodTime]
			,[PayrollContractTypeOfPensionContribution]
			,[PayrollGroupCode]
			,[PayrollBankCode]
			,[PayrollContractBankAccountNumber]
			,[PayrollContractBankAccountType]
			,[PayrollContractHoursDaily]
			,[PayrollContractContingency]
			,[HealthCode]
			,[PensionCode]
			,[UnemploymentCode]
			,[OccupationalAccidentInsuranceCode]
			,[FamilyWelfareCode]
			,[EsCambioFechaVinculación]
		)
		SELECT
			[CommonThirdPartyNit]
			,[CommonPersonIdentificationType]
			,[CommonIDCityName]
			,[CommonPersonIdentificationExpeditionDate]
			,[CommonPersonMilitaryCardId]
			,[CommonPersonMilitaryCardNumber]
			,[CommonPersonFirstName]
			,[CommonPersonSecondName]
			,[CommonPersonFirstLastName]
			,[CommonPersonSecondLastName]
			,[CommonPersonBirthDate]
			,[CommonBirthCityName]
			,[CommonPersonGender]
			,[CommonPersonBloodGroup]
			,[CommonPersonRH]
			,[CommonPersonMaritalStatus]
			,[PayrollProfessionalRiskCode]
			,[PayrollEmployeePensionary]
			,[PayrollEmployeeTypeCode]
			,[PayrollCostCenterCode]
			,[PayrollWorkCenterCode]
			,[PayrollPositionCode]
			,[PayrollFunctionalUnitCode]
			,[PayrollContractTypeCode]
			,[PayrollContractContractInitialDate]
			,[PayrollContractContractEndingDate]
			,[PayrollContractBasicSalary]
			,[PayrollContractPaymentPeriod]
			,[PayrollContractPaymentType]
			,[PayrollContractTrialPeriod]
			,[PayrollContractTrialPeriodTime]
			,[PayrollContractTypeOfPensionContribution]
			,[PayrollGroupCode]
			,[PayrollBankCode]
			,[PayrollContractBankAccountNumber]
			,[PayrollContractBankAccountType]
			,[PayrollContractHoursDaily]
			,[PayrollContractContingency]
			,[HealthCode]
			,[PensionCode]
			,[UnemploymentCode]
			,[OccupationalAccidentInsuranceCode]
			,[FamilyWelfareCode]
			,[EsCambioFechaVinculación]
		FROM cteDF

		-- =============================================
		-- VALIDACIONES
		-- =============================================
		
		-- Validar duplicados de IdentificationNumber en Common.Person (solo para nuevos registros)
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[EsCambioFechaVinculación] = 0
				AND EXISTS(
					SELECT 1
					FROM Common.Person p
					WHERE p.[IdentificationNumber] = df.[CommonThirdPartyNit]
				)
		)
		BEGIN
			DECLARE @DuplicateIdentificationNumber VARCHAR(20);
			SELECT TOP 1 @DuplicateIdentificationNumber = df.[CommonThirdPartyNit]
			FROM @dataFile df
			WHERE df.[EsCambioFechaVinculación] = 0
				AND EXISTS(
					SELECT 1
					FROM Common.Person p
					WHERE p.[IdentificationNumber] = df.[CommonThirdPartyNit]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'El número de identificación ''' + @DuplicateIdentificationNumber + ''' ya existe en la base de datos';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar duplicados de IdentificationNumber dentro del mismo lote XML (solo para nuevos registros)
		IF EXISTS(
			SELECT [CommonThirdPartyNit], COUNT(*) AS CountRows
			FROM @dataFile
			WHERE [EsCambioFechaVinculación] = 0
			GROUP BY [CommonThirdPartyNit]
			HAVING COUNT(*) > 1
		)
		BEGIN
			DECLARE @DuplicateInBatch VARCHAR(20);
			SELECT TOP 1 @DuplicateInBatch = [CommonThirdPartyNit]
			FROM @dataFile
			WHERE [EsCambioFechaVinculación] = 0
			GROUP BY [CommonThirdPartyNit]
			HAVING COUNT(*) > 1;
			
			SET @CodeResult = 1;
			SET @MessageResult = 'El número de identificación ''' + @DuplicateInBatch + ''' está duplicado en el lote de datos';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar duplicados de Nit en Common.ThirdParty (solo para nuevos registros)
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[EsCambioFechaVinculación] = 0
				AND EXISTS(
					SELECT 1
					FROM Common.ThirdParty tp
					WHERE tp.[Nit] = df.[CommonThirdPartyNit]
				)
		)
		BEGIN
			DECLARE @DuplicateNit VARCHAR(20);
			SELECT TOP 1 @DuplicateNit = df.[CommonThirdPartyNit]
			FROM @dataFile df
			WHERE df.[EsCambioFechaVinculación] = 0
				AND EXISTS(
					SELECT 1
					FROM Common.ThirdParty tp
					WHERE tp.[Nit] = df.[CommonThirdPartyNit]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'El NIT ''' + @DuplicateNit + ''' ya existe en la base de datos';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar existencia de ciudades de identificación
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[EsCambioFechaVinculación] = 0
				AND df.[CommonIDCityName] IS NOT NULL
				AND df.[CommonIDCityName] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Common.City c
					WHERE c.[Name] = df.[CommonIDCityName]
				)
		)
		BEGIN
			DECLARE @InvalidCityName VARCHAR(100);
			SELECT TOP 1 @InvalidCityName = df.[CommonIDCityName]
			FROM @dataFile df
			WHERE df.[EsCambioFechaVinculación] = 0
				AND df.[CommonIDCityName] IS NOT NULL
				AND df.[CommonIDCityName] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Common.City c
					WHERE c.[Name] = df.[CommonIDCityName]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'La ciudad ''' + @InvalidCityName + ''' no existe en la base de datos';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar existencia de ciudades de nacimiento
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[EsCambioFechaVinculación] = 0
				AND df.[CommonBirthCityName] IS NOT NULL
				AND df.[CommonBirthCityName] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Common.City c
					WHERE c.[Name] = df.[CommonBirthCityName]
				)
		)
		BEGIN
			DECLARE @InvalidBirthCityName VARCHAR(100);
			SELECT TOP 1 @InvalidBirthCityName = df.[CommonBirthCityName]
			FROM @dataFile df
			WHERE df.[EsCambioFechaVinculación] = 0
				AND df.[CommonBirthCityName] IS NOT NULL
				AND df.[CommonBirthCityName] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Common.City c
					WHERE c.[Name] = df.[CommonBirthCityName]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'La ciudad ''' + @InvalidBirthCityName + ''' no existe en la base de datos';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar existencia de tipo de empleado
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[PayrollEmployeeTypeCode] IS NOT NULL
				AND df.[PayrollEmployeeTypeCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.EmployeeType et
					WHERE et.[Code] = df.[PayrollEmployeeTypeCode]
				)
		)
		BEGIN
			DECLARE @InvalidEmployeeTypeCode VARCHAR(20);
			SELECT TOP 1 @InvalidEmployeeTypeCode = df.[PayrollEmployeeTypeCode]
			FROM @dataFile df
			WHERE df.[PayrollEmployeeTypeCode] IS NOT NULL
				AND df.[PayrollEmployeeTypeCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.EmployeeType et
					WHERE et.[Code] = df.[PayrollEmployeeTypeCode]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'El tipo de empleado con código ''' + @InvalidEmployeeTypeCode + ''' no existe';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar existencia de centro de costo
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[PayrollCostCenterCode] IS NOT NULL
				AND df.[PayrollCostCenterCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.CostCenter cc
					WHERE cc.[Code] = df.[PayrollCostCenterCode]
				)
		)
		BEGIN
			DECLARE @InvalidCostCenterCode VARCHAR(20);
			SELECT TOP 1 @InvalidCostCenterCode = df.[PayrollCostCenterCode]
			FROM @dataFile df
			WHERE df.[PayrollCostCenterCode] IS NOT NULL
				AND df.[PayrollCostCenterCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.CostCenter cc
					WHERE cc.[Code] = df.[PayrollCostCenterCode]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'El centro de costo con código ''' + @InvalidCostCenterCode + ''' no existe';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar existencia de centro de trabajo
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[PayrollWorkCenterCode] IS NOT NULL
				AND df.[PayrollWorkCenterCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.WorkCenter wc
					WHERE wc.[Code] = df.[PayrollWorkCenterCode]
				)
		)
		BEGIN
			DECLARE @InvalidWorkCenterCode VARCHAR(20);
			SELECT TOP 1 @InvalidWorkCenterCode = df.[PayrollWorkCenterCode]
			FROM @dataFile df
			WHERE df.[PayrollWorkCenterCode] IS NOT NULL
				AND df.[PayrollWorkCenterCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.WorkCenter wc
					WHERE wc.[Code] = df.[PayrollWorkCenterCode]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'El centro de trabajo con código ''' + @InvalidWorkCenterCode + ''' no existe';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar existencia de cargo
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[PayrollPositionCode] IS NOT NULL
				AND df.[PayrollPositionCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.Position p
					WHERE p.[Code] = df.[PayrollPositionCode]
				)
		)
		BEGIN
			DECLARE @InvalidPositionCode VARCHAR(20);
			SELECT TOP 1 @InvalidPositionCode = df.[PayrollPositionCode]
			FROM @dataFile df
			WHERE df.[PayrollPositionCode] IS NOT NULL
				AND df.[PayrollPositionCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.Position p
					WHERE p.[Code] = df.[PayrollPositionCode]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'El cargo con código ''' + @InvalidPositionCode + ''' no existe';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar existencia de unidad funcional
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[PayrollFunctionalUnitCode] IS NOT NULL
				AND df.[PayrollFunctionalUnitCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.FunctionalUnit fu
					WHERE fu.[Code] = df.[PayrollFunctionalUnitCode]
				)
		)
		BEGIN
			DECLARE @InvalidFunctionalUnitCode VARCHAR(20);
			SELECT TOP 1 @InvalidFunctionalUnitCode = df.[PayrollFunctionalUnitCode]
			FROM @dataFile df
			WHERE df.[PayrollFunctionalUnitCode] IS NOT NULL
				AND df.[PayrollFunctionalUnitCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.FunctionalUnit fu
					WHERE fu.[Code] = df.[PayrollFunctionalUnitCode]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'La unidad funcional con código ''' + @InvalidFunctionalUnitCode + ''' no existe';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar existencia de tipo de contrato
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[PayrollContractTypeCode] IS NOT NULL
				AND df.[PayrollContractTypeCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.ContractType ct
					WHERE ct.[Code] = df.[PayrollContractTypeCode]
				)
		)
		BEGIN
			DECLARE @InvalidContractTypeCode VARCHAR(20);
			SELECT TOP 1 @InvalidContractTypeCode = df.[PayrollContractTypeCode]
			FROM @dataFile df
			WHERE df.[PayrollContractTypeCode] IS NOT NULL
				AND df.[PayrollContractTypeCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.ContractType ct
					WHERE ct.[Code] = df.[PayrollContractTypeCode]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'El tipo de contrato con código ''' + @InvalidContractTypeCode + ''' no existe';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar existencia de grupo
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[PayrollGroupCode] IS NOT NULL
				AND df.[PayrollGroupCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.[Group] g
					WHERE g.[Code] = df.[PayrollGroupCode]
				)
		)
		BEGIN
			DECLARE @InvalidGroupCode VARCHAR(20);
			SELECT TOP 1 @InvalidGroupCode = df.[PayrollGroupCode]
			FROM @dataFile df
			WHERE df.[PayrollGroupCode] IS NOT NULL
				AND df.[PayrollGroupCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.[Group] g
					WHERE g.[Code] = df.[PayrollGroupCode]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'El grupo con código ''' + @InvalidGroupCode + ''' no existe';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar existencia de banco
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[PayrollBankCode] IS NOT NULL
				AND df.[PayrollBankCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.Bank b
					WHERE b.[Code] = df.[PayrollBankCode]
				)
		)
		BEGIN
			DECLARE @InvalidBankCode VARCHAR(20);
			SELECT TOP 1 @InvalidBankCode = df.[PayrollBankCode]
			FROM @dataFile df
			WHERE df.[PayrollBankCode] IS NOT NULL
				AND df.[PayrollBankCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.Bank b
					WHERE b.[Code] = df.[PayrollBankCode]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'El banco con código ''' + @InvalidBankCode + ''' no existe';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar existencia de riesgo profesional
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE df.[PayrollProfessionalRiskCode] IS NOT NULL
				AND df.[PayrollProfessionalRiskCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.ProfessionalRisk pr
					WHERE pr.[Code] = df.[PayrollProfessionalRiskCode]
				)
		)
		BEGIN
			DECLARE @InvalidProfessionalRiskCode VARCHAR(20);
			SELECT TOP 1 @InvalidProfessionalRiskCode = df.[PayrollProfessionalRiskCode]
			FROM @dataFile df
			WHERE df.[PayrollProfessionalRiskCode] IS NOT NULL
				AND df.[PayrollProfessionalRiskCode] <> ''
				AND NOT EXISTS(
					SELECT 1
					FROM Payroll.ProfessionalRisk pr
					WHERE pr.[Code] = df.[PayrollProfessionalRiskCode]
				);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'El riesgo profesional con código ''' + @InvalidProfessionalRiskCode + ''' no existe';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- Validar existencia de fondos (HealthCode, PensionCode, UnemploymentCode, OccupationalAccidentInsuranceCode, FamilyWelfareCode)
		IF EXISTS(
			SELECT 1
			FROM @dataFile df
			WHERE (
				(df.[HealthCode] IS NOT NULL AND df.[HealthCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[HealthCode]))
				OR (df.[PensionCode] IS NOT NULL AND df.[PensionCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[PensionCode]))
				OR (df.[UnemploymentCode] IS NOT NULL AND df.[UnemploymentCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[UnemploymentCode]))
				OR (df.[OccupationalAccidentInsuranceCode] IS NOT NULL AND df.[OccupationalAccidentInsuranceCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[OccupationalAccidentInsuranceCode]))
				OR (df.[FamilyWelfareCode] IS NOT NULL AND df.[FamilyWelfareCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[FamilyWelfareCode]))
			)
		)
		BEGIN
			DECLARE @InvalidFundCode VARCHAR(20);
			SELECT TOP 1 @InvalidFundCode = COALESCE(
				CASE WHEN df.[HealthCode] IS NOT NULL AND df.[HealthCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[HealthCode]) THEN df.[HealthCode] END,
				CASE WHEN df.[PensionCode] IS NOT NULL AND df.[PensionCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[PensionCode]) THEN df.[PensionCode] END,
				CASE WHEN df.[UnemploymentCode] IS NOT NULL AND df.[UnemploymentCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[UnemploymentCode]) THEN df.[UnemploymentCode] END,
				CASE WHEN df.[OccupationalAccidentInsuranceCode] IS NOT NULL AND df.[OccupationalAccidentInsuranceCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[OccupationalAccidentInsuranceCode]) THEN df.[OccupationalAccidentInsuranceCode] END,
				CASE WHEN df.[FamilyWelfareCode] IS NOT NULL AND df.[FamilyWelfareCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[FamilyWelfareCode]) THEN df.[FamilyWelfareCode] END
			)
			FROM @dataFile df
			WHERE (
				(df.[HealthCode] IS NOT NULL AND df.[HealthCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[HealthCode]))
				OR (df.[PensionCode] IS NOT NULL AND df.[PensionCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[PensionCode]))
				OR (df.[UnemploymentCode] IS NOT NULL AND df.[UnemploymentCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[UnemploymentCode]))
				OR (df.[OccupationalAccidentInsuranceCode] IS NOT NULL AND df.[OccupationalAccidentInsuranceCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[OccupationalAccidentInsuranceCode]))
				OR (df.[FamilyWelfareCode] IS NOT NULL AND df.[FamilyWelfareCode] <> '' AND NOT EXISTS(SELECT 1 FROM Payroll.Fund f WHERE f.[Code] = df.[FamilyWelfareCode]))
			);
			
			SET @CodeResult = 1;
			SET @MessageResult = 'El fondo con código ''' + @InvalidFundCode + ''' no existe';
			
			IF @trancount = 0
				ROLLBACK TRAN [tran1];
			
			SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
			RETURN;
		END

		-- =============================================
		-- FIN DE VALIDACIONES
		-- =============================================


		INSERT INTO Common.Person(
			[IdentificationNumber]
			,[IdentificationType]
			,[IdentificacionCityId]
			,[IdentificationExpeditionDate]
			,[MilitaryCardId]
			,[MilitaryCardNumber]
			,[FirstName]
			,[SecondName]
			,[FirstLastName]
			,[SecondLastName]
			,[BirthDate]
			,[BirthCityId]
			,[Gender]
			,[BloodGroup]
			,[RH]
			,[MaritalStatus]
			,[State]
		)
		OUTPUT
			inserted.[Id],
			inserted.[IdentificationNumber]
		INTO @commonPerson(
			[CommonPersonPersonID],
			[CommonThirdPartyNit]
		)
		SELECT
			df.[CommonThirdPartyNit] AS [IdentificationNumber],
			CASE UPPER(df.CommonPersonIdentificationType)
				WHEN 'CC' THEN 1 
				WHEN 'CE' THEN 2
				WHEN 'TI' THEN 3 
				WHEN 'RC' THEN 4
				WHEN 'PA' THEN 5
				WHEN 'AS' THEN 6
				WHEN 'MS' THEN 7
				WHEN 'NU' THEN 8
				WHEN 'CN' THEN 9
				WHEN 'CD' THEN 10
				WHEN 'SC' THEN 11
				WHEN 'PE' THEN 12 END
				AS [IdentificationType],
			idCity.Id AS [IdentificacionCityId],
			CONVERT(DATE, df.[CommonPersonIdentificationExpeditionDate], 103) AS [IdentificationExpeditionDate],
			CASE UPPER(df.CommonPersonMilitaryCardId)
				WHEN 'PRIMERA CLASE' THEN 1 
				WHEN 'SEGUNDA CLASE' THEN 2
				ELSE 0 END AS [MilitaryCardId],
			df.[CommonPersonMilitaryCardNumber] AS [MilitaryCardNumber],
			df.[CommonPersonFirstName] AS [FirstName],
			df.[CommonPersonSecondName] AS [SecondName],
			df.[CommonPersonFirstLastName] AS [FirstLastName],
			df.[CommonPersonSecondLastName] AS [SecondLastName],
			df.[CommonPersonBirthDate] AS [BirthDate],
			birthCity.[Id] AS [BirthCityId],
			CASE UPPER(df.[CommonPersonGender])
				WHEN 'MASCULINO' THEN 1 
				WHEN 'FEMENINO' THEN 2
				ELSE 3 END AS [Gender],
			df.[CommonPersonBloodGroup] AS [BloodGroup],
			df.[CommonPersonRH] AS [RH],
			CASE UPPER(df.CommonPersonMaritalStatus)
				WHEN 'SEPARADO(A)' THEN 2
				WHEN 'DIVORCIADO(A)' THEN 2
				WHEN 'CASADO(A)' THEN 1
				WHEN 'SOLTERO(A)' THEN 0
				WHEN 'UNION LIBRE' THEN 4
				ELSE 0 END AS [MaritalStatus],
			1 AS [State]
		FROM @dataFile df
		JOIN Common.City idCity ON idCity.[Name] = df.[CommonIDCityName]
		JOIN Common.City birthCity ON birthCity.[Name] = df.[CommonBirthCityName]
		WHERE [EsCambioFechaVinculación] = 0

		INSERT INTO Common.ThirdParty(
			[PersonId],
			[Nit],
			[Name],
			[PersonType],
			[RetentionType],
			[ContributionType],
			[StateEnterpriseType],
			[Ica],
			[IcaPercentage],
			[IcaTop],
			[IcaTopValue],
			[State],
			[CreationDate],
			[UserId]
		)
		OUTPUT
			inserted.[Id],
			inserted.[Nit]
		INTO @commonThirdParty(
				[CommonThirdPartyThirdPartyID],
				[CommonThirdPartyNit]
			)
		SELECT
			prsn.[CommonPersonPersonID] AS [PersonId],
			prsn.[CommonThirdPartyNit] AS [Nit],	
			FORMATMESSAGE('%s %s %s %s', 
				df.[CommonPersonFirstName], 
				df.[CommonPersonSecondName], 
				df.[CommonPersonFirstLastName], 
				df.[CommonPersonSecondLastName]
			) AS [Name],
			1 AS [PersonType],
			0 AS [RetentionType],
			0 AS [ContributionType],
			0 AS [StateEnterpriseType],
			0 AS [Ica],
			0 AS [IcaPercentage],
			0 AS [IcaTop],
			0 AS [IcaTopValue],
			1 AS [State],
			[Common].[GETDATE]() AS [CreationDate],
			@pIdUser AS [UserId]
		FROM @commonPerson prsn
		JOIN @dataFile df ON df.[CommonThirdPartyNit] = prsn.[CommonThirdPartyNit]
		WHERE [EsCambioFechaVinculación] = 0

		--SELECT TOP 2 * FROM Common.ThirdParty ORDER BY 1 DESC
--		SELECT * FROM @commonThirdParty


		INSERT INTO Payroll.Employee(
			[ThirdPartyId],
			[AdmissionDate],
			[HousingDeductionValue],
			[EducationDeductionValue],
			[ProfessionalRiskPercentage],
			[Pensionary],
			[EmployeeTypeId],
			[TradeUnion],
			[VacationLastDateLiquidation],
			[ProcedureTypeRTF],
			[UserModified],
			[DateModified],
			[State],
			[DeclarantType],
			[HealthContributorRTF],
			[Relocation],
			[CostCenterId],
			[WorkCenterId]
		)
		OUTPUT
			inserted.[Id],
			inserted.[ThirdPartyId]
		INTO @payrollEmployee(
			[PayrollEmployeeEmployeeID],
			[CommonThirdPartyThirdPartyID]
		)
		SELECT
			tPrty.[CommonThirdPartyThirdPartyID] AS [ThirdPartyId],
			df.[PayrollContractContractInitialDate] AS [AdmissionDate],
			0 AS [HousingDeductionValue],
			0 AS [EducationDeductionValue],
			pRisk.[Percentage] AS [ProfessionalRiskPercentage],
			df.[PayrollEmployeePensionary] AS [Pensionary],
			emplyTp.[Id] AS [EmployeeTypeId],
			0 AS [TradeUnion],
			df.[PayrollContractContractInitialDate] AS [VacationLastDateLiquidation],
			1,
			@pCodeUser AS [UserModified],
			[Common].[GETDATE]() AS [DateModified],
			1 AS [State],
			1,
			0 AS [HealthContributorRTF],
			0,
			cCntr.[Id] AS [CostCenterId],
			wCntr.Id AS [WorkCenterId]
		FROM @commonThirdParty tPrty
		JOIN @dataFile df ON df.[CommonThirdPartyNit] = tPrty.[CommonThirdPartyNit]
		JOIN Payroll.EmployeeType emplyTp ON emplyTp.Code = df.[PayrollEmployeeTypeCode]
		JOIN Payroll.CostCenter cCntr ON cCntr.Code = df.[PayrollCostCenterCode]
		JOIN Payroll.WorkCenter wCntr ON wCntr.Code = df.[PayrollWorkCenterCode]
		JOIN Payroll.ProfessionalRisk pRisk ON pRisk.Code = df.PayrollProfessionalRiskCode
		WHERE [EsCambioFechaVinculación] = 0

		--SELECT TOP 2 * FROM Payroll.Employee ORDER BY 1 DESC
		--SELECT * FROM @payrollEmployee




		INSERT INTO Payroll.[Contract](
			[RowType],
			[InitialContractNumber],
			[EmployeeId],
			[PositionId],
			[FunctionalUnitId],
			[ContractTypeId],
			[JobBondingDate],
			[ContractInitialDate],
			[ContractEndingDate],
			[BasicSalary],
			[Status],
			[PaymentPeriod],
			[PaymentType],
			[TrialPeriod],
			[TrialPeriodTime],
			[TrialPeriodSalaryPercentage],
			[ContractCreationDate],
			[CreationUserId],
			[ModificationDate],
			[TypeOfPensionContribution],
			[Valid],
			[GroupId],
			[BankId],
			[BankAccountNumber],
			[BankAccountType],
			[LiquidationPayroll],
			[HoursDaily],
			[ModificationUserId],
			[LastModificationDate],
			[Contingency]
		)
		OUTPUT
			inserted.Id,
			inserted.EmployeeId
		INTO @payrollContract(
			[PayrollContractContractID],
			[PayrollEmployeeEmployeeID]
		)
		SELECT
			1 AS [RowType],
			0 AS [InitialContractNumber],
			emply.[PayrollEmployeeEmployeeID] AS [EmployeeId],
			pstn.Id [PositionId],
			fnt.Id AS [FunctionalUnitId],
			cntrcTp.Id AS [ContractTypeId],
			df.[PayrollContractContractInitialDate] AS [JobBondingDate],
			df.[PayrollContractContractInitialDate] AS [ContractInitialDate],
			df.[PayrollContractContractEndingDate] AS [ContractEndingDate],
			df.[PayrollContractBasicSalary] AS [BasicSalary],
			1 AS [Status],
			df.[PayrollContractPaymentPeriod] AS [PaymentPeriod],
			df.[PayrollContractPaymentType] AS [PaymentType],
			df.[PayrollContractTrialPeriod] AS [TrialPeriod],
			df.[PayrollContractTrialPeriodTime] AS [TrialPeriodTime],
			100,
			[Common].[GETDATE]() AS [ContractCreationDate],
			0  AS [CreationUserId],
			[Common].[GETDATE]() AS [ModificationDate],
			df.[PayrollContractTypeOfPensionContribution] AS [TypeOfPensionContribution],
			1 AS [Valid],
			grp.Id AS [GroupId],
			bnk.Id AS [BankId],
			df.[PayrollContractBankAccountNumber] AS [BankAccountNumber],
			df.[PayrollContractBankAccountType] AS [BankAccountType],
			0 AS [LiquidationPayroll],
			df.[PayrollContractHoursDaily] AS [HoursDaily],
			0 AS [ModificationUserId],
			[Common].[GETDATE]() AS [LastModificationDate],
			CASE df.[PayrollContractContingency]
				WHEN 'NINGUNO' THEN 0
				WHEN 'LICENCIAS' THEN 1
				WHEN 'VACACIONES' THEN 2
				WHEN 'INCAPACIDADES' THEN 3
				ELSE 0 END AS [Contingency]
		FROM @dataFile df
		JOIN @commonThirdParty tPrty 
			ON tPrty.[CommonThirdPartyNit] = df.[CommonThirdPartyNit]
		JOIN @payrollEmployee emply 
			ON emply.[CommonThirdPartyThirdPartyID] = tPrty.[CommonThirdPartyThirdPartyID]
		JOIN Payroll.Position pstn 
			ON pstn.[Code] = df.[PayrollPositionCode]
		JOIN Payroll.FunctionalUnit fnt 
			ON fnt.[Code] = df.[PayrollFunctionalUnitCode]
		JOIN Payroll.ContractType cntrcTp 
			ON cntrcTp.[Code] = df.[PayrollContractTypeCode]
		JOIN Payroll.[Group] grp 
			ON grp.[Code] = df.[PayrollGroupCode]
		JOIN Payroll.Bank bnk 
			ON bnk.[Code] = df.[PayrollBankCode]
		WHERE df.[EsCambioFechaVinculación] = 0

		;WITH cteDataFile AS(
			SELECT
				[CommonThirdPartyNit],
				[PayrollContractContractInitialDate],
				[HealthCode],
				[PensionCode],
				[UnemploymentCode],
				[OccupationalAccidentInsuranceCode],
				[FamilyWelfareCode]
			FROM @dataFile df
			WHERE [EsCambioFechaVinculación] = 0
		), cteFC AS(
			SELECT
				CommonThirdPartyNit,
				FundName,
				FundCode,
				PayrollContractContractInitialDate,
				CASE FundName
					WHEN 'HealthCode' THEN 1
					WHEN 'PensionCode' THEN 2
					WHEN 'UnemploymentCode' THEN 3
					WHEN 'OccupationalAccidentInsuranceCode' THEN 4
					WHEN 'FamilyWelfareCode' THEN 5
					WHEN 'VoluntaryHealthCode' THEN 1
					WHEN 'VoluntaryPensionCode' THEN 2
				END AS [FundType]
			FROM cteDataFile df
			UNPIVOT(
				FundCode FOR FundName IN(
					HealthCode,
					PensionCode,
					UnemploymentCode,
					OccupationalAccidentInsuranceCode,
					FamilyWelfareCode	
				)
			) u
			WHERE NULLIF(FundCode, '') IS NOT NULL
		)
		INSERT INTO Payroll.FundContract(
			[FundId],
			[ContractId],
			[FundType],
			[InitialDate],
			[MembershipNumber],
			[VoluntaryContribution],
			[VoluntaryContributionValue],
			[State]
		)
		SELECT
			fnd.Id AS [FundId],
			cntrc.[PayrollContractContractID] AS [ContractId],
			cteFC.[FundType],
			cteFC.[PayrollContractContractInitialDate] AS [InitialDate],
			0 AS [MembershipNumber],
			0 AS [VoluntaryContribution],
			0 AS [VoluntaryContributionValue],
			1 AS [State]
		FROM cteFC
		JOIN @commonThirdParty tPrty 
			ON tPrty.[CommonThirdPartyNit] = cteFC.[CommonThirdPartyNit]
		JOIN @payrollEmployee emply 
			ON emply.[CommonThirdPartyThirdPartyID] = tPrty.[CommonThirdPartyThirdPartyID]
		JOIN @payrollContract cntrc 
			ON cntrc.[PayrollEmployeeEmployeeID] = emply.[PayrollEmployeeEmployeeID]
		JOIN Payroll.Fund fnd 
			ON fnd.Code = cteFC.FundCode

		DELETE fcntrc
		FROM @dataFile df
		JOIN Common.ThirdParty tPrty ON tPrty.Nit = df.[CommonThirdPartyNit]
		JOIN Payroll.Employee emply ON emply.[ThirdPartyId] = tPrty.[Id]
		JOIN Payroll.[Contract] cntrc ON cntrc.EmployeeId = emply.Id
		JOIN Payroll.FundContract fcntrc ON fcntrc.ContractId = cntrc.Id
		WHERE df.EsCambioFechaVinculación = 1
		
		DELETE cntrc
		FROM @dataFile df
		JOIN Common.ThirdParty tPrty ON tPrty.Nit = df.[CommonThirdPartyNit]
		JOIN Payroll.Employee emply ON emply.[ThirdPartyId] = tPrty.[Id]
		JOIN Payroll.[Contract] cntrc ON cntrc.EmployeeId = emply.Id
		WHERE df.EsCambioFechaVinculación = 1

		DELETE FROM @payrollContract

		INSERT INTO Payroll.[Contract](
			[RowType],
			[InitialContractNumber],
			[EmployeeId],
			[PositionId],
			[FunctionalUnitId],
			[ContractTypeId],
			[JobBondingDate],
			[ContractInitialDate],
			[ContractEndingDate],
			[BasicSalary],
			[Status],
			[PaymentPeriod],
			[PaymentType],
			[TrialPeriod],
			[TrialPeriodTime],
			[TrialPeriodSalaryPercentage],
			[ContractCreationDate],
			[CreationUserId],
			[ModificationDate],
			[TypeOfPensionContribution],
			[Valid],
			[GroupId],
			[BankId],
			[BankAccountNumber],
			[BankAccountType],
			[LiquidationPayroll],
			[HoursDaily],
			[ModificationUserId],
			[LastModificationDate],
			[Contingency]
		)
		OUTPUT
			inserted.Id,
			inserted.EmployeeId
		INTO @payrollContract(
			[PayrollContractContractID],
			[PayrollEmployeeEmployeeID]
		)
		SELECT
			1 AS [RowType],
			0 AS [InitialContractNumber],
			emply.[Id] AS [EmployeeId],
			pstn.Id [PositionId],
			fnt.Id AS [FunctionalUnitId],
			cntrcTp.Id AS [ContractTypeId],
			df.[PayrollContractContractInitialDate] AS [JobBondingDate],
			df.[PayrollContractContractInitialDate] AS [ContractInitialDate],
			df.[PayrollContractContractEndingDate] AS [ContractEndingDate],
			df.[PayrollContractBasicSalary] AS [BasicSalary],
			1 AS [Status],
			df.[PayrollContractPaymentPeriod] AS [PaymentPeriod],
			df.[PayrollContractPaymentType] AS [PaymentType],
			df.[PayrollContractTrialPeriod] AS [TrialPeriod],
			df.[PayrollContractTrialPeriodTime] AS [TrialPeriodTime],
			100,
			[Common].[GETDATE]() AS [ContractCreationDate],
			0  AS [CreationUserId],
			[Common].[GETDATE]() AS [ModificationDate],
			df.[PayrollContractTypeOfPensionContribution] AS [TypeOfPensionContribution],
			1 AS [Valid],
			grp.Id AS [GroupId],
			bnk.Id AS [BankId],
			df.[PayrollContractBankAccountNumber] AS [BankAccountNumber],
			df.[PayrollContractBankAccountType] AS [BankAccountType],
			0 AS [LiquidationPayroll],
			df.[PayrollContractHoursDaily] AS [HoursDaily],
			0 AS [ModificationUserId],
			[Common].[GETDATE]() AS [LastModificationDate],
			df.[PayrollContractContingency] AS [Contingency]
		FROM @dataFile df
		JOIN Common.ThirdParty tPrty
			ON tPrty.[Nit] = df.[CommonThirdPartyNit]
		JOIN Payroll.Employee emply
			ON emply.ThirdPartyId = tPrty.[Id]
		JOIN Payroll.Position pstn 
			ON pstn.[Code] = df.[PayrollPositionCode]
		JOIN Payroll.FunctionalUnit fnt 
			ON fnt.[Code] = df.[PayrollFunctionalUnitCode]
		JOIN Payroll.ContractType cntrcTp 
			ON cntrcTp.[Code] = df.[PayrollContractTypeCode]
		JOIN Payroll.[Group] grp 
			ON grp.[Code] = df.[PayrollGroupCode]
		JOIN Payroll.Bank bnk 
			ON bnk.[Code] = df.[PayrollBankCode]
		WHERE df.[EsCambioFechaVinculación] = 1




		;WITH cteDataFile AS(
			SELECT
				[CommonThirdPartyNit],
				[PayrollContractContractInitialDate],
				[HealthCode],
				[PensionCode],
				[UnemploymentCode],
				[OccupationalAccidentInsuranceCode],
				[FamilyWelfareCode]
			FROM @dataFile df
			WHERE [EsCambioFechaVinculación] = 1
		), cteFC AS(
			SELECT
				[CommonThirdPartyNit],
				[FundName],
				[FundCode],
				[PayrollContractContractInitialDate],
				CASE [FundName]
					WHEN 'HealthCode' THEN 1
					WHEN 'PensionCode' THEN 2
					WHEN 'UnemploymentCode' THEN 3
					WHEN 'OccupationalAccidentInsuranceCode' THEN 4
					WHEN 'FamilyWelfareCode' THEN 5
					WHEN 'VoluntaryHealthCode' THEN 1
					WHEN 'VoluntaryPensionCode' THEN 2
				END AS [FundType]
			FROM cteDataFile df
			UNPIVOT(
				FundCode FOR FundName IN(
					[HealthCode],
					[PensionCode],
					[UnemploymentCode],
					[OccupationalAccidentInsuranceCode],
					[FamilyWelfareCode]
				)
			) u
			WHERE NULLIF([FundCode], '') IS NOT NULL
		)
		INSERT INTO Payroll.FundContract(
			[FundId],
			[ContractId],
			[FundType],
			[InitialDate],
			[MembershipNumber],
			[VoluntaryContribution],
			[VoluntaryContributionValue],
			[State]
		)
		SELECT
			fnd.Id AS [FundId],
			cntrc.[PayrollContractContractID] AS [ContractId],
			cteFC.[FundType],
			cteFC.[PayrollContractContractInitialDate] AS [InitialDate],
			0 AS [MembershipNumber],			
			0 AS [VoluntaryContribution],
			0 AS [VoluntaryContributionValue],
			1 AS [State]
		FROM cteFC
		JOIN Common.ThirdParty tPrty
			ON tPrty.[Nit] = cteFC.[CommonThirdPartyNit]
		JOIN Payroll.Employee emply 
			ON emply.[ThirdPartyId] = tPrty.[Id]
		JOIN @payrollContract cntrc 
			ON cntrc.[PayrollEmployeeEmployeeID] = emply.[Id]
		JOIN Payroll.Fund fnd 
			ON fnd.Code = cteFC.FundCode

		
		UPDATE Payroll.Contract SET InitialContractNumber = Id WHERE InitialContractNumber = 0 and RowType = 1

		-- Asignar resultado de éxito
		SET @CodeResult = 0;
		SET @MessageResult = 'Contratos agregados exitosamente';

		if @trancount = 0
		COMMIT TRAN [tran1]
		
		-- Retornar resultado de éxito
		SELECT @CodeResult AS CodeResult, @MessageResult AS MessageResult;
	END TRY
	BEGIN CATCH
		-- Manejar rollback según el estado de la transacción
		IF XACT_STATE() = -1
			ROLLBACK;
		IF XACT_STATE() = 1 AND @trancount = 0
			ROLLBACK TRAN [tran1];
		
		-- Retornar resultado de error no controlado
		SELECT 999 AS CodeResult, ERROR_MESSAGE() AS MessageResult;
	END CATCH
END