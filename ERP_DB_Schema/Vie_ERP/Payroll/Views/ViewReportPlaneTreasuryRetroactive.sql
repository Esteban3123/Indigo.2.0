CREATE VIEW [Payroll].[ViewReportPlaneTreasuryRetroactive]
AS
SELECT RC.Id,C.BankAccountNumber, B.Name as BankName, TP.Nit, TP.Name as ThirdPartyName, 
(SELECT SUM(RD.ValueConceptWithRetroactive) from Payroll.RetroactiveD RD, Payroll.Concept CONC WHERE CONC.Id = RD.IdConcept and RD.IdRetroactiveC = RC.ID AND CONC.ConceptType = 1) - (SELECT SUM(RD.ValueConceptWithRetroactive) from Payroll.RetroactiveD RD, Payroll.Concept CONC WHERE CONC.Id = RD.IdConcept and RD.IdRetroactiveC = RC.ID AND CONC.ConceptType = 2) as TotalRetroactiveValue,
--RC.TotalRetroactiveValue, 
RC.InitialDateRetroactive, G.Code as CodeGroup, G.Name as NameGroup 
FROM Payroll.RetroactiveC RC,
Payroll.[Group] G, 
Payroll.[Contract] C, 
Common.ThirdParty TP, 
Payroll.Employee E, 
Payroll.Bank B
WHERE RC.IdContract = C.Id and RC.IdEmployee = E.Id AND E.ThirdPartyId = TP.Id AND C.BankId = B.Id AND G.Id = RC.IdGroup