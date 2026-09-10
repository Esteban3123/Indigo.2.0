-- =============================================
-- Author:		Oscar stiven Astudillo Reyes
-- Create date: 2026-02-05
-- Description: Vista para obtener la informacion de contratos liquidados y su retencion
-- =============================================


CREATE VIEW [Payroll].[ViewReportContractLiquidationWithholding]
AS
WITH

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

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de liquidaciones de contrato con sus bases y valores de retención en la fuente, junto con aportes y deducciones del empleado, para reportes tributarios.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en ContractLiquidationDetail con conceptos cuya ConceptClass sea ''020'' o ''061'' (retención) para que aparezcan en el resultado; Cada ContractLiquidation debe tener Contract y Employee asociados (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran aportes/deducciones cuando ConceptType = 2; Article siempre se entrega con valor fijo 1; Las deducciones DependentDeduction y HousingDeduction siempre se exponen como 0 (no se calculan en la vista); Los valores de aportes nunca son nulos: se aplica ISNULL a 0; Solo aparecen liquidaciones que tengan al menos un concepto de retención (ConceptClass 020 o 061)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de contrato; Retención en la fuente; Base gravable (TaxableBase); Aporte a pensión; Aporte voluntario a pensión; Fondo de solidaridad; AFC; Aporte a salud; Medicina prepagada; Deducción por dependientes; Deducción por vivienda; Tipo de declarante; Procedimiento de retención (RTF); Unidad funcional; Grupo de nómina; Tercero / NIT del empleado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportContractLiquidationWithholding: Devuelve una fila por cada detalle de liquidación cuyo concepto pertenezca a ConceptClass ''020'' o ''061'', enriquecido con totales agrupados de aportes (clases 014, 016, 017, 019, 038, 045) por liquidación', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ConceptClass = ''014'' en detalle con ConceptType=2 → Suma el Deducted como PensionContribution; si ConceptClass = ''016'' → Suma el Deducted como VoluntaryPensionContribution; si ConceptClass = ''038'' → Suma el Deducted como SolidarityFund; si ConceptClass = ''045'' → Suma el Deducted como AFC; si ConceptClass = ''017'' → Suma el Deducted como HealthContribution; si ConceptClass = ''019'' → Suma el Deducted como PrepaidMedicine; si ConceptClass IN (''020'',''061'') → El detalle se considera registro de retención (TaxableBase = RetentionBase, TotalWithholding = Deducted); si ThirdParty del empleado existe → Identification y Name se toman del tercero; en caso contrario Identification = EmployeeId y Name = ''''; si Employee.ProcedureTypeRTF es nulo → Procedure se asume como 1', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ContractLiquidationDetail; Payroll.Concept; Payroll.ContractLiquidation; Payroll.Contract; Payroll.Employee; Payroll.Group; Payroll.FunctionalUnit; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportContractLiquidationWithholding';
GO
