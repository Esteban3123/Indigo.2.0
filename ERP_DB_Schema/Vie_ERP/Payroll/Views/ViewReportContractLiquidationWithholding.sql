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
