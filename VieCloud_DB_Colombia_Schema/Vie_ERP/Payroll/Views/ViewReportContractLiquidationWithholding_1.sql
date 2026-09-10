-- =============================================
-- View: Payroll.ViewReportContractLiquidationWithholding
-- Withholding report for contract liquidation. Aliases in English.
-- Contributions/deductions from ContractLiquidationDetail + Concept (ConceptClass).
-- =============================================
--
-- ConceptClass (Payroll.Concept.ConceptClass) VARCHAR, 3 digits:
-- 001-013: Overtime, Primes, Salary, Transport, Indemnities, Provisions, ARP, etc.
-- 014 Pensión Empleado | 015 Pensión Patrono | 016 Pensión Voluntaria
-- 017 Salud Empleado   | 018 Salud Patrono   | 019 Salud Voluntaria
-- 020 Retención
-- 021-028: Incapacity, Maternity, Licenses, Permits
-- 030-037: Vacation, Provisions, Parafiscals
-- 038 Aporte Fondo Seguridad Pensional | 041 Convenios | 044 Sindicato
-- 045 Cuentas AFC | 046 Bonificación por Año de Servicio
-- 047-076: Representation, Withholding adjustment, Embargos, Viáticos, etc.
-- =============================================

CREATE VIEW [Payroll].[ViewReportContractLiquidationWithholding]
AS
WITH
-- Contributions/deductions by concept class (deductions only, ConceptType = 2)
Contributions AS (
    SELECT
        cld.ContractLiquidationId,
        SUM(CASE WHEN RTRIM(co.ConceptClass) = '014' THEN cld.Deducted ELSE 0 END) AS PensionContribution,
        SUM(CASE WHEN RTRIM(co.ConceptClass) = '016' THEN cld.Deducted ELSE 0 END) AS VoluntaryPensionContribution,
        SUM(CASE WHEN RTRIM(co.ConceptClass) = '038' THEN cld.Deducted ELSE 0 END) AS SolidarityFund,
        SUM(CASE WHEN RTRIM(co.ConceptClass) = '045' THEN cld.Deducted ELSE 0 END) AS AFC,
        SUM(CASE WHEN RTRIM(co.ConceptClass) = '017' THEN cld.Deducted ELSE 0 END) AS HealthContribution,
        SUM(CASE WHEN RTRIM(co.ConceptClass) = '019' THEN cld.Deducted ELSE 0 END) AS PrepaidMedicine,
        CAST(0 AS DECIMAL(18,2)) AS DependentDeduction,
        CAST(0 AS DECIMAL(18,2)) AS HousingDeduction
    FROM Payroll.ContractLiquidationDetail cld
    INNER JOIN Payroll.Concept co ON co.Id = cld.IdConcept
    WHERE cld.ConceptType = 2
    GROUP BY cld.ContractLiquidationId
),
-- Withholding: one row per retention detail line
-- 020 Retención en la fuente | 061 Retención indemnización
Withholding AS (
    SELECT
        cld.ContractLiquidationId,
        cld.Id AS ContractLiquidationDetailId,
        ISNULL(cld.RetentionBase, 0) AS TaxableBase,
        cld.Deducted AS TotalWithholding,
        CAST(1 AS TINYINT) AS Article
    FROM Payroll.ContractLiquidationDetail cld
    INNER JOIN Payroll.Concept co ON co.Id = cld.IdConcept
    WHERE co.ConceptClass IN ('020', '061')
)
-- Result: una fila por cada línea de retención (FROM Withholding asegura N filas por N retenciones)
SELECT
    rt.ContractLiquidationDetailId AS Id,
    cl.Id AS ContractLiquidationId,
    cl.EmployeeId,
    ISNULL(tp.Nit, CAST(cl.EmployeeId AS VARCHAR(15))) AS Identification,
    ISNULL(tp.Name, '') AS Name,
    ISNULL(CAST(E.ProcedureTypeRTF AS TINYINT), 1) AS [Procedure],
    CAST(cl.RetirementDate AS DATETIME) AS LiquidationDate,
    c.GroupId,
    ISNULL(g.Code, '') AS GroupCode,
    ISNULL(g.Name, '') AS GroupName,
    cl.TotalAccrued AS TotalAccrued,
    cl.TotalAccrued AS TotalAccruedRTF,
    ISNULL(ap.PensionContribution, 0) AS PensionContribution,
    ISNULL(ap.VoluntaryPensionContribution, 0) AS VoluntaryPensionContribution,
    ISNULL(ap.SolidarityFund, 0) AS SolidarityFund,
    ISNULL(ap.AFC, 0) AS AFC,
    ISNULL(ap.HealthContribution, 0) AS HealthContribution,
    ISNULL(ap.PrepaidMedicine, 0) AS PrepaidMedicine,
    ISNULL(ap.DependentDeduction, 0) AS DependentDeduction,
    ISNULL(ap.HousingDeduction, 0) AS HousingDeduction,
        rt.TaxableBase,
    -- Renta exenta de trabajo: 25% del ingreso neto laboral, tope 240 UVT (Art. 206 ET)
    CAST(
        CASE
            WHEN (cl.TotalAccrued
                    - ISNULL(ap.PensionContribution, 0)
                    - ISNULL(ap.VoluntaryPensionContribution, 0)
                    - ISNULL(ap.SolidarityFund, 0)
                    - ISNULL(ap.HealthContribution, 0)
                 ) * 0.25
                 <= ISNULL(pp.UVTValue, 0) * 240
            THEN (cl.TotalAccrued
                    - ISNULL(ap.PensionContribution, 0)
                    - ISNULL(ap.VoluntaryPensionContribution, 0)
                    - ISNULL(ap.SolidarityFund, 0)
                    - ISNULL(ap.HealthContribution, 0)
                 ) * 0.25
            ELSE ISNULL(pp.UVTValue, 0) * 240
        END
    AS DECIMAL(18,2)) AS ExemptValueRetention,
    rt.TotalWithholding,
    CAST(rt.Article AS TINYINT) AS Article,
    ISNULL(E.DeclarantType, 0) AS DeclarantType,
    ISNULL(fu.BranchOfficeId, 0) AS BranchOfficeId
