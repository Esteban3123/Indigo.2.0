-- =============================================================================
-- Migración histórica: CHREGESTA → Authorization.AuthorizationControl
-- SubjectType = 5 (Estancia)
-- SubjectRecordId = CHREGESTA.ID (INT IDENTITY, PK)
--
-- Solo se migran estancias activas o pendientes de liquidar (REGESTADO IN (1, 2))
-- con ingreso vigente (IESTADOIN IN (' ', 'B')).
-- PENDIENTE: confirmar si el dashboard debe mostrar una fila por CHREGESTA.ID
-- (cada cambio de cama) o una fila por ingreso activo (cama actual solamente).
--
-- Status = 1 (NoSolicitado) para todos: no existía workflow de autorización
-- de estancias en el modelo legado.
--
-- Campos resueltos mediante joins:
--   TreatingPhysicianCode/Name → CHREGESTA.CODPROSAL + INPROFSAL
--   TreatingSpecialtyCode/Name → CHREGESTA.CODESPECI + INESPECIA
--   StayTypeCode/Name          → CHREGESTA.CODTIPEST + CHTIPESTA
--   PatientName                → INPACIENT
--   EntityCode, CareCenterCode,
--   FunctionalUnitCode, Bed    → ADINGRESO
--   PayerName                  → INENTIDAD
--   CareGroupName              → Contract.HealthAdministrator
--   FunctionalUnitName         → INUNIFUNC
--   CreationUser               → COALESCE(INPROFSAL.CODUSUARI, CHREGESTA.REGUSUARI, @UsuarioMigracion)
--
-- Idempotente: clave de deduplicación = SubjectType=5 + SubjectRecordId = CHREGESTA.ID.
-- Transaccional: ROLLBACK automático en error.
-- No se incluye en el .sqlproj — script de migración de datos, no de esquema.
-- =============================================================================
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @FechaInicial date = '2022-01-01';
DECLARE @FechaFinal   date = '2026-06-15';
DECLARE @UsuarioMigracion varchar(20) = 'MIGRACION_EST';

