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