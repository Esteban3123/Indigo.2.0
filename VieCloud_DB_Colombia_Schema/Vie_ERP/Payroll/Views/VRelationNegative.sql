
Create VIEW [Payroll].[VRelationNegative]
AS
select PayrollDateLiquidated,t.Nit, t.Name, SUM(ld.AccruedValue) as TotalDevengados, SUM(DeductedValue) as TotalDeducidos, SUM(ld.AccruedValue) - SUM(DeductedValue) as Diferencia
from Payroll.Liquidation l
inner join Payroll.Employee e on e.Id = l.EmployeeId
inner join Common.ThirdParty t on t.Id = e.ThirdPartyId
inner join Payroll.LiquidationDetail ld on ld.PayrollId = l.Id
where ld.ConceptType in (1,2)
group by PayrollDateLiquidated,e.Id,t.Nit, t.Name
having (SUM(ld.AccruedValue) - SUM(DeductedValue)) <0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de nómina que identifica empleados con diferencia negativa entre devengados y deducciones en cada período de liquidación: es decir, casos donde el total deducido supera al total devengado, lo cual representa una inconsistencia o alerta en la liquidación de nómina. Cruza la liquidación de nómina con el detalle de conceptos (solo devengados y deducciones, tipos 1 y 2), el maestro de empleados y el tercero asociado para obtener el NIT y nombre del empleado. Sirve para auditoría y control de nómina, permitiendo detectar empleados con valores de nómina en rojo o descuadre entre lo pagado y lo descontado en un período dado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'VRelationNegative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'VRelationNegative';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las liquidaciones de nómina por empleado y fecha en las que el total de deducciones supera al total de devengados, generando una diferencia negativa.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRelationNegative';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en Payroll.Liquidation con su correspondiente Employee y ThirdParty asociado.; Existencia de detalles en Payroll.LiquidationDetail con ConceptType 1 o 2 (devengados/deducciones).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRelationNegative';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran detalles cuyo ConceptType esté en (1,2), interpretados como devengados y deducciones.; El resultado únicamente expone liquidaciones donde la suma de devengados es menor que la suma de deducciones (Diferencia < 0).; La agregación se realiza por fecha de liquidación y por empleado (identificado por Id, NIT y Nombre del tercero).; Cada empleado se vincula a un tercero mediante Employee.ThirdPartyId, de donde se obtienen NIT y Nombre.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRelationNegative';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'liquidación de nómina; devengados; deducciones; empleado; tercero (NIT); concepto de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRelationNegative';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Liquidation: Devuelve filas agrupadas por PayrollDateLiquidated y empleado solo cuando SUM(AccruedValue) - SUM(DeductedValue) < 0, exponiendo NIT, Nombre, total devengado, total deducido y la diferencia.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRelationNegative';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.Employee; Common.ThirdParty; Payroll.LiquidationDetail', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRelationNegative';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRelationNegative';
GO
