-- =============================================
-- Author:		Iván Ospina
-- Create date: 2019-12-13
-- Description:	Valida la información del archivo Excel
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ValidateMassiveContract] 
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
			t.x.value('CommonPersonMaritalStatus[1]', 'VARCHAR(100)') AS [CommonPersonMaritalStatus],
			t.x.value('PayrollProfessionalRiskCode[1]', 'VARCHAR(100)') AS [PayrollProfessionalRiskCode],
			t.x.value('PayrollEmployeePensionary[1]', 'VARCHAR(100)') AS [PayrollEmployeePensionary],
			t.x.value('PayrollEmployeeTypeCode[1]', 'VARCHAR(100)') AS [PayrollEmployeeTypeCode],
			t.x.value('PayrollCostCenterCode[1]', 'VARCHAR(100)') AS [PayrollCostCenterCode],
			t.x.value('PayrollWorkCenterCode[1]', 'VARCHAR(100)') AS [PayrollWorkCenterCode],
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
			t.x.value('PensionCode[1]', 'VARCHAR(100)') AS [PensionCode],			
			t.x.value('UnemploymentCode[1]', 'VARCHAR(100)') AS [UnemploymentCode],
			t.x.value('OccupationalAccidentInsuranceCode[1]', 'VARCHAR(100)') AS [OccupationalAccidentInsuranceCode],
			t.x.value('FamilyWelfareCode[1]', 'VARCHAR(100)') AS [FamilyWelfareCode]
		FROM @XMLObj.nodes('/Data/Row') t(x)
	), cteType AS(
		SELECT
			Linea, 
			Columna, 
			Valor
		FROM cteXML
		UNPIVOT(
			Valor FOR Columna IN(
				CommonPersonIdentificationType
				,CommonPersonIdentificationExpeditionDate
				,CommonPersonMilitaryCardId
				,CommonPersonBirthDate
				,CommonPersonGender
				,CommonPersonMaritalStatus
				,PayrollEmployeePensionary
				,PayrollContractContractInitialDate
				,PayrollContractContractEndingDate
				,PayrollContractBasicSalary
				,PayrollContractPaymentPeriod
				,PayrollContractPaymentType
				,PayrollContractTrialPeriod
				,PayrollContractTrialPeriodTime
				,PayrollContractTrialPeriodSalaryPercentage
				,PayrollContractTypeOfPensionContribution
				,PayrollContractBankAccountType
				,PayrollContractHoursDaily
				,PayrollContractContingency		
			)
		) u
	), cteTC AS(
		SELECT
			Linea,
			'Caracteres inválidos o formato incorrecto' AS Mensaje,
			Columna,
			Valor,
			CASE		
				WHEN Columna = 'CommonPersonIdentificationType'	AND UPPER(Valor) IN ('CC','TI','RC','PA','AS','MS','NU','CN','CD','SC','PE') THEN 1
				WHEN Columna = 'CommonPersonMilitaryCardId'	AND UPPER(Valor) IN ('NO APLICA','NO TIENE','PRIMERA CLASE','SEGUNDA CLASE') THEN 1
				WHEN Columna = 'CommonPersonGender'	AND UPPER(Valor) IN ('FEMENINO', 'MASCULINO', 'OTRO') THEN 1
				WHEN Columna = 'CommonPersonIdentificationExpeditionDate' AND TRY_CONVERT(DATE, Valor, 103) IS NOT NULL THEN 1
				WHEN Columna = 'CommonPersonBirthDate' AND TRY_CONVERT(DATE, Valor, 103) IS NOT NULL THEN 1
				WHEN Columna = 'CommonPersonMaritalStatus' AND UPPER(Valor) IN ('SEPARADO(A)', 'DIVORCIADO(A)', 'CASADO(A)', 'SOLTERO(A)', 'UNION LIBRE') THEN 1
				WHEN Columna = 'PayrollProfessionalRiskCode' AND Valor IS NOT NULL OR Valor <> '' THEN 1
				WHEN Columna = 'PayrollEmployeePensionary' AND TRY_CONVERT(BIT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollEmployeeDeclarantType' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollEmployeeRelocation' AND TRY_CONVERT(BIT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollEmployeeProcedureTypeRTF' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollContractContractInitialDate' AND TRY_CONVERT(DATE, Valor, 103) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollContractContractEndingDate' AND TRY_CONVERT(DATE, Valor, 103) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollContractBasicSalary' AND TRY_CONVERT(NUMERIC(18, 0), Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollContractPaymentPeriod' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollContractPaymentType' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollContractTrialPeriod' AND TRY_CONVERT(BIT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollContractTrialPeriodTime' AND TRY_CONVERT(INT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollContractTrialPeriodSalaryPercentage' AND TRY_CONVERT(INT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollContractTypeOfPensionContribution' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollContractBankAccountType' AND TRY_CONVERT(INT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollContractHoursDaily' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollContractContingency' AND UPPER(Valor) IN ('LICENCIAS', 'NINGUNO', 'VACACIONES', 'INCAPACIDADES') THEN 1
				WHEN Columna = 'PayrollFundContractVoluntaryHealthContributionValue' AND TRY_CONVERT(NUMERIC(18, 0), COALESCE(NULLIF(Valor, ''), 0)) IS NOT NULL THEN 1
				WHEN Columna = 'PayrollFundContractVoluntaryPensionContributionValue' AND TRY_CONVERT(NUMERIC(18, 0), COALESCE(NULLIF(Valor, ''), 0)) IS NOT NULL THEN 1
				ELSE 0
			END AS TC
		FROM cteType
	), cte1Employee AS(
		SELECT
			cteXML.Linea,
			'El empleado ya tiene un contrato activo' AS Mensaje,
			'Número Identificación' AS Columna,
			cteXML.[CommonThirdPartyNit] AS Valor
		FROM Payroll.Employee emply
		JOIN Common.ThirdParty tPrty ON tPrty.Id = emply.ThirdPartyId
		JOIN cteXML ON cteXML.[CommonThirdPartyNit] = tPrty.Nit
		JOin Payroll.Contract C ON C.EmployeeId = emply.Id
		WHERE C.Valid = 1 AND c.[Status] = 1 AND c.ContractInitialDate = [PayrollContractContractInitialDate]
	), cte2Contract AS(
		SELECT
			cteXML.Linea,
			'La fecha de inicio del contrato es menor que la próxima nómina' AS Mensaje,
			'Fecha Fin Contrato' AS Columna,
			cteXML.[PayrollContractContractInitialDate] AS Valor
		FROM Payroll.[Group] grp
		JOIN cteXML ON cteXML.[PayrollGroupCode] = grp.Code
		WHERE TRY_CONVERT(DATE, cteXML.[PayrollContractContractInitialDate], 103) < CAST(grp.NextDateLiquidation AS DATE)
	), cte3Salary AS(
		SELECT
			cteXML.Linea,
			'El salario no está en el rango para el cargo' AS Mensaje,
			'Salario Básico' AS Columna, 
			FORMAT(CAST(cteXML.[PayrollContractBasicSalary] AS NUMERIC(18,2)), 'c', 'en-US') AS Valor
		FROM cteXML 
		JOIN Payroll.Position pstn ON pstn.Code = cteXML.[PayrollPositionCode]
		WHERE 
			TRY_CONVERT(NUMERIC(18,2), cteXML.[PayrollContractBasicSalary])
				NOT BETWEEN pstn.MinBasicSalary AND pstn.MaxBasicSalary 
	), cte4City AS(
		SELECT
			cteXML.Linea,
			'La ciudad del documento no existe en el sistema' AS Mensaje,
			'Nombre Ciudad Exp. Documento' AS Columna,
			cteXML.[CommonIDCityName] AS Valor
		FROM cteXML
		LEFT JOIN Common.City docCty ON docCty.[Name] = cteXML.[CommonIDCityName] 
		WHERE docCty.[Name] IS NULL
		UNION ALL
		SELECT
			cteXML.Linea,
			'La ciudad de nacimiento no existe en el sistema' AS Mensaje,
			'Nombre Ciudad Nacimiento' AS Columna,
			cteXML.[CommonBirthCityName] AS Valor
		FROM cteXML
		LEFT JOIN Common.City birthCty ON birthCty.[Name] = cteXML.[CommonBirthCityName]
		WHERE birthCty.[Name] IS NULL
	), cte5EmployeeType AS(
		SELECT
			cteXML.Linea,
			'El código del tipo de empleado no existe en el sistema' AS Mensaje,
			'Código Tipo De Empleado' AS Columna,
			cteXML.[PayrollEmployeeTypeCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.EmployeeType emplyTp ON emplyTp.Code = cteXML.[PayrollEmployeeTypeCode]
		WHERE emplyTp.Code IS NULL
	), cte6CostCenter AS(
		SELECT
			cteXML.Linea,
			'El código de centro de costo no existe' AS Mensaje,
			'Código Centro Costo' AS Columna,
			cteXML.[PayrollCostCenterCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.CostCenter cCntr ON cCntr.Code = cteXML.[PayrollCostCenterCode]
		WHERE cCntr.Code IS NULL
	), cte7WorkCenter AS(
		SELECT
			cteXML.Linea,
			'El código de centro de trabajo no existe' AS Mensaje,
			'Código Centro Trabajo' AS Columna,
			cteXML.[PayrollWorkCenterCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.WorkCenter wCntr ON wCntr.Code = cteXML.[PayrollWorkCenterCode]
		WHERE wCntr.Code IS NULL
	), cte8Position AS(
		SELECT
			cteXML.Linea,
			'El código del cargo no existe' AS Mensaje,
			'Código Cargo' AS Columna,
			cteXML.[PayrollPositionCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Position pstn ON pstn.Code = cteXML.[PayrollPositionCode]
		WHERE pstn.Code IS NULL
	), cte9FunctionalUnit AS(
		SELECT
			cteXML.Linea,
			'El código de la unidad funcional no existe en el sistema' AS Mensaje,
			'Código Unidad Funcional' AS Columna,
			cteXML.[PayrollFunctionalUnitCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.FunctionalUnit fnt ON fnt.Code = cteXML.[PayrollFunctionalUnitCode]
		WHERE fnt.Code IS NULL
	), cte10ContractType AS(
		SELECT
			cteXML.Linea,
			'El código del tipo de contrato no existe en el sistema' AS Mensaje,
			'Código Tipo Contrato' AS Columna,
			cteXML.[PayrollContractTypeCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.ContractType cntrcTp ON cntrcTp.Code = cteXML.[PayrollContractTypeCode]
		WHERE cntrcTp.Code IS NULL
	), cte11Group AS(
		SELECT
			cteXML.Linea,
			'El código del grupo no existe en el sistema' AS Mensaje,
			'Código Grupo' AS Columna,
			cteXML.[PayrollGroupCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.[Group] grp ON grp.Code = cteXML.[PayrollGroupCode]
		WHERE grp.Code IS NULL
	), cte12Bank AS(
		SELECT
			cteXML.Linea,
			'El código del banco no existe en el sistema' AS Mensaje,
			'Código Banco' AS Columna,
			cteXML.[PayrollBankCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Bank bnk ON bnk.Code = cteXML.[PayrollBankCode]
		WHERE bnk.Code IS NULL
	), cte13Health AS(
		SELECT
			cteXML.Linea,
			'El código del fondo de salud no existe en el sistema' AS Mensaje,
			'Código Fondo Salud' AS Columna,
			cteXML.[HealthCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Fund fnd ON fnd.Code = cteXML.[HealthCode]
		WHERE fnd.Code IS NULL
	), cte13Pension AS(
		SELECT
			cteXML.Linea,
			'El código del fondo de pensión no existe en el sistema' AS Mensaje,
			'Código Fondo Pensión' AS Columna,
			cteXML.[PensionCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Fund fnd ON fnd.Code = cteXML.[PensionCode]
		WHERE fnd.Code IS NULL
	), cte13Unemployment AS(
		SELECT
			cteXML.Linea,
			'El código del fondo de cesantias no existe en el sistema' AS Mensaje,
			'Código Fondo Cesantias' AS Columna,
			cteXML.[UnemploymentCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Fund fnd ON fnd.Code = cteXML.[UnemploymentCode]
		WHERE fnd.Code IS NULL
	), cte13OccupationalAccidentInsurance AS(
		SELECT
			cteXML.Linea,
			'El código del fondo de ARL no existe en el sistema' AS Mensaje,
			'Código Fondo ARL' AS Columna,
			cteXML.[OccupationalAccidentInsuranceCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Fund fnd ON fnd.Code = cteXML.[OccupationalAccidentInsuranceCode]
		WHERE fnd.Code IS NULL
	), cte13FamilyWelfare AS(
		SELECT
			cteXML.Linea,
			'El código del fondo de caja de compensación familiar no existe en el sistema' AS Mensaje,
			'Código Fondo Caja Compensacion' AS Columna,
			cteXML.[FamilyWelfareCode] AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Fund fnd ON fnd.Code = cteXML.[FamilyWelfareCode]
		WHERE fnd.Code IS NULL
	),  cte14SalaryMinimun AS(
		SELECT
			cteXML.Linea,
			'El salario es Inferior al Salario Mínimo Legal Vigente' AS Mensaje,
			'Salario Básico' AS Columna, 
			FORMAT(CAST(cteXML.[PayrollContractBasicSalary] AS NUMERIC(18,2)), 'c', 'en-US') AS Valor
		FROM cteXML 
		JOIN Payroll.[Group] pstn ON pstn.Code = cteXML.PayrollGroupCode
		JOIN PAyroll.PayrollParameter pp on pp.Id = pstn.PayrollParameterId
		WHERE 
			TRY_CONVERT(NUMERIC(18,2), cteXML.[PayrollContractBasicSalary]) < pp.LegalSalaryMinimum
	), cte15ProfessionalRiskCode AS(
		SELECT
			cteXML.Linea,
			'El Código de Riesgos Laborales no existe' AS Mensaje,
			'Riesgos Laborales' AS Columna, 
			cteXML.[PayrollProfessionalRiskCode] AS Valor
		FROM cteXML 
		LEFT JOIN Payroll.ProfessionalRisk fnd ON fnd.Code = cteXML.[PayrollProfessionalRiskCode]
		WHERE fnd.Code IS NULL
	 ),cte AS(
		SELECT Linea, Mensaje, Columna, Valor FROM cteTC WHERE TC = 0
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte1Employee
		UNION ALL 
		SELECT Linea, Mensaje, Columna, Valor FROM cte2Contract
		UNION ALL 
		SELECT Linea, Mensaje, Columna, Valor FROM cte3Salary
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte4City
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte5EmployeeType
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte6CostCenter
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte7WorkCenter
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte8Position
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte9FunctionalUnit
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte10ContractType
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte11Group
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte12Bank
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte13Health
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte13Pension
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte13Unemployment
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte13OccupationalAccidentInsurance
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte13FamilyWelfare
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte14SalaryMinimun
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte15ProfessionalRiskCode
	)

	SELECT
		Linea,
		Mensaje,
		Columna,
		Valor
	FROM cte
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida masivamente la información de contratos laborales enviada desde un archivo Excel convertido a XML, antes de su carga definitiva en el sistema de nómina. Recorre cada fila del XML y verifica que los datos del empleado y del contrato cumplan formatos, valores permitidos y consistencia de negocio: tipo de identificación, fechas, género, estado civil, salario dentro del rango del cargo (consultando Payroll.Position), existencia del grupo de nómina (Payroll.Group), tipo de empleado (Payroll.EmployeeType), centro de costo (Payroll.CostCenter), ciudad (Common.City), NIT del tercero (Common.ThirdParty) y vigencia del contrato activo del empleado (Payroll.Contract y Payroll.Employee). Devuelve una lista de errores por línea y columna indicando qué campo falló, permitiendo al operador de nómina corregir el archivo antes de la carga masiva de contratos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveContract';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveContract';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida fila por fila la información de un archivo Excel (recibido como XML) para carga masiva de contratos de nómina, devolviendo el listado de inconsistencias de formato, integridad referencial y reglas laborales.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /Data/Row con los nodos de columnas esperados; Los catálogos de referencia (City, EmployeeType, CostCenter, WorkCenter, Position, FunctionalUnit, ContractType, Group, Bank, Fund, ProfessionalRisk) deben estar poblados para que los códigos válidos no se reporten como inexistentes; Payroll.Group debe tener PayrollParameterId que apunte a un PayrollParameter con LegalSalaryMinimum vigente; Payroll.Position debe tener configurados MinBasicSalary y MaxBasicSalary para validar el rango salarial', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila del XML recibe un Linea consecutivo (ROW_NUMBER) que identifica el registro al reportar errores; La validación de existencia de fondos (salud, pensión, cesantías, ARL, caja de compensación) se hace contra una única tabla Payroll.Fund por código, sin distinguir tipo de fondo en el SQL; El procedimiento solo realiza lectura: nunca inserta, actualiza o elimina datos; Un empleado se considera con contrato activo si Valid=1 AND Status=1 AND la fecha de inicio coincide con la del nuevo contrato; Las fechas se interpretan en formato británico/colombiano dd/mm/yyyy (estilo 103); Los códigos de tipo de identificación admitidos cubren documentos colombianos: CC, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve un único conjunto de resultados con columnas Linea, Mensaje, Columna y Valor consolidando todas las inconsistencias detectadas; si no hay errores el resultset es vacío (no lanza excepciones ni modifica datos).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Columna=''CommonPersonIdentificationType'' y UPPER(Valor) NOT IN (''CC'',''TI'',''RC'',''PA'',''AS'',''MS'',''NU'',''CN'',''CD'',''SC'',''PE'') → Reporta ''Caracteres inválidos o formato incorrecto''; si Columna=''CommonPersonMilitaryCardId'' y UPPER(Valor) NOT IN (''NO APLICA'',''NO TIENE'',''PRIMERA CLASE'',''SEGUNDA CLASE'') → Reporta error de formato; si Columna=''CommonPersonGender'' y UPPER(Valor) NOT IN (''FEMENINO'',''MASCULINO'',''OTRO'') → Reporta error de formato; si Columna=''CommonPersonMaritalStatus'' y UPPER(Valor) NOT IN (''SEPARADO(A)'',''DIVORCIADO(A)'',''CASADO(A)'',''SOLTERO(A)'',''UNION LIBRE'') → Reporta error de formato; si Columna=''PayrollContractContingency'' y UPPER(Valor) NOT IN (''LICENCIAS'',''NINGUNO'',''VACACIONES'',''INCAPACIDADES'') → Reporta error de formato; si Columnas de fecha (CommonPersonIdentificationExpeditionDate, CommonPersonBirthDate, PayrollContractContractInitialDate, PayrollContractContractEndingDate) que no convierten a DATE con formato 103 (dd/mm/yyyy) → Reporta ''Caracteres inválidos o formato incorrecto''; si Columnas booleanas (PayrollEmployeePensionary, PayrollContractTrialPeriod) que no convierten a BIT → Reporta error de formato; si Columnas numéricas (PayrollContractBasicSalary NUMERIC(18,0); PayrollContractPaymentPeriod/PaymentType/TypeOfPensionContribution/HoursDaily TINYINT; PayrollContractTrialPeriodTime/SalaryPercentage/BankAccountType INT) que no convierten al tipo correspondiente → Reporta error de formato; si Existe en Payroll.Employee/Common.ThirdParty/Payroll.Contract un contrato con C.Valid=1, C.Status=1, mismo Nit y misma ContractInitialDate → Reporta ''El empleado ya tiene un contrato activo''; si TRY_CONVERT(DATE, PayrollContractContractInitialDate, 103) < CAST(Group.NextDateLiquidation AS DATE) → Reporta ''La fecha de inicio del contrato es menor que la próxima nómina''; si PayrollContractBasicSalary NOT BETWEEN Position.MinBasicSalary AND Position.MaxBasicSalary → Reporta ''El salario no está en el rango para el cargo''; si PayrollContractBasicSalary < PayrollParameter.LegalSalaryMinimum del grupo → Reporta ''El salario es Inferior al Salario Mínimo Legal Vigente''; si El valor de CommonIDCityName o CommonBirthCityName no existe en Common.City.Name → Reporta que la ciudad no existe en el sistema; si El código no existe en su catálogo correspondiente (EmployeeType, CostCenter, WorkCenter, Position, FunctionalUnit, ContractType, Group, Bank, Fund para Health/Pension/Unemployment/ARL/FamilyWelfare, ProfessionalRisk) → Reporta que el código no existe en el sistema', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.Group; Payroll.Position; Common.City; Payroll.EmployeeType; Payroll.CostCenter; Payroll.WorkCenter; Payroll.FunctionalUnit; Payroll.ContractType; Payroll.Bank; Payroll.Fund; Payroll.PayrollParameter; Payroll.ProfessionalRisk', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContract';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContract';
-- GO
