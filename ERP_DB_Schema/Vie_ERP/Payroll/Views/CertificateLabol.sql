CREATE VIEW [Payroll].[CertificateLabol]
AS SELECT 
	CONCAT(p.FirstName,' ', p.SecondName,' ', p.FirstLastName,' ', p.SecondLastName)AS 'Nombre Completo',
	CASE 
		WHEN p.IdentificationType = 0 THEN 'CEDULA DE CIUDADANIA'
		WHEN p.IdentificationType = 1 THEN 'CEDULA DE EXTRANGERIA'
		WHEN p.IdentificationType = 2 THEN 'TARJETA DE IDENTIDAD'
		WHEN p.IdentificationType = 3 THEN 'REGISTRO CIVIL'
		WHEN p.IdentificationType = 4 THEN 'PASAPORTE'
		WHEN p.IdentificationType = 5 THEN 'ADULTO SIN IDENTIFICACION'
		WHEN p.IdentificationType = 6 THEN 'MENOR SIN IDENTIFICACION'
		WHEN p.IdentificationType = 7 THEN 'NIT'
	END AS 'Tipo documento',
	c.JobBondingDate AS 'Fecha vinculación', 
	ps.[Name] AS 'Cargo',
	c.BasicSalary AS 'Salario',
	ct.[Name] AS 'Tipo contrato'
FROM Common.ThirdParty t
JOIN Common.Person p ON t.PersonId = p.Id
JOIN Payroll.Employee e ON t.Id = e.ThirdPartyId
JOIN Payroll.[Contract] c ON e.Id = c.EmployeeId
JOIN Payroll.Position ps ON ps.Id = c.PositionId
JOIN Payroll.ContractType ct ON ct.Id = c.ContractTypeId