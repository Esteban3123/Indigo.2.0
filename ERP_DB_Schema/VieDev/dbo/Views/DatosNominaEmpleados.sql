CREATE VIEW [dbo].[DatosNominaEmpleados]
AS
SELECT b.Nit, b.Name AS NombreCompleto, a.JobBondingDate AS InicioContrato, a.ContractEndingDate AS FinalContrato, a.BasicSalary AS SalarioBasico, a.BankAccountNumber AS NumeroCuenta, D.Name AS Banco
FROM   Payroll.Employee AS C INNER JOIN
             Common.ThirdParty AS b ON C.ThirdPartyId = b.Id INNER JOIN
             Payroll.Contract AS a ON C.Id = a.EmployeeId INNER JOIN
             Payroll.Bank AS D ON a.BankId = D.Id