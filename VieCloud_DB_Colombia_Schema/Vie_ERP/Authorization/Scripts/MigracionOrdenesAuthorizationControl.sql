-- =============================================================================
-- Migración histórica: ADAUTOSER → Authorization.AuthorizationControl
-- SubjectType = 1 (Servicio) y 2 (Medicamento)
-- ADAUTOSER.MEDIOSERV: '1' → SubjectType=1, '2' → SubjectType=2
-- SubjectType=3 (Insumos) no existía en ADAUTOSER — no se migra.
--
-- Mapeo de estado (PROESTADO → Status):
--   '1' (Pendiente)   → 1 (NoSolicitado)
--   '2' (Solicitado)  → 2 (EnTramite)
--   '3' (Autorizado)  → 4 (Autorizado)  ← salta 3=Radicado, no existía en legado
--   '4' (Anulado)     → 7 (Cancelado)
--
-- Campos resueltos mediante joins:
--   ServiceDescription     → INCUPSIPS.DESSERIPS  (MEDIOSERV='1')
--                            IHLISTPRO.DESPRODUC   (MEDIOSERV='2')
--   RequestingPhysicianCode/Name → HCHISPACA + INPROFSAL via NUMEFOLIO (si no NULL)
--   DiagnosisCode/Name     → HCHISPACA + INDIAGNOS via NUMEFOLIO
--   CreationUser           → COALESCE(ADAUTOSER.CODUSUARI, @UsuarioMigracion)
--   StayTypeCode/Name      → CHREGESTA (OUTER APPLY, estancia activa del ingreso)
--   PatientName            → INPACIENT
--   PayerName              → INENTIDAD
--   FunctionalUnitName     → INUNIFUNC
--   CareGroupName          → Contract.HealthAdministrator
--
-- Idempotente: clave de deduplicación = SubjectRecordId = ADAUTOSER.CODCONCEC (PK).
-- Transaccional: ROLLBACK automático en error.
-- No se incluye en el .sqlproj — script de migración de datos, no de esquema.
-- =============================================================================
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @FechaInicial date = '2022-01-01';
DECLARE @FechaFinal   date = '2026-06-15';
DECLARE @UsuarioMigracion varchar(20) = 'MIGRACION_ORD';

