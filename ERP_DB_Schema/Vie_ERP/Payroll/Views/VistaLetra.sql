

CREATE VIEW [Payroll].[VistaLetra]
AS
SELECT        A.EmployeeId, dbo.GetLetter(1) AS 'Numero 1'
FROM            Payroll.Schedule AS A INNER JOIN
                         Payroll.Employee AS B ON A.EmployeeId = B.Id INNER JOIN
                         Common.ThirdParty AS C ON B.ThirdPartyId = C.Id