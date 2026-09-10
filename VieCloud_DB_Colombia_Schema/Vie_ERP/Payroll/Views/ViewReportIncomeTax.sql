

CREATE   VIEW [Payroll].[ViewReportIncomeTax]
AS

SELECT	
	pl.Id,
    pl.PayrollDateLiquidated,
    tp.Nit AS IdentificationNumber,
    tp.Name AS EmployeeName,
    SUM(pl.TotalBaseRetention) AS TotalBaseRetention,
    SUM(CASE WHEN c.Code = 'DQ97' THEN 
		pld.ConceptTotalValue 
		ELSE 0 END) AS ConceptRentPaid,
    SUM(CASE WHEN c.Code = 'BQ41' THEN
		pld.ConceptTotalValue 
		ELSE 0 END) AS ConceptWithholding,
    SUM(pl.DependentsSupplementary) AS SupplementaryPension,
    SUM(pl.DependentsDeduction) AS TaxCredits,
    'SL' AS Code
FROM Payroll.Liquidation pl
JOIN Payroll.Employee e ON e.Id = pl.EmployeeId
JOIN Common.ThirdParty tp ON tp.Id = e.ThirdPartyId
LEFT JOIN Payroll.LiquidationDetail pld ON pld.PayrollId = pl.Id and PLD.ConceptCode IN ('DQ97','BQ41') 
LEFT JOIN Payroll.Concept c ON c.Id = pld.ConceptId
GROUP BY pl.Id, pl.PayrollDateLiquidated, tp.Nit, tp.Name
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de retención en la fuente sobre ingresos laborales (impuesto de renta de empleados). Consolida por empleado y período de liquidación los valores relevantes para la declaración de renta: base total sujeta a retención, renta pagada (concepto DQ97), retención en la fuente practicada (concepto BQ41), deducción por dependientes y créditos tributarios. Integra la liquidación de nómina con los datos de identificación del empleado (NIT/cédula) obtenidos del tercero asociado, y desglosa los conceptos específicos del detalle de liquidación para producir el informe requerido por la DIAN o para certificados de ingresos y retenciones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportIncomeTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportIncomeTax';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por liquidación de nómina los valores requeridos para el certificado/reporte de retención en la fuente por rentas de trabajo: base de retención, renta pagada, retención practicada, aportes voluntarios y deducción por dependientes.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncomeTax';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de liquidaciones en Payroll.Liquidation con empleado válido y tercero asociado (INNER JOIN obliga la relación).; El catálogo Payroll.Concept debe contener los códigos ''DQ97'' (renta pagada) y ''BQ41'' (retención) para que las sumas condicionales aporten valor.; Los campos TotalBaseRetention, DependentsSupplementary y DependentsDeduction deben estar calculados previamente en Payroll.Liquidation.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncomeTax';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las liquidaciones sin detalle (LiquidationDetail) o sin concepto asociado siguen apareciendo gracias al LEFT JOIN, con ConceptRentPaid y ConceptWithholding en 0.; El código de reporte siempre se emite como literal ''SL''.; La identificación del empleado se obtiene a través de Employee.ThirdPartyId → ThirdParty.Nit; un empleado sin tercero asociado no se incluye (INNER JOIN).; Los valores de renta pagada y retención solo se reconocen cuando el concepto del detalle coincide exactamente con los códigos ''DQ97'' y ''BQ41'' respectivamente.; La agregación se realiza por liquidación individual (pl.Id), no consolidada por empleado ni por período.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncomeTax';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención en la fuente; Impuesto de renta; Liquidación de nómina; Base de retención; Dependientes (deducción tributaria); Aportes voluntarios a pensión; Empleado; Tercero (NIT); Conceptos de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncomeTax';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportIncomeTax: Retorna una fila por cada Payroll.Liquidation (agrupado por Id, PayrollDateLiquidated, Nit y Name del tercero) con sumas de TotalBaseRetention, aportes complementarios y deducciones de dependientes.; [RETURN_RESULT] Payroll.ViewReportIncomeTax: Cuando Concept.Code=''DQ97'' suma ConceptTotalValue en la columna ConceptRentPaid; en cualquier otro caso aporta 0.; [RETURN_RESULT] Payroll.ViewReportIncomeTax: Cuando Concept.Code=''BQ41'' suma ConceptTotalValue en la columna ConceptWithholding; en cualquier otro caso aporta 0.; [RETURN_RESULT] Payroll.ViewReportIncomeTax: Emite la columna Code con el literal fijo ''SL'' para identificar el tipo/origen del reporte.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncomeTax';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si c.Code = ''DQ97'' → Suma pld.ConceptTotalValue como ConceptRentPaid (renta pagada) else Aporta 0 a ConceptRentPaid; si c.Code = ''BQ41'' → Suma pld.ConceptTotalValue como ConceptWithholding (retención en la fuente) else Aporta 0 a ConceptWithholding', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncomeTax';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.Employee; Common.ThirdParty; Payroll.LiquidationDetail; Payroll.Concept', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncomeTax';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportIncomeTax';
GO
