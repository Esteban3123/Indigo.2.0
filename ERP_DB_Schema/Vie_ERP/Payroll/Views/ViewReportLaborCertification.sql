



CREATE VIEW [Payroll].[ViewReportLaborCertification]
AS
SELECT
em.Id,
c.[Status],
co.Name AS 'CompanyName',
p.FirstLastName, 
p.SecondLastName, 
p.FirstName, 
p.SecondName, 
p.IdentificationNumber, 
ci.Name AS 'CityName', 
c.JobBondingDate, 
po.Name AS 'Rol', 
c.BasicSalary,
ct.Name,
(SELECT top 1 thi.Name FROM Payroll.Employee AS emp INNER JOIN Common.ThirdParty AS thi ON thi.Id = emp.ThirdPartyId WHERE emp.Id = bo.PayrollManagerId) AS 'HumanResource',
(SELECT top 1 pos.Name FROM Payroll.[Contract] AS con INNER JOIN Payroll.Position AS pos ON pos.Id = con.PositionId INNER JOIN Payroll.Employee AS emp ON emp.Id = con.EmployeeId INNER JOIN Common.ThirdParty AS thir ON thir.Id = emp.ThirdPartyId WHERE emp.Id = bo.PayrollManagerId AND con.[Status] = '1') AS 'ProfessionHumanResource'
, jt.Code + ' - ' + jt.[Name] as JobBondingType
FROM 
Payroll.[Contract] AS c
INNER JOIN Payroll.Employee AS em ON em.Id = c.EmployeeId
INNER JOIN Common.ThirdParty AS thi ON thi.Id = em.ThirdPartyId
INNER JOIN Common.Person AS p ON p.Id = thi.PersonId
LEFT JOIN Common.City AS ci ON ci.Id = p.IdentificacionCityId
INNER JOIN Payroll.Position AS po ON po.Id = c.PositionId
INNER JOIN Payroll.ContractType AS ct ON ct.Id = c.ContractTypeId
inner join Payroll.JobBondingType as jt on jt.Id = ct.JobBondingTypeId
INNER JOIN Payroll.[Group] AS g ON g.Id = c.GroupId
INNER JOIN Payroll.Company AS co ON co.Id = g.CompanyId
inner join Payroll.FunctionalUnit as fu on fu.Id = c.FunctionalUnitId
INNER JOIN Payroll.BranchOffice AS bo on bo.Id = fu.BranchOfficeId
WHERE c.[Status] = '1' and c.Valid =1