-- =============================================
-- Author:		Iván Ospina
-- Create date: 2019-12-21
-- Description:	Valida la información del archivo Excel
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ValidateMassiveContractExtension] 
	@XMLObj XML
AS
BEGIN
;WITH cteXML AS(
		SELECT 
			ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS Linea,
			t.x.value('Nit[1]', 'VARCHAR(100)') AS Nit,
			t.x.value('ProfessionalRiskPercentage[1]', 'VARCHAR(100)') AS ProfessionalRiskPercentage,
			CASE WHEN (t.x.value('Workcenter[1]', 'VARCHAR(100)')) = ''  THEN '0' ELSE (t.x.value('Workcenter[1]', 'VARCHAR(100)')) END AS Workcenter,
			t.x.value('ContractModificationReasonCode[1]', 'VARCHAR(100)') AS ContractModificationReasonCode,
			t.x.value('PositionCode[1]', 'VARCHAR(100)') AS PositionCode,
			t.x.value('FunctionalUnitCode[1]', 'VARCHAR(100)') AS FunctionalUnitCode,
			t.x.value('ContractTypeCode[1]', 'VARCHAR(100)') AS ContractTypeCode,
			t.x.value('ContractInitialDate[1]', 'VARCHAR(100)') AS ContractInitialDate,
			t.x.value('ContractEndingDate[1]', 'VARCHAR(100)') AS ContractEndingDate,
			t.x.value('BasicSalary[1]', 'VARCHAR(100)') AS BasicSalary,
			t.x.value('PaymentPeriod[1]', 'VARCHAR(100)') AS PaymentPeriod,
			t.x.value('PaymentType[1]', 'VARCHAR(100)') AS PaymentType,
			t.x.value('GroupCode[1]', 'VARCHAR(100)') AS GroupCode,
			t.x.value('BankCode[1]', 'VARCHAR(100)') AS BankCode,
			t.x.value('BankAccountNumber[1]', 'VARCHAR(100)') AS BankAccountNumber,
			t.x.value('BankAccountType[1]', 'VARCHAR(100)') AS BankAccountType,
			t.x.value('HoursDaily[1]', 'VARCHAR(100)') AS HoursDaily,
			t.x.value('Contingency[1]', 'VARCHAR(100)') AS Contingency,
			t.x.value('HealthCode[1]', 'VARCHAR(100)') AS HealthCode,
			t.x.value('PensionCode[1]', 'VARCHAR(100)') AS PensionCode,
			t.x.value('UnemploymentCode[1]', 'VARCHAR(100)') AS UnemploymentCode,
			t.x.value('OccupationalAccidentInsurance[1]', 'VARCHAR(100)') AS OccupationalAccidentInsurance,
			t.x.value('FamilyWelfare[1]', 'VARCHAR(100)') AS FamilyWelfare
		FROM @XMLObj.nodes('/Data/Row') t(x)
	), cteType AS(
		SELECT
			Linea,
			'Caracteres inválidos o formato incorrecto' AS Mensaje,
			Columna, 
			Valor,
			CASE
				WHEN Columna = 'ProfessionalRiskPercentage' AND TRY_CONVERT(VARCHAR(20), Valor) IS NOT NULL THEN 1
				WHEN Columna = 'Workcenter' AND TRY_CONVERT(VARCHAR(20), Valor) IS NOT NULL THEN 1
				WHEN Columna = 'ContractInitialDate' AND TRY_CONVERT(DATE, Valor, 103) IS NOT NULL THEN 1
				WHEN Columna = 'ContractEndingDate' AND TRY_CONVERT(DATE, Valor, 103) IS NOT NULL THEN 1
				WHEN Columna = 'BasicSalary' AND TRY_CONVERT(NUMERIC(18, 0), Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PaymentPeriod' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PaymentType' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'BankAccountType' AND TRY_CONVERT(INT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'HoursDaily' AND TRY_CONVERT(TINYINT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'Contingency' AND UPPER(Valor) IN ('LICENCIAS', 'NINGUNO', 'VACACIONES', 'INCAPACIDADES') THEN 1
				ELSE 0
			END AS TC
		FROM cteXML
		UNPIVOT(
			Valor FOR Columna IN(
				ProfessionalRiskPercentage,
				Workcenter,
				ContractInitialDate,
				ContractEndingDate,
				BasicSalary,
				PaymentPeriod,
				PaymentType,
				BankAccountType,
				HoursDaily,
				Contingency
			)
		) u
	), cte1Employee AS(
		SELECT
			cteXML.Linea,
			'El empleado no existe' AS Mensaje,
			'Número Identificación' AS Columna,
			cteXML.Nit AS Valor
		FROM cteXML
		LEFT JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		LEFT JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		WHERE emply.ThirdpartyId IS NULL
	), cte1ProfessionalRiskPercentage AS(
		SELECT
			cteXML.Linea,
			'El codigo de riesgo profesional no existe' AS Mensaje,
			'Codigo de riesgo profesional' AS Columna,
			cteXML.ProfessionalRiskPercentage AS Valor
		FROM cteXML
		LEFT JOIN Payroll.ProfessionalRisk pr WITH(NOLOCK)  ON pr.Code = cteXML.ProfessionalRiskPercentage
		WHERE pr.Code IS NULL
	), cte1Workcenter AS(
		SELECT
			cteXML.Linea,
			'El codigo de centro de trabajo no existe' AS Mensaje,
			'Codigo de centro de trabajo' AS Columna,
			cteXML.Workcenter AS Valor
		FROM cteXML
		LEFT JOIN Payroll.WorkCenter wc WITH(NOLOCK) ON wc.Code = cteXML.Workcenter
		WHERE wc.Code IS NULL
	), cte2Contract AS(
		SELECT
			cteXML.Linea,
			'El empleado no tiene un contrato activo' AS Mensaje,
			'Número Identificación' AS Columna,
			cteXML.Nit AS Valor
		FROM cteXML
		LEFT JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		LEFT JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		LEFT JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		WHERE cntrc.EmployeeId IS NULL

	), cte5Salary AS(
		SELECT
			cteXML.Linea,
			'El salario no está en el rango para el cargo' AS Mensaje,
			'BasicSalary' AS Columna, 
			cteXML.BasicSalary AS Valor
		FROM cteXML 
		JOIN Payroll.Position pstn WITH(NOLOCK) ON pstn.Code = cteXML.PositionCode
		CROSS JOIN Payroll.PayrollSettings ps
		WHERE 
			TRY_CONVERT(NUMERIC(18,2), cteXML.BasicSalary) 
				NOT BETWEEN pstn.MinBasicSalary AND pstn.MaxBasicSalary 
			AND ps.MinimunLegalSalaryLastYear > TRY_CONVERT(NUMERIC(18,2), cteXML.BasicSalary)
	), cte6Position AS(
		SELECT
			cteXML.Linea,
			'El código del cargo no existe' AS Mensaje,
			'PositionCode' AS Columna,
			cteXML.PositionCode AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Position pstn WITH(NOLOCK) ON pstn.Code = cteXML.PositionCode
		WHERE pstn.Code IS NULL
	), cte7FunctionalUnit AS(
		SELECT
			cteXML.Linea,
			'El código de la unidad funcional no existe en el sistema' AS Mensaje,
			'FunctionalUnitCode' AS Columna,
			cteXML.FunctionalUnitCode AS Valor
		FROM cteXML
		LEFT JOIN Payroll.FunctionalUnit fnt WITH(NOLOCK)  ON fnt.Code = cteXML.FunctionalUnitCode
		WHERE fnt.Code IS NULL
	), cte8ContractType AS(
		SELECT
			cteXML.Linea,
			'El código del tipo de contrato no existe en el sistema' AS Mensaje,
			'ContractTypeCode' AS Columna,
			cteXML.ContractTypeCode AS Valor
		FROM cteXML
		LEFT JOIN Payroll.ContractType cntrcTp WITH(NOLOCK) ON cntrcTp.Code = cteXML.ContractTypeCode
		WHERE cntrcTp.Code IS NULL
	), cte9Group AS(
		SELECT
			cteXML.Linea,
			'El código del grupo no existe en el sistema' AS Mensaje,
			'GroupCode' AS Columna,
			cteXML.GroupCode AS Valor
		FROM cteXML
		LEFT JOIN Payroll.[Group] grp WITH(NOLOCK) ON grp.Code = cteXML.GroupCode
		WHERE grp.Code IS NULL
	), cte10Bank AS(
		SELECT
			cteXML.Linea,
			'El código del banco no existe en el sistema' AS Mensaje,
			'BankCode' AS Columna,
			cteXML.BankCode AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Bank bnk WITH(NOLOCK) ON bnk.Code = cteXML.BankCode
		WHERE bnk.Code IS NULL
	), cte11Health AS(
		SELECT
			cteXML.Linea,
			'El código del fondo de salud no existe en el sistema' AS Mensaje,
			'HealthCode' AS Columna,
			cteXML.HealthCode AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Fund fnd WITH(NOLOCK) ON fnd.Code = cteXML.HealthCode
		WHERE fnd.Code IS NULL
	), cte11Pension AS(
		SELECT
			cteXML.Linea,
			'El código del fondo de pensión no existe en el sistema' AS Mensaje,
			'PensionCode' AS Columna,
			cteXML.PensionCode AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Fund fnd WITH(NOLOCK) ON fnd.Code = cteXML.PensionCode
		WHERE fnd.Code IS NULL AND cteXML.PensionCode IS NOT NULL AND cteXML.PensionCode <> ''
	), cte11Unemployment AS(
		SELECT
			cteXML.Linea,
			'El código del fondo de cesantias no existe en el sistema' AS Mensaje,
			'UnemploymentCode' AS Columna,
			cteXML.UnemploymentCode AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Fund fnd WITH(NOLOCK) ON fnd.Code = cteXML.UnemploymentCode
		WHERE fnd.Code IS NULL
	), cte11OccupationalAccidentInsurance AS(
		SELECT
			cteXML.Linea,
			'El código del fondo de ARL no existe en el sistema' AS Mensaje,
			'OccupationalAccidentInsurance' AS Columna,
			cteXML.OccupationalAccidentInsurance AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Fund fnd WITH(NOLOCK) ON fnd.Code = cteXML.OccupationalAccidentInsurance
		WHERE fnd.Code IS NULL
	), cte11FamilyWelfare AS(
		SELECT
			cteXML.Linea,
			'El código del fondo de caja de compensación familiar no existe en el sistema' AS Mensaje,
			'FamilyWelfare' AS Columna,
			cteXML.FamilyWelfare AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Fund fnd WITH(NOLOCK) ON fnd.Code = cteXML.FamilyWelfare
		WHERE fnd.Code IS NULL
	), cte1InsertOrUpdate AS( --Linea para determinar si es prorroga o actualizacion del contrato.
		--SELECT cntrc.Valid,cntrc.Status, DATEDIFF(day,cntrc.ContractEndingDate,df.ContractInitialDate) AS DIFERENCIA, cntrc.BasicSalary , df.BasicSalary as Newsalary, cntrc.ContractTypeId , cntrc.ContractTypeId as newcontracttype , Pos.Code , df.PositionCode , Pr.Code as riesgocode , REPLACE (str( df.ProfessionalRiskPercentage,3),SPACE(1),'0') as ProfessionalRiskPercentage , wc.Code as workcentercode, df.Workcenter
			SELECT
			df.Linea,
			'No se identifico prorroga, ni cambio en condiciones laborales. ' AS Mensaje,
			'Empleado:' AS Columna,
			df.Nit AS Valor

		FROM Payroll.[Contract] cntrc 
		INNER Join  Payroll.Employee emply WITH(NOLOCK) ON emply.Id = cntrc.EmployeeId
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK)  ON tPrty.Id = emply.ThirdPartyId
		INNER JOIN cteXML df WITH(NOLOCK) ON df.Nit = tPrty.Nit
		LEFT OUTER JOIN Payroll.ContractType ct WITH(NOLOCK) On cntrc.ContractTypeId = ct.id
		LEFT OUTER JOIN Payroll.Position Pos WITH(NOLOCK) On Pos.id = cntrc.PositionId
		LEFT OUTER JOIN Payroll.ProfessionalRisk Pr WITH(NOLOCK) on Pr.Percentage = emply.ProfessionalRiskPercentage
		LEFT OUTER JOIN Payroll.WorkCenter wc WITH(NOLOCK) on wc.id = emply.WorkCenterId
		where cntrc.Valid = 1 and cntrc.Status = 1 and DATEDIFF(day,cntrc.ContractEndingDate,df.ContractInitialDate)<>1 and cntrc.BasicSalary = df.BasicSalary and cntrc.ContractTypeId = cntrc.ContractTypeId and Pos.Code = df.PositionCode and Pr.Code = REPLACE (str( df.ProfessionalRiskPercentage,3),SPACE(1),'0') 
	), cte AS(
		SELECT Linea, Mensaje, Columna, Valor FROM cteType WHERE TC = 0
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte1Employee
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte2Contract
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte5Salary
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte6Position
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte7FunctionalUnit
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte8ContractType
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte9Group
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte10Bank
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte11Health
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte11Pension
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte11Unemployment
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte11OccupationalAccidentInsurance
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte11FamilyWelfare
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte1InsertOrUpdate
	)

	SELECT
		Linea,
		Mensaje,
		Columna,
		Valor
	FROM cte
	ORDER BY Linea	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida de forma masiva la información de prórrogas o extensiones de contratos laborales cargada desde un archivo Excel (convertido a XML). Recibe un XML con los datos de cada fila del archivo y verifica, línea por línea, que el empleado exista (por NIT/cédula), que tenga un contrato activo, que el código de riesgo profesional, centro de trabajo, cargo, unidad funcional y tipo de contrato sean válidos en nómina, y que el salario propuesto esté dentro del rango permitido para el cargo y por encima del salario mínimo legal. Devuelve un listado de errores por línea indicando el campo, el valor inválido y el mensaje descriptivo del problema, permitiendo al usuario corregir el archivo antes de procesar la extensión masiva de contratos en el módulo de nómina (Payroll).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveContractExtension';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveContractExtension';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida fila por fila un XML con datos de prórroga/modificación masiva de contratos laborales y devuelve los errores detectados (formato, existencia de catálogos, rango salarial y detección de prórroga).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro XML debe seguir la estructura /Data/Row con los nodos esperados (Nit, ProfessionalRiskPercentage, Workcenter, ContractInitialDate, ContractEndingDate, BasicSalary, PositionCode, etc.).; Las fechas deben venir en formato 103 (dd/mm/yyyy) para pasar TRY_CONVERT(DATE,...,103).; Debe existir al menos un registro en Payroll.PayrollSettings (se hace CROSS JOIN sin filtro).; El campo Workcenter vacío se normaliza a ''0'' antes de validar.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La validación se ejecuta en modo solo-lectura: ninguna CTE realiza INSERT/UPDATE/DELETE.; Todos los lookups a catálogos usan WITH(NOLOCK).; Una prórroga válida exige que la nueva ContractInitialDate sea exactamente un día después de la ContractEndingDate del contrato vigente (DATEDIFF=1).; El código de riesgo profesional se compara contra el porcentaje del empleado formateado con STR(...,3) reemplazando espacios por ''0'' (ej. ''01'',''05'').; Solo se consideran contratos con Valid=1 y Status=1 como contrato vigente del empleado.; La validación de salario solo dispara cuando el valor está fuera del rango del cargo Y por debajo del salario mínimo legal del año anterior.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prórroga de contrato; Modificación masiva de contrato; Empleado; Contrato laboral activo; Riesgo profesional (ARL); Centro de trabajo; Cargo y rango salarial; Salario mínimo legal; Unidad funcional; Tipo de contrato; Grupo de nómina; Banco y cuenta bancaria; Fondo de salud (EPS); Fondo de pensión (AFP); Fondo de cesantías; Caja de compensación familiar; Contingencias laborales (licencias, vacaciones, incapacidades)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve un conjunto (Linea, Mensaje, Columna, Valor) ordenado por Linea con todos los errores de validación detectados por fila del XML.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Columna validada por tipo (ProfessionalRiskPercentage, Workcenter como VARCHAR(20); ContractInitialDate/EndingDate como DATE formato 103; BasicSalary NUMERIC(18,0); PaymentPeriod/PaymentType/HoursDaily TINYINT; BankAccountType INT) → Si TRY_CONVERT falla (TC=0) se reporta ''Caracteres inválidos o formato incorrecto''. else Fila pasa la validación de tipo.; si Columna = ''Contingency'' → Solo es válido si UPPER(Valor) ∈ (''LICENCIAS'',''NINGUNO'',''VACACIONES'',''INCAPACIDADES''); en otro caso se reporta error de formato.; si No existe coincidencia en Common.ThirdParty/Payroll.Employee para el Nit → Reporta ''El empleado no existe''.; si El empleado no tiene Contract con Valid=1 y Status=1 → Reporta ''El empleado no tiene un contrato activo''.; si BasicSalary fuera del rango [MinBasicSalary, MaxBasicSalary] del Position Y además PayrollSettings.MinimunLegalSalaryLastYear > BasicSalary → Reporta ''El salario no está en el rango para el cargo''. else No se reporta error de salario.; si PensionCode no existe en Payroll.Fund → Solo se reporta error si PensionCode no es NULL ni vacío (campo opcional).; si Existe contrato activo cuyo DATEDIFF(día, ContractEndingDate, ContractInitialDate del XML) <> 1 y BasicSalary, ContractType, Position y ProfessionalRisk son iguales a los del XML → Reporta ''No se identifico prorroga, ni cambio en condiciones laborales''. else Se considera prórroga o cambio válido.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.ProfessionalRisk; Payroll.WorkCenter; Payroll.Contract; Payroll.Position; Payroll.PayrollSettings; Payroll.FunctionalUnit; Payroll.ContractType; Payroll.Group; Payroll.Bank; Payroll.Fund', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveContractExtension';
-- GO
