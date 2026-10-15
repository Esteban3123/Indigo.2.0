



CREATE VIEW [Payroll].[VSurcharges]
AS
WITH cteA AS(
	SELECT DISTINCT 
		TP.Nit, 
		TP.[Name] AS Nombre, 
		C.Code AS CodigoConcepto, 
		C.[Name] AS NombreConcepto, 
		SUM(LD.TotalNumberHours) AS TotalHoras,
		LD.ConceptTotalValue AS ValorTotal, 
		L.PayrollDateLiquidated AS Fecha, 
		L.RegisterStatus AS Estado, 
		L.EmployeeId AS Empleado, 
		L.ContractId AS ContratoP, 
		G.Code AS GrupoCod, 
		G.[Name] AS GrupoN
	FROM Common.ThirdParty AS TP 
	JOIN Payroll.Employee AS E ON TP.Id = E.ThirdPartyId 
	JOIN Payroll.Liquidation AS L ON L.EmployeeId = E.Id
	JOIN Payroll.LiquidationDetail AS LD ON LD.PayrollId = L.Id
	JOIN Payroll.Concept AS C ON LD.ConceptId = C.Id 
	JOIN Payroll.[Group] AS G ON L.GroupId = G.Id
	WHERE (LD.TotalNumberHours > 0) AND (C.Code <> '001')
	GROUP BY TP.Nit, TP.[Name], C.Code, C.[Name], LD.ConceptTotalValue, L.PayrollDateLiquidated, L.RegisterStatus, L.EmployeeId, L.ContractId, G.Code, G.[Name]
	--UNION ALL
	--SELECT DISTINCT 
	--	TP.Nit, 
	--	TP.[Name], 
	--	C.Code, 
	--	C.[Name] AS Expr1, 
	--	MC.QuoteValue AS TotalHoras, 
	--	LD.ConceptTotalValue AS ValorTotal, 
	--	L.PayrollDateLiquidated AS Fecha, 
	--	L.RegisterStatus AS Estado, 
	--	L.EmployeeId AS Empleado, 
	--	L.ContractId AS ContratoP, 
	--	G.Code AS GrupoCod, 
	--	G.[Name] AS GrupoN
	--FROM Payroll.Employee AS E 
	--JOIN Common.ThirdParty AS TP ON E.ThirdPartyId = TP.Id 
	--JOIN Payroll.ManualConcepts AS MC ON E.Id = MC.EmployeeId 
	--JOIN Payroll.Concept AS C ON MC.ConceptId = C.Id 
	--JOIN Payroll.LiquidationDetail AS LD ON MC.ConceptId = LD.ConceptId AND C.Id = LD.ConceptId 
	--JOIN Payroll.Liquidation AS L ON LD.PayrollId = L.Id AND E.Id = L.EmployeeId AND MONTH(MC.InitialDate) = MONTH(L.PayrollDateLiquidated) AND YEAR(MC.InitialDate) = YEAR(L.PayrollDateLiquidated) 
	--JOIN Payroll.[Group] AS G ON L.GroupId = G.Id
	--WHERE (C.Code <> '001') AND (C.ConceptClass IN ('001', '012', '013')) AND (MC.State <> 3)
	--GROUP BY 
	--	TP.Nit, 
	--	TP.[Name], 
	--	C.Code, 
	--	C.[Name], 
	--	MC.QuoteValue, 
	--	LD.ConceptTotalValue, 
	--	L.PayrollDateLiquidated, 
	--	L.RegisterStatus, 
	--	L.EmployeeId, 
	--	L.ContractId, 
	--	G.Code, 
	--	G.[Name]
)

SELECT
	Nit + CodigoConcepto + cast(ContratoP as varchar) + convert(varchar, Fecha, 120) as RowString, 
	Nit as Cedula, 
	Nombre as NombreEmpleado , 
	CodigoConcepto, 
	NombreConcepto, 
	sum(TotalHoras) as TotalHoras,
	ValorTotal, 
	Fecha, 
	Estado, 
	Empleado, 
	ContratoP, 
	GrupoCod, 
	GrupoN,
	fUnit.BranchOfficeId
FROM cteA
OUTER APPLY(
	SELECT TOP 1
		fUnit.BranchOfficeId
	FROM Payroll.[Contract] cntrc
	JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = cntrc.FunctionalUnitId
	WHERE cntrc.Id = cteA.ContratoP
	ORDER BY cntrc.Id DESC
) fUnit
GROUP BY 
	Nit, 
	Nombre,
	CodigoConcepto, 
	NombreConcepto, 
	ValorTotal, 
	Fecha, 
	Estado, 
	Empleado, 
	ContratoP, 
	GrupoCod, 
	GrupoN,
	fUnit.BranchOfficeId