

CREATE VIEW [Payroll].[VistaLetra]
AS
SELECT        A.EmployeeId, dbo.GetLetter(1) AS 'Numero 1'
FROM            Payroll.Schedule AS A INNER JOIN
                         Payroll.Employee AS B ON A.EmployeeId = B.Id INNER JOIN
                         Common.ThirdParty AS C ON B.ThirdPartyId = C.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que cruza la programación de turnos laborales de cada empleado con su información maestra de nómina y los datos del tercero asociado (persona o entidad), para obtener el identificador del empleado junto con el resultado de la función GetLetter(1), que genera o recupera la representación en letra del número 1 (posiblemente para encabezados o formatos de nómina). Integra las tablas de horario (Schedule), empleado (Employee) y terceros (ThirdParty) del módulo de Nómina. Su propósito principal es apoyar la generación de reportes o documentos de nómina que requieren valores numéricos expresados en letras, como liquidaciones, comprobantes de pago o planillas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'VistaLetra';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'VistaLetra';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, por cada empleado con turno programado, una letra fija obtenida vía función auxiliar, vinculando su registro maestro y el tercero asociado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de empleados en Payroll.Employee con ThirdPartyId válido en Common.ThirdParty.; Existencia de registros en Payroll.Schedule asociados a EmployeeId.; Disponibilidad de la función escalar dbo.GetLetter.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen empleados que tengan al menos un registro en Payroll.Schedule y un tercero asociado en Common.ThirdParty (los INNER JOIN excluyen huérfanos).; La columna ''Numero 1'' siempre resulta de invocar dbo.GetLetter con el argumento literal 1, por lo que es constante en todas las filas.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado de nómina; Programación de turnos; Tercero', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.VistaLetra: Devuelve EmployeeId junto a la letra resultante de dbo.GetLetter(1) solo para empleados con coincidencia en Schedule, Employee y ThirdParty (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetLetter', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Schedule; Payroll.Employee; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VistaLetra';
GO
