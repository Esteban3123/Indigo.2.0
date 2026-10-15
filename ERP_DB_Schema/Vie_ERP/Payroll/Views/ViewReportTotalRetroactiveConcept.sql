CREATE VIEW [Payroll].[ViewReportTotalRetroactiveConcept]
AS
SELECT
	D.Id AS IdRetoractive, 
	PG.Code AS GroupCode, 
	PG.[Name] AS GroupName, 
	C.InitialDateRetroactive, 
	CONC.Id, 
	CONC.Code, 
	CONC.[Name], 
	CONC.ConceptType, 
	CONC.ConceptClass, 
	C.IdEmployee, 
	D.ValueConceptWithRetroactive AS [Value],
	fUnit.BranchOfficeId
FROM Payroll.RetroactiveC AS C 
JOIN Payroll.RetroactiveD AS D ON C.Id = D.IdRetroactiveC 
JOIN Payroll.Concept AS CONC ON D.IdConcept = CONC.Id 
JOIN Payroll.[Group] AS PG ON C.IdGroup = PG.Id
JOIN Payroll.[Contract] cntrc ON cntrc.Id = C.IdContract
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = cntrc.FunctionalUnitId