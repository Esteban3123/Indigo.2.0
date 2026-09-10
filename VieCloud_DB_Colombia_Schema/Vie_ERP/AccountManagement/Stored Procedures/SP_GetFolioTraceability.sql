
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

    ORDER BY FT.AdmissionNumber, FT.FolioOrder, FT.TransferSeq, FT.TransferOrder, FT.EventDate;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene la trazabilidad completa de folios mostrando todos los estados de traslado de forma cronológica. Filtra obligatoriamente por centro de atención y opcionalmente por usuario, ingreso o paciente.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'PROCEDURE', @level1name = N'SP_GetFolioTraceability';
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye la línea de tiempo de trazabilidad de folios de un centro de atención, integrando asignaciones automáticas, traslados entre gestores (solicitados, aceptados, rechazados) y datos del ingreso, paciente y factura asociada.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetFolioTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@AttentionCenter es obligatorio: si es NULL o vacío tras LTRIM/RTRIM, se lanza RAISERROR severidad 16 y se aborta la ejecución.; El ingreso debe existir en dbo.ADINGRESO con CODCENATE igual al centro solicitado para ser considerado.; Para que un ingreso sea relevante debe tener registro en AccountManagement.AutomaticEntryDistribution o un FolioTransfer con TransferStatus en (2,3,4).', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetFolioTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ingresos cuyo CODCENATE coincide con @AttentionCenter.; Los traslados con TransferStatus distintos de 2, 3 o 4 nunca aparecen en la línea de tiempo.; Cada traslado cerrado (estado 3 o 4) produce siempre dos filas: una de ''Solicitado'' inferida y otra de cierre.; El evento inicial ''Sin traslado'' (TransferStatus=1) tiene TransferOrder=0 y TransferSeq=0, garantizando que aparece primero por folio.; TransferOrder de eventos ''Solicitado'' es impar (2n-1) y de cierre es par (2n), preservando el orden cronológico dentro de cada traslado.; El área de gestión del usuario origen se toma como la PRIMERA fila por MU.Id ASC en ManagementAreasUser (TOP 1), no necesariamente la activa.; Los joins usan WITH (NOLOCK) en todas las tablas, aceptando lecturas sucias.; El campo Comments es siempre cadena (COALESCE con '''') aunque no haya RejectionObservation.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetFolioTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/admisión de paciente; Folio de facturación; Control de ingresos (RevenueControl); Traslado de folio entre gestores; Asignación automática de gestor; Estado del folio (Registrado, Facturado, Bloqueado, Anulado, Reconocimiento Ingresos, Factura Asociada, Cerrado); Tipo de folio (EAPB con/sin contrato, Particulares, Aseguradoras); Grupo de atención (CareGroup); Área de gestión; Unidad funcional; Diagnóstico de ingreso; Factura asociada; Centro de atención; Observación de rechazo de traslado', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetFolioTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve un conjunto ordenado por AdmissionNumber, FolioOrder, TransferSeq, TransferOrder y EventDate con la cronología de eventos de cada folio (Sin traslado / Solicitado / Aceptado / Rechazado).; [RAISERROR] (error): Cuando @AttentionCenter es NULL o cadena vacía, lanza ''El parámetro @AttentionCenter es obligatorio'' con severidad 16 y retorna sin consultar.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetFolioTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @AttentionCenter IS NULL OR LTRIM(RTRIM(@AttentionCenter))='''' → RAISERROR y RETURN; no se ejecuta el resto del SP. else Procede a calcular RelevantAdmissions y la línea de tiempo.; si FolioTransfer.TransferStatus = 2 (Solicitado explícito) → Genera evento ''Solicitado'' con CurrentUserCode = PreviousUser y TransferOrder = TransferSeq*2-1.; si FolioTransfer.TransferStatus IN (3,4) (cerrados: Aceptado o Rechazado) → Genera DOS eventos: uno ''Solicitado'' inferido (TransferOrder=TransferSeq*2-1) y otro de cierre (TransferOrder=TransferSeq*2) con EventDate=COALESCE(ModificationDate,CreationDate).; si Evento de cierre con TransferStatus = 3 (Aceptado) → CurrentUserCode = ReceivingUser y CurrentUserFullName = URec.FullName. else Si TransferStatus = 4 (Rechazado): CurrentUserCode = PreviousUser y CurrentUserFullName = UPrev.FullName.; si FolioTimeline.ManagementAreaId IS NOT NULL → ReceivingManagementAreaName = MA_Transfer.Code + '' - '' + MA_Transfer.Name. else ReceivingManagementAreaName = NULL.; si @UserCode IS NOT NULL → Filtra eventos donde el usuario participe como PreviousUser, ReceivingUser o CurrentUserCode. else No se aplica filtro por usuario.; si FolioType en {1,2,3,4} → Mapea a ''EAPB con contrato'', ''EAPB sin contrato'', ''Particulares'' o ''Aseguradoras'' respectivamente. else Cadena vacía.; si FolioStatus en {1..7} → Mapea a ''Registrado'',''Facturado'',''Bloqueado'',''Anulado'',''Reconocimiento Ingresos'',''Factura Asociada'',''Folio Cerrado''. else Cadena vacía.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetFolioTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'AccountManagement.AutomaticEntryDistribution; dbo.ADINGRESO; AccountManagement.FolioTransfer; Billing.RevenueControl; Billing.RevenueControlDetail; AccountManagement.UsersAssignment; Contract.CareGroup; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INDIAGNOS; Billing.Invoice; AccountManagement.ManagementAreas; AccountManagement.ManagementAreasUser', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetFolioTraceability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetFolioTraceability';
-- GO
