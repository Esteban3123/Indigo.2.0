
CREATE VIEW [dbo].[VistaLetra]
AS
SELECT        A.EmployeeId, dbo.GetLetter(1) AS 'Numero 1'
FROM            Payroll.Schedule AS A INNER JOIN
                         Payroll.Employee AS B ON A.EmployeeId = B.Id INNER JOIN
                         Common.ThirdParty AS C ON B.ThirdPartyId = C.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que cruza la programación de turnos laborales (Payroll.Schedule) con el maestro de empleados (Payroll.Employee) y los datos del tercero asociado (Common.ThirdParty), obteniendo para cada empleado programado su identificador y el valor de la letra o concepto número 1 calculado por la función GetLetter. Sirve como base para reportes de nómina o liquidación que requieren representar en letra (texto escrito) un valor numérico asociado al empleado, como montos de pago o saldos. Une información laboral, contractual e identidad del trabajador en una sola consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VistaLetra';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VistaLetra';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los empleados con programación de nómina vinculados a un tercero, agregando una letra calculada por la función dbo.GetLetter(1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la función escalar dbo.GetLetter.; Cada empleado en Payroll.Schedule debe tener correspondencia en Payroll.Employee y este a su vez en Common.ThirdParty.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone empleados que tienen programación en Payroll.Schedule y un tercero asociado en Common.ThirdParty (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'empleado; programación/turno laboral; tercero', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Schedule: Devuelve una fila por cada registro de Payroll.Schedule cuyo empleado existe en Payroll.Employee y cuyo tercero existe en Common.ThirdParty, incluyendo la letra obtenida de dbo.GetLetter(1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetLetter', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Schedule; Payroll.Employee; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
