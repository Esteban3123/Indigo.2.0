CREATE VIEW [Payroll].[ViewReportTotalRetroactiveConcept]
AS
SELECT
	D.Id AS IdRetoractive, 
	PG.Code AS GroupCode, 
	PG.[Name] AS GroupName, 
	C.InitialDateRetroactive, 
	CONC.Id, 
	CONC.Code, 
	CONC.[Name], 
	CONC.ConceptType, 
	CONC.ConceptClass, 
	C.IdEmployee, 
	D.ValueConceptWithRetroactive AS [Value],
	fUnit.BranchOfficeId
FROM Payroll.RetroactiveC AS C 
JOIN Payroll.RetroactiveD AS D ON C.Id = D.IdRetroactiveC 
JOIN Payroll.Concept AS CONC ON D.IdConcept = CONC.Id 
JOIN Payroll.[Group] AS PG ON C.IdGroup = PG.Id
JOIN Payroll.[Contract] cntrc ON cntrc.Id = C.IdContract
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = cntrc.FunctionalUnitId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida el total de conceptos de nómina liquidados de forma retroactiva por empleado. Combina los encabezados de liquidación retroactiva (período inicial afectado y empleado), el detalle de cada concepto ajustado con su valor retroactivo, el catálogo de conceptos (tipo y clase: devengado, deducción o aporte), el grupo de nómina al que pertenece el empleado y la sede o sucursal obtenida a través del contrato y la unidad funcional. Sirve para reportería de auditoría y control de reliquidaciones salariales, permitiendo identificar qué conceptos de nómina se ajustaron retroactivamente, por cuánto valor y en qué grupo, sede y período.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportTotalRetroactiveConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportTotalRetroactiveConcept';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los valores de conceptos de nómina liquidados con efecto retroactivo por empleado, grupo de nómina y sede, exponiendo el valor ajustado de cada concepto.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalRetroactiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada RetroactiveC debe tener un IdContract válido en Payroll.Contract (INNER JOIN obliga existencia); Cada Contract debe estar asociado a una FunctionalUnit existente; Cada RetroactiveD debe referenciar un Concept y un RetroactiveC existentes; El RetroactiveC debe tener un Group asignado existente en Payroll.Group', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalRetroactiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen detalles de retroactivo cuya cabecera tenga contrato y unidad funcional asociados (todos los JOIN son INNER, por lo que registros sin estas relaciones quedan excluidos); El valor reportado siempre es el ajustado con retroactivo (ValueConceptWithRetroactive), nunca el valor original previo al ajuste; La sede (BranchOfficeId) reportada proviene de la unidad funcional del contrato, no del empleado ni del grupo; Cada fila representa un único concepto liquidado retroactivamente para un empleado dentro de una liquidación retroactiva específica', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalRetroactiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retroactivo de nómina; Concepto de nómina; Tipo y clase de concepto; Grupo de nómina; Contrato laboral; Unidad funcional; Sede (BranchOffice); Empleado; Fecha inicial de retroactivo', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalRetroactiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportTotalRetroactiveConcept: Devuelve una fila por cada detalle de retroactivo (RetroactiveD) cruzado con su cabecera, concepto, grupo, contrato y unidad funcional, exponiendo el valor con retroactivo (ValueConceptWithRetroactive) como Value', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalRetroactiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.RetroactiveC; Payroll.RetroactiveD; Payroll.Concept; Payroll.Group; Payroll.Contract; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalRetroactiveConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportTotalRetroactiveConcept';
GO
