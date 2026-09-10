
CREATE VIEW [AccountManagement].[ViewFolioTransferInformation] AS
WITH 
/* CTE para obtener solo los ingresos relevantes (FILTRO INICIAL PARA OPTIMIZACIÓN) */
RelevantAdmissions AS (
	SELECT DISTINCT AdmissionNumber
	FROM (
		-- Ingresos asignados automáticamente
		SELECT AdmissionNumber 
		FROM AccountManagement.AutomaticEntryDistribution WITH (NOLOCK)
		
		UNION
		
		-- Ingresos que tienen folios con traslados
		SELECT AdmissionNumber
		FROM AccountManagement.FolioTransfer WITH (NOLOCK)
		WHERE TransferStatus IN (2, 3, 4) -- Pendiente, Aceptado o Rechazado
	) AS Combined
),
/* CTE para obtener el último traslado de cada folio */
LastTransfer AS (
	SELECT 
		FT.RevenueControlDetailId,
		FT.PreviousUser,
		FT.ReceivingUser,
		FT.TransferStatus,
		FT.CreationDate,
		FT.ManagementAreaId,
		ROW_NUMBER() OVER (PARTITION BY FT.RevenueControlDetailId ORDER BY FT.Id DESC) AS RowNum
	FROM AccountManagement.FolioTransfer FT WITH (NOLOCK)
	-- Optimización: Solo procesar traslados de ingresos relevantes
	WHERE EXISTS (
		SELECT 1 FROM RelevantAdmissions RA 
		WHERE RA.AdmissionNumber = FT.AdmissionNumber
	)
),
/* CTE para determinar el owner actual de cada folio y su información de traslado */
FolioOwnership AS (
	SELECT 
		RC.AdmissionNumber,
		RCD.Id AS RevenueControlDetailId,
		RCD.FolioOrder,
		RCD.TotalFolio,
		RCD.Status AS FolioStatus,
		RCD.FolioType,
		RCD.CareGroupId,
		
		/* Determinar el owner actual del folio basándose en el estado del traslado */
		CASE 
			-- Si no hay traslado o el traslado fue rechazado, el owner es el usuario asignado originalmente
			WHEN LT.RevenueControlDetailId IS NULL OR LT.TransferStatus IN (1, 4) 
				THEN UA.UserCode
			-- Si el traslado fue aceptado, el owner es el usuario receptor
			WHEN LT.TransferStatus = 3 
				THEN LT.ReceivingUser
			-- Si el traslado está pendiente, el owner sigue siendo el usuario anterior
			WHEN LT.TransferStatus = 2 
				THEN LT.PreviousUser
			ELSE UA.UserCode
		END AS CurrentOwner,
		
		/* Usuario visible en el dashboard (el que fue asignado originalmente o el actual) */
		CASE 
			WHEN LT.RevenueControlDetailId IS NOT NULL AND LT.TransferStatus = 3 
				THEN LT.ReceivingUser
			ELSE UA.UserCode
		END AS AssignedUserCode,
		
		/* Nombre del usuario actual */
		CASE 
			WHEN LT.RevenueControlDetailId IS NOT NULL AND LT.TransferStatus = 3 
				THEN UAReceiving.FullName
			ELSE UA.FullName
		END AS AssignedUserFullname,
		
		/* Información del traslado */
		COALESCE(LT.TransferStatus, 1) AS TransferStatus,
		LT.PreviousUser,
		LT.ReceivingUser,
		
		/* Fecha de asignación o traslado */
		CASE 
			WHEN LT.RevenueControlDetailId IS NOT NULL AND LT.TransferStatus = 3 
				THEN LT.CreationDate
			ELSE AED.AssignmentDate
		END AS AssignmentDate,
		
		AED.Id AS AutomaticEntryDistributionId,
		AED.AssignedUserId AS OriginalAssignedUserId,
		AED.EntryType
		
	FROM RelevantAdmissions RA
	-- OPTIMIZACIÓN CLAVE: Usar el filtro de ingresos relevantes primero
	JOIN Billing.RevenueControl RC WITH (NOLOCK) ON RC.AdmissionNumber = RA.AdmissionNumber
	JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON RCD.RevenueControlId = RC.Id
	LEFT JOIN AccountManagement.AutomaticEntryDistribution AED WITH (NOLOCK) ON AED.AdmissionNumber = RC.AdmissionNumber
	LEFT JOIN AccountManagement.UsersAssignment UA WITH (NOLOCK) ON UA.Id = AED.AssignedUserId
	LEFT JOIN LastTransfer LT ON LT.RevenueControlDetailId = RCD.Id AND LT.RowNum = 1
	LEFT JOIN AccountManagement.UsersAssignment UAReceiving WITH (NOLOCK) 
		ON UAReceiving.UserCode = LT.ReceivingUser 
		AND UAReceiving.EntryType = AED.EntryType
)

SELECT 
	ROW_NUMBER() OVER (ORDER BY AD.NUMINGRES, FO.FolioOrder) AS Id,
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
	FO.FolioOrder FolioNumber,
	FO.TotalFolio FolioTotalValue,
	FO.RevenueControlDetailId,
	UF.UFUDESCRI FunctionalUnitName,
	CG.Name CareGroupName,
	CG.Id CareGroupId,
	AD.CODCENATE AttentionCenterCode,

	CASE FO.FolioType
		WHEN 1 THEN 'EAPB con contrato'
		WHEN 2 THEN 'EAPB sin contrato'
		WHEN 3 THEN 'Particulares'
		WHEN 4 THEN 'Aseguradoras'
		ELSE ''
	END AS FolioType,

	CASE FO.FolioStatus
		WHEN 1 THEN 'Registrado'
		WHEN 2 THEN 'Facturado'
		WHEN 3 THEN 'Bloqueado'
		WHEN 4 THEN 'Anulado'
		WHEN 5 THEN 'Reconocimiento Ingresos'
		WHEN 6 THEN 'Factura Asociada'
		WHEN 7 THEN 'Folio Cerrado'
		ELSE ''
	END AS FolioStatusDescription,

	FO.AssignmentDate, --Fecha de asignación o traslado efectivo
	FO.AssignedUserFullname,
	FO.AssignedUserCode,
	MApick.ManagementAreaName, -- Area de gestión del usuario asignado
	AD.CODUSUCRE AdmissionCreationUser,
	AD.CODUSUMOD AdmissionModificationUser,
	I.InvoicedUser InvoiceUser,
	I.InvoiceNumber AssociatedInvoice,

	3 AS IsOnTime,

	CASE FO.TransferStatus
		WHEN 1 THEN 'Sin traslado'
		WHEN 2 THEN 'Pendiente de aceptación'
		WHEN 3 THEN 'Aceptada'
		WHEN 4 THEN 'Rechazada'
		ELSE 'Sin Traslado'
	END AS TransferStatus,
	
	/* Información adicional sobre usuarios en traslados */
	FO.CurrentOwner AS CurrentOwnerCode,
	FO.PreviousUser AS TransferPreviousUser,
	FO.ReceivingUser AS TransferReceivingUser,
	
	/* Indicadores para filtrado */
	CASE 
		WHEN FO.TransferStatus = 2 AND FO.ReceivingUser = FO.AssignedUserCode 
			THEN CAST(1 AS BIT)
		ELSE CAST(0 AS BIT)
	END AS IsPendingToAccept,
	
	CASE 
		WHEN FO.TransferStatus = 4 AND FO.PreviousUser = FO.AssignedUserCode 
			THEN CAST(1 AS BIT)
		ELSE CAST(0 AS BIT)
	END AS WasRejected,
	
	CASE 
		WHEN FO.TransferStatus = 2 AND FO.PreviousUser = FO.AssignedUserCode 
			THEN CAST(1 AS BIT)
		ELSE CAST(0 AS BIT)
	END AS IsPendingFromMe

FROM FolioOwnership FO
JOIN ADINGRESO AD WITH (NOLOCK) ON AD.NUMINGRES = FO.AdmissionNumber

/* Info adicional del ingreso */
JOIN Contract.CareGroup CG WITH (NOLOCK) ON CG.Id = FO.CareGroupId
JOIN INPACIENT IP WITH (NOLOCK) ON IP.IPCODPACI = AD.IPCODPACI
JOIN INUNIFUNC UF WITH (NOLOCK) ON UF.UFUCODIGO = AD.UFUCODIGO
LEFT JOIN INDIAGNOS DIAG WITH (NOLOCK) ON DIAG.CODDIAGNO = AD.CODDIAING

