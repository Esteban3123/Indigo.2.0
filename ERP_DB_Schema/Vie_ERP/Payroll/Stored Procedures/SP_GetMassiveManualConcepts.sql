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
				LEFT JOIN Payroll.Employee E ON NULLIF(LTRIM(RTRIM(cteXML.InternalCode)), '') IS NOT NULL AND E.InternalCode = cteXML.InternalCode
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