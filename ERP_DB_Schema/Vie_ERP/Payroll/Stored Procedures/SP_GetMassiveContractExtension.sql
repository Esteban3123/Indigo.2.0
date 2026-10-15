-- =============================================
-- Author:		Iván Ospina
-- Create date: 2019-12-21
-- Description:	
-- =============================================
CREATE PROCEDURE [Payroll].[SP_GetMassiveContractExtension]
	@XMLObj XML 
AS
BEGIN
	;WITH cteXML AS(
		SELECT 
			ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS Linea,
			t.x.value('Nit[1]', 'VARCHAR(100)') AS Nit,
			t.x.value('TradeUnion[1]', 'VARCHAR(100)') AS TradeUnion,
			t.x.value('Relocation[1]', 'VARCHAR(100)') AS Relocation,
			t.x.value('HealthContributorRTF[1]', 'VARCHAR(100)') AS HealthContributorRTF,
			t.x.value('ContractModificationReasonCode[1]', 'VARCHAR(100)') AS ContractModificationReasonCode,
			t.x.value('PositionCode[1]', 'VARCHAR(100)') AS PositionCode,
			t.x.value('FunctionalUnitCode[1]', 'VARCHAR(100)') AS FunctionalUnitCode,
			t.x.value('ContractTypeCode[1]', 'VARCHAR(100)') AS ContractTypeCode,
			t.x.value('ContractInitialDate[1]', 'VARCHAR(100)') AS ContractInitialDate,
			t.x.value('ContractEndingDate[1]', 'VARCHAR(100)') AS ContractEndingDate,
			t.x.value('BasicSalary[1]', 'VARCHAR(100)') AS BasicSalary,
			t.x.value('PaymentPeriod[1]', 'VARCHAR(100)') AS PaymentPeriod,
			t.x.value('PaymentType[1]', 'VARCHAR(100)') AS PaymentType,
			t.x.value('TypeOfPensionContribution[1]', 'VARCHAR(100)') AS TypeOfPensionContribution,
			t.x.value('GroupCode[1]', 'VARCHAR(100)') AS GroupCode,
			t.x.value('BankCode[1]', 'VARCHAR(100)') AS BankCode,
			t.x.value('BankAccountNumber[1]', 'VARCHAR(100)') AS BankAccountNumber,
			t.x.value('BankAccountType[1]', 'VARCHAR(100)') AS BankAccountType,
			t.x.value('HoursDaily[1]', 'VARCHAR(100)') AS HoursDaily,
			t.x.value('Contingency[1]', 'VARCHAR(100)') AS Contingency,
			t.x.value('HealthCode[1]', 'VARCHAR(100)') AS HealthCode,
			t.x.value('VoluntaryHealthCode[1]', 'VARCHAR(100)') AS VoluntaryHealthCode,
			t.x.value('VoluntaryHealthContributionValue[1]', 'VARCHAR(100)') AS VoluntaryHealthContributionValue,
			t.x.value('PensionCode[1]', 'VARCHAR(100)') AS PensionCode,
			t.x.value('VoluntaryPensionCode[1]', 'VARCHAR(100)') AS VoluntaryPensionCode,
			t.x.value('VoluntaryPensionContributionValue[1]', 'VARCHAR(100)') AS VoluntaryPensionContributionValue,
			t.x.value('UnemploymentCode[1]', 'VARCHAR(100)') AS UnemploymentCode,
			t.x.value('OccupationalAccidentInsurance[1]', 'VARCHAR(100)') AS OccupationalAccidentInsurance,
			t.x.value('FamilyWelfare[1]', 'VARCHAR(100)') AS FamilyWelfare
		FROM @XMLObj.nodes('/Data/Row') t(x)
	)

	SELECT
		cteXML.Nit,
		tPrty.[Name] AS [Employee],
		cntrc.ContractInitialDate,
		cntrc.ContractEndingDate,
		FORMAT(cntrc.BasicSalary, 'c', 'en-us') AS [BasicSalary],
		CONVERT(DATE, cteXML.ContractInitialDate, 103) AS [NewContractInitialDate],
		CONVERT(DATE, cteXML.ContractEndingDate, 103) AS [NewContractEndingDate],
		FORMAT(CAST(cteXML.BasicSalary AS NUMERIC(18,2)), 'c', 'en-us') AS [NewBasicSalary],
		pstn.[Name] AS [NewPositionName]
	FROM cteXML
	JOIN Common.ThirdParty tPrty ON tPrty.Nit = cteXML.Nit
	JOIN Payroll.Employee emply ON emply.ThirdPartyId = tPrty.Id
	JOIN Payroll.Position pstn ON pstn.Code = cteXML.PositionCode
	CROSS APPLY(
		SELECT TOP 1 
			cntrc.Id,
			cntrc.ContractInitialDate,
			cntrc.ContractEndingDate,
			cntrc.BasicSalary
		FROM Payroll.[Contract] cntrc
		WHERE cntrc.EmployeeId = emply.Id
			AND cntrc.Valid = 1
			AND cntrc.[Status] = 1
		ORDER BY cntrc.Id DESC
	) cntrc

END