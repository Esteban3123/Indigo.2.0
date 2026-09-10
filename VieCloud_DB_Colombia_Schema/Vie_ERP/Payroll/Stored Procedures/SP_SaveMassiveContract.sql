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

	BEGIN TRY

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

		if @trancount = 0
		COMMIT TRAN [tran1]
	END TRY
	BEGIN CATCH
	--print 'hola'
	select ERROR_NUMBER(),  ERROR_MESSAGE(), XACT_STATE();
       
        if XACT_STATE() = -1
            rollback;
        if XACT_STATE() = 1 
            rollback
        if XACT_STATE() = 1 
            rollback transaction [tran1];
			 
		--ROLLBACK TRAN [tran1]
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de carga masiva de contratos laborales a partir de un archivo XML con múltiples registros de empleados. Por cada registro del XML, crea o actualiza los datos personales del empleado (nombre, identificación, fecha de nacimiento, género, estado civil, libreta militar), registra o actualiza el tercero en Common.ThirdParty, vincula al empleado en Payroll.Employee (con su centro de costo, tipo, afiliaciones a pensión y salud, riesgos profesionales) y genera el contrato laboral en Payroll.Contract (tipo de contrato, salario básico, fechas de inicio y fin, período de prueba, cuenta bancaria para pago de nómina, horas diarias). También tiene en cuenta si existe una liquidación activa para el empleado y si se está modificando la fecha de vinculación, garantizando la integridad transaccional de todo el proceso de vinculación masiva de personal.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveMassiveContract';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveMassiveContract';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Carga masiva desde XML para crear o reemplazar contratos de nómina junto con sus personas, terceros, empleados y afiliaciones a fondos de seguridad social, distinguiendo entre alta nueva y cambio de fecha de vinculación.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Data/Row con los campos esperados (NIT, nombres, fechas en formato dd/mm/yyyy estilo 103, códigos de catálogos).; Las ciudades indicadas (CommonIDCityName y CommonBirthCityName) deben existir en Common.City; en caso contrario los registros se descartan por el INNER JOIN.; Los códigos de EmployeeType, CostCenter, WorkCenter, ProfessionalRisk, Position, FunctionalUnit, ContractType, Group, Bank y Fund deben existir en sus catálogos respectivos.; Para considerar un registro como ''cambio de fecha de vinculación'' debe existir en Payroll.Contract un contrato con Status=1 y Valid=1 cuyo empleado no tenga liquidaciones (Payroll.Liquidation con count=0) y la fecha inicial del XML sea distinta a la del contrato existente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Common.Person: Cuando EsCambioFechaVinculación=0 se inserta una persona nueva mapeando el tipo de identificación textual (CC,CE,TI,RC,PA,AS,MS,NU,CN,CD,SC,PE) a su entero, género (MASCULINO=1, FEMENINO=2, otros=3) y estado civil (SEPARADO/DIVORCIADO=2, CASADO=1, SOLTERO=0, UNION LIBRE=4).; [INSERT] Common.ThirdParty: Cuando EsCambioFechaVinculación=0 se crea el tercero asociado a la persona recién insertada, con PersonType=1, Nit=identificación, Name concatenado con FORMATMESSAGE de los 4 nombres y State=1.; [INSERT] Payroll.Employee: Cuando EsCambioFechaVinculación=0 se crea el empleado con AdmissionDate y VacationLastDateLiquidation iguales a la fecha inicial del contrato, ProfessionalRiskPercentage tomado de Payroll.ProfessionalRisk y State=1.; [INSERT] Payroll.Contract: Cuando EsCambioFechaVinculación=0 se inserta el contrato con RowType=1, Status=1, Valid=1, TrialPeriodSalaryPercentage=100 y Contingency mapeado por texto (NINGUNO=0, LICENCIAS=1, VACACIONES=2, INCAPACIDADES=3, otro=0).; [INSERT] Payroll.FundContract: Tras insertar el contrato (caso alta) se hace UNPIVOT de HealthCode/PensionCode/UnemploymentCode/OccupationalAccidentInsuranceCode/FamilyWelfareCode y se inserta una fila por cada código no vacío con FundType 1..5 según el tipo y State=1.; [DELETE] Payroll.FundContract: Cuando EsCambioFechaVinculación=1 se eliminan los FundContract existentes asociados al contrato vigente del empleado identificado por NIT antes de regenerarlos.; [DELETE] Payroll.Contract: Cuando EsCambioFechaVinculación=1 se eliminan los contratos existentes del empleado (NIT) para reemplazarlos por uno nuevo con la fecha de vinculación actualizada.; [INSERT] Payroll.Contract: Cuando EsCambioFechaVinculación=1 se reinserta el contrato sobre el empleado existente (no se crea nueva persona/tercero/empleado), con la nueva fecha inicial; en este flujo Contingency se inserta tal cual viene en el dataFile (no se aplica el mapeo textual).; [INSERT] Payroll.FundContract: Cuando EsCambioFechaVinculación=1 se reinsertan los FundContract a partir del UNPIVOT de los códigos de fondo, ligados al nuevo ContractId.; [UPDATE] Payroll.Contract: Al final del proceso se ejecuta UPDATE Payroll.Contract SET InitialContractNumber=Id WHERE InitialContractNumber=0 AND RowType=1, garantizando que el número inicial de contrato quede igual al Id generado.; [RETURN_RESULT] (resultset): En el bloque CATCH se devuelve un resultset con ERROR_NUMBER(), ERROR_MESSAGE() y XACT_STATE() en lugar de relanzar el error.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @trancount = 0 → Inicia BEGIN TRAN [tran1] y al final hace COMMIT; si ya hay transacción externa, se ejecuta sin abrir/cerrar transacción propia. else No abre ni confirma transacción local.; si Existe en Payroll.Contract un contrato con Status=1, Valid=1, sin liquidaciones (Payroll.Liquidation count=0) cuyo ContractInitialDate difiere del valor del XML → EsCambioFechaVinculación=1: se borra el contrato y sus FundContract y se reinsertan con la nueva fecha, reutilizando empleado/tercero/persona. else EsCambioFechaVinculación=0: se crea persona, tercero, empleado, contrato y fondos como alta nueva.; si LEN(REPLACE(CommonPersonBloodGroup,'' '','''')) = 3 → Toma BloodGroup como SUBSTRING(...,0,3) y RH como SUBSTRING(...,3,4). else Toma BloodGroup como SUBSTRING(...,0,2) y RH como SUBSTRING(...,2,3).; si XACT_STATE() = -1 o 1 dentro del CATCH → Ejecuta ROLLBACK (incluyendo rollback de la transacción nombrada [tran1]).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContract';
-- GO
