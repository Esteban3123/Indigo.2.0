
CREATE VIEW [AccountManagement].[ViewAdmissionsPending]
AS
WITH CTE_AdmissionsInfo AS (
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
    ROW_NUMBER() OVER (ORDER BY AI.AdmissionNumber, ISNULL(RCD.Id, 0)) AS RowId,
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

	CASE AI.TypeIncome
		WHEN 1 THEN 'Ambulatorio'
		WHEN 2 THEN 'Hospitalario'
	END									AS TypeIncomeName,

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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta para el módulo de gestión de cuentas que expone admisiones registradas en `ADINGRESO` que aún no han sido asignadas en la distribución automática de ingresos, filtradas desde la fecha de inicio configurada en los parámetros del módulo. Para cada admisión pendiente, presenta datos del paciente, unidad funcional, grupo de atención, diagnóstico y tipo de ingreso (ambulatorio/hospitalario), además de indicar si ya existen folios de facturación asociados mediante la cadena de control de ingresos, detalle de folios, facturas y cuentas por cobrar activas (estado de cartera nulo, 1 o 2).', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewAdmissionsPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewAdmissionsPending';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las admisiones de pacientes pendientes de asignación automática a un usuario gestor de cuentas, enriquecidas con datos del paciente, unidad funcional, diagnóstico, grupo de atención y estado de folios/cartera.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewAdmissionsPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en AccountManagement.AccountManagementParameters con StartDateAssignment definido (CROSS APPLY exige al menos una fila).; Cada admisión retornada debe tener paciente (INPACIENT), unidad funcional (INUNIFUNC), diagnóstico (INDIAGNOS), centro de atención (ADCENATEN) y grupo de atención (Contract.CareGroup) existentes (JOINs internos).; La fecha de ingreso (IFECHAING) debe ser mayor o igual a la fecha de inicio de asignación parametrizada.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewAdmissionsPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se listan admisiones que ya tengan registro en AutomaticEntryDistribution (es decir, ya distribuidas automáticamente).; Nunca se listan admisiones con fecha de ingreso anterior a StartDateAssignment configurada en los parámetros del módulo.; Nunca se listan admisiones cuya cartera esté en estados distintos de NULL, 1 o 2.; Una admisión puede aparecer duplicada si tiene múltiples RevenueControlDetail/Invoice/AccountReceivable que satisfagan el filtro (no hay DISTINCT).', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewAdmissionsPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'admisión/ingreso de paciente; paciente; unidad funcional; diagnóstico; centro de atención; grupo de atención (CareGroup); tipo de ingreso ambulatorio/hospitalario; folios de facturación; control de ingresos; factura; cuenta por cobrar/cartera; asignación automática de cuentas', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewAdmissionsPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AccountManagement.ViewAdmissionsPending: Devuelve solo admisiones (ADINGRESO) que NO existen aún en AutomaticEntryDistribution y cuyo IFECHAING >= StartDateAssignment parametrizado.; [RETURN_RESULT] AccountManagement.ViewAdmissionsPending: Filtra a admisiones cuya cuenta por cobrar (AccountReceivable.PortfolioStatus) sea NULL o esté en (1,2); excluye admisiones con cartera en otros estados.; [RETURN_RESULT] AccountManagement.ViewAdmissionsPending: Etiqueta ''Folios asociados'' si existe RevenueControlDetail vinculado al RevenueControl de la admisión; en caso contrario ''Folios no asociados''.; [RETURN_RESULT] AccountManagement.ViewAdmissionsPending: Traduce TypeIncome=1 a ''Ambulatorio'' y TypeIncome=2 a ''Hospitalario''; otros valores quedan NULL en TypeIncomeName.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewAdmissionsPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AI.TypeIncome = 1 → TypeIncomeName = ''Ambulatorio'' else Si TypeIncome = 2 → ''Hospitalario''; otro valor → NULL; si RCD.Id IS NOT NULL (existe detalle de control de ingresos para la admisión) → Folio = ''Folios asociados'' else Folio = ''Folios no asociados''; si AR.PortfolioStatus IS NULL OR AR.PortfolioStatus IN (1,2) → Se incluye la admisión en el resultado else Se excluye', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewAdmissionsPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; AccountManagement.AccountManagementParameters; AccountManagement.AutomaticEntryDistribution; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INDIAGNOS; dbo.ADCENATEN; Contract.CareGroup; Billing.RevenueControl; Billing.RevenueControlDetail; Billing.Invoice; Portfolio.AccountReceivable', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewAdmissionsPending';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewAdmissionsPending';
GO
