-- =============================================
-- Author:		Iván Ospina
-- Create date: 2019-09-30
-- Description:	Obtiene el nombre del empleado y el concepto para mostrar en el Grid
-- =============================================
CREATE PROCEDURE [Payroll].[SP_GetMassiveManualConcepts]
	@XMLObj XML 
AS
BEGIN
	;WITH cteXML AS(
		SELECT
			t.x.value('Process[1]','TINYINT') AS Process,
			t.x.value('Nit[1]','VARCHAR(20)') AS Nit,
			t.x.value('InternalCode[1]','VARCHAR(32)') AS InternalCode,
			t.x.value('Code[1]','VARCHAR(4)') AS Code,
			t.x.value('QuoteValue[1]','NUMERIC(18,2)') AS QuoteValue,
			t.x.value('PaidEndContract[1]','BIT') AS PaidEndContract,
			t.x.value('QuoteNumber[1]','TINYINT') AS QuoteNumber,
			t.x.value('PaidFormat[1]','TINYINT') AS PaidFormat,
			t.x.value('Description[1]','VARCHAR(200)') AS [Description]
		FROM @XMLObj.nodes('/Data/Row') t(x)
	), cte AS(
		SELECT
			IIF(cteXML.Nit ='',cteXML.InternalCode,cteXML.Nit) Nit,
			IIF(cteXML.Nit ='',tParty2.[Name],tParty.[Name]) AS EmployeeName,
			cteXML.Process,
			CASE cteXML.PaidEndContract
				WHEN 1 THEN 'Fijo'
				ELSE 'Variable'
			END AS PaidEndContract,
			cteXML.QuoteNumber,
			CAST(CG.NextDateLiquidation AS DATE) AS NextDateLiquidation,
			cteXML.Code,
			concept.[Name] AS ConceptName,
			cteXML.QuoteValue
		FROM cteXML
		LEFT JOIN Common.ThirdParty tParty ON tParty.Nit = cteXML.Nit
		LEFT JOIN Payroll.Employee emply ON emply.ThirdPartyId = tParty.Id
		LEFT JOIN Payroll.Employee E ON cteXML.InternalCode = E.InternalCode
		LEFT JOIN Common.ThirdParty tParty2 ON tParty2.ID = E.ThirdPartyId
		LEFT JOIN Payroll.Concept concept ON concept.Code = cteXML.Code
		OUTER APPLY(
			SELECT TOP 1
				grp.NextDateLiquidation,
				cntrc.[Status],
				cntrc.Valid
			FROM Payroll.[Contract] cntrc
			JOIN Payroll.[Group] grp ON grp.Id = cntrc.GroupId
			WHERE cntrc.EmployeeId = emply.Id OR cntrc.EmployeeId = E.Id
			ORDER BY cntrc.Id DESC
		) CG
		WHERE CG.[Status] = 1
			AND CG.Valid = 1
	)

	SELECT
		Nit
		,EmployeeName
		,Process
		,PaidEndContract
		,QuoteNumber
		,NextDateLiquidation
		,Code
		,ConceptName
		,QuoteValue
	FROM cte	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que recibe un lote masivo de conceptos manuales en formato XML (filas con NIT o código interno del empleado, código de concepto, valor, número de cuotas y formato de pago) y retorna la información enriquecida para mostrar en una grilla de carga masiva. Para cada fila del XML, resuelve el nombre del empleado consultando el tercero correspondiente (por NIT o código interno), obtiene el nombre del concepto de nómina y recupera la próxima fecha de liquidación del grupo al que pertenece el contrato activo y vigente del empleado. Se usa en la pantalla de ingreso masivo de novedades manuales de nómina (devengados o deducciones) antes de su grabación definitiva.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetMassiveManualConcepts';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetMassiveManualConcepts';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resuelve, a partir de un XML de filas de conceptos manuales masivos, el nombre del empleado y del concepto de nómina junto con datos del último contrato vigente, para alimentar una grilla de revisión.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro XML debe seguir la estructura /Data/Row con nodos Process, Nit, InternalCode, Code, QuoteValue, PaidEndContract, QuoteNumber, PaidFormat y Description.; El empleado debe poseer al menos un contrato cuyo Status=1 y Valid=1 para que la fila aparezca en el resultado.; El Code del concepto debe existir en Payroll.Concept para mostrar nombre del concepto (de lo contrario queda NULL por LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el contrato más reciente del empleado (TOP 1 ORDER BY cntrc.Id DESC) para evaluar estado, validez y próxima fecha de liquidación.; La búsqueda del empleado se hace por dos vías alternativas: vía Nit del tercero o vía InternalCode del empleado, nunca ambas a la vez en la salida.; NextDateLiquidation se reporta como DATE (sin hora), tomado del Group del contrato más reciente.; El procedimiento es de solo lectura: no modifica datos.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Concepto de nómina; Contrato laboral; Grupo de nómina; Próxima fecha de liquidación; Cuota / valor de cuota; Pago fin de contrato (Fijo/Variable); Tercero (Nit); Conceptos manuales masivos', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada Row del XML cuyo último contrato (mayor Id) tenga Status=1 y Valid=1, con Nit/InternalCode, nombre del empleado, proceso, modalidad de pago fin de contrato (Fijo/Variable), número de cuotas, próxima fecha de liquidación del grupo, código y nombre del concepto y valor de la cuota.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cteXML.Nit = '''' → Se usa InternalCode como identificador y el nombre se toma del ThirdParty asociado al Employee localizado por InternalCode (tParty2). else Se usa el Nit como identificador y el nombre se toma del ThirdParty con ese Nit (tParty).; si PaidEndContract = 1 → Se etiqueta como ''Fijo''. else Se etiqueta como ''Variable''.; si CG.Status = 1 AND CG.Valid = 1 → La fila se incluye en el resultado final. else La fila se descarta (no aparece en la grilla).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Concept; Payroll.Contract; Payroll.Group', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveManualConcepts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetMassiveManualConcepts';
-- GO
