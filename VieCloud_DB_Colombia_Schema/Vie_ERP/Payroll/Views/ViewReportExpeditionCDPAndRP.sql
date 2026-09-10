

CREATE VIEW [Payroll].[ViewReportExpeditionCDPAndRP]
AS
SELECT	
	ROW_NUMBER() OVER(ORDER BY C.Id ASC) AS [Row],  
	C.Id, C.Code + ' - ' + C.[Name] AS [Name], 
	SUM(LD.ConceptTotalValue) AS 'ConceptTotalValue', 
	ET.Code, 
	C.ConceptType, 
	L.PayrollDateLiquidated,
	bOffice.Id AS BranchOfficeID,
	bOffice.Code AS BranchOfficeCode,
	bOffice.[Name] AS BranchOffice
FROM Payroll.Concept AS C
JOIN Payroll.LiquidationDetail AS LD 
JOIN Payroll.Employee AS E 
JOIN Payroll.Liquidation AS L ON E.Id = L.EmployeeId ON LD.PayrollId = L.Id ON C.Id = LD.ConceptId 
JOIN Payroll.EmployeeType AS ET ON E.EmployeeTypeId = ET.Id
JOIN Payroll.[Contract] cntrc ON cntrc.EmployeeId = E.Id
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = cntrc.FunctionalUnitId
JOIN Payroll.BranchOffice bOffice ON bOffice.Id = fUnit.BranchOfficeId
WHERE cntrc.Id = L.ContractId
GROUP BY 
	C.Code,
	C.[Name], 
	C.Id, 
	ET.Code, 
	C.ConceptType, 
	L.PayrollDateLiquidated,
	bOffice.Id,
	bOffice.Code,
	bOffice.[Name]
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte para la expedición de CDP (Certificado de Disponibilidad Presupuestal) y RP (Registro Presupuestal) en el módulo de nómina. Consolida los valores totales liquidados por concepto de nómina (devengados, deducciones y aportes) agrupados por concepto, tipo de empleado, fecha de liquidación y sede o sucursal. Integra los conceptos del catálogo de nómina, el detalle de valores liquidados por empleado, el tipo de vinculación laboral, el contrato vigente y la unidad funcional asociada a cada sede, permitiendo identificar cuánto se comprometió y ejecutó presupuestalmente por concepto en cada periodo y oficina. Se usa para reportería presupuestal y contable de nómina, facilitando la trazabilidad del gasto laboral por sede y tipo de empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportExpeditionCDPAndRP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportExpeditionCDPAndRP';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los valores totales liquidados por concepto de nómina, agrupados por tipo de empleado, fecha de liquidación y sucursal, para soportar reportes de expedición de CDP y RP.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen liquidaciones de nómina con detalle por concepto (Payroll.Liquidation y Payroll.LiquidationDetail).; Cada empleado liquidado tiene un contrato con unidad funcional asociada a una sucursal.; El contrato vinculado al empleado coincide con el contrato de la liquidación (cntrc.Id = L.ContractId).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen detalles de liquidación cuyo contrato del empleado coincide con el contrato registrado en la liquidación (cntrc.Id = L.ContractId), evitando mezclar conceptos entre contratos distintos del mismo empleado.; El nombre del concepto se presenta concatenado como ''Code - Name''.; Cada fila representa un concepto liquidado por combinación única de tipo de empleado, fecha de liquidación y sucursal.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Concepto de nómina; Liquidación de nómina; Tipo de empleado; Contrato laboral; Unidad funcional; Sucursal/Sede; CDP (Certificado de Disponibilidad Presupuestal); RP (Registro Presupuestal)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportExpeditionCDPAndRP: Devuelve la suma de LD.ConceptTotalValue agrupada por concepto (Id, Code, Name, ConceptType), tipo de empleado (ET.Code), fecha liquidada (L.PayrollDateLiquidated) y sucursal (BranchOffice Id/Code/Name), numerando filas con ROW_NUMBER ordenado por C.Id.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Concept; Payroll.LiquidationDetail; Payroll.Employee; Payroll.Liquidation; Payroll.EmployeeType; Payroll.Contract; Payroll.FunctionalUnit; Payroll.BranchOffice', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRP';
GO
