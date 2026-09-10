CREATE VIEW [dbo].[DatosNominaEmpleados]
AS
SELECT b.Nit, b.Name AS NombreCompleto, a.JobBondingDate AS InicioContrato, a.ContractEndingDate AS FinalContrato, a.BasicSalary AS SalarioBasico, a.BankAccountNumber AS NumeroCuenta, D.Name AS Banco
FROM   Payroll.Employee AS C INNER JOIN
             Common.ThirdParty AS b ON C.ThirdPartyId = b.Id INNER JOIN
             Payroll.Contract AS a ON C.Id = a.EmployeeId INNER JOIN
             Payroll.Bank AS D ON a.BankId = D.Id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta consolidada que aplana la información de nómina de empleados combinando datos de identificación fiscal del tercero (NIT y nombre), fechas de inicio y fin del contrato laboral, salario básico, número de cuenta bancaria y nombre del banco. Orientada a procesos de liquidación y dispersión de nómina, facilitando la obtención en un solo resultado de los datos necesarios para pagos bancarios por empleado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'DatosNominaEmpleados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'DatosNominaEmpleados';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los datos básicos de nómina de cada empleado (identificación, contrato, salario y cuenta bancaria) consolidando empleado, tercero, contrato y banco.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'DatosNominaEmpleados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado debe estar asociado a un tercero existente en Common.ThirdParty.; El empleado debe tener al menos un contrato registrado en Payroll.Contract.; El contrato debe tener un banco asignado existente en Payroll.Bank.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'DatosNominaEmpleados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen empleados que tengan tercero, contrato y banco registrados (todos los joins son INNER).; Si un empleado tiene varios contratos, aparecerá tantas veces como contratos válidos posea.; El nombre y NIT provienen siempre del tercero, no del empleado.; El nombre del banco proviene del catálogo Payroll.Bank vinculado al contrato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'DatosNominaEmpleados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Tercero; Contrato laboral; Salario básico; Cuenta bancaria; Banco; Nómina; NIT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'DatosNominaEmpleados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por cada combinación empleado-contrato vinculada con tercero y banco mediante INNER JOIN; empleados sin contrato, sin tercero o sin banco quedan excluidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'DatosNominaEmpleados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.Bank', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'DatosNominaEmpleados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'DatosNominaEmpleados';
GO
