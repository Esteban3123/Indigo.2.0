
CREATE VIEW [Payroll].[ViewReportPaymentRelationshipByConcept]
AS
SELECT 
	ROW_NUMBER() OVER (ORDER BY tp.Nit) AS Id,
	g.[Name] AS NombreGrupo, 
	tp.Nit AS Cedula, 
	tp.[Name] AS NombreEmpleado, 
	c.Code AS CodigoConcepto, 
	C.[Name] AS NombreConcepto, 
	RD.ValueConceptWithRetroactive AS Valor,
	RC.InitialDateRetroactive AS Fecha,
	FU.Code AS CodeFunctionalUnit,
	G.Code AS CodeGroup,
	CC.Code AS CodeCostCenter,
	CC.[Name] AS NameCostCenter,
	c.Id AS IdConcept,
	E.Id AS EmployeeId,
	FU.BranchOfficeId
FROM Payroll.RetroactiveC AS RC
JOIN  Payroll.Employee AS E ON E.Id = RC.IdEmployee
JOIN  Common.ThirdParty AS TP ON TP.Id = E.ThirdPartyId
JOIN Payroll.[Contract] AS CONT ON CONT.Id = RC.IdContract
JOIN Payroll.[Group] AS G ON G.Id = RC.IdGroup
JOIN Payroll.RetroactiveD RD ON RC.Id = RD.IdRetroactiveC
JOIN Payroll.Concept C ON C.Id = RD.IdConcept
JOIN Payroll.FunctionalUnit AS FU ON FU.Id = CONT.FunctionalUnitId
LEFT JOIN Payroll.CostCenter AS CC ON CC.Id = E.CostCenterId