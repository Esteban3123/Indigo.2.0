CREATE VIEW [AccountManagement].[ViewReportAccountManagement] AS
WITH
    -- 1) Ingresos relevantes (asignados o con traslados)
    BaseAdmissions AS (
        SELECT DISTINCT AdmissionNumber
        FROM (
            SELECT AED.AdmissionNumber
            FROM AccountManagement.AutomaticEntryDistribution AED WITH (NOLOCK)

            UNION

            SELECT FT.AdmissionNumber
            FROM AccountManagement.FolioTransfer FT WITH (NOLOCK)
        ) AD
    ),

    -- 2) Base de folios de esos ingresos
    BaseFolios AS (
        SELECT DISTINCT
            BA.AdmissionNumber      AS AdmissionNumber,
            AD.TIPOINGRE            AS EntryType,
            AD.CODCENATE            AS AttentionCenterCode,
            AD.IFECHAING            AS AdmissionDate,
            AD.IPCODPACI            AS PatientCode,
            AD.UFUCODIGO            AS FunctionalUnitCode,
            RCD.Id                  AS RevenueControlDetailId,
            RCD.FolioOrder          AS FolioOrder,
            RCD.TotalFolio          AS TotalFolio,
            RCD.Status              AS Status,
            RCD.CareGroupId         AS CareGroupId
            
        FROM BaseAdmissions BA
        JOIN ADINGRESO AD                     WITH (NOLOCK) ON AD.NUMINGRES = BA.AdmissionNumber
        JOIN Billing.RevenueControl RC        WITH (NOLOCK) ON RC.AdmissionNumber = BA.AdmissionNumber
        JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON RCD.RevenueControlId = RC.Id
		WHERE RCD.Status IN (1,2)
    ) 

SELECT 
	BF.RevenueControlDetailId			AS RevenueControlDetailId,
    BF.AttentionCenterCode				AS AttentionCenterCode,
    COALESCE(MA_FT.Name, MA_AD.Name)	AS ManagementAreaName,
	COALESCE(MA_FT.Id, MA_AD.Id)		AS ManagementAreaId,
    BF.Status							AS FolioStatus,
    CASE BF.Status
        WHEN 1 THEN 'Registrado'
        WHEN 2 THEN 'Facturado'
        ELSE ''
    END                                 AS FolioStatusDescription,
    CG.Name								AS CareGroupName, 
	CG.Id								AS CareGroupId,
    HA.Name								AS HealthAdministrationName, 
	HA.Id								AS HealthAdministrationId,
    BF.TotalFolio						AS FolioTotalValue,
    CONCAT(IP.IPCODPACI, ' - ', IP.IPNOMCOMP)						AS PatientCodeName,
	IP.IPCODPACI						AS PatientCode,
    BF.AdmissionNumber					AS AdmissionNumber,
    BF.AdmissionDate					AS AdmissionDate, 
    UF.UFUDESCRI						AS FunctionalUnitName,  
    I.InvoiceNumber						AS InvoiceNumber,
    UA.FullName							AS CurrentOwnerFullName,
	UA.UserCode							AS CurrentOwnerCode
    
FROM BaseFolios BF
JOIN Contract.CareGroup CG WITH (NOLOCK) ON CG.Id = BF.CareGroupId
JOIN INPACIENT IP WITH (NOLOCK) ON IP.IPCODPACI = BF.PatientCode
JOIN INUNIFUNC UF WITH (NOLOCK) ON UF.UFUCODIGO = BF.FunctionalUnitCode
JOIN Billing.Invoice I WITH (NOLOCK) ON I.RevenueControlDetailId = BF.RevenueControlDetailId
JOIN Contract.HealthAdministrator HA WITH (NOLOCK) ON HA.Id = I.HealthAdministratorId

LEFT JOIN AccountManagement.AutomaticEntryDistribution AED WITH (NOLOCK) 
	ON AED.AdmissionNumber = BF.AdmissionNumber 
	AND AED.EntryType = BF.EntryType

LEFT JOIN (
    SELECT 
        FT.RevenueControlDetailId,
        FT.Id,
        FT.CreationDate,
        FT.TransferStatus,
        FT.AdmissionNumber,
        FT.ReceivingUser,
		FT.ManagementAreaId,
        ROW_NUMBER() OVER (PARTITION BY FT.RevenueControlDetailId ORDER BY FT.CreationDate DESC, FT.Id DESC) AS RowNum
    FROM AccountManagement.FolioTransfer FT WITH (NOLOCK)
    WHERE FT.TransferStatus = 3 -- Aceptada
) FT_Accepted ON FT_Accepted.RevenueControlDetailId = BF.RevenueControlDetailId AND FT_Accepted.RowNum = 1

OUTER APPLY (
    SELECT TOP 1 UA.*
    FROM AccountManagement.UsersAssignment UA WITH (NOLOCK)
    WHERE UA.UserCode = COALESCE(FT_Accepted.ReceivingUser, NULL)
       OR (FT_Accepted.ReceivingUser IS NULL AND UA.Id = AED.AssignedUserId)
) UA

OUTER APPLY (
	SELECT TOP 1 MAU.*
	FROM AccountManagement.ManagementAreasUser MAU
	WHERE MAU.Usercode = UA.UserCode
) MAU 

OUTER APPLY (
    SELECT TOP 1 MA.*
    FROM AccountManagement.ManagementAreas MA WITH (NOLOCK)
    WHERE MA.Id = MAU.ManagementAreasId
) MA_AD

LEFT JOIN AccountManagement.ManagementAreas MA_FT ON MA_FT.Id = FT_Accepted.ManagementAreaId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting para el módulo de gestión de cuentas que consolida, por folio de facturación (estados Registrado o Facturado), la información de admisión, paciente, unidad funcional, factura, entidad pagadora (EPS/administradora), grupo de atención y área de gestión responsable. Determina el propietario actual del folio priorizando el usuario receptor de la última transferencia aceptada o, en su defecto, el asignado automáticamente. Sirve como fuente aplanada para reportes de seguimiento y control de cartera por gestor y área administrativa.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewReportAccountManagement';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewReportAccountManagement';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada para reporte de gestión de cuentas: lista los folios facturables (registrados o facturados) de admisiones que han sido asignadas automáticamente o transferidas, junto con su responsable actual, área de gestión, factura, paciente, unidad funcional y administradora de salud.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewReportAccountManagement';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La admisión debe existir en AutomaticEntryDistribution o en FolioTransfer para ser considerada en el reporte.; El folio (RevenueControlDetail) debe tener Status 1 (Registrado) o 2 (Facturado).; Debe existir una factura (Billing.Invoice) asociada al RevenueControlDetailId; de lo contrario el folio no aparece (JOIN obligatorio).; Debe existir grupo de atención (CareGroup), paciente (INPACIENT), unidad funcional (INUNIFUNC) y administradora de salud (HealthAdministrator) referenciados; son JOIN internos.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewReportAccountManagement';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen folios con Status 1 o 2 (Registrado o Facturado).; Cada fila corresponde a un único RevenueControlDetailId con su factura asociada.; Cuando hay múltiples transferencias aceptadas para un mismo folio, prevalece la más reciente por CreationDate y, en empate, por Id mayor (RowNum=1).; El área de gestión y el dueño actual reflejan la transferencia aceptada más reciente; si no hay transferencia, reflejan la asignación automática original.; Las admisiones sin asignación automática ni transferencias quedan excluidas del reporte.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewReportAccountManagement';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión; Folio de facturación; Control de ingresos; Factura; Transferencia de folios; Asignación automática de cuentas; Área de gestión; Grupo de atención (CareGroup); Administradora de salud (EPS); Unidad funcional; Paciente; Gestor de cuentas (responsable)', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewReportAccountManagement';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AccountManagement.ViewReportAccountManagement: Devuelve una fila por RevenueControlDetailId que cumpla Status IN (1,2) y cuya admisión esté en AutomaticEntryDistribution o FolioTransfer, enriquecida con datos del responsable actual y el área de gestión.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewReportAccountManagement';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si BF.Status = 1 → FolioStatusDescription = ''Registrado'' else Si Status = 2 → ''Facturado''; cualquier otro valor → cadena vacía (filtrado previamente por WHERE Status IN (1,2)).; si Existe FolioTransfer con TransferStatus = 3 (Aceptada) para el RevenueControlDetailId → El responsable actual se toma del ReceivingUser de la transferencia más reciente (ORDER BY CreationDate DESC, Id DESC) y el área de gestión proviene de FolioTransfer.ManagementAreaId (MA_FT). else El responsable actual se toma de AutomaticEntryDistribution.AssignedUserId y el área de gestión se resuelve vía ManagementAreasUser → ManagementAreas (MA_AD).; si FolioTransfer.TransferStatus = 3 → Solo se consideran transferencias en estado ''Aceptada'' para determinar el dueño actual; transferencias en otros estados se ignoran.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewReportAccountManagement';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'AccountManagement.AutomaticEntryDistribution; AccountManagement.FolioTransfer; ADINGRESO; Billing.RevenueControl; Billing.RevenueControlDetail; Contract.CareGroup; INPACIENT; INUNIFUNC; Billing.Invoice; Contract.HealthAdministrator; AccountManagement.UsersAssignment; AccountManagement.ManagementAreasUser; AccountManagement.ManagementAreas', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewReportAccountManagement';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewReportAccountManagement';
GO
