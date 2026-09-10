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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedura almacenada que recibe un XML con múltiples registros de empleados y devuelve una vista previa comparativa para la extensión o renovación masiva de contratos laborales. Para cada fila del XML, cruza el NIT del empleado con los terceros registrados, obtiene el contrato laboral vigente más reciente (fechas actuales y salario básico actual) y lo compara contra los nuevos valores propuestos (nuevas fechas de inicio y fin, nuevo salario y nuevo cargo). Se utiliza en el módulo de nómina para que el usuario pueda revisar y confirmar los cambios antes de aplicar modificaciones contractuales en bloque, mostrando lado a lado la información contractual actual versus la nueva.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetMassiveContractExtension';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetMassiveContractExtension';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Previsualiza, a partir de un XML masivo de prórrogas/modificaciones contractuales, los datos actuales del contrato vigente de cada empleado frente a los nuevos valores propuestos (fechas, salario y cargo).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@XMLObj debe seguir la estructura /Data/Row con los nodos esperados (Nit, PositionCode, ContractInitialDate, ContractEndingDate, BasicSalary, etc.).; Las fechas en el XML deben venir en formato compatible con CONVERT estilo 103 (dd/mm/yyyy).; BasicSalary del XML debe ser convertible a NUMERIC(18,2).; Cada Nit del XML debe existir en Common.ThirdParty y tener un Employee asociado en Payroll.Employee.; El PositionCode del XML debe existir en Payroll.Position.; El empleado debe tener al menos un Contract con Valid=1 y Status=1.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el contrato vigente más reciente del empleado: Valid=1 y Status=1, ordenado por Id DESC TOP 1.; Las fechas nuevas de inicio/fin de contrato vienen en formato británico (dd/mm/yyyy, estilo 103) y se convierten a DATE.; El salario básico nuevo se interpreta como NUMERIC(18,2) y se formatea como moneda en inglés (en-us).; El empleado se identifica vinculando Nit del XML → ThirdParty → Employee (1 a 1 por ThirdPartyId).; Solo se devuelven filas cuyo Nit, PositionCode y contrato vigente existan simultáneamente (JOINs internos).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Contrato laboral; Prórroga/extensión de contrato; Cargo (Position); Salario básico; Tercero (Nit)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Retorna por cada Row del XML: Nit, nombre del empleado, fechas y salario del contrato vigente (Valid=1 y Status=1, último Id) versus las nuevas fechas (estilo 103), nuevo salario formateado y nombre del nuevo cargo (Position.Code = PositionCode).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Position; Payroll.Contract', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveContractExtension';
-- GO
