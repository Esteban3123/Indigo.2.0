

CREATE VIEW [Payroll].[ViewReportExpeditionCDPAndRPIncentivePayment]
AS
SELECT 
	ROW_NUMBER() OVER(ORDER BY C.Id ASC) AS [Row],
	C.Id, 
	C.Name,
	SUM(ipd.AccruedValue + ipd.DeductedValue) AS 'ConceptTotalValue', 
	ET.Code, 
	C.ConceptType, 
	[ip].PeriodInitialDate AS PayrollDateLiquidated,
	bOffice.Id AS BranchOfficeID,
	bOffice.Code AS BranchOfficeCode,
	bOffice.[Name] AS BranchOffice
FROM Payroll.IncentivePayment [ip]
JOIN Payroll.IncentivePaymentDetail ipd on [ip].Id = ipd.IncentivePaymentId
JOIN Payroll.Concept c on c.Id = ipd.ConceptId
JOIN Payroll.[Contract] con on con.Id = [ip].ContractId
JOIN Payroll.Employee e on e.Id = con.EmployeeId
JOIN Payroll.EmployeeType et on et.Id = e.EmployeeTypeId
JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = con.FunctionalUnitId
JOIN Payroll.BranchOffice bOffice ON bOffice.Id = fUnit.BranchOfficeId
WHERE c.ConceptType <> 3
GROUP BY 
	C.[Name], 
	C.Id,
	ET.Code, 
	C.ConceptType, 
	[ip].PeriodInitialDate,
	bOffice.Id,
	bOffice.Code,
	bOffice.[Name]
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte para la expedición de CDP (Certificado de Disponibilidad Presupuestal) y RP (Registro Presupuestal) asociados a pagos de incentivos de nómina. Consolida los valores totales por concepto de pago (devengados más deducciones) agrupados por concepto, tipo de empleado, sede y período de liquidación, excluyendo los conceptos de tipo 3. Integra los pagos de incentivos con su detalle de conceptos, los contratos laborales, los empleados, sus tipos de vinculación y las unidades funcionales con sus respectivas sucursales o sedes. Sirve para reportería presupuestal y contable de los incentivos y bonificaciones pagadas al personal por período y sede.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportExpeditionCDPAndRPIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportExpeditionCDPAndRPIncentivePayment';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por concepto, tipo de empleado, período y sucursal los valores devengados y deducidos de pagos de incentivos de nómina, para soporte de reportes de expedición de CDP y RP.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada IncentivePayment debe tener un Contract asociado, y este un Employee con EmployeeType.; Cada Contract debe estar ligado a una FunctionalUnit que pertenezca a una BranchOffice.; Cada IncentivePaymentDetail debe referenciar un Concept válido.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se reportan conceptos cuyo ConceptType sea igual a 3.; El total por fila siempre corresponde a la suma combinada de valores devengados y deducidos del detalle de incentivos.; La granularidad de agregación es: concepto + tipo de empleado + tipo de concepto + período inicial + sucursal.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'nómina; pago de incentivos; concepto de nómina; devengado; deducción; tipo de empleado; contrato laboral; unidad funcional; sucursal; período de liquidación; CDP (Certificado de Disponibilidad Presupuestal); RP (Registro Presupuestal)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve la suma (AccruedValue + DeductedValue) agrupada por Concepto, código de tipo de empleado, tipo de concepto, fecha inicial del período y sucursal, numerando cada fila con ROW_NUMBER ordenado por Concept.Id ascendente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si c.ConceptType <> 3 → Se incluye el concepto en el reporte (excluye los conceptos de ConceptType = 3, típicamente aportes patronales/terceros).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePayment; Payroll.IncentivePaymentDetail; Payroll.Concept; Payroll.Contract; Payroll.Employee; Payroll.EmployeeType; Payroll.FunctionalUnit; Payroll.BranchOffice', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportExpeditionCDPAndRPIncentivePayment';
GO
