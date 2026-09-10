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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que procesa la creación masiva de contratos de empleados a partir de un archivo XML con múltiples registros. Recibe en un parámetro XML todos los datos personales, laborales y financieros de cada empleado (cédula, nombres, fechas de contrato, salario, tipo de contrato, cuenta bancaria, fondos de salud, pensión y cesantías, entre otros) y valida que los códigos de grupo de nómina, cargo y unidad funcional existan en las tablas maestras correspondientes. Devuelve un resumen por empleado con el NIT, las fechas de inicio y fin del contrato, el salario básico formateado y los nombres descriptivos del grupo de liquidación, el cargo y el área o departamento, facilitando la verificación masiva antes de confirmar la vinculación de nuevos trabajadores.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetMassiveContract';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetMassiveContract';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Parsea un XML con filas de contratos masivos de nómina y devuelve un resumen enriquecido (NIT, fechas de contrato, salario formateado y nombres de grupo, cargo y unidad funcional) para previsualización antes de su carga.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /Data/Row con los nodos esperados (CommonThirdPartyNit, PayrollGroupCode, PayrollPositionCode, PayrollFunctionalUnitCode, fechas y salario).; Las fechas PayrollContractContractInitialDate y PayrollContractContractEndingDate deben venir en formato británico dd/mm/yyyy (estilo 103) para que CONVERT a DATE sea válido.; PayrollContractBasicSalary debe ser numérico convertible a NUMERIC(18,2).; Los códigos PayrollGroupCode, PayrollPositionCode y PayrollFunctionalUnitCode deben existir en Payroll.Group, Payroll.Position y Payroll.FunctionalUnit respectivamente; de lo contrario la fila se descarta por el JOIN.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las filas del XML cuyos códigos de grupo, cargo o unidad funcional no existan en sus tablas maestras quedan excluidas del resultado (INNER JOIN).; El salario básico siempre se devuelve formateado como moneda con cultura ''en-us'' (símbolo $ y dos decimales).; Las fechas de inicio y fin de contrato siempre se interpretan con estilo 103 (dd/mm/yyyy).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato de nómina; Carga masiva de contratos; Grupo de nómina; Cargo/Posición laboral; Unidad funcional; Salario básico; Tercero (NIT); Periodo de prueba; Aportes a salud, pensión, ARL, cesantías y caja de compensación', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve una fila por cada /Data/Row del XML que tenga correspondencia en Payroll.Group, Payroll.Position y Payroll.FunctionalUnit, con fechas convertidas a DATE y salario formateado como moneda en formato ''en-us''.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Group; Payroll.Position; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContract';
-- GO
