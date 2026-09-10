

CREATE VIEW [Payroll].[ViewDistributionOfScheduledExpenses]
AS
SELECT 
	sd.EmployeeId, sd.DateDetail, sdc.ConceptId, cas.AccruedAccount, cas.DeductedAccount, fu.CostCenterId, SUM(sdh.TotalNumberHours) TotalHours, sd.GroupId
FROM Payroll.ScheduleDetail sd
JOIN Payroll.ScheduleDetailHour sdh ON sd.Id = sdh.ScheduleDetailId
JOIN Payroll.ScheduleDetailConcept sdc ON sdh.Id = sdc.ScheduleDetailHourId
JOIN Payroll.FunctionalUnit fu ON sd.ScheduleFunctionalUnitId = fu.Id
JOIN Payroll.AccountingStructure acs ON fu.AccountingStructureId = acs.Id
JOIN Payroll.ConceptAccountingStructure cas ON acs.Id = cas.AccountingStructureId AND sdc.ConceptId = cas.ConceptId
WHERE sd.TotalNumberHours > 0
GROUP BY sd.EmployeeId, sd.DateDetail, sdc.ConceptId, cas.AccruedAccount, cas.DeductedAccount, fu.CostCenterId, sd.groupID
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de distribución de gastos programados por turno en nómina. Consolida, por empleado y fecha, las horas totales trabajadas en cada turno junto con los conceptos de liquidación que aplican (recargos, horas extras, bonificaciones), cruzando la unidad funcional con la estructura contable para obtener las cuentas de causación y deducción correspondientes, así como el centro de costo. Sirve para la interfaz contable y la distribución de costos laborales, permitiendo saber cuántas horas debe imputar cada concepto de nómina a qué cuenta contable y centro de costo, según la programación de turnos previamente registrada.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewDistributionOfScheduledExpenses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewDistributionOfScheduledExpenses';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las horas programadas por empleado, fecha, concepto de nómina, cuentas contables (causación/deducción) y centro de costo, base para distribuir el gasto contable de la nómina programada.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewDistributionOfScheduledExpenses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ScheduleDetail debe estar enlazado a una FunctionalUnit con AccountingStructure definida; Debe existir registro en ConceptAccountingStructure que empareje la AccountingStructure con el ConceptId del detalle de horas; El detalle del turno debe registrar horas (TotalNumberHours > 0)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewDistributionOfScheduledExpenses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se distribuyen turnos con horas reales (>0) sobre el ScheduleDetail; Cada fila del resultado representa una combinación única empleado-fecha-concepto-cuentaCausación-cuentaDeducción-centroCosto-grupo; Las cuentas contables (AccruedAccount/DeductedAccount) provienen exclusivamente del cruce ConceptAccountingStructure entre la estructura contable de la unidad funcional y el concepto del detalle horario; El centro de costo se toma de la unidad funcional asignada al turno, no del empleado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewDistributionOfScheduledExpenses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución contable de nómina; Concepto de nómina; Cuenta de causación (devengado); Cuenta de deducción; Centro de costo; Unidad funcional; Estructura contable; Programación de turnos; Horas trabajadas; Grupo de horario', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewDistributionOfScheduledExpenses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewDistributionOfScheduledExpenses: Devuelve la suma de horas (SUM(sdh.TotalNumberHours)) agrupada por empleado, fecha, concepto, cuenta de causación, cuenta de deducción, centro de costo y grupo de horario', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewDistributionOfScheduledExpenses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si sd.TotalNumberHours > 0 → Incluye el detalle del turno en la distribución de gastos else Los turnos sin horas registradas se excluyen del resultado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewDistributionOfScheduledExpenses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ScheduleDetail; Payroll.ScheduleDetailHour; Payroll.ScheduleDetailConcept; Payroll.FunctionalUnit; Payroll.AccountingStructure; Payroll.ConceptAccountingStructure', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewDistributionOfScheduledExpenses';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewDistributionOfScheduledExpenses';
GO
