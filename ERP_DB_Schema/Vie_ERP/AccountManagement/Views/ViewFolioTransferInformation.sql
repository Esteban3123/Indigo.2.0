CREATE VIEW [AccountManagement].[ViewFolioTransferInformation] AS
SELECT	ROW_NUMBER() OVER (ORDER BY AD.NUMINGRES) AS Id,
		LTRIM(RTRIM(AD.NUMINGRES)) AS AdmissionNumber,
		IP.IPNOMCOMP PatientFullName,
		AD.IFECHAING AdmissionDate,
		AD.IPCODPACI PatientCode,
		
		CASE 
			WHEN FA.Id IS NOT NULL THEN CAST(1 AS BIT)
			ELSE CAST(0 AS BIT)
		END AS HasFolioAlert,

		AD.CODICAMHO BedNumber,
		DIAG.NOMDIAGNO Diagnosis,
		RCD.FolioOrder FolioNumber,
		RCD.TotalFolio FolioTotalValue,
		RCD.Id RevenueControlDetailId,
		UF.UFUDESCRI FunctionalUnitName,
		CG.Name CareGroupName,
		CG.Id CareGroupId,
		AD.CODCENATE AttentionCenterCode,

		CASE RCD.FolioType
			WHEN 1 THEN 'EAPB con contrato'
			WHEN 2 THEN 'EAPB sin contrato'
			WHEN 3 THEN 'Particulares'
			WHEN 4 THEN 'Aseguradoras'
			ELSE ''
		END AS FolioType,

		CASE RCD.Status
			WHEN 1 THEN 'Registrado'
			WHEN 2 THEN 'Facturado'
			WHEN 3 THEN 'Bloqueado'
			WHEN 4 THEN 'Anulado'
			WHEN 5 THEN 'Reconocimiento Ingresos'
			WHEN 6 THEN 'Factura Asociada'
			WHEN 7 THEN 'Folio Cerrado'
			ELSE ''
		END AS FolioStatusDescription,

		AED.AssignmentDate AssignmentDate, --Fecha de asignación para comparar con tiempo de gestión
		UA.FullName AssignedUserFullname,
		UA.UserCode AssignedUserCode,
		MApick.ManagementAreaName AS ManagementAreaName, -- Area de gestión del usuario asignado (El usuario viene de Asignación de usuarios, no de areas de gestión)
		AD.CODUSUCRE AdmissionCreationUser,
		AD.CODUSUMOD AdmissionModificationUser,
		I.InvoicedUser InvoiceUser,
		I.InvoiceNumber AssociatedInvoice,
	
		3 AS IsOnTime,

		CASE FT.TransferStatus
			WHEN 1 THEN 'Sin traslado'
			WHEN 2 THEN 'Pendiente de aceptación'
			WHEN 3 THEN 'Aceptada'
			WHEN 4 THEN 'Rechazada'
			ELSE 'Sin Traslado'
		END AS TransferStatus

/* Core */
FROM AccountManagement.AutomaticEntryDistribution AED
JOIN ADINGRESO AD WITH (NOLOCK) ON AED.AdmissionNumber = AD.NUMINGRES
JOIN Billing.RevenueControl RC WITH (NOLOCK) ON RC.AdmissionNumber = AED.AdmissionNumber
JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON RCD.RevenueControlId = RC.Id
LEFT JOIN Billing.Invoice I WITH (NOLOCK) ON I.RevenueControlDetailId = RCD.Id

/* Info adicional del ingreso */
JOIN Contract.CareGroup CG WITH (NOLOCK) ON CG.Id = RCD.CareGroupId
JOIN INPACIENT IP WITH (NOLOCK) ON IP.IPCODPACI = AD.IPCODPACI
JOIN INUNIFUNC UF WITH (NOLOCK) ON UF.UFUCODIGO = AD.UFUCODIGO
LEFT JOIN INDIAGNOS DIAG WITH (NOLOCK) ON DIAG.CODDIAGNO = AD.CODDIAING

/* Traslados */
LEFT JOIN AccountManagement.FolioTransfer FT WITH (NOLOCK) ON FT.RevenueControlDetailId = RCD.Id
LEFT JOIN AccountManagement.FolioAlert FA WITH (NOLOCK) ON FA.RevenueControlDetailId = RCD.Id AND FA.Status = 1

/* Usuarios */
LEFT JOIN AccountManagement.UsersAssignment UA WITH (NOLOCK) ON UA.Id = AED.AssignedUserId
OUTER APPLY (
    SELECT TOP (1)
           MA.Id   AS ManagementAreaId,
           MA.Name AS ManagementAreaName
    FROM AccountManagement.ManagementAreasUser MAU WITH (NOLOCK)
    JOIN AccountManagement.ManagementAreas MA WITH (NOLOCK)
      ON MA.Id = MAU.ManagementAreasId
    WHERE MAU.Usercode = UA.UserCode
    ORDER BY MAU.Id DESC
) MApick