/*
   ===================================================================================
   PROCEDIMIENTO: GetFolioTraceability
   ===================================================================================
   PROPÓSITO: Obtiene la trazabilidad completa de los folios, mostrando todos los 
              estados de traslado por los que ha pasado cada folio de forma cronológica.
   
   PARÁMETROS:
   - @AttentionCenter: Código del centro de atención (OBLIGATORIO)
   - @UserCode: Código del usuario involucrado en traslados (opcional)
   - @AdmissionNumber: Número de ingreso específico (opcional)
   - @PatientCode: Código del paciente específico (opcional)
   
   TRAZABILIDAD:
   Para cada folio se muestra su historia completa:
   1. Estado inicial: Sin traslado
   2. Por cada traslado: Pendiente → Aceptado/Rechazado
   
   EJEMPLO DE SALIDA:
   FOLIO01 | Sin traslado     | UserA
   FOLIO01 | Solicitado       | UserA → UserB
   FOLIO01 | Aceptado         | UserB
   FOLIO01 | Solicitado       | UserB → UserC
   FOLIO01 | Rechazado        | UserB (con comentario)
   ===================================================================================
*/
CREATE PROCEDURE [AccountManagement].[SP_GetFolioTraceability]
    @AttentionCenter   VARCHAR(20),
    @UserCode          VARCHAR(20) = NULL,
    @AdmissionNumber   VARCHAR(50) = NULL,
    @PatientCode       VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @AttentionCenter IS NULL OR LTRIM(RTRIM(@AttentionCenter)) = ''
    BEGIN
        RAISERROR('El parámetro @AttentionCenter es obligatorio', 16, 1);
        RETURN;
    END;

    ;WITH
    -- 1) Ingresos relevantes (asignados o con traslados)
    RelevantAdmissions AS (
        SELECT DISTINCT AdmissionNumber
        FROM (
            SELECT AED.AdmissionNumber
            FROM AccountManagement.AutomaticEntryDistribution AED WITH (NOLOCK)
            JOIN dbo.ADINGRESO AD WITH (NOLOCK) ON AD.NUMINGRES = AED.AdmissionNumber
            WHERE AD.CODCENATE = @AttentionCenter
              AND (@AdmissionNumber IS NULL OR LTRIM(RTRIM(AD.NUMINGRES)) = @AdmissionNumber)
              AND (@PatientCode IS NULL OR AD.IPCODPACI = @PatientCode)

            UNION

            SELECT FT.AdmissionNumber
            FROM AccountManagement.FolioTransfer FT WITH (NOLOCK)
            JOIN dbo.ADINGRESO AD WITH (NOLOCK) ON AD.NUMINGRES = FT.AdmissionNumber
            WHERE AD.CODCENATE = @AttentionCenter
              AND (@AdmissionNumber IS NULL OR LTRIM(RTRIM(AD.NUMINGRES)) = @AdmissionNumber)
              AND (@PatientCode IS NULL OR AD.IPCODPACI = @PatientCode)
              AND FT.TransferStatus IN (2,3,4)
        ) X
    ),

    -- 2) Base de folios de esos ingresos
    FolioBase AS (
        SELECT DISTINCT
            RC.AdmissionNumber,
            RCD.Id              AS RevenueControlDetailId,
            RCD.FolioOrder,
            RCD.TotalFolio,
            RCD.Status          AS FolioStatus,
            RCD.FolioType,
            RCD.CareGroupId,
            AED.AssignmentDate,
            AED.AssignedUserId,
            AED.EntryType
        FROM RelevantAdmissions RA
        JOIN Billing.RevenueControl RC        WITH (NOLOCK) ON RC.AdmissionNumber = RA.AdmissionNumber
        JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON RCD.RevenueControlId = RC.Id
        LEFT JOIN AccountManagement.AutomaticEntryDistribution AED WITH (NOLOCK)
               ON AED.AdmissionNumber = RC.AdmissionNumber
    ),

    -- 3) Traslados del folio con secuencia cronológica por folio
    FT_Seq AS (
        SELECT
            FT.Id,
            FT.AdmissionNumber,
            FT.RevenueControlDetailId,
            FT.PreviousUser,
            FT.ReceivingUser,
            FT.TransferStatus,          -- 2=Pendiente, 3=Aceptado, 4=Rechazado
            FT.CreationDate,
            FT.ModificationDate,
            FT.RejectionObservation,
            FB.FolioOrder,
            FB.TotalFolio,
            FB.FolioStatus,
            FB.FolioType,
            FB.CareGroupId,
            FB.EntryType,
			FT.ManagementAreaId,
            ROW_NUMBER() OVER (
                PARTITION BY FT.RevenueControlDetailId
                ORDER BY FT.CreationDate, FT.Id
            ) AS TransferSeq
        FROM AccountManagement.FolioTransfer FT WITH (NOLOCK)
        JOIN FolioBase FB ON FB.RevenueControlDetailId = FT.RevenueControlDetailId
    ),

    -- 4) Línea de tiempo unificada
    FolioTimeline AS (
        -- 4.1) Evento inicial: "Sin traslado"
        SELECT
            FB.AdmissionNumber,
            FB.RevenueControlDetailId,
            FB.FolioOrder,
            FB.TotalFolio,
            FB.FolioStatus,
            FB.FolioType,
            FB.CareGroupId,
            CAST(1 AS TINYINT) AS TransferStatus,          -- Sin traslado
            FB.AssignmentDate   AS EventDate,
            NULL                AS PreviousUser,
            NULL                AS ReceivingUser,
            CAST(0 AS INT)      AS TransferOrder,
            UA.UserCode         AS CurrentUserCode,
            UA.FullName         AS CurrentUserFullName,
            NULL                AS RejectionObservation,
			CAST(NULL AS INT)   AS ManagementAreaId,
			CAST(0 AS INT)      AS TransferSeq
        FROM FolioBase FB
        LEFT JOIN AccountManagement.UsersAssignment UA WITH (NOLOCK)
               ON UA.Id = FB.AssignedUserId
              AND UA.EntryType = FB.EntryType

        UNION ALL

        -- 4.2) "Solicitado" explícito (status=2)
        SELECT
            S.AdmissionNumber,
            S.RevenueControlDetailId,
            S.FolioOrder,
            S.TotalFolio,
            S.FolioStatus,
            S.FolioType,
            S.CareGroupId,
            CAST(2 AS TINYINT)                AS TransferStatus,      -- Solicitado
            S.CreationDate                    AS EventDate,
            S.PreviousUser,
            S.ReceivingUser,
            (S.TransferSeq * 2 - 1)           AS TransferOrder,
            S.PreviousUser                    AS CurrentUserCode,      -- aún lo tiene el anterior
            UPrev.FullName                    AS CurrentUserFullName,
            NULL                              AS RejectionObservation,
			S.ManagementAreaId,
			S.TransferSeq					  AS TransferSeq  
        FROM FT_Seq S
        LEFT JOIN AccountManagement.UsersAssignment UPrev WITH (NOLOCK)
               ON UPrev.UserCode = S.PreviousUser
              AND UPrev.EntryType = S.EntryType
        WHERE S.TransferStatus = 2

        UNION ALL

        -- 4.3) "Solicitado" inferido para registros cerrados (3/4)
        SELECT
            S.AdmissionNumber,
            S.RevenueControlDetailId,
            S.FolioOrder,
            S.TotalFolio,
            S.FolioStatus,
            S.FolioType,
            S.CareGroupId,
            CAST(2 AS TINYINT)                AS TransferStatus,      -- Solicitado
            S.CreationDate                    AS EventDate,
            S.PreviousUser,
            S.ReceivingUser,
            (S.TransferSeq * 2 - 1)           AS TransferOrder,
            S.PreviousUser                    AS CurrentUserCode,
            UPrev.FullName                    AS CurrentUserFullName,
            NULL                              AS RejectionObservation,
			S.ManagementAreaId,
			S.TransferSeq					  AS TransferSeq  
        FROM FT_Seq S
        LEFT JOIN AccountManagement.UsersAssignment UPrev WITH (NOLOCK)
               ON UPrev.UserCode = S.PreviousUser
              AND UPrev.EntryType = S.EntryType
        WHERE S.TransferStatus IN (3,4)

        UNION ALL

        -- 4.4) Cierre: "Aceptado" o "Rechazado"
        SELECT
            S.AdmissionNumber,
            S.RevenueControlDetailId,
            S.FolioOrder,
            S.TotalFolio,
            S.FolioStatus,
            S.FolioType,
            S.CareGroupId,
            S.TransferStatus,                                          -- 3 o 4
            COALESCE(S.ModificationDate, S.CreationDate) AS EventDate, -- respaldo
            S.PreviousUser,
            S.ReceivingUser,
            (S.TransferSeq * 2)                AS TransferOrder,
            CASE WHEN S.TransferStatus = 3 THEN S.ReceivingUser ELSE S.PreviousUser END AS CurrentUserCode,
            CASE WHEN S.TransferStatus = 3 THEN URec.FullName    ELSE UPrev.FullName  END AS CurrentUserFullName,
            S.RejectionObservation,
			S.ManagementAreaId,
			S.TransferSeq      AS TransferSeq
        FROM FT_Seq S
        LEFT JOIN AccountManagement.UsersAssignment UPrev WITH (NOLOCK)
               ON UPrev.UserCode = S.PreviousUser
              AND UPrev.EntryType = S.EntryType
        LEFT JOIN AccountManagement.UsersAssignment URec  WITH (NOLOCK)
               ON URec.UserCode  = S.ReceivingUser
              AND URec.EntryType = S.EntryType
        WHERE S.TransferStatus IN (3,4)
    )

    SELECT
        ROW_NUMBER() OVER (
            ORDER BY FT.AdmissionNumber, FT.FolioOrder, FT.EventDate, FT.TransferOrder
        ) AS Id,

        -- Ingreso
        LTRIM(RTRIM(FT.AdmissionNumber))            AS AdmissionNumber,
        IP.IPNOMCOMP                                 AS PatientFullName,
        AD.IFECHAING                                 AS AdmissionDate,
        AD.IPCODPACI                                 AS PatientCode,
        AD.CODCENATE                                 AS AttentionCenterCode,
        AD.CODICAMHO                                 AS BedNumber,
        DIAG.NOMDIAGNO                               AS Diagnosis,
        UF.UFUDESCRI                                 AS FunctionalUnitName,

        -- Folio
        FT.FolioOrder                                AS FolioNumber,
        FT.TotalFolio                                AS FolioTotalValue,
        FT.RevenueControlDetailId                    AS RevenueControlDetailId,
        CG.Name                                      AS CareGroupName,

        CASE FT.FolioType
            WHEN 1 THEN 'EAPB con contrato'
            WHEN 2 THEN 'EAPB sin contrato'
            WHEN 3 THEN 'Particulares'
            WHEN 4 THEN 'Aseguradoras'
            ELSE ''
        END                                           AS FolioType,

        CASE FT.FolioStatus
            WHEN 1 THEN 'Registrado'
            WHEN 2 THEN 'Facturado'
            WHEN 3 THEN 'Bloqueado'
            WHEN 4 THEN 'Anulado'
            WHEN 5 THEN 'Reconocimiento Ingresos'
            WHEN 6 THEN 'Factura Asociada'
            WHEN 7 THEN 'Folio Cerrado'
            ELSE ''
        END                                           AS FolioStatusDescription,

        -- Evento
        FT.EventDate,
        CASE FT.TransferStatus
            WHEN 1 THEN 'Sin traslado'
            WHEN 2 THEN 'Solicitado'
            WHEN 3 THEN 'Aceptado'
            WHEN 4 THEN 'Rechazado'
            ELSE 'Sin traslado'
        END                                           AS TransferStatus,
        FT.TransferOrder,

        -- Usuarios
        FT.CurrentUserCode,
        FT.CurrentUserFullName						  AS CurrentUserCodeName,
        FT.PreviousUser                               AS PreviousUserCode,
        FT.ReceivingUser                              AS ReceivingUserCode,

        -- Ingreso/Factura extra
        AD.CODUSUCRE                                  AS AdmissionCreationUser,
        AD.CODUSUMOD                                  AS AdmissionModificationUser,
        I.InvoiceNumber,
        COALESCE(FT.RejectionObservation, '')         AS Comments,

		APrev1.Area									  AS PreviousManagementAreaName,
		CASE WHEN FT.ManagementAreaId IS NOT NULL
			 THEN MA_Transfer.Code + ' - ' + MA_Transfer.Name
			 ELSE NULL
		END											  AS ReceivingManagementAreaName

    FROM FolioTimeline FT
    JOIN dbo.ADINGRESO AD       WITH (NOLOCK) ON AD.NUMINGRES  = FT.AdmissionNumber
    JOIN Contract.CareGroup CG  WITH (NOLOCK) ON CG.Id         = FT.CareGroupId
    JOIN dbo.INPACIENT IP       WITH (NOLOCK) ON IP.IPCODPACI  = AD.IPCODPACI
    JOIN dbo.INUNIFUNC UF       WITH (NOLOCK) ON UF.UFUCODIGO  = AD.UFUCODIGO
    LEFT JOIN dbo.INDIAGNOS DIAG WITH (NOLOCK) ON DIAG.CODDIAGNO = AD.CODDIAING
    LEFT JOIN Billing.Invoice I WITH (NOLOCK) ON I.RevenueControlDetailId = FT.RevenueControlDetailId
    LEFT JOIN Portfolio.AccountReceivable AR WITH (NOLOCK) ON AR.InvoiceId = I.Id
	
	-- Área destino del traslado (desde ManagementAreaId del folio transfer)
	LEFT JOIN AccountManagement.ManagementAreas MA_Transfer WITH (NOLOCK)
       ON MA_Transfer.Id = FT.ManagementAreaId

	-- Área de gestión del usuario ORIGEN (primera que se le encuentre)
	OUTER APPLY (
		SELECT TOP (1)
			   CONCAT(MA.Code, ' - ', MA.Name) AS Area
		FROM AccountManagement.ManagementAreasUser MU WITH (NOLOCK)
		JOIN AccountManagement.ManagementAreas MA WITH (NOLOCK)
		  ON MA.Id = MU.ManagementAreasId
		WHERE MU.Usercode = FT.PreviousUser
		ORDER BY MU.Id ASC  -- "primera que se le encuentre"
	) APrev1

    WHERE
        -- Participación del usuario (en cualquiera de los roles del traslado).
        (
            @UserCode IS NULL
            OR FT.PreviousUser  = @UserCode
            OR FT.ReceivingUser = @UserCode
            OR FT.CurrentUserCode = @UserCode
        )
	AND (AR.PortfolioStatus IS NULL OR AR.PortfolioStatus IN (1,2))
    ORDER BY FT.AdmissionNumber, FT.FolioOrder, FT.TransferSeq, FT.TransferOrder, FT.EventDate;
END
