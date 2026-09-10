
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de incrementos salariales aplicados a empleados de nómina. Consolida, para cada ajuste de salario, la información del empleado (NIT y nombre), el salario base anterior, el nuevo salario y el porcentaje de aumento, junto con el cargo, grupo de nómina, unidad funcional y sede a los que pertenece el empleado al momento del incremento. Sirve para consultar el historial de aumentos salariales, auditar modificaciones de remuneración y generar reportes de gestión de nómina por área, sede o grupo de liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportIncreaseSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportIncreaseSalary';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los incrementos salariales con la información descriptiva del empleado, su tercero, cargo, unidad funcional, sucursal y grupo de nómina, para generar reportes de aumentos de salario.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncreaseSalary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada registro de incremento salarial debe tener referencias válidas a empleado, cargo, unidad funcional y grupo; de lo contrario es excluido del reporte.; El empleado debe estar asociado a un tercero (Common.ThirdParty) para aparecer en el reporte.; La unidad funcional debe tener una sucursal asociada para que el incremento se incluya.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncreaseSalary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan incrementos salariales cuyo empleado, cargo, unidad funcional, grupo y sucursal asociados existen vigentes (todos los joins son INNER JOIN).; La sucursal reportada se obtiene a través de la unidad funcional vinculada al incremento, no directamente del incremento.; La identidad y nombre del empleado provienen del tercero (Common.ThirdParty) asociado al empleado, no del propio registro de empleado.; La fecha de creación se expone truncada a fecha (sin componente de hora).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncreaseSalary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Incremento salarial; Empleado; Tercero; Cargo; Unidad funcional; Sucursal; Grupo de nómina; Salario básico; Nuevo salario; Porcentaje de incremento', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncreaseSalary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.IncreaseSalary: Devuelve un registro por cada incremento salarial que tenga empleado, tercero, cargo, unidad funcional, sucursal y grupo válidos, exponiendo salario básico, nuevo salario, porcentaje de aumento y datos descriptivos asociados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncreaseSalary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncreaseSalary; Payroll.Employee; Common.ThirdParty; Payroll.Position; Payroll.FunctionalUnit; Payroll.BranchOffice; Payroll.Group', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncreaseSalary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncreaseSalary';
GO
