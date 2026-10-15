
CREATE VIEW [Payroll].[vConceptLiquidation]
AS
	WITH cteActiveEmployee AS(
		SELECT
			cntrc.Id AS ContractID,
			emply.Id AS EmployeeID, 
			tParty.[Name] AS Employee,
			tParty.Nit AS Cédula, 
			grp.[Name] AS Grupo, 
			pstn.[Name] AS Cargo,
			fUnit.[Name] AS UnidadFuncional,
			cntrc.BasicSalary AS Salario
		FROM Common.ThirdParty AS tParty 
		JOIN Payroll.Employee AS emply ON emply.ThirdPartyId = tParty.Id 
		JOIN Payroll.[Contract] AS cntrc ON cntrc.EmployeeId = emply.Id 
		JOIN Payroll.Position AS pstn ON cntrc.PositionId = pstn.Id 
		JOIN Payroll.[Group] AS grp ON cntrc.GroupId = grp.Id
		JOIN Payroll.FunctionalUnit AS fUnit ON cntrc.FunctionalUnitId = fUnit.Id

	), cteEmployeeConcept AS(
		SELECT
			cteActiveEmployee.ContractID,
			cteActiveEmployee.EmployeeID,
			cteActiveEmployee.Employee,
			cteActiveEmployee.Cédula,
			cteActiveEmployee.Cargo,
			cteActiveEmployee.Grupo,
			cteActiveEmployee.UnidadFuncional,
			cteActiveEmployee.Salario,
			cncpt.Id AS ConceptID,
			cncpt.[Name] AS Concept,
			lqd.ConceptTotalValue,
			lqd.PayrollDate
		FROM cteActiveEmployee 
		JOIN Payroll.Liquidation lq ON lq.ContractId = cteActiveEmployee.ContractID
		JOIN Payroll.LiquidationDetail lqd ON lqd.PayrollId = lq.Id
		JOIN Payroll.Concept cncpt ON cncpt.Id = lqd.ConceptId
	)
	
	SELECT
		ROW_NUMBER() OVER(ORDER BY(SELECT 0)) AS ID
		,ContractID	
		,EmployeeID	
		,Employee
		,Cédula
		,Cargo
		,Grupo
		,UnidadFuncional
		,Salario
		,ConceptID	
		,Concept	
		,ConceptTotalValue	
		,PayrollDate
	FROM cteEmployeeConcept