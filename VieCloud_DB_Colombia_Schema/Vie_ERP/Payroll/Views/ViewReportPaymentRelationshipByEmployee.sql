
CREATE VIEW [Payroll].[ViewReportPaymentRelationshipByEmployee]
AS
SELECT 
	ROW_NUMBER() OVER (ORDER BY tp.Nit) AS Id,
	g.[Name] as NombreGrupo, 
	tp.Nit as Cedula, 
	tp.[Name] as NombreEmpleado, 
	RC.InitialDateRetroactive AS Fecha, 
	CONT.BasicSalary AS SalarioBasico, 
	30 as DiasNomina, 
	30 as DiasLab,  
	(SELECT SUM(RDOPC.ValueConceptWithRetroactive) from Payroll.Concept C, Payroll.RetroactiveD RDOPC WHERE RDOPC.IdConcept = C.Id and RDOPC.IdRetroactiveC = RC.Id AND C.ConceptType = 1) AS TotalDevengado,
	(SELECT SUM(RDOPC.ValueConceptWithRetroactive) from Payroll.Concept C, Payroll.RetroactiveD RDOPC WHERE RDOPC.IdConcept = C.Id and RDOPC.IdRetroactiveC = RC.Id AND C.ConceptType = 2) as TotalDeducido,
	RC.TotalRetroactiveValue AS TotalPagado,
	FU.Code AS CodeFunctionalUnit,
	G.Code AS CodeGroup,
	CC.Code AS CodeCostCenter,
	CC.[Name] AS NameCostCenter,
	E.Id AS EmployeeId,
	FU.BranchOfficeId
FROM Payroll.RetroactiveC AS RC
JOIN  Payroll.Employee AS E ON E.Id = RC.IdEmployee
JOIN  Common.ThirdParty AS TP ON TP.Id = E.ThirdPartyId
JOIN Payroll.[Contract] AS CONT ON CONT.Id = RC.IdContract
JOIN Payroll.[Group] AS G ON G.Id = RC.IdGroup
JOIN Payroll.FunctionalUnit AS FU ON FU.Id = CONT.FunctionalUnitId
LEFT JOIN Payroll.CostCenter AS CC ON CC.Id = E.CostCenterId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la relación de pagos retroactivos de nómina por empleado, integrando datos del cálculo retroactivo (RetroactiveC), el contrato laboral, el grupo de nómina, la unidad funcional y el centro de costo. Para cada liquidación retroactiva muestra la cédula y nombre del empleado, la fecha de inicio del retroactivo, el salario básico, los días de nómina fijos (30), el total devengado y el total deducido (sumando los conceptos de tipo devengado y deducción del detalle RetroactiveD respectivamente), y el valor neto total pagado. Sirve para reportería de relación de pagos retroactivos, permitiendo auditar y revisar qué se pagó a cada trabajador por reliquidaciones salariales, clasificado por grupo de nómina, unidad funcional, centro de costo y sucursal.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPaymentRelationshipByEmployee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPaymentRelationshipByEmployee';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la relación de pagos retroactivos por empleado, mostrando para cada liquidación retroactiva el salario básico, totales devengados y deducidos por tipo de concepto, y la clasificación organizacional (grupo, unidad funcional, centro de costo, sucursal).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByEmployee';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado del retroactivo debe existir en Payroll.Employee y tener un ThirdParty asociado en Common.ThirdParty.; El retroactivo debe tener un contrato (IdContract), un grupo de nómina (IdGroup) y una unidad funcional asociados al contrato.; Los conceptos referenciados en Payroll.RetroactiveD deben existir en Payroll.Concept y estar tipificados como devengado (ConceptType=1) o deducción (ConceptType=2) para alimentar los totales.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByEmployee';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los días de nómina y días laborados se reportan siempre como 30, asumiendo el mes comercial estándar para liquidación de retroactivos.; TotalDevengado se calcula sumando solo conceptos con ConceptType = 1 (devengados) del detalle del retroactivo.; TotalDeducido se calcula sumando solo conceptos con ConceptType = 2 (deducciones) del detalle del retroactivo.; Cada fila representa una liquidación retroactiva (RetroactiveC) por empleado, no un consolidado.; Empleados sin centro de costo asignado igual aparecen en el reporte (LEFT JOIN con CostCenter); el resto de relaciones (empleado, tercero, contrato, grupo, unidad funcional) son obligatorias.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByEmployee';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'retroactivo de nómina; empleado; contrato; salario básico; devengado; deducido; grupo de nómina; unidad funcional; centro de costo; sucursal; concepto de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByEmployee';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.RetroactiveC: Devuelve una fila por cada registro en Payroll.RetroactiveC enriquecida con datos del empleado, tercero, contrato, grupo, unidad funcional y centro de costo; numerada con ROW_NUMBER ordenado por Nit del tercero.; [RETURN_RESULT] Payroll.RetroactiveD: Para cada retroactivo calcula TotalDevengado como SUM(RetroactiveD.ValueConceptWithRetroactive) filtrando Concept.ConceptType = 1.; [RETURN_RESULT] Payroll.RetroactiveD: Para cada retroactivo calcula TotalDeducido como SUM(RetroactiveD.ValueConceptWithRetroactive) filtrando Concept.ConceptType = 2.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByEmployee';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.RetroactiveC; Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.Group; Payroll.FunctionalUnit; Payroll.CostCenter; Payroll.Concept; Payroll.RetroactiveD', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByEmployee';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByEmployee';
GO