FROM Withholding rt
INNER JOIN Payroll.ContractLiquidation cl ON cl.Id = rt.ContractLiquidationId
INNER JOIN Payroll.Contract c ON c.Id = cl.ContractId
INNER JOIN Payroll.Employee E ON E.Id = cl.EmployeeId
LEFT JOIN Payroll.[Group] g ON g.Id = c.GroupId
LEFT JOIN Payroll.PayrollParameter pp ON pp.Id = g.PayrollParameterId  
LEFT JOIN Payroll.FunctionalUnit fu ON fu.Id = c.FunctionalUnitId
LEFT JOIN Common.ThirdParty tp ON tp.Id = E.ThirdPartyId
LEFT JOIN Contributions ap ON ap.ContractLiquidationId = cl.Id;

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reportería para la retención en la fuente sobre liquidaciones definitivas de contratos laborales. Combina los datos de la liquidación del contrato (valores devengados totales, fecha de retiro) con las deducciones del empleado por aportes a pensión obligatoria y voluntaria, fondo de solidaridad, salud, medicina prepagada y AFC, para calcular la renta exenta de trabajo del 25% con tope de 240 UVT según el Artículo 206 del Estatuto Tributario. Expone una fila por cada línea de retención en la fuente o retención por indemnización (clases de concepto 020 y 061), incluyendo la base gravable, el valor total retenido, el NIT e identificación del empleado, el grupo y parámetros de nómina, y la sede o sucursal. Se usa para generar el reporte de retención en la fuente de empleados retirados y alimentar los archivos RTEFTE de liquidaciones de contratos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportContractLiquidationWithholding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportContractLiquidationWithholding';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida, por cada liquidación definitiva de contrato laboral, la base gravable y el valor de retención en la fuente junto con los aportes y deducciones que disminuyen dicha base, para alimentar reportes tributarios.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle de liquidación debe tener registros con ConceptType = 2 (deducciones) para poder agregar aportes.; Los conceptos de retención deben estar parametrizados en Payroll.Concept con ConceptClass ''020'' o ''061''.; Cada liquidación debe tener contrato, empleado y tercero asociados; las uniones a Group, FunctionalUnit y ThirdParty son opcionales (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan liquidaciones que poseen al menos un concepto de retención clasificado como ''020'' o ''061''.; Los aportes (PensionContribution, AFC, SolidarityFund, etc.) se calculan únicamente sobre detalles con ConceptType = 2.; DependentDeduction y HousingDeduction siempre se entregan en 0 (no se calculan en la vista).; Article siempre se emite con valor 1 (TINYINT).; TotalAccrued y TotalAccruedRTF se exponen con el mismo valor de cl.TotalAccrued.; Todos los valores monetarios y códigos opcionales se normalizan con ISNULL para evitar NULLs en el reporte.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de contrato laboral; Retención en la fuente; Base gravable; Aporte a pensión obligatoria; Aporte voluntario a pensión; Fondo de solidaridad pensional; AFC (Cuenta de Ahorro para el Fomento de la Construcción); Aporte a salud; Medicina prepagada; Deducción por dependientes; Deducción por vivienda; Tipo de declarante; Procedimiento de retención (RTF); Unidad funcional; Grupo de nómina; Tercero / NIT', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportContractLiquidationWithholding: Devuelve una fila por cada detalle de liquidación cuyo concepto tenga ConceptClass IN (''020'',''061''), enriquecido con los aportes agregados de la misma liquidación.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si co.ConceptClass = ''014'' → El valor Deducted suma a PensionContribution; si co.ConceptClass = ''016'' → El valor Deducted suma a VoluntaryPensionContribution; si co.ConceptClass = ''038'' → El valor Deducted suma a SolidarityFund; si co.ConceptClass = ''045'' → El valor Deducted suma a AFC; si co.ConceptClass = ''017'' → El valor Deducted suma a HealthContribution; si co.ConceptClass = ''019'' → El valor Deducted suma a PrepaidMedicine; si co.ConceptClass IN (''020'',''061'') → El detalle se considera retención: Deducted = TotalWithholding y RetentionBase = TaxableBase; si tp.Nit es NULL → Se usa cl.EmployeeId convertido a VARCHAR(15) como Identification else Se usa tp.Nit como Identification; si E.ProcedureTypeRTF es NULL → Procedure se reporta como 1 por defecto', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ContractLiquidationDetail; Payroll.Concept; Payroll.ContractLiquidation; Payroll.Contract; Payroll.Employee; Payroll.Group; Payroll.FunctionalUnit; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
