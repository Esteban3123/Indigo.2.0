
CREATE VIEW [Payroll].[ViewReportPaymentRelationshipByEmployee]
AS
SELECT 
	ROW_NUMBER() OVER (ORDER BY tp.Nit) AS Id,
	g.[Name] as NombreGrupo, 
	tp.Nit as Cedula, 
	tp.[Name] as NombreEmpleado, 
	RC.InitialDateRetroactive AS Fecha, 
	CONT.BasicSalary AS SalarioBasico, 
	30 as DiasNomina, 
	30 as DiasLab,  
	(SELECT SUM(RDOPC.ValueConceptWithRetroactive) from Payroll.Concept C, Payroll.RetroactiveD RDOPC WHERE RDOPC.IdConcept = C.Id and RDOPC.IdRetroactiveC = RC.Id AND C.ConceptType = 1) AS TotalDevengado,
	(SELECT SUM(RDOPC.ValueConceptWithRetroactive) from Payroll.Concept C, Payroll.RetroactiveD RDOPC WHERE RDOPC.IdConcept = C.Id and RDOPC.IdRetroactiveC = RC.Id AND C.ConceptType = 2) as TotalDeducido,
	RC.TotalRetroactiveValue AS TotalPagado,
	FU.Code AS CodeFunctionalUnit,
	G.Code AS CodeGroup,
	CC.Code AS CodeCostCenter,
	CC.[Name] AS NameCostCenter,
	E.Id AS EmployeeId,
	FU.BranchOfficeId
FROM Payroll.RetroactiveC AS RC
JOIN  Payroll.Employee AS E ON E.Id = RC.IdEmployee
JOIN  Common.ThirdParty AS TP ON TP.Id = E.ThirdPartyId
JOIN Payroll.[Contract] AS CONT ON CONT.Id = RC.IdContract
JOIN Payroll.[Group] AS G ON G.Id = RC.IdGroup
JOIN Payroll.FunctionalUnit AS FU ON FU.Id = CONT.FunctionalUnitId
LEFT JOIN Payroll.CostCenter AS CC ON CC.Id = E.CostCenterId