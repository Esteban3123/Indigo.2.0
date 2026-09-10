

CREATE VIEW [Payroll].[ViewReportWithholdingRetroactive]
AS

SELECT RD.id, E.Id as IdEmployee, G.Id as IdGroup, RC.InitialDateRetroactive, TP.Nit, TP.Name as NombreEmpleado, E.ProcedureTypeRTF as ProcedimientoRete, G.Name as NombreGrupo, 
(SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.ConceptType = 1) as TotalDevengado,
(SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '502') as AportePension,
0 as AportePensionVoluntaria, 
(SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '503') as FondoSolidaridad,
0 as AFC,
(SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '501') as AporteSalud,
0 as MedicinaPrepagada,
0 as DeduccionDependendiente,
E.HousingDeductionValue as DeduccionVivienda,
(SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.ConceptType = 1 AND opcCONC.AffectIBCRTF = 1) -  (SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '502') - (SELECT ISNULL(SUM(opcRD.ValueConceptWithRetroactive),0) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '503') - (SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '501') - (ISNULL(E.HousingDeductionValue,0)) - (((SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.ConceptType = 1 AND opcCONC.AffectIBCRTF = 1) -  (SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '502')- (SELECT ISNULL(SUM(opcRD.ValueConceptWithRetroactive),0) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '503') - (SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '501') - (ISNULL(E.HousingDeductionValue,0)) )*0.25) as BaseGravable,
RD.ValueConceptWithRetroactive as TotalRetencion,
E.ProcedureTypeRTF as Articulo,
E.DeclarantType,
fUnit.BranchOfficeId
FROM Common.ThirdParty TP, Payroll.Employee E, Payroll.[Group] G, Payroll.RetroactiveC RC, Payroll.RetroactiveD RD, Payroll.[Contract] CONT, Payroll.Concept C, Payroll.FunctionalUnit fUnit 

WHERE TP.Id = E.ThirdPartyId and RC.Id = RD.IdRetroactiveC AND RC.IdContract = CONT.Id AND CONT.EmployeeId = E.Id and G.Id = CONT.GroupId AND RD.IdConcept = C.Id
and C.Code = '701'
AND fUnit.Id = CONT.FunctionalUnitId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de retención en la fuente sobre pagos retroactivos de nómina. Consolida, para cada empleado con un ajuste salarial retroactivo (concepto 701 - retención en la fuente), los valores necesarios para calcular la base gravable y el impuesto: total devengado, aportes a salud (501), pensión (502), fondo de solidaridad (503), deducción por vivienda, valor exento del 25% y base gravable resultante. Integra datos del tercero/empleado, grupo de nómina, unidad funcional y sede, combinando las tablas de liquidación retroactiva (RetroactiveC y RetroactiveD) con el catálogo de conceptos, contratos y empleados. Se utiliza para la generación del reporte de retención en la fuente sobre reliquidaciones retroactivas de nómina, permitiendo auditar y declarar los valores correctos ante la DIAN.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportWithholdingRetroactive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportWithholdingRetroactive';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida, por empleado y grupo, los valores devengados, deducciones y retención en la fuente correspondientes a un cálculo retroactivo de nómina, calculando la base gravable según parámetros tributarios.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un encabezado de retroactivo (RetroactiveC) asociado a un contrato vigente y a un empleado con tercero registrado.; El detalle del retroactivo debe contener al menos un concepto con Code=''701'' (retención) para que la fila aparezca en la vista.; Los conceptos referenciados deben existir en Payroll.Concept con sus códigos estándar (''501'' salud, ''502'' pensión, ''503'' fondo solidaridad, ''701'' retención).; El contrato debe tener asignados grupo (GroupId) y unidad funcional (FunctionalUnitId).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La base gravable se calcula como: (devengados que afectan IBC RTF) − aporte pensión (502) − fondo solidaridad (503, ISNULL=0) − aporte salud (501) − deducción de vivienda − 25% de ese mismo neto (renta exenta del 25%).; FondoSolidaridad y DeduccionVivienda se tratan como 0 cuando son NULL al calcular la base gravable; los demás aportes se restan sin ISNULL.; AportePensionVoluntaria, AFC, MedicinaPrepagada y DeduccionDependiente se exponen siempre como 0 (no se calculan en esta vista).; Solo se incluyen empleados cuyo contrato esté vinculado a un grupo y a una unidad funcional existentes.; El procedimiento de retención (ProcedureTypeRTF) se expone tanto como ProcedimientoRete como Articulo (mismo origen).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención en la fuente; Retroactivo de nómina; Base gravable; Aporte a salud; Aporte a pensión; Fondo de solidaridad pensional; Deducción por vivienda; Renta exenta del 25%; Procedimiento de retención (Artículo); Tipo de declarante; IBC (Ingreso Base de Cotización RTF)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportWithholdingRetroactive: Devuelve una fila por cada detalle de retroactivo cuyo concepto tenga Code=''701'', exponiendo totales devengados, aportes (501/502/503), deducción de vivienda, base gravable y retención.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si opcCONC.ConceptType = 1 → El concepto se suma como devengado (TotalDevengado y, si AffectIBCRTF=1, aporta al cálculo de la base gravable).; si opcCONC.Code = ''501'' / ''502'' / ''503'' → El valor se clasifica respectivamente como AporteSalud, AportePension o FondoSolidaridad y se resta de la base gravable.; si C.Code = ''701'' (filtro principal del FROM/WHERE) → Solo se reportan registros cuyo concepto detalle sea retención en la fuente (TotalRetencion = RD.ValueConceptWithRetroactive).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Group; Payroll.RetroactiveC; Payroll.RetroactiveD; Payroll.Contract; Payroll.Concept; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingRetroactive';
GO
