/****** Object:  StoredProcedure [AccountManagement].[SP_GetUserFolios]    Script Date: 23/04/2026 2:08:38 p. m. ******/
/*
   ===================================================================================
   PROCEDIMIENTO: GetUserFolios
   ===================================================================================

: Obtiene todos los folios que un usuario debe gestionar en su dashboard
              de gestión de cuentas, considerando asignaciones automáticas y traslados.
   
   PARÁMETROS:
   - @UserCode: Código del usuario (obligatorio)
   - @EntryType: Tipo de ingreso a filtrar - 1=Ambulatorio, 2=Hospitalario, NULL=Ambos (opcional)
   
   NOTA: Un usuario puede tener 1 o 2 registros en UsersAssignment:
   - 1 registro con EntryType=1: solo gestiona ambulatorios
   - 1 registro con EntryType=2: solo gestiona hospitalarios
   - 2 registros (EntryType=1 y 2): gestiona ambos tipos
   
   CASOS CUBIERTOS:
   1. Folios de ingresos asignados automáticamente al usuario
   2. Folios trasladados por el usuario pendientes de aceptación
   3. Folios trasladados por el usuario que fueron rechazados
   4. Folios que el usuario aceptó por traslado
   ===================================================================================
*/
CREATE PROCEDURE [AccountManagement].[SP_GetUserFolios]
    @UserCode VARCHAR(20),
    @EntryType TINYINT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    ---- Validar que el usuario exista
    --IF NOT EXISTS (SELECT 1 FROM AccountManagement.UsersAssignment WITH (NOLOCK) WHERE UserCode = @UserCode)
    --BEGIN
    --    RAISERROR('El código de usuario no existe en UsersAssignment', 16, 1);
    --    RETURN;
    --END
 
    -- Trabajamos con @UserCode y el JOIN con UsersAssignment capturará automáticamente
    -- TODOS los registros del usuario, permitiendo ver ambos tipos de ingreso si aplica

    ;WITH 
    /* CTE para obtener el último traslado de cada folio */
    LastTransfer AS (
        SELECT 
            FT.RevenueControlDetailId,
            FT.PreviousUser,
            FT.ReceivingUser,
            FT.TransferStatus,
            FT.CreationDate,
            FT.ManagementAreaId,
            FT.AdmissionNumber,
            ROW_NUMBER() OVER (PARTITION BY FT.RevenueControlDetailId ORDER BY FT.Id DESC) AS RowNum
        FROM AccountManagement.FolioTransfer FT WITH (NOLOCK)
    ),
    /* CTE para folios del usuario */
    UserFolios AS (
        -- ============================================================================
        -- CASO 1: Folios de ingresos asignados automáticamente al usuario
        -- ============================================================================
        SELECT DISTINCT
            RCD.Id AS RevenueControlDetailId,
            RC.AdmissionNumber,
            RCD.FolioOrder,
            RCD.TotalFolio,
            RCD.Status AS FolioStatus,
            RCD.FolioType,
            RCD.CareGroupId,
            
            -- El usuario es el owner por asignación automática
            @UserCode AS CurrentOwnerCode,
            UA.FullName AS AssignedUserFullname,
            @UserCode AS AssignedUserCode,
            
            -- Información del traslado (si existe)
            COALESCE(LT.TransferStatus, 1) AS TransferStatus,
            LT.PreviousUser AS TransferPreviousUser,
            LT.ReceivingUser AS TransferReceivingUser,
            
            -- Fecha de asignación
            AED.AssignmentDate,
            AED.EntryType,
            
            -- Tipo de ingreso
            AD.TIPOINGRE AS AdmissionEntryType
            
        FROM AccountManagement.AutomaticEntryDistribution AED WITH (NOLOCK)
        JOIN AccountManagement.UsersAssignment UA WITH (NOLOCK) ON UA.Id = AED.AssignedUserId
        JOIN ADINGRESO AD WITH (NOLOCK) ON AD.NUMINGRES = AED.AdmissionNumber
        JOIN Billing.RevenueControl RC WITH (NOLOCK) ON RC.AdmissionNumber = AED.AdmissionNumber
        JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON RCD.RevenueControlId = RC.Id
        LEFT JOIN LastTransfer LT WITH (NOLOCK) ON LT.RevenueControlDetailId = RCD.Id AND LT.RowNum = 1
        
        WHERE UA.UserCode = @UserCode
          -- CRÍTICO: Filtrar por el tipo de ingreso correcto para evitar duplicados
          AND AED.EntryType = AD.TIPOINGRE
          -- CRÍTICO: El EntryType del registro en UsersAssignment debe coincidir con el tipo de ingreso
          -- Si el usuario tiene 2 registros (EntryType=1 y EntryType=2), el JOIN capturará ambos
          AND UA.EntryType = AD.TIPOINGRE
          -- Filtro opcional por tipo de ingreso
          AND (@EntryType IS NULL OR AD.TIPOINGRE = @EntryType)
          -- EXCLUIR folios que trasladé y fueron ACEPTADOS (ya no son míos)
          AND (LT.RevenueControlDetailId IS NULL OR LT.TransferStatus <> 3 OR LT.PreviousUser <> @UserCode)
        
        UNION ALL
        
        -- ============================================================================
        -- CASO 2: Folios que me trasladaron y YO ACEPTÉ
        -- ============================================================================
        SELECT DISTINCT
            RCD.Id AS RevenueControlDetailId,
            RC.AdmissionNumber,
            RCD.FolioOrder,
            RCD.TotalFolio,
            RCD.Status AS FolioStatus,
            RCD.FolioType,
            RCD.CareGroupId,
            
            -- Ahora soy el owner porque acepté el traslado
            @UserCode AS CurrentOwnerCode,
            '' AS AssignedUserFullname,
            @UserCode AS AssignedUserCode,
            
            -- Información del traslado
            LT.TransferStatus,
            LT.PreviousUser AS TransferPreviousUser,
            LT.ReceivingUser AS TransferReceivingUser,
            
            -- Fecha del traslado aceptado
            LT.CreationDate AS AssignmentDate,
            AD.TIPOINGRE AS EntryType,
            AD.TIPOINGRE AS AdmissionEntryType
            
        FROM LastTransfer LT
        JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON RCD.Id = LT.RevenueControlDetailId
        JOIN Billing.RevenueControl RC WITH (NOLOCK) ON RC.Id = RCD.RevenueControlId
        JOIN ADINGRESO AD WITH (NOLOCK) ON AD.NUMINGRES = RC.AdmissionNumber
        
        WHERE LT.RowNum = 1
          AND LT.TransferStatus = 3  -- Aceptado
          AND LT.ReceivingUser = @UserCode
          -- Filtro opcional por tipo de ingreso
          AND (@EntryType IS NULL OR AD.TIPOINGRE = @EntryType)
          -- EXCLUIR si ya está en el CASO 1 (asignación automática)
          AND NOT EXISTS (
              SELECT 1 
              FROM AccountManagement.AutomaticEntryDistribution AED2 WITH (NOLOCK)
              JOIN AccountManagement.UsersAssignment UA2 WITH (NOLOCK) ON UA2.Id = AED2.AssignedUserId
              WHERE AED2.AdmissionNumber = RC.AdmissionNumber
                AND UA2.UserCode = @UserCode
                AND AED2.EntryType = AD.TIPOINGRE
                AND UA2.EntryType = AD.TIPOINGRE
          )
    )
    
    -- ============================================================================
    -- SELECT FINAL con toda la información
    -- ============================================================================
    SELECT 
        ROW_NUMBER() OVER (ORDER BY AD.NUMINGRES, UF.FolioOrder) AS Id,
        LTRIM(RTRIM(AD.NUMINGRES)) AS AdmissionNumber,
        IP.IPNOMCOMP AS PatientFullName,
        AD.IFECHAING AS AdmissionDate,
        AD.IPCODPACI AS PatientCode,
        
        CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM AccountManagement.FolioAlert FA2 WITH (NOLOCK)
                WHERE FA2.RevenueControlDetailId = UF.RevenueControlDetailId 
                  AND FA2.Status = 1
            ) THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS HasFolioAlert,

        AD.CODICAMHO AS BedNumber,
        DIAG.NOMDIAGNO AS Diagnosis,
        UF.FolioOrder AS FolioNumber,
        UF.TotalFolio AS FolioTotalValue,
        UF.RevenueControlDetailId,
        UFUNC.UFUDESCRI AS FunctionalUnitName,
        CG.Name AS CareGroupName,
        CG.Id AS CareGroupId,
        AD.CODCENATE AS AttentionCenterCode,

        CASE UF.FolioType
            WHEN 1 THEN 'EAPB con contrato'
            WHEN 2 THEN 'EAPB sin contrato'
            WHEN 3 THEN 'Particulares'
            WHEN 4 THEN 'Aseguradoras'
            ELSE ''
        END AS FolioType,

        CASE UF.FolioStatus
            WHEN 1 THEN 'Registrado'
            WHEN 2 THEN 'Facturado'
            WHEN 3 THEN 'Bloqueado'
            WHEN 4 THEN 'Anulado'
            WHEN 5 THEN 'Reconocimiento Ingresos'
            WHEN 6 THEN 'Factura Asociada'
            WHEN 7 THEN 'Folio Cerrado'
            ELSE ''
        END AS FolioStatusDescription,

        UF.AssignmentDate,
        UF.AssignedUserFullname,
        UF.AssignedUserCode,
        MApick.ManagementAreaName,
        AD.CODUSUCRE AS AdmissionCreationUser,
        AD.CODUSUMOD AS AdmissionModificationUser,
        I.InvoicedUser AS InvoiceUser,
        I.InvoiceNumber AS AssociatedInvoice,

		CASE 
            WHEN MApick.ManagementTime IS NULL THEN 1  -- Sin área asignada = verde por defecto
            ELSE 
                CASE 
                    -- Calcular minutos transcurridos
                    WHEN DATEDIFF(MINUTE, UF.AssignmentDate, GETDATE()) <= 
                         (CASE MApick.ManagementUnit
                             WHEN 1 THEN MApick.ManagementTime * 60                -- Horas a minutos
                             WHEN 2 THEN MApick.ManagementTime * 1440              -- Días a minutos
                             WHEN 3 THEN MApick.ManagementTime * 10080             -- Semanas a minutos
                             ELSE MApick.ManagementTime
                          END * 0.9)  -- 90% del tiempo = verde
                    THEN 1  -- Verde: dentro del tiempo
                    
                    WHEN DATEDIFF(MINUTE, UF.AssignmentDate, GETDATE()) <= 
                         (CASE MApick.ManagementUnit
                             WHEN 1 THEN MApick.ManagementTime * 60                -- Horas a minutos
                             WHEN 2 THEN MApick.ManagementTime * 1440              -- Días a minutos
                             WHEN 3 THEN MApick.ManagementTime * 10080             -- Semanas a minutos
                             ELSE MApick.ManagementTime
                          END)
                    THEN 2  -- Amarillo: entre 90% y 100%
                    
                    ELSE 3  -- Rojo: se pasó el tiempo
                END
        END AS IsOnTime,

        CASE UF.TransferStatus
            WHEN 1 THEN 'Sin traslado'
            WHEN 2 THEN 'Pendiente de aceptación'
            WHEN 3 THEN 'Aceptada'
            WHEN 4 THEN 'Rechazada'
            ELSE 'Sin Traslado'
        END AS TransferStatus,
        
        -- Información adicional sobre usuarios en traslados
        UF.CurrentOwnerCode,
        UF.TransferPreviousUser,
        UF.TransferReceivingUser,
        
        -- Indicadores para filtrado
        CASE 
            WHEN UF.TransferStatus = 2 AND UF.TransferReceivingUser = @UserCode 
                THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPendingToAccept,
        
        CASE 
            WHEN UF.TransferStatus = 4 AND UF.TransferPreviousUser = @UserCode 
                THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS WasRejected,
        
        CASE 
            WHEN UF.TransferStatus = 2 AND UF.TransferPreviousUser = @UserCode 
                THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS IsPendingFromMe,
        
        -- Tipo de ingreso
        CASE UF.AdmissionEntryType
            WHEN 1 THEN 'Ambulatorio'
            WHEN 2 THEN 'Hospitalario'
            ELSE 'Desconocido'
        END AS EntryTypeDescription,
        UF.AdmissionEntryType AS EntryType

    FROM UserFolios UF
    JOIN ADINGRESO AD WITH (NOLOCK) ON AD.NUMINGRES = UF.AdmissionNumber

    /* Info adicional del ingreso */
    JOIN Contract.CareGroup CG WITH (NOLOCK) ON CG.Id = UF.CareGroupId
    JOIN INPACIENT IP WITH (NOLOCK) ON IP.IPCODPACI = AD.IPCODPACI
    JOIN INUNIFUNC UFUNC WITH (NOLOCK) ON UFUNC.UFUCODIGO = AD.UFUCODIGO
    LEFT JOIN INDIAGNOS DIAG WITH (NOLOCK) ON DIAG.CODDIAGNO = AD.CODDIAING

    /* Factura asociada */
    LEFT JOIN Billing.Invoice I WITH (NOLOCK) ON I.RevenueControlDetailId = UF.RevenueControlDetailId
	LEFT JOIN Portfolio.AccountReceivable AR WITH (NOLOCK) ON AR.InvoiceId = I.Id

    /* Area de gestión del usuario actual */
    OUTER APPLY (
        SELECT TOP (1)
               MA.Id   AS ManagementAreaId,
               MA.Name AS ManagementAreaName,
			   MA.Time AS ManagementTime,
               MA.Unit AS ManagementUnit -- 1=Horas, 2=Días, 3=Semanas
        FROM AccountManagement.ManagementAreasUser MAU WITH (NOLOCK)
        JOIN AccountManagement.ManagementAreas MA WITH (NOLOCK)
          ON MA.Id = MAU.ManagementAreasId
        WHERE MAU.Usercode = @UserCode
        ORDER BY MAU.Id DESC
    ) MApick

	WHERE (AR.PortfolioStatus IS NULL OR AR.PortfolioStatus IN (1,2))

    ORDER BY AD.NUMINGRES, UF.FolioOrder;

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene todos los folios que un usuario debe gestionar, incluyendo asignaciones automáticas y traslados aceptados. Filtra correctamente por EntryType para evitar duplicados.', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'PROCEDURE', @level1name = N'SP_GetUserFolios';
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los folios de facturación que un gestor debe trabajar en su tablero, combinando asignaciones automáticas y traslados aceptados, con semáforo de cumplimiento de tiempos.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El UserCode debe existir al menos en un registro de AccountManagement.UsersAssignment; de lo contrario se aborta con RAISERROR severidad 16.; Se asume que UsersAssignment puede tener uno o dos registros por usuario, uno por cada EntryType (1=Ambulatorio, 2=Hospitalario).; Las admisiones referenciadas deben existir en ADINGRESO y tener control de ingresos en Billing.RevenueControl/RevenueControlDetail.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El EntryType del registro en UsersAssignment debe coincidir con AD.TIPOINGRE y con AED.EntryType para que el folio aparezca en el CASO 1, evitando duplicados cuando el usuario gestiona ambos tipos.; Solo se considera el último FolioTransfer por RevenueControlDetailId (ROW_NUMBER ORDER BY Id DESC, RowNum=1).; Un folio nunca aparece simultáneamente por asignación automática y por traslado aceptado para el mismo usuario y tipo de ingreso (NOT EXISTS en CASO 2).; Solo se incluyen folios cuya factura asociada esté sin cuenta por cobrar o tenga PortfolioStatus en {1,2}.; El usuario que trasladó un folio aceptado deja de verlo como propio en su dashboard.; Los códigos de FolioType, FolioStatus, TransferStatus y EntryType se traducen a etiquetas fijas: FolioType {1:EAPB con contrato, 2:EAPB sin contrato, 3:Particulares, 4:Aseguradoras}; FolioStatus {1:Registrado,2:Facturado,3:Bloqueado,4:Anulado,5:Reconocimiento Ingresos,6:Factura Asociada,7:Folio Cerrado}; TransferStatus {1:Sin traslado,2:Pendiente,3:Aceptada,4:Rechazada}; EntryType {1:Ambulatorio,2:Hospitalario}.; El semáforo de cumplimiento usa el 90% del ManagementTime configurado en el área de gestión como umbral verde→amarillo.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RAISERROR] N/A: Si no existe ningún registro en AccountManagement.UsersAssignment con UserCode = @UserCode, lanza error ''El código de usuario no existe en UsersAssignment'' con severidad 16.; [RETURN_RESULT] Resultset: Devuelve un conjunto de folios del usuario combinando asignación automática y traslados aceptados, ordenado por NUMINGRES y FolioOrder, filtrando solo cuentas por cobrar con PortfolioStatus IN (1,2) o sin factura asociada.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EntryType IS NULL → No filtra por tipo de ingreso y devuelve folios ambulatorios y hospitalarios. else Restringe los folios a aquellos cuyo AD.TIPOINGRE coincide con @EntryType (1=Ambulatorio, 2=Hospitalario).; si Existe último FolioTransfer (RowNum=1) con TransferStatus=3 y PreviousUser=@UserCode → Excluye el folio del CASO 1 (asignación automática) porque el usuario ya lo trasladó y fue aceptado por otro. else El folio sigue mostrándose como propio del usuario gestor.; si Último FolioTransfer del folio tiene TransferStatus=3 y ReceivingUser=@UserCode → Incluye el folio en CASO 2 como aceptado por traslado, usando la fecha del traslado como AssignmentDate, salvo que ya esté asignado automáticamente al mismo usuario para el mismo TIPOINGRE.; si MApick.ManagementTime IS NULL (usuario sin área de gestión asignada) → Marca IsOnTime=1 (verde por defecto). else Calcula IsOnTime comparando minutos transcurridos desde AssignmentDate contra ManagementTime convertido según ManagementUnit (1=Horas, 2=Días, 3=Semanas): ≤90% verde(1), ≤100% amarillo(2), >100% rojo(3).; si UF.TransferStatus = 2 AND TransferReceivingUser = @UserCode → Marca IsPendingToAccept = 1 (folio que le trasladaron y aún no acepta). else IsPendingToAccept = 0.; si UF.TransferStatus = 4 AND TransferPreviousUser = @UserCode → Marca WasRejected = 1 (traslado del usuario fue rechazado). else WasRejected = 0.; si UF.TransferStatus = 2 AND TransferPreviousUser = @UserCode → Marca IsPendingFromMe = 1 (folio que el usuario trasladó y está pendiente de aceptación). else IsPendingFromMe = 0.; si Existe FolioAlert con Status=1 para el RevenueControlDetailId → HasFolioAlert = 1. else HasFolioAlert = 0.', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'AccountManagement.UsersAssignment; AccountManagement.FolioTransfer; AccountManagement.AutomaticEntryDistribution; AccountManagement.FolioAlert; AccountManagement.ManagementAreasUser; AccountManagement.ManagementAreas; Billing.RevenueControl; Billing.RevenueControlDetail; Billing.Invoice; Contract.CareGroup; Portfolio.AccountReceivable; ADINGRESO; INPACIENT; INUNIFUNC; INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'AccountManagement', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserFolios';
-- GO
