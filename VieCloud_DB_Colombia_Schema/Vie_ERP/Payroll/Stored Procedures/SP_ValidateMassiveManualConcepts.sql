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
			AND TRY_CONVERT(NUMERIC(18,2), NULLIF(LTRIM(RTRIM(cteXML.QuoteValue)), '')) > 99 AND Formulates NOT LIKE '%Valor Concepto Manual%'
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida masivamente los conceptos manuales de nómina cargados desde un archivo Excel (recibido como XML) antes de procesarlos. Recorre cada fila del archivo y verifica: que los campos numéricos (proceso, valor de cuota, número de cuota, formato de pago, pago fin de contrato) tengan el formato correcto; que el empleado identificado por NIT o código interno exista, esté activo y tenga un contrato vigente; que el código de concepto de nómina exista en el catálogo y no pertenezca a la clase ''Convenio''; y que el tipo de proceso ingresado sea un valor permitido (1 al 5). Devuelve un listado de errores por línea indicando la columna con problema, el mensaje de validación y el valor incorrecto, permitiendo al usuario corregir el archivo antes de la carga definitiva de conceptos de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveManualConcepts';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveManualConcepts';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida fila por fila un archivo XML de carga masiva de conceptos manuales de nómina y devuelve un listado de errores con línea, columna, mensaje y valor inválido.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de entrada debe ser un XML con estructura /Data/Row conteniendo los nodos Process, Nit, InternalCode, Code, QuoteValue, PaidEndContract, QuoteNumber, PaidFormat y Description.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran procesos válidos los valores numéricos 1, 2, 3, 4 o 5.; La clase de concepto ''041'' está vetada para carga masiva manual (corresponde a Convenio).; La validación contractual usa el contrato más reciente del empleado (TOP 1 ORDER BY Id DESC).; El procedimiento es de solo lectura: no realiza INSERT/UPDATE/DELETE sobre las tablas consultadas.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; NIT; Código interno; Contrato laboral; Concepto de nómina; Clase de concepto Convenio; Proceso de nómina; Carga masiva de conceptos manuales', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve un conjunto de filas (Linea, Mensaje, Columna, Valor) con los errores detectados, ordenado por línea.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Columna numérica (Process, QuoteValue, PaidEndContract, QuoteNumber, PaidFormat) cuyo TRY_CONVERT a NUMERIC retorna NULL → Reporta error ''Caracteres inválidos'' en esa columna; si El Nit no corresponde a un ThirdParty con Employee asociado, o el InternalCode no coincide con un Employee, o el último contrato del empleado tiene Status<>1 y Valid<>1, o no existe contrato → Reporta ''El empleado no está activo, no existe, no tiene NIT ni código interno, o no tiene contrato.'' en columna ''Cédula''; si El Code del XML no existe en Payroll.Concept → Reporta ''El código del concepto no existe'' en columna ''Código Concepto''; si El Concept.ConceptClass del código ingresado es ''041'' (Convenio) → Reporta ''La clase del concepto es Convenio'' en columna ''Clase Concepto''; si Process convertido a NUMERIC(9,0) no está en (1,2,3,4,5) → Reporta ''El Proceso no existe'' en columna ''Proceso''', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Contract; Payroll.Concept', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveManualConcepts';
-- GO