BEGIN TRY
    BEGIN TRANSACTION;

    ;WITH EstanciasFuente AS
    (
        SELECT
            -- ---- Identificación del sujeto ----
            CAST(5 AS tinyint)                            AS SubjectType,
            CAST(re.ID AS numeric(18, 0))                 AS SubjectRecordId,

            -- ---- Contexto de ingreso ----
            LEFT(LTRIM(RTRIM(re.IPCODPACI)), 25)          AS PatientCode,
            LEFT(LTRIM(RTRIM(i.CODENTIDA)), 9)            AS EntityCode,
            LEFT(LTRIM(RTRIM(CONVERT(varchar(10), re.NUMINGRES))), 10) AS AdmissionNumber,
            LEFT(LTRIM(RTRIM(i.CODCENATE)), 10)           AS CareCenterCode,
            LEFT(LTRIM(RTRIM(i.UFUACTPAC)), 10)           AS FunctionalUnitCode,
            i.GENCAREGROUP                                AS CareGroupId,

            -- ---- Estado ----
            CAST(1 AS tinyint)                            AS Status,

            -- ---- Display del paciente ----
            LEFT(LTRIM(RTRIM(p.IPNOMCOMP)), 255)          AS PatientName,
            LEFT(LTRIM(RTRIM(TipoId.CODIGO)), 10)         AS PatientIdType,
            p.IPFECNACI                                   AS BirthDate,

            -- ---- Display del ingreso ----
            re.FECINIEST                                  AS AdmissionDate,
            TRY_CONVERT(int, i.CODCAMACT)                 AS Bed,

            LEFT(LTRIM(RTRIM(ga.[Name])), 255)            AS CareGroupName,
            LEFT(LTRIM(RTRIM(ent.NOMENTIDA)), 255)        AS PayerName,
            LEFT(LTRIM(RTRIM(U.UFUDESCRI)), 255)          AS FunctionalUnitName,

            -- ---- Tipo de estancia ----
            LEFT(LTRIM(RTRIM(re.CODTIPEST)), 3)           AS StayTypeCode,
            LEFT(LTRIM(RTRIM(est.DESTIPEST)), 40)         AS StayTypeName,

            -- ---- Display clínico — médico y especialidad tratantes ----
            LEFT(LTRIM(RTRIM(re.CODPROSAL)), 20)          AS TreatingPhysicianCode,
            LEFT(LTRIM(RTRIM(prof.NOMMEDICO)), 120)       AS TreatingPhysicianName,
            LEFT(LTRIM(RTRIM(re.CODESPECI)), 3)           AS TreatingSpecialtyCode,
            LEFT(LTRIM(RTRIM(esp.DESESPECI)), 120)        AS TreatingSpecialtyName,

            -- ---- Auditoría ----
            LEFT(
                COALESCE(
                    LTRIM(RTRIM(prof.CODUSUARI)),
                    LTRIM(RTRIM(re.REGUSUARI)),
                    @UsuarioMigracion
                ), 20
            )                                             AS CreationUser,
            re.FECINIEST                                  AS CreationDate

        FROM dbo.CHREGESTA AS re

        INNER JOIN dbo.ADINGRESO AS i
            ON i.NUMINGRES = re.NUMINGRES

        INNER JOIN dbo.INPACIENT AS p
            ON p.IPCODPACI = re.IPCODPACI

        LEFT JOIN dbo.ADTIPOIDENTIFICA AS TipoId
            ON TipoId.CODIGO = p.IPTIPODOC

        LEFT JOIN [Contract].HealthAdministrator AS ga
            ON ga.ID = i.GENCAREGROUP

        LEFT JOIN dbo.INENTIDAD AS ent
            ON ent.CODENTIDA = i.CODENTIDA

        LEFT JOIN dbo.INUNIFUNC AS U
            ON U.UFUCODIGO = i.UFUACTPAC

        LEFT JOIN dbo.CHTIPESTA AS est
            ON est.CODTIPEST = re.CODTIPEST

        LEFT JOIN dbo.INPROFSAL AS prof
            ON prof.CODPROSAL = re.CODPROSAL

        LEFT JOIN dbo.INESPECIA AS esp
            ON esp.CODESPECI = re.CODESPECI

        WHERE re.REGESTADO IN (1, 2)
          AND i.IESTADOIN  IN (' ', 'B')
          AND re.FECINIEST >= @FechaInicial
          AND re.FECINIEST  < DATEADD(DAY, 1, @FechaFinal)
          AND NULLIF(LTRIM(RTRIM(re.IPCODPACI)), '') IS NOT NULL
          AND NULLIF(LTRIM(RTRIM(i.CODENTIDA)), '')  IS NOT NULL
          AND NULLIF(LTRIM(RTRIM(i.CODCENATE)), '')  IS NOT NULL
    )

    INSERT INTO [Authorization].[AuthorizationControl]
    (
        SubjectType,
        SubjectRecordId,
        PatientCode,
        EntityCode,
        AdmissionNumber,
        CareCenterCode,
        FunctionalUnitCode,
        CareGroupId,
        Status,
        FromMedicalRecord,
        FromSurgicalReport,
        FromExtramural,
        CreationUser,
        CreationDate,
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
        TreatingPhysicianCode,
        TreatingPhysicianName,
        TreatingSpecialtyCode,
        TreatingSpecialtyName
    )

    SELECT
        F.SubjectType,
        F.SubjectRecordId,
        F.PatientCode,
        F.EntityCode,
        F.AdmissionNumber,
        F.CareCenterCode,
        F.FunctionalUnitCode,
        F.CareGroupId,
        F.Status,
        CAST(0 AS bit)              AS FromMedicalRecord,
        CAST(0 AS bit)              AS FromSurgicalReport,
        CAST(0 AS bit)              AS FromExtramural,
        F.CreationUser,
        F.CreationDate,
        F.PatientName,
        F.PatientIdType,

        CASE
            WHEN F.BirthDate IS NULL THEN NULL
            WHEN F.BirthDate > COALESCE(F.AdmissionDate, GETDATE()) THEN 0
            ELSE
                DATEDIFF(YEAR, F.BirthDate, COALESCE(F.AdmissionDate, GETDATE()))
                -
                CASE
                    WHEN DATEADD(
                             YEAR,
                             DATEDIFF(YEAR, F.BirthDate, COALESCE(F.AdmissionDate, GETDATE())),
                             F.BirthDate
                         ) > COALESCE(F.AdmissionDate, GETDATE())
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
        F.TreatingPhysicianCode,
        F.TreatingPhysicianName,
        F.TreatingSpecialtyCode,
        F.TreatingSpecialtyName

    FROM EstanciasFuente AS F

    WHERE NOT EXISTS
    (
        SELECT 1
        FROM [Authorization].[AuthorizationControl] AS AC
        WHERE AC.SubjectType     = 5
          AND AC.SubjectRecordId = F.SubjectRecordId
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