BEGIN TRY
    BEGIN TRANSACTION;

    ;WITH OrdenesFuente AS
    (
        SELECT
            -- ---- Identificación del sujeto ----
            CAST(s.CODCONCEC AS numeric(18, 0))           AS SubjectRecordId,

            CASE s.MEDIOSERV
                WHEN '1' THEN CAST(1 AS tinyint)
                ELSE          CAST(2 AS tinyint)
            END                                           AS SubjectType,

            LEFT(LTRIM(RTRIM(s.CODSERIPS)), 20)           AS SubjectCode,

            CAST(s.TIPOSERIPS AS tinyint)                 AS ServiceType,

            -- ---- Contexto de ingreso ----
            LEFT(LTRIM(RTRIM(s.IPCODPACI)), 25)           AS PatientCode,
            LEFT(LTRIM(RTRIM(s.CODENTIDA)), 9)            AS EntityCode,
            LEFT(LTRIM(RTRIM(CONVERT(varchar(10), s.NUMINGRES))), 10) AS AdmissionNumber,
            LEFT(LTRIM(RTRIM(s.NUMEFOLIO)), 10)           AS Folio,
            LEFT(LTRIM(RTRIM(s.CODCENATE)), 10)           AS CareCenterCode,
            LEFT(LTRIM(RTRIM(s.UFUCODIGO)), 10)           AS FunctionalUnitCode,
            COALESCE(s.IDCareGroup, i.GENCAREGROUP)       AS CareGroupId,

            -- ---- Estado y cantidades ----
            CASE s.PROESTADO
                WHEN '1' THEN CAST(1 AS tinyint)   -- NoSolicitado
                WHEN '2' THEN CAST(2 AS tinyint)   -- EnTramite
                WHEN '3' THEN CAST(4 AS tinyint)   -- Autorizado (sin Radicado en legado)
                WHEN '4' THEN CAST(7 AS tinyint)   -- Cancelado
                ELSE          CAST(1 AS tinyint)
            END                                           AS Status,

            s.CANSERIPS                                   AS RequestedQuantity,

            CASE
                WHEN s.PROESTADO = '3'
                THEN CAST(s.CANSERAUT AS int)
                ELSE NULL
            END                                           AS AuthorizedQuantity,

            -- ---- Marcas de origen ----
            s.SERSUSCEP                                   AS IsSusceptible,
            s.PROSERIPS                                   AS FromMedicalRecord,
            ISNULL(s.SOLINFOQX, CAST(0 AS bit))          AS FromSurgicalReport,
            ISNULL(s.SOLEXTRAM, CAST(0 AS bit))          AS FromExtramural,

            -- ---- Justificaciones ----
            s.JUSCLISER                                   AS ClinicalJustification,
            LEFT(s.JUSANULA, 250)                         AS CancellationJustification,
            LEFT(LTRIM(RTRIM(s.CODUSUANU)), 20)          AS CancellationUser,

            -- ---- Auditoría ----
            LEFT(
                COALESCE(LTRIM(RTRIM(s.CODUSUARI)), @UsuarioMigracion),
                20
            )                                             AS CreationUser,
            s.FECREGIST                                   AS CreationDate,
            s.FECREGIST                                   AS RequestDate,

            -- ---- Display del paciente ----
            LEFT(LTRIM(RTRIM(p.IPNOMCOMP)), 255)          AS PatientName,
            LEFT(LTRIM(RTRIM(TipoId.CODIGO)), 10)         AS PatientIdType,
            p.IPFECNACI                                   AS BirthDate,

            -- ---- Display del ingreso ----
            i.IFECHAING                                   AS AdmissionDate,
            TRY_CONVERT(int, i.CODCAMACT)                 AS Bed,

            LEFT(LTRIM(RTRIM(ga.[Name])), 255)            AS CareGroupName,
            LEFT(LTRIM(RTRIM(ent.NOMENTIDA)), 255)        AS PayerName,
            LEFT(LTRIM(RTRIM(U.UFUDESCRI)), 255)          AS FunctionalUnitName,

            -- ---- Tipo de estancia (estancia activa del ingreso al momento de la orden) ----
            reg.CODTIPEST                                 AS StayTypeCode,
            LEFT(LTRIM(RTRIM(est.DESTIPEST)), 40)         AS StayTypeName,

            -- ---- Display clínico — médico solicitante y diagnóstico (vía NUMEFOLIO) ----
            LEFT(LTRIM(RTRIM(hc.CODPROSAL)), 20)          AS RequestingPhysicianCode,
            LEFT(LTRIM(RTRIM(prof.NOMMEDICO)), 120)       AS RequestingPhysicianName,
            LEFT(LTRIM(RTRIM(hc.CODDIAGNO)), 4)           AS DiagnosisCode,
            LEFT(LTRIM(RTRIM(diag.NOMDIAGNO)), 350)       AS DiagnosisName,

            -- ---- Display del servicio/producto ----
            COALESCE(
                LEFT(LTRIM(RTRIM(cups.DESSERIPS)), 255),
                LEFT(LTRIM(RTRIM(prod.DESPRODUC)), 255)
            )                                             AS ServiceDescription

        FROM dbo.ADAUTOSER AS s

        INNER JOIN dbo.ADINGRESO AS i
            ON i.NUMINGRES = s.NUMINGRES

        INNER JOIN dbo.INPACIENT AS p
            ON p.IPCODPACI = s.IPCODPACI

        LEFT JOIN dbo.ADTIPOIDENTIFICA AS TipoId
            ON TipoId.CODIGO = p.IPTIPODOC

        LEFT JOIN [Contract].HealthAdministrator AS ga
            ON ga.ID = COALESCE(s.IDCareGroup, i.GENCAREGROUP)

        LEFT JOIN dbo.INENTIDAD AS ent
            ON ent.CODENTIDA = s.CODENTIDA

        LEFT JOIN dbo.INUNIFUNC AS U
            ON U.UFUCODIGO = s.UFUCODIGO

        -- Médico y diagnóstico: via NUMEFOLIO → HCHISPACA (NULL si sin folio)
        OUTER APPLY
        (
            SELECT TOP (1)
                hc.CODPROSAL,
                hc.CODDIAGNO
            FROM dbo.HCHISPACA AS hc
            WHERE hc.NUMEFOLIO = s.NUMEFOLIO
              AND hc.NUMINGRES = s.NUMINGRES
            ORDER BY hc.CODPROSAL DESC
        ) AS hc

        LEFT JOIN dbo.INPROFSAL AS prof
            ON prof.CODPROSAL = hc.CODPROSAL

        LEFT JOIN dbo.INDIAGNOS AS diag
            ON diag.CODDIAGNO = hc.CODDIAGNO

        -- Tipo de estancia activa del ingreso al momento del registro de la orden
        OUTER APPLY
        (
            SELECT TOP (1) re.CODTIPEST
            FROM dbo.CHREGESTA AS re
            WHERE re.NUMINGRES = s.NUMINGRES
              AND re.REGESTADO = 1
            ORDER BY re.CODTIPEST DESC
        ) AS reg

        LEFT JOIN dbo.CHTIPESTA AS est
            ON est.CODTIPEST = reg.CODTIPEST

        -- Descripción del servicio CUPS (SubjectType=1)
        LEFT JOIN dbo.INCUPSIPS AS cups
            ON cups.CODSERIPS = s.CODSERIPS
           AND s.MEDIOSERV    = '1'

        -- Descripción del producto/medicamento (SubjectType=2)
        LEFT JOIN dbo.IHLISTPRO AS prod
            ON prod.CODPRODUC = s.CODSERIPS
           AND s.MEDIOSERV    = '2'

        WHERE s.FECREGIST >= @FechaInicial
          AND s.FECREGIST  < DATEADD(DAY, 1, @FechaFinal)
          AND NULLIF(LTRIM(RTRIM(s.IPCODPACI)), '') IS NOT NULL
          AND NULLIF(LTRIM(RTRIM(s.CODENTIDA)), '') IS NOT NULL
          AND NULLIF(LTRIM(RTRIM(s.CODCENATE)), '') IS NOT NULL
    )

    INSERT INTO [Authorization].[AuthorizationControl]
    (
        SubjectType,
        SubjectCode,
        SubjectRecordId,
        ServiceType,
        PatientCode,
        EntityCode,
        AdmissionNumber,
        Folio,
        CareCenterCode,
        FunctionalUnitCode,
        CareGroupId,
        Status,
        RequestedQuantity,
        AuthorizedQuantity,
        IsSusceptible,
        FromMedicalRecord,
        FromSurgicalReport,
        FromExtramural,
        ClinicalJustification,
        CancellationJustification,
        CancellationUser,
        CreationUser,
        CreationDate,
        RequestDate,
        PatientName,
        PatientIdType,
        PatientAge,
        AdmissionDate,
        Bed,
        StayTypeCode,
        StayTypeName,
        CareGroupName,
        PayerName,
        FunctionalUnitName,
        RequestingPhysicianCode,
        RequestingPhysicianName,
        DiagnosisCode,
        DiagnosisName,
        ServiceDescription
    )

    SELECT
        F.SubjectType,
        F.SubjectCode,
        F.SubjectRecordId,
        F.ServiceType,
        F.PatientCode,
        F.EntityCode,
        F.AdmissionNumber,
        F.Folio,
        F.CareCenterCode,
        F.FunctionalUnitCode,
        F.CareGroupId,
        F.Status,
        F.RequestedQuantity,
        F.AuthorizedQuantity,
        F.IsSusceptible,
        F.FromMedicalRecord,
        F.FromSurgicalReport,
        F.FromExtramural,
        F.ClinicalJustification,
        F.CancellationJustification,
        F.CancellationUser,
        F.CreationUser,
        F.CreationDate,
        F.RequestDate,
        F.PatientName,
        F.PatientIdType,

        CASE
            WHEN F.BirthDate IS NULL THEN NULL
            WHEN F.BirthDate > COALESCE(F.RequestDate, F.AdmissionDate, GETDATE()) THEN 0
            ELSE
                DATEDIFF(YEAR, F.BirthDate, COALESCE(F.RequestDate, F.AdmissionDate, GETDATE()))
                -
                CASE
                    WHEN DATEADD(
                             YEAR,
                             DATEDIFF(YEAR, F.BirthDate, COALESCE(F.RequestDate, F.AdmissionDate, GETDATE())),
                             F.BirthDate
                         ) > COALESCE(F.RequestDate, F.AdmissionDate, GETDATE())
                    THEN 1
                    ELSE 0
                END
        END                         AS PatientAge,

        F.AdmissionDate,
        F.Bed,
        F.StayTypeCode,
        F.StayTypeName,
        F.CareGroupName,
        F.PayerName,
        F.FunctionalUnitName,
        F.RequestingPhysicianCode,
        F.RequestingPhysicianName,
        F.DiagnosisCode,
        F.DiagnosisName,
        F.ServiceDescription

    FROM OrdenesFuente AS F

    WHERE NOT EXISTS
    (
        SELECT 1
        FROM [Authorization].[AuthorizationControl] AS AC
        WHERE AC.SubjectType     IN (1, 2)
          AND AC.SubjectRecordId  = F.SubjectRecordId
    );

    DECLARE @CantidadInsertada int = @@ROWCOUNT;

    COMMIT TRANSACTION;

    SELECT
        @CantidadInsertada  AS RegistrosInsertados,
        @FechaInicial       AS FechaInicial,
        @FechaFinal         AS FechaFinal;

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
