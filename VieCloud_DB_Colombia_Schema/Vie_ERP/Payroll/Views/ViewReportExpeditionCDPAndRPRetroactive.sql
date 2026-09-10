

Create VIEW [Payroll].[ViewReportExpeditionCDPAndRPRetroactive]
AS
SELECT ROW_NUMBER() OVER(ORDER BY C.Id ASC) as Row,  C.Id, C.Name, SUM(RD.ValueConceptWithRetroactive) AS 'ConceptTotalValue', ET.Code, C.ConceptType, RC.InitialDateRetroactive
FROM Payroll.RetroactiveD RD, Payroll.RetroactiveC RC, Payroll.Concept C, Payroll.Employee E, Payroll.EmployeeType ET
WHERE 
E.Id = RC.IdEmployee 
AND RC.Id = RD.IdRetroactiveC
AND RD.IdConcept = C.Id
AND ET.Id = E.EmployeeTypeId
GROUP BY C.Name, C.Id, ET.Code, C.ConceptType, RC.InitialDateRetroactive
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de expedición de CDP (Certificados de Disponibilidad Presupuestal) y RP (Registros Presupuestales) para liquidaciones retroactivas de nómina. Consolida, por concepto de nómina y tipo de empleado, el valor total a reconocer con efecto retroactivo, cruzando los detalles de reliquidación (RetroactiveD) con los encabezados de liquidación retroactiva (RetroactiveC), el catálogo de conceptos de nómina (Concept) y la clasificación del empleado (EmployeeType). Permite a las áreas de presupuesto y nómina conocer cuánto se debe expedir por cada concepto (devengado, deducción o aporte), a partir de qué fecha aplica el retroactivo y bajo qué categoría de vinculación laboral, facilitando los trámites presupuestales y el control de los ajustes salariales con efecto retroactivo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportExpeditionCDPAndRPRetroactive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportExpeditionCDPAndRPRetroactive';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los valores totales de conceptos de nómina afectados por cálculos retroactivos, agrupados por concepto y tipo de empleado, para soportar el reporte de expedición de CDP y RP.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en RetroactiveC vinculados a empleados (E.Id = RC.IdEmployee) con tipo de empleado válido (ET.Id = E.EmployeeTypeId).; Cada cabecera retroactiva (RetroactiveC) debe tener detalle asociado en RetroactiveD (RC.Id = RD.IdRetroactiveC).; Cada detalle retroactivo debe referenciar un concepto válido (RD.IdConcept = C.Id).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen conceptos cuyo retroactivo está completamente encadenado: empleado → cabecera retroactiva → detalle → concepto (INNER JOIN implícito vía WHERE).; El total reportado corresponde exclusivamente al valor del concepto con retroactivo aplicado (ValueConceptWithRetroactive), no al valor original.; La granularidad de la fila combina concepto + tipo de empleado + fecha inicial del retroactivo; un mismo concepto puede aparecer en varias filas si difieren estos atributos.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retroactivo de nómina; Concepto de nómina; Tipo de empleado; Empleado; CDP (Certificado de Disponibilidad Presupuestal); RP (Registro Presupuestal); Expedición presupuestal', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve la suma de RD.ValueConceptWithRetroactive como ConceptTotalValue agrupada por concepto (Id, Name, ConceptType), código de tipo de empleado y fecha inicial del retroactivo, numerando filas con ROW_NUMBER ordenado por C.Id ascendente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.RetroactiveD; Payroll.RetroactiveC; Payroll.Concept; Payroll.Employee; Payroll.EmployeeType', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPRetroactive';
GO
