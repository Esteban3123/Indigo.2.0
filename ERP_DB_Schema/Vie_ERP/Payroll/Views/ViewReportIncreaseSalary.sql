
CREATE VIEW [Payroll].[ViewReportIncreaseSalary]
AS
SELECT ISA.Id, E.Id as IdEmployee, TP.Nit as NitEmployee, TP.Name as NameEmployee, ISA.BasicSalary, ISA.NewSalary, ISA.PercentageIncrease, 
G.Code as CodeGroup, G.[Name] as NameGroup, F.Code as CodeFunctionalUnit, F.Name as NameFunctionalUnit,
P.Code as CodePosition, P.Name as NamePosition, BO.Code as CodeBranchOffice, BO.Name as NameBranchOffice, CAST(ISA.CreationDate AS date) AS CreationDate
FROM Payroll.IncreaseSalary ISA
INNER JOIN Payroll.Employee E ON E.Id = ISA.EmployeeId
INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
INNER JOIN Payroll.Position P ON P.Id = ISA.PositionId
INNER JOIN Payroll.FunctionalUnit F ON F.Id = ISA.FunctionalUnitId
INNER JOIN Payroll.BranchOffice BO ON BO.Id = F.BranchOfficeId
INNER JOIN Payroll.[Group] G ON G.Id = ISA.GroupId