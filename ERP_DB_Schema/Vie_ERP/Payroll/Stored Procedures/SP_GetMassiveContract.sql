-- =============================================
-- Author:		Iván Ospina
-- Create date: 2019-12-18
-- Description:	
-- =============================================
CREATE PROCEDURE [Payroll].[SP_GetMassiveContract]
	@XMLObj XML 
AS
BEGIN
	;WITH cteXML AS(
		SELECT
			ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS Linea,
			t.x.value('CommonThirdPartyNit[1]', 'VARCHAR(100)') AS [CommonThirdPartyNit],
			t.x.value('CommonPersonIdentificationType[1]', 'VARCHAR(100)') AS [CommonPersonIdentificationType],
			t.x.value('CommonIDCityName[1]', 'VARCHAR(100)') AS [CommonIDCityName],
			t.x.value('CommonPersonIdentificationExpeditionDate[1]', 'VARCHAR(100)') AS [CommonPersonIdentificationExpeditionDate],
			t.x.value('CommonPersonMilitaryCardId[1]', 'VARCHAR(100)') AS [CommonPersonMilitaryCardId],
			t.x.value('CommonPersonMilitaryCardNumber[1]', 'VARCHAR(100)') AS [CommonPersonMilitaryCardNumber],
			t.x.value('CommonPersonFirstName[1]', 'VARCHAR(100)') AS [CommonPersonFirstName],
			t.x.value('CommonPersonSecondName[1]', 'VARCHAR(100)') AS [CommonPersonSecondName],
			t.x.value('CommonPersonFirstLastName[1]', 'VARCHAR(100)') AS [CommonPersonFirstLastName],
			t.x.value('CommonPersonSecondLastName[1]', 'VARCHAR(100)') AS [CommonPersonSecondLastName],
			t.x.value('CommonPersonBirthDate[1]', 'VARCHAR(100)') AS [CommonPersonBirthDate],
			t.x.value('CommonBirthCityName[1]', 'VARCHAR(100)') AS [CommonBirthCityName],
			t.x.value('CommonPersonGender[1]', 'VARCHAR(100)') AS [CommonPersonGender],
			t.x.value('CommonPersonBloodGroup[1]', 'VARCHAR(100)') AS [CommonPersonBloodGroup],
			t.x.value('CommonPersonRH[1]', 'VARCHAR(100)') AS [CommonPersonRH],
			t.x.value('CommonPersonMaritalStatus[1]', 'VARCHAR(100)') AS [CommonPersonMaritalStatus],
			t.x.value('PayrollProfessionalRiskCode[1]', 'VARCHAR(100)') AS [PayrollProfessionalRiskCode],
			t.x.value('PayrollEmployeePensionary[1]', 'VARCHAR(100)') AS [PayrollEmployeePensionary],
			t.x.value('PayrollEmployeeTypeCode[1]', 'VARCHAR(100)') AS [PayrollEmployeeTypeCode],
			t.x.value('PayrollCostCenterCode[1]', 'VARCHAR(100)') AS [PayrollCostCenterCode],
			t.x.value('PayrollWorkCenterCode[1]', 'VARCHAR(100)') AS [PayrollWorkCenterCode],
			t.x.value('PayrollEmployeeDeclarantType[1]', 'VARCHAR(100)') AS [PayrollEmployeeDeclarantType],
			t.x.value('PayrollEmployeeRelocation[1]', 'VARCHAR(100)') AS [PayrollEmployeeRelocation],
			t.x.value('PayrollEmployeeProcedureTypeRTF[1]', 'VARCHAR(100)') AS [PayrollEmployeeProcedureTypeRTF],
			t.x.value('PayrollPositionCode[1]', 'VARCHAR(100)') AS [PayrollPositionCode],
			t.x.value('PayrollFunctionalUnitCode[1]', 'VARCHAR(100)') AS [PayrollFunctionalUnitCode],
			t.x.value('PayrollContractTypeCode[1]', 'VARCHAR(100)') AS [PayrollContractTypeCode],
			t.x.value('PayrollContractContractInitialDate[1]', 'VARCHAR(100)') AS [PayrollContractContractInitialDate],
			t.x.value('PayrollContractContractEndingDate[1]', 'VARCHAR(100)') AS [PayrollContractContractEndingDate],
			t.x.value('PayrollContractBasicSalary[1]', 'VARCHAR(100)') AS [PayrollContractBasicSalary],
			t.x.value('PayrollContractPaymentPeriod[1]', 'VARCHAR(100)') AS [PayrollContractPaymentPeriod],
			t.x.value('PayrollContractPaymentType[1]', 'VARCHAR(100)') AS [PayrollContractPaymentType],
			t.x.value('PayrollContractTrialPeriod[1]', 'VARCHAR(100)') AS [PayrollContractTrialPeriod],
			t.x.value('PayrollContractTrialPeriodTime[1]', 'VARCHAR(100)') AS [PayrollContractTrialPeriodTime],
			t.x.value('PayrollContractTrialPeriodSalaryPercentage[1]', 'VARCHAR(100)') AS [PayrollContractTrialPeriodSalaryPercentage],
			t.x.value('PayrollContractTypeOfPensionContribution[1]', 'VARCHAR(100)') AS [PayrollContractTypeOfPensionContribution],
			t.x.value('PayrollGroupCode[1]', 'VARCHAR(100)') AS [PayrollGroupCode],
			t.x.value('PayrollBankCode[1]', 'VARCHAR(100)') AS [PayrollBankCode],
			t.x.value('PayrollContractBankAccountNumber[1]', 'VARCHAR(100)') AS [PayrollContractBankAccountNumber],
			t.x.value('PayrollContractBankAccountType[1]', 'VARCHAR(100)') AS [PayrollContractBankAccountType],
			t.x.value('PayrollContractHoursDaily[1]', 'VARCHAR(100)') AS [PayrollContractHoursDaily],
			t.x.value('PayrollContractContingency[1]', 'VARCHAR(100)') AS [PayrollContractContingency],
			t.x.value('HealthCode[1]', 'VARCHAR(100)') AS [HealthCode],
			t.x.value('VoluntaryHealthCode[1]', 'VARCHAR(100)') AS [VoluntaryHealthCode],
			t.x.value('PayrollFundContractVoluntaryHealthContributionValue[1]', 'VARCHAR(100)') AS [PayrollFundContractVoluntaryHealthContributionValue],
			t.x.value('PensionCode[1]', 'VARCHAR(100)') AS [PensionCode],
			t.x.value('VoluntaryPensionCode[1]', 'VARCHAR(100)') AS [VoluntaryPensionCode],
			t.x.value('PayrollFundContractVoluntaryPensionContributionValue[1]', 'VARCHAR(100)') AS [PayrollFundContractVoluntaryPensionContributionValue],
			t.x.value('UnemploymentCode[1]', 'VARCHAR(100)') AS [UnemploymentCode],
			t.x.value('OccupationalAccidentInsuranceCode[1]', 'VARCHAR(100)') AS [OccupationalAccidentInsuranceCode],
			t.x.value('FamilyWelfareCode[1]', 'VARCHAR(100)') AS [FamilyWelfareCode]
		FROM @XMLObj.nodes('/Data/Row') t(x)
	)

	SELECT
		cteXML.[CommonThirdPartyNit],
		CONVERT(DATE, cteXML.[PayrollContractContractInitialDate], 103) AS [PayrollContractContractInitialDate],
		CONVERT(DATE, cteXML.[PayrollContractContractEndingDate], 103) AS [PayrollContractContractEndingDate],
		FORMAT(CAST(cteXML.[PayrollContractBasicSalary] AS NUMERIC(18,2)), 'c', 'en-us') AS [PayrollContractBasicSalary],
		grp.[Name] AS [GroupName],
		pst.[Name] AS [PositionName],
		fnt.[Name] AS [FunctionalUnitName]
	FROM cteXML
	JOIN Payroll.[Group] grp ON grp.Code = cteXML.[PayrollGroupCode]
	JOIN Payroll.Position pst ON pst.Code = cteXML.[PayrollPositionCode]
	JOIN Payroll.FunctionalUnit fnt ON fnt.Code = cteXML.[PayrollFunctionalUnitCode]
END