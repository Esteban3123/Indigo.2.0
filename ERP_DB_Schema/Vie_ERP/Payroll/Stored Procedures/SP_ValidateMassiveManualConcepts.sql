-- =============================================
-- Author:		Iván Ospina
-- Create date: 2019-09-25
-- Description:	Valida la información del archivo Excel
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ValidateMassiveManualConcepts] 
	@XMLObj XML
AS
BEGIN
	;WITH cteXML AS (
		SELECT
			ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS Linea,
			t.x.value('Process[1]','VARCHAR(32)') AS Process,
			t.x.value('Nit[1]','VARCHAR(32)') AS Nit,
			t.x.value('InternalCode[1]','VARCHAR(10)') AS InternalCode,
			t.x.value('Code[1]','VARCHAR(32)') AS Code,			
			t.x.value('QuoteValue[1]','VARCHAR(32)') AS QuoteValue,
			t.x.value('PaidEndContract[1]','VARCHAR(32)') AS PaidEndContract,
			t.x.value('QuoteNumber[1]','VARCHAR(32)') AS QuoteNumber,
			t.x.value('PaidFormat[1]','VARCHAR(32)') AS PaidFormat,
			t.x.value('Description[1]','VARCHAR(200)') AS [Description]
		FROM @XMLObj.nodes('/Data/Row') t(x)
	), cteValidTypes AS(
		SELECT
			Linea,
			'Caracteres inválidos' AS Mensaje,
			Columna,
			CASE
				WHEN Columna = 'Process' AND TRY_CONVERT(NUMERIC(9,0), Valor) IS NOT NULL THEN 1
				WHEN Columna = 'QuoteValue' AND TRY_CONVERT(NUMERIC(18,2), Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PaidEndContract' AND TRY_CONVERT(NUMERIC(9,0), Valor) IS NOT NULL THEN 1
				WHEN Columna = 'QuoteNumber' AND TRY_CONVERT(NUMERIC(9,0), Valor) IS NOT NULL THEN 1
				WHEN Columna = 'PaidFormat' AND TRY_CONVERT(NUMERIC(9,0), Valor) IS NOT NULL THEN 1
				ELSE 0
			END AS TC,
			Valor
		FROM cteXML
		UNPIVOT(
			Valor FOR Columna IN(
				Process,
				QuoteValue,
				PaidEndContract,
				QuoteNumber,
				PaidFormat
			)
		) u
	), cteEmployee AS(
		SELECT
			cteXML.Linea,
			'El empleado no está activo, no existe, no tiene NIT ni código interno, o no tiene contrato.' AS Mensaje, 
			'Cédula' AS Columna,
			cteXML.Nit AS Valor
		FROM cteXML
		LEFT JOIN Common.ThirdParty tParty ON tParty.Nit = cteXML.Nit 
		LEFT JOIN Payroll.Employee emply ON emply.ThirdPartyId = tParty.Id 
		LEFT JOIN Payroll.Employee E ON NULLIF(LTRIM(RTRIM(cteXML.InternalCode)), '') IS NOT NULL AND E.InternalCode = cteXML.InternalCode
		OUTER APPLY(
			SELECT TOP 1
				cntrc.Id,
				cntrc.[Status],
				cntrc.Valid, 
				cntrc.EmployeeId
			FROM Payroll.[Contract] cntrc
			WHERE cntrc.EmployeeId = emply.Id OR cntrc.EmployeeId = E.Id
			ORDER BY cntrc.Id DESC
		) cntrc
		WHERE (cntrc.[Status] != 1 AND cntrc.Valid != 1) OR (cntrc.Id IS NULL) 
	), cteConcept AS(
		SELECT
			cteXML.Linea,
			'El código del concepto no existe' AS Mensaje,
			'Código Concepto' AS Columna,
			cteXML.Code  AS Valor
		FROM Payroll.Concept concept
		RIGHT JOIN cteXML ON cteXML.Code = concept.Code
		WHERE concept.Code IS NULL
	), cte_Concept AS(
		SELECT
			cteXML.Linea,
			'La clase del concepto es Convenio' AS Mensaje,
			'Clase Concepto' AS Columna,
			cteXML.Code  AS Valor
		FROM Payroll.Concept concept
		RIGHT JOIN cteXML ON cteXML.Code = concept.Code
		WHERE concept.ConceptClass = '041'
	), cte_ConceptQuoteValueMax AS(
		SELECT
			cteXML.Linea,
			'El valor cotizado no puede ser mayor a 99 para esta clase de concepto' AS Mensaje,
			'QuoteValue' AS Columna,
			cteXML.QuoteValue AS Valor
		FROM cteXML
		RIGHT JOIN Payroll.Concept concept ON cteXML.Code = concept.Code
		WHERE concept.ConceptClass IN ('001', '012', '013', '051', '042', '043', '050')
			AND TRY_CONVERT(NUMERIC(18,2), NULLIF(LTRIM(RTRIM(cteXML.QuoteValue)), '')) > 99
	), 
	cteProcess AS(
		SELECT
			cteXML.Linea,
			'El Proceso no existe' AS Mensaje,
			'Proceso' AS Columna,
			cteXML.Process AS Valor
		FROM cteXML
		WHERE TRY_CONVERT(NUMERIC(9,0), cteXML.Process) NOT IN(1,2,3,4,5)
	), cte AS(
		SELECT Linea, Mensaje, Columna, Valor FROM cteValidTypes WHERE TC = 0
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cteEmployee
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cteConcept
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte_Concept
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cte_ConceptQuoteValueMax
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor FROM cteProcess
	)

	SELECT
		Linea,
		Mensaje,
		Columna,
		Valor
	FROM cte
	ORDER BY 1
END