/*
Vista: ViewReportAccountManagement
Propósito: Consolidar información de folios para reportes de gestión de cuentas
Retorna: Folios (registrados/facturados) con su propietario actual, área de gestión y datos asociados
Lógica de propietario: 
  - Si existe traslado aceptado más reciente -> Usuario receptor del traslado
  - Si no -> Usuario asignado automáticamente
*/
CREATE VIEW [AccountManagement].[ViewReportAccountManagement] AS
WITH
    -- Ingresos relevantes: con distribución automática o con traslados
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

    -- Folios de los ingresos relevantes (Status 1=Registrado, 2=Facturado)
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
    IP.IPNOMCOMP						AS PatientCodeName,
	IP.IPCODPACI						AS PatientCode,
    BF.AdmissionNumber					AS AdmissionNumber,
    BF.AdmissionDate					AS AdmissionDate, 
    UF.UFUDESCRI						AS FunctionalUnitName,  
    I.InvoiceNumber						AS InvoiceNumber,
    UA.FullName							AS CurrentOwnerFullName,
	UA.UserCode							AS CurrentOwnerCode
    
FROM BaseFolios BF
-- Datos maestros del folio, paciente, factura
JOIN Contract.CareGroup CG WITH (NOLOCK) ON CG.Id = BF.CareGroupId
JOIN INPACIENT IP WITH (NOLOCK) ON IP.IPCODPACI = BF.PatientCode
JOIN INUNIFUNC UF WITH (NOLOCK) ON UF.UFUCODIGO = BF.FunctionalUnitCode
JOIN Billing.Invoice I WITH (NOLOCK) ON I.RevenueControlDetailId = BF.RevenueControlDetailId
JOIN Contract.HealthAdministrator HA WITH (NOLOCK) ON HA.Id = I.HealthAdministratorId

-- Asignación automática inicial del folio
LEFT JOIN AccountManagement.AutomaticEntryDistribution AED WITH (NOLOCK) 
	ON AED.AdmissionNumber = BF.AdmissionNumber 
	AND AED.EntryType = BF.EntryType

-- Último traslado aceptado del folio (si existe)
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

-- Propietario actual: usuario receptor del traslado o asignado inicialmente
OUTER APPLY (
    SELECT TOP 1 UA.*
    FROM AccountManagement.UsersAssignment UA WITH (NOLOCK)
    WHERE UA.UserCode = COALESCE(FT_Accepted.ReceivingUser, NULL)
       OR (FT_Accepted.ReceivingUser IS NULL AND UA.Id = AED.AssignedUserId)
) UA

-- Área de gestión del propietario (por asignación inicial)
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

-- Área de gestión del traslado (si existe, prevalece sobre la asignación)
LEFT JOIN AccountManagement.ManagementAreas MA_FT ON MA_FT.Id = FT_Accepted.ManagementAreaId

GO