

Create VIEW [Payroll].[ViewReportExpeditionCDPAndRPRetroactive]
AS
SELECT ROW_NUMBER() OVER(ORDER BY C.Id ASC) as Row,  C.Id, C.Name, SUM(RD.ValueConceptWithRetroactive) AS 'ConceptTotalValue', ET.Code, C.ConceptType, RC.InitialDateRetroactive
FROM Payroll.RetroactiveD RD, Payroll.RetroactiveC RC, Payroll.Concept C, Payroll.Employee E, Payroll.EmployeeType ET
WHERE 
E.Id = RC.IdEmployee 
AND RC.Id = RD.IdRetroactiveC
AND RD.IdConcept = C.Id
AND ET.Id = E.EmployeeTypeId
GROUP BY C.Name, C.Id, ET.Code, C.ConceptType, RC.InitialDateRetroactive