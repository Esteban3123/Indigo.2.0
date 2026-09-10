CREATE VIEW [Payroll].[ViewReportLiquidationDetaill]
as
select 
l.BasicSalary, 
thi.CreationDate, 
thi.Nit, 
thi.Name as NameThi, 
ld.Id, 
c.Name
from 
Payroll.LiquidationDetail as ld
INNER JOIN Payroll.Liquidation as l on l.Id = ld.PayrollId
INNER JOIN Payroll.Employee as em on em.Id = l.EmployeeId
INNER JOIN Common.ThirdParty as thi on thi.Id = em.ThirdPartyId
INNER JOIN Payroll.Concept as c on c.Id = ld.ConceptId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de liquidación de nómina por empleado, combinando los conceptos liquidados (devengados y deducciones) con los datos del tercero asociado a cada trabajador (NIT, nombre, fecha de creación) y el salario básico de cada liquidación. Une las tablas de detalle de liquidación, liquidación, empleado, tercero y catálogo de conceptos para ofrecer un reporte plano de los conceptos de nómina pagados o descontados a cada persona en un período. Se usa para reportería de nómina, auditoría de liquidaciones y verificación de conceptos aplicados por empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportLiquidationDetaill';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportLiquidationDetaill';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida el detalle de liquidaciones de nómina cruzando cada concepto liquidado con el empleado, su tercero asociado y el concepto de nómina correspondiente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLiquidationDetaill';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle de liquidación debe tener una liquidación padre existente (PayrollId); Cada liquidación debe estar asociada a un empleado existente (EmployeeId); Cada empleado debe tener un tercero registrado en Common.ThirdParty (ThirdPartyId); Cada detalle debe referenciar un concepto válido del catálogo Payroll.Concept (ConceptId)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLiquidationDetaill';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se exponen detalles de liquidación que tienen empleado y tercero válidos (INNER JOIN encadenado); El salario básico reportado proviene de la liquidación (Liquidation.BasicSalary), no del maestro de empleado; La identidad del empleado se reporta vía el tercero asociado (Nit y Name de ThirdParty), no vía la entidad Employee', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLiquidationDetaill';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de nómina; Detalle de liquidación; Concepto de nómina; Salario básico; Empleado; Tercero; NIT', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLiquidationDetaill';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas únicamente cuando existen coincidencias en TODAS las relaciones (LiquidationDetail↔Liquidation↔Employee↔ThirdParty y LiquidationDetail↔Concept) por uso de INNER JOIN; detalles huérfanos quedan excluidos', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLiquidationDetaill';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.LiquidationDetail; Payroll.Liquidation; Payroll.Employee; Common.ThirdParty; Payroll.Concept', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLiquidationDetaill';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportLiquidationDetaill';
GO