/* Factura asociada */
LEFT JOIN Billing.Invoice I WITH (NOLOCK) ON I.RevenueControlDetailId = FO.RevenueControlDetailId

/* Area de gestión del usuario actual */
OUTER APPLY (
    SELECT TOP (1)
           MA.Id   AS ManagementAreaId,
           MA.Name AS ManagementAreaName
    FROM AccountManagement.ManagementAreasUser MAU WITH (NOLOCK)
    JOIN AccountManagement.ManagementAreas MA WITH (NOLOCK)
      ON MA.Id = MAU.ManagementAreasId
    WHERE MAU.Usercode = FO.AssignedUserCode
    ORDER BY MAU.Id DESC
) MApick

/* Alertas de folio */
LEFT JOIN AccountManagement.FolioAlert FA WITH (NOLOCK) ON FA.RevenueControlDetailId = FO.RevenueControlDetailId AND FA.Status = 1

/* 
   ===================================================================================
   NOTAS DE USO DE LA VISTA:
   ===================================================================================
   
   Esta vista muestra todos los folios con su información de ownership actual.
   
   Para el DASHBOARD principal, filtrar los folios de un usuario específico usando:
   WHERE CurrentOwnerCode = @UserCode
   
   Esto mostrará:
   1. Folios asignados automáticamente (sin traslados)
   2. Folios aceptados por traslado (donde el usuario es el nuevo owner)
   3. Folios rechazados que volvieron al usuario
   4. Folios trasladados por el usuario que están pendientes (sigue siendo owner)
   
   NOTA: Los folios pendientes de aceptación DONDE EL USUARIO ES RECEPTOR se muestran
   en otro formulario separado. Para obtenerlos usar:
   WHERE TransferStatus = 2 AND TransferReceivingUser = @UserCode
   
   ESCENARIOS DE OWNERSHIP:
   - Sin traslado: Owner = usuario asignado originalmente
   - Traslado aceptado: Owner = usuario receptor del traslado
   - Traslado rechazado: Owner = usuario anterior (quien intentó trasladar)
   - Traslado pendiente: Owner = usuario anterior (hasta que se acepte)
   
   COLUMNAS DE AYUDA PARA FILTRADO:
   - CurrentOwnerCode: El código del usuario que es owner actual del folio
   - IsPendingToAccept: TRUE si el folio está pendiente de aceptar (receptor = usuario actual)
   - WasRejected: TRUE si el folio fue rechazado y volvió al usuario actual
   - IsPendingFromMe: TRUE si el usuario actual trasladó el folio y está pendiente
*/
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista orientada a dashboards y consultas de gestión de cartera hospitalaria. Consolida la información de folios de facturación por admisión de paciente, determinando el propietario actual (owner) de cada folio según el estado de sus traslados entre gestores. Expone indicadores de estado de transferencia (pendiente, aceptado, rechazado), alertas activas, datos clínicos del ingreso (diagnóstico, cama, unidad funcional) y la factura asociada cuando existe.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewFolioTransferInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewFolioTransferInformation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola fila por folio la información del ingreso, paciente, control de ingresos y el estado actual de traslado/ownership entre usuarios de gestión, para alimentar dashboards de seguimiento de folios.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewFolioTransferInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en AutomaticEntryDistribution o en FolioTransfer con TransferStatus en (2,3,4) para que la admisión sea considerada relevante y aparezca en la vista.; Cada AdmissionNumber debe tener su correspondiente RevenueControl con RevenueControlDetail, ADINGRESO, INPACIENT y INUNIFUNC; de lo contrario la fila se descarta por los JOIN internos.; El CareGroupId del RevenueControlDetail debe existir en Contract.CareGroup.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewFolioTransferInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran admisiones con distribución automática registrada o con al menos un traslado en estado Pendiente(2), Aceptado(3) o Rechazado(4); admisiones sin esos registros no aparecen.; El último traslado por folio se determina con ROW_NUMBER() OVER (PARTITION BY RevenueControlDetailId ORDER BY Id DESC) tomando RowNum=1.; Cuando no existe traslado registrado, TransferStatus se reporta como 1 (''Sin traslado'') vía COALESCE(LT.TransferStatus,1).; El owner actual depende exclusivamente del último traslado: aceptado→receptor, pendiente→usuario anterior, rechazado/sin traslado→usuario asignado original.; El área de gestión (ManagementAreaName) corresponde al último vínculo (mayor MAU.Id) del usuario actualmente asignado al folio.; Solo se vinculan alertas de folio activas (FolioAlert.Status = 1) para calcular HasFolioAlert.; El receptor de traslado se vincula a UsersAssignment exigiendo coincidencia de EntryType con el de la distribución automática original.; El campo IsOnTime siempre se devuelve con valor fijo 3 (no se calcula a partir de tiempos).', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewFolioTransferInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Folio de facturación; Ingreso/Admisión del paciente; Traslado de folio entre usuarios de gestión; Ownership de folio (usuario responsable); Distribución automática de ingresos; Área de gestión; Grupo de atención (CareGroup); Unidad funcional; Diagnóstico de ingreso; Factura asociada; Alerta de folio; Estado de folio (Registrado/Facturado/Bloqueado/Anulado/Reconocimiento Ingresos/Factura Asociada/Cerrado); Tipo de folio (EAPB con/sin contrato, Particulares, Aseguradoras)', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewFolioTransferInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AccountManagement.ViewFolioTransferInformation: Devuelve una fila por cada RevenueControlDetail (folio) de admisiones que tengan distribución automática o traslados en estado 2/3/4, con su owner actual, estado de traslado y datos del paciente/ingreso.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewFolioTransferInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LT.RevenueControlDetailId IS NULL OR LT.TransferStatus IN (1,4) (sin traslado o rechazado) → CurrentOwner = UA.UserCode (usuario asignado originalmente en AutomaticEntryDistribution).; si LT.TransferStatus = 3 (traslado aceptado) → CurrentOwner = LT.ReceivingUser; AssignedUserCode/Fullname y AssignmentDate también se toman del receptor y de la fecha de creación del traslado.; si LT.TransferStatus = 2 (traslado pendiente) → CurrentOwner = LT.PreviousUser; el folio sigue siendo del usuario anterior hasta aceptación.; si FA.Id IS NOT NULL (existe FolioAlert con Status=1 para el folio) → HasFolioAlert = 1. else HasFolioAlert = 0.; si TransferStatus = 2 AND ReceivingUser = AssignedUserCode → IsPendingToAccept = 1 (el folio está pendiente de que el usuario actual lo acepte). else IsPendingToAccept = 0.; si TransferStatus = 4 AND PreviousUser = AssignedUserCode → WasRejected = 1 (el traslado fue rechazado y volvió al usuario que lo originó). else WasRejected = 0.; si TransferStatus = 2 AND PreviousUser = AssignedUserCode → IsPendingFromMe = 1 (el usuario trasladó el folio y aún está pendiente). else IsPendingFromMe = 0.; si Mapeo de FolioType (1..4) → 1=''EAPB con contrato'', 2=''EAPB sin contrato'', 3=''Particulares'', 4=''Aseguradoras'', otros=''''.; si Mapeo de FolioStatus (1..7) → 1=Registrado, 2=Facturado, 3=Bloqueado, 4=Anulado, 5=Reconocimiento Ingresos, 6=Factura Asociada, 7=Folio Cerrado.; si Mapeo de TransferStatus (1..4) → 1=''Sin traslado'', 2=''Pendiente de aceptación'', 3=''Aceptada'', 4=''Rechazada''. else ''Sin Traslado''.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewFolioTransferInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'AccountManagement.AutomaticEntryDistribution; AccountManagement.FolioTransfer; AccountManagement.UsersAssignment; AccountManagement.ManagementAreasUser; AccountManagement.ManagementAreas; AccountManagement.FolioAlert; Billing.RevenueControl; Billing.RevenueControlDetail; Billing.Invoice; Contract.CareGroup; ADINGRESO; INPACIENT; INUNIFUNC; INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewFolioTransferInformation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'VIEW', @level1name=N'ViewFolioTransferInformation';
GO
