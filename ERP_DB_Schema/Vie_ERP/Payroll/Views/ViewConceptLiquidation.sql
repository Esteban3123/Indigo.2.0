-- =============================================
-- Author:		Iván Dario Ospina Acosta
-- Create date: 2019-12-03
-- Description:	
-- =============================================

CREATE VIEW [Payroll].[ViewConceptLiquidation]
AS
	WITH cteActiveEmployee AS(
		SELECT
			cntrc.Id AS ContractID,
			emply.Id AS EmployeeID, 
			tParty.[Name] AS Empleado,
			tParty.Nit AS Cédula, 
			grp.Code AS GroupCode,
			grp.[Name] AS Grupo, 
			pstn.[Name] AS Cargo,
			fUnit.[Name] AS UnidadFuncional,
			cntrc.BasicSalary AS Salario
		FROM Common.ThirdParty AS tParty 
		JOIN Payroll.Employee AS emply ON emply.ThirdPartyId = tParty.Id
		CROSS APPLY(
			SELECT TOP 1
				cntrc.Id,
				cntrc.EmployeeId,
				cntrc.PositionId,
				cntrc.GroupId,
				cntrc.FunctionalUnitId,
				cntrc.BasicSalary
			FROM Payroll.[Contract] cntrc
			WHERE cntrc.EmployeeId = emply.Id
			ORDER BY cntrc.Id DESC
		) cntrc
		JOIN Payroll.Position AS pstn ON cntrc.PositionId = pstn.Id 
		JOIN Payroll.[Group] AS grp ON cntrc.GroupId = grp.Id
		JOIN Payroll.FunctionalUnit AS fUnit ON cntrc.FunctionalUnitId = fUnit.Id

	), cteEmployeeConcept AS(
		SELECT
			cteActiveEmployee.ContractID,
			cteActiveEmployee.EmployeeID,
			cteActiveEmployee.Empleado,
			cteActiveEmployee.Cédula,
			cteActiveEmployee.Cargo,
			cteActiveEmployee.GroupCode,
			cteActiveEmployee.Grupo,
			cteActiveEmployee.UnidadFuncional,
			cteActiveEmployee.Salario,
			cncpt.Id AS ConceptID,
			cncpt.Code AS ConceptCode,
			cncpt.[Name] AS Concept,
			cncpt.ConceptType,
			lqd.ConceptTotalValue,
			lqd.PayrollDate,
			lq.RegisterStatus
		FROM cteActiveEmployee 
		JOIN Payroll.Liquidation lq ON lq.ContractId = cteActiveEmployee.ContractID
		JOIN Payroll.LiquidationDetail lqd ON lqd.PayrollId = lq.Id
		JOIN Payroll.Concept cncpt ON cncpt.Id = lqd.ConceptId
	)
	
	SELECT
		ROW_NUMBER() OVER(ORDER BY(SELECT 0)) AS ID
		,ContractID	
		,EmployeeID	
		,Empleado
		,Cédula
		,Cargo
		,GroupCode
		,Grupo
		,UnidadFuncional
		,Salario
		,ConceptID	
		,ConceptCode
		,Concept	
		,ConceptTotalValue
		,ConceptType
		,PayrollDate
		,RegisterStatus
	FROM cteEmployeeConcept