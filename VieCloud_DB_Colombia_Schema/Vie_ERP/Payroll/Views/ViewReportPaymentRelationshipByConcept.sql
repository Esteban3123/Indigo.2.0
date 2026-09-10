
CREATE VIEW [Payroll].[ViewReportPaymentRelationshipByConcept]
AS
SELECT 
	ROW_NUMBER() OVER (ORDER BY tp.Nit) AS Id,
	g.[Name] AS NombreGrupo, 
	tp.Nit AS Cedula, 
	tp.[Name] AS NombreEmpleado, 
	c.Code AS CodigoConcepto, 
	C.[Name] AS NombreConcepto, 
	RD.ValueConceptWithRetroactive AS Valor,
	RC.InitialDateRetroactive AS Fecha,
	FU.Code AS CodeFunctionalUnit,
	G.Code AS CodeGroup,
	CC.Code AS CodeCostCenter,
	CC.[Name] AS NameCostCenter,
	c.Id AS IdConcept,
	E.Id AS EmployeeId,
	FU.BranchOfficeId
FROM Payroll.RetroactiveC AS RC
JOIN  Payroll.Employee AS E ON E.Id = RC.IdEmployee
JOIN  Common.ThirdParty AS TP ON TP.Id = E.ThirdPartyId
JOIN Payroll.[Contract] AS CONT ON CONT.Id = RC.IdContract
JOIN Payroll.[Group] AS G ON G.Id = RC.IdGroup
JOIN Payroll.RetroactiveD RD ON RC.Id = RD.IdRetroactiveC
JOIN Payroll.Concept C ON C.Id = RD.IdConcept
JOIN Payroll.FunctionalUnit AS FU ON FU.Id = CONT.FunctionalUnitId
LEFT JOIN Payroll.CostCenter AS CC ON CC.Id = E.CostCenterId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación de pagos retroactivos de nómina desglosados por concepto y empleado. Consolida la información del cálculo retroactivo (encabezado y detalle) con los datos del empleado, su contrato, grupo de nómina, unidad funcional y centro de costo, para mostrar cuánto se pagó por cada concepto de nómina (devengado, deducción o aporte) como resultado de una reliquidación retroactiva. Sirve para reportes de auditoría y conciliación de nómina, permitiendo identificar por cédula o NIT del empleado, grupo de liquidación, unidad funcional y centro de costo, el valor retroactivo reconocido en cada concepto y la fecha inicial del período reliquidado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPaymentRelationshipByConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPaymentRelationshipByConcept';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los valores liquidados por concepto en cálculos retroactivos de nómina, asociándolos al empleado, grupo, contrato, unidad funcional y centro de costo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Payroll.RetroactiveC con su detalle en Payroll.RetroactiveD vinculado por IdRetroactiveC.; Cada retroactivo referencia un empleado (IdEmployee), un contrato (IdContract) y un grupo de nómina (IdGroup) válidos.; El empleado tiene asociado un ThirdParty en Common.ThirdParty para obtener Nit y nombre.; El contrato tiene una unidad funcional (FunctionalUnitId) válida en Payroll.FunctionalUnit.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan empleados que tengan al menos un registro de retroactivo (RetroactiveC) con detalle de conceptos (RetroactiveD); los INNER JOIN excluyen retroactivos sin contrato, grupo, empleado, tercero, concepto o unidad funcional asociados.; El valor reportado siempre corresponde al valor del concepto ajustado con retroactivo (ValueConceptWithRetroactive), no al valor original.; La identificación del empleado se toma del Nit del tercero asociado en Common.ThirdParty.; La sucursal expuesta (BranchOfficeId) proviene de la unidad funcional del contrato, no directamente del empleado o del centro de costo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retroactivo de nómina; Concepto de nómina; Empleado; Contrato laboral; Grupo de nómina; Unidad funcional; Centro de costo; Tercero (Nit); Sucursal', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportPaymentRelationshipByConcept: Devuelve una fila por cada concepto (RetroactiveD) de cada cabecera de retroactivo (RetroactiveC), numerada con ROW_NUMBER() OVER (ORDER BY tp.Nit), exponiendo el valor con retroactivo (ValueConceptWithRetroactive) y la fecha inicial del retroactivo (InitialDateRetroactive).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LEFT JOIN Payroll.CostCenter ON CC.Id = E.CostCenterId → Se incluyen el código y nombre del centro de costo del empleado cuando existe. else Si el empleado no tiene centro de costo asignado, las columnas CodeCostCenter y NameCostCenter quedan NULL pero la fila se conserva.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.RetroactiveC; Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.Group; Payroll.RetroactiveD; Payroll.Concept; Payroll.FunctionalUnit; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaymentRelationshipByConcept';
GO
