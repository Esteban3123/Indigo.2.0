

CREATE VIEW [Payroll].[VRetroactive]
AS

SELECT 
	rd.Id,G.Id as GroupId, 
	G.Code as GroupCode, 
	G.Name as GroupName, 
	TP.Id as ThirdPartyId, 
	TP.Nit as NitEmployee, 
	TP.Name as EmployeeName,
	POS.Code + ' - ' + POS.Name as Position, 
	CONT.BasicSalary as BasicSalary,
	30 as PayrollDays, 
	0 as WorkedDays, 
	0 as InabilityDays, 
	0 as VacationDays,
	CONC.Id as ConceptId,
	CONC.Code as CodeConcept, 
	CONC.Name as NameConcept, 
	CONC.ConceptType as ConceptType, 
	0 as Recharges, 
	RD.ValueConceptWithRetroactive as ValueConcept, 
	rc.InitialDateRetroactive AS RetroactiveDate, 
	rc.[Status] as RegisterStatus,
	emp.Id as EmployeeId,
	fUnit.BranchOfficeId
FROM Payroll.RetroactiveD as rd
JOIN Payroll.RetroactiveC as rc on rc.Id = rd.IdRetroactiveC
JOIN Payroll.Concept as CONC on CONC.Id = rd.IdConcept
JOIN Payroll.Employee as Emp on Emp.Id = rc.IdEmployee
JOIN Payroll.[Contract] as CONT on CONT.Id = RC.IdContract
JOIN Payroll.Position as POS on POS.Id = CONT.PositionId
JOIN Common.ThirdParty as TP on TP.Id = Emp.ThirdPartyId
JOIN Payroll.[Group] as G on G.Id = CONT.GroupId
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = CONT.FunctionalUnitId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de nómina que consolida los ajustes retroactivos pagados a empleados, combinando el detalle de conceptos reliquidados (devengados y deducciones) con la información del empleado, su contrato, cargo, grupo de nómina y unidad funcional. Para cada registro muestra el NIT y nombre del empleado, el cargo desempeñado, el salario básico, el concepto de nómina ajustado y su valor incluyendo el retroactivo, así como la fecha de inicio del período retroactivo y el estado del proceso. Sirve para reportería de reliquidaciones salariales, auditoría de diferencias por ajustes con efecto retroactivo y seguimiento del estado de pago de esos ajustes por empleado y grupo de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'VRetroactive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'VRetroactive';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de conceptos de liquidación retroactiva de nómina con datos del empleado, contrato, cargo, grupo y unidad funcional, listo para reportes o procesos de pago retroactivo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle retroactivo (RetroactiveD) debe tener un encabezado RetroactiveC asociado; El contrato del retroactivo debe tener PositionId, GroupId y FunctionalUnitId válidos (INNER JOIN); El empleado debe tener un ThirdParty asociado para obtener Nit y nombre; El concepto referenciado en el detalle debe existir en Payroll.Concept', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los días de nómina se fijan siempre en 30 (PayrollDays = 30); WorkedDays, InabilityDays, VacationDays y Recharges se exponen siempre en 0 (no se calculan en la vista); El valor del concepto reportado corresponde al valor ya ajustado con retroactivo (ValueConceptWithRetroactive), no al valor original; La fecha de retroactivo expuesta es la InitialDateRetroactive del encabezado; El cargo (Position) se concatena como ''Code - Name''; Solo se incluyen registros cuyo contrato esté ligado a una unidad funcional, cargo y grupo válidos (INNER JOIN excluye huérfanos)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retroactivo de nómina; Concepto de nómina; Empleado; Contrato laboral; Cargo/Posición; Grupo de nómina; Unidad funcional; Salario básico; Sucursal (BranchOffice); Tercero (NIT)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.VRetroactive: Devuelve una fila por cada Payroll.RetroactiveD, enriquecida con datos de empleado, contrato, cargo, grupo, tercero y unidad funcional mediante INNER JOIN', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.RetroactiveD; Payroll.RetroactiveC; Payroll.Concept; Payroll.Employee; Payroll.Contract; Payroll.Position; Common.ThirdParty; Payroll.Group; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VRetroactive';
GO
