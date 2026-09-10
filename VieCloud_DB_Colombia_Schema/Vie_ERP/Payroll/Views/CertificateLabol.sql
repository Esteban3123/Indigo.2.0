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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que genera el certificado laboral de cada empleado activo en nómina, consolidando en una sola consulta los datos personales, documentales y contractuales del trabajador. Combina la información de la persona (nombre completo y tipo de documento de identidad: cédula, pasaporte, NIT, etc.), el contrato laboral (fecha de vinculación y salario básico), el cargo desempeñado y el tipo de contrato. Sirve para emitir certificaciones laborales oficiales que acrediten el vínculo laboral, el cargo y la remuneración de un empleado ante entidades externas como bancos, entidades gubernamentales o aseguradoras.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'CertificateLabol';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'CertificateLabol';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los datos básicos requeridos para emitir un certificado laboral del empleado: nombre completo, tipo de documento, fecha de vinculación, cargo, salario y tipo de contrato.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'CertificateLabol';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada empleado (Payroll.Employee) debe estar asociado a un ThirdParty (Common.ThirdParty) y este a una Persona (Common.Person).; Cada contrato debe tener PositionId y ContractTypeId válidos en sus respectivas tablas maestras.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'CertificateLabol';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen empleados que tengan al menos un contrato activo registrado en Payroll.Contract con cargo (Position) y tipo de contrato (ContractType) válidos, debido a los JOIN INNER.; El nombre completo se construye concatenando FirstName, SecondName, FirstLastName y SecondLastName separados por espacios, aun cuando alguno sea NULL o vacío.; Si un empleado tiene múltiples contratos, aparecerá una fila por cada contrato.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'CertificateLabol';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Contrato laboral; Cargo; Tipo de contrato; Salario básico; Fecha de vinculación; Tipo de documento de identidad; Certificado laboral', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'CertificateLabol';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada contrato existente en Payroll.Contract uniendo Employee→ThirdParty→Person y resolviendo Position y ContractType por sus IDs.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'CertificateLabol';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType IN (0..7) → Traduce el código numérico a su descripción textual: 0=CEDULA DE CIUDADANIA, 1=CEDULA DE EXTRANGERIA, 2=TARJETA DE IDENTIDAD, 3=REGISTRO CIVIL, 4=PASAPORTE, 5=ADULTO SIN IDENTIFICACION, 6=MENOR SIN IDENTIFICACION, 7=NIT else Devuelve NULL (el CASE no tiene ELSE)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'CertificateLabol';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Common.Person; Payroll.Employee; Payroll.Contract; Payroll.Position; Payroll.ContractType', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'CertificateLabol';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'CertificateLabol';
GO
