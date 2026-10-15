CREATE OR ALTER VIEW AccountManagement.ViewAdmissionsPending
AS
;WITH CTE_AdmissionsInfo AS (
    SELECT 
    AD.NUMINGRES AS AdmissionNumber,
    AD.IPCODPACI AS PatientCode,
    AD.IFECHAING AS AdmissionDate,
    AD.CODCENATE AS AttentionCenterCode,
    AD.GENCAREGROUP AS CareGroupId,
    AD.UFUCODIGO AS FunctionalUnitCode,
    AD.CODCAMACT AS Bed,
    AD.CODDIAING AS DiagnosisCode,
    AD.TIPOINGRE AS TypeIncome,
    AD.IESTADOIN AS IncomeStatus,
    AD.CODUSUCRE AS UserCreation,
    AD.CODUSUMOD AS UserModification
    FROM dbo.ADINGRESO AD WITH (NOLOCK)
    CROSS APPLY (
        SELECT StartDateAssignment 
        FROM AccountManagement.AccountManagementParameters
    ) Params
    WHERE NOT EXISTS (
        SELECT 1 
        FROM AccountManagement.AutomaticEntryDistribution AED WITH (NOLOCK)
        WHERE AED.AdmissionNumber = AD.NUMINGRES
    )
    AND AD.IFECHAING >= Params.StartDateAssignment
)
SELECT 
    AI.AdmissionNumber                  AS AdmissionNumber,
    IP.IPNOMCOMP                        AS PatientFullName,
    AI.AdmissionDate                    AS AdmissionDate,
    AI.PatientCode                      AS PatientCode,
    IP.CODIGONIT                        AS Nit,
    UF.UFUDESCRI                        AS FunctionalUnitCodeName,
    CONCAT(CG.Code, ' - ', CG.Name)     AS CareGroup,
    AI.Bed                              AS Bed,
    DIAG.NOMDIAGNO                      AS Diagnosis,
    AI.TypeIncome                       AS TypeIncome,
    AI.AttentionCenterCode              AS AttentionCenterCode,
    AI.CareGroupId                      AS CareGroupId,
    CG.Code                             AS CareGroupCode,
    AI.FunctionalUnitCode               AS FunctionalUnitCode,
    AI.DiagnosisCode                    AS DiagnosisCode,
    AI.IncomeStatus                     AS IncomeStatus,
    AI.UserCreation                     AS UserCreation,
    AI.UserModification                 AS UserModification,

    CASE 
        WHEN RCD.Id IS NOT NULL 
            THEN 'Folios asociados'
        ELSE 'Folios no asociados'
    END                                 AS Folio

FROM CTE_AdmissionsInfo AI
JOIN INPACIENT IP WITH (NOLOCK) ON IP.IPCODPACI = AI.PatientCode
JOIN INUNIFUNC UF WITH (NOLOCK) ON UF.UFUCODIGO = AI.FunctionalUnitCode
JOIN INDIAGNOS DIAG WITH (NOLOCK) ON DIAG.CODDIAGNO = AI.DiagnosisCode
JOIN ADCENATEN CEN WITH (NOLOCK) ON CEN.CODCENATE = AI.AttentionCenterCode
JOIN Contract.CareGroup CG WITH (NOLOCK) ON CG.Id = AI.CareGroupId

LEFT JOIN Billing.RevenueControl RC WITH (NOLOCK) ON RC.AdmissionNumber = AI.AdmissionNumber
LEFT JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON RCD.RevenueControlId = RC.Id
LEFT JOIN Billing.Invoice I WITH (NOLOCK) ON I.RevenueControlDetailId = RCD.Id
LEFT JOIN Portfolio.AccountReceivable AR WITH (NOLOCK) ON AR.InvoiceId = I.Id

WHERE (AR.PortfolioStatus IS NULL OR AR.PortfolioStatus IN (1,2))
GO
