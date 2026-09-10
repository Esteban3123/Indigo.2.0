-- =============================================================================
-- Migración histórica: ADATEINIU → Authorization.AuthorizationControl
-- SubjectType = 4 (Urgencia)
--
-- Correcciones respecto al borrador original:
--   · Folio          = ADATEINIU.NUMEFOLIO  (antes NULL)
--   · TreatingPhysicianName  = INPROFSAL.NOMMEDICO via HCHISPACA.CODPROSAL
--   · TreatingSpecialtyCode  = HCHISPACA.CODESPTRA  (antes NULL)
--   · CreationUser   = COALESCE(prof.CODUSUARI, a.CODUSUARI, @UsuarioMigracion)
--   · StayTypeCode   = CHREGESTA.CODTIPEST  (campo faltante)
--   · CreationDate   = a.FECINFORM           (evita que quede como fecha de migración)
--   · Eliminado RequiresAuthorization        (columna no existe en la tabla)
--
-- Idempotente: no inserta si ya existe un control con el mismo SubjectRecordId
-- (SubjectRecordId = ADATEINIU.CODCONCEC).
-- Transaccional: ROLLBACK automático en error.
-- No se incluye en el .sqlproj — script de migración de datos, no de esquema.
-- =============================================================================
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @FechaInicial date = '2022-01-01';
DECLARE @FechaFinal   date = '2026-06-15';
DECLARE @UsuarioMigracion varchar(20) = 'MIGRACION_URG';

BEGIN TRY
    BEGIN TRANSACTION;

    ;WITH UrgenciasFuente AS
    (
        SELECT
            CAST(a.CODCONCEC AS numeric(18, 0))                      AS SubjectRecordId,

            LTRIM(RTRIM(CONVERT(varchar(10), i.NUMINGRES)))           AS AdmissionNumber,

            a.FECINFORM                                               AS RequestDate,
            a.FECINFORM                                               AS CreationDate,

            LEFT(LTRIM(RTRIM(TipoIdentificacion.CODIGO)), 10)         AS PatientIdType,
            LEFT(LTRIM(RTRIM(i.IPCODPACI)), 25)                       AS PatientCode,
            LEFT(LTRIM(RTRIM(p.IPNOMCOMP)), 255)                      AS PatientName,

            i.IFECHAING                                               AS AdmissionDate,

            LEFT(LTRIM(RTRIM(i.CODENTIDA)), 9)                        AS EntityCode,
            LEFT(LTRIM(RTRIM(i.CODCENATE)), 10)                       AS CareCenterCode,
            LEFT(LTRIM(RTRIM(i.UFUACTPAC)), 10)                       AS FunctionalUnitCode,

            i.GENCAREGROUP                                            AS CareGroupId,

            LEFT(LTRIM(RTRIM(GrupoAtencion.[Name])), 255)             AS CareGroupName,
            LEFT(LTRIM(RTRIM(ent.NOMENTIDA)), 255)                    AS PayerName,
            LEFT(LTRIM(RTRIM(U.UFUDESCRI)), 255)                      AS FunctionalUnitName,

            TRY_CONVERT(int, i.CODCAMACT)                             AS Bed,

            reg.CODTIPEST                                             AS StayTypeCode,
            LEFT(LTRIM(RTRIM(est.DESTIPEST)), 40)                     AS StayTypeName,

            p.IPFECNACI                                               AS BirthDate,

            LEFT(LTRIM(RTRIM(a.NUMEFOLIO)), 10)                       AS Folio,

            LEFT(LTRIM(RTRIM(hc.CODPROSAL)), 20)                      AS TreatingPhysicianCode,
            LEFT(LTRIM(RTRIM(prof.NOMMEDICO)), 120)                   AS TreatingPhysicianName,

            LEFT(LTRIM(RTRIM(hc.CODESPTRA)), 3)                       AS TreatingSpecialtyCode,
            LEFT(LTRIM(RTRIM(esp.DESESPECI)), 120)                    AS TreatingSpecialtyName,

            LEFT(LTRIM(RTRIM(hc.CODDIAGNO)), 4)                      AS DiagnosisCode,
            LEFT(LTRIM(RTRIM(diag.NOMDIAGNO)), 350)                   AS DiagnosisName,

            LEFT(
                COALESCE(
                    LTRIM(RTRIM(prof.CODUSUARI)),
                    LTRIM(RTRIM(a.CODUSUARI)),
                    @UsuarioMigracion
                ), 20
            )                                                         AS CreationUser,

            ROW_NUMBER() OVER
            (
                PARTITION BY a.CODCONCEC
                ORDER BY a.FECINFORM DESC, hc.NUMEFOLIO DESC
            ) AS NumeroFila

        FROM dbo.ADATEINIU AS a

        INNER JOIN dbo.ADINGRESO AS i
            ON i.NUMINGRES = a.NUMINGRES

        INNER JOIN dbo.INPACIENT AS p
            ON p.IPCODPACI = i.IPCODPACI

        INNER JOIN dbo.ADTIPOIDENTIFICA AS TipoIdentificacion
            ON TipoIdentificacion.CODIGO = p.IPTIPODOC

        INNER JOIN dbo.HCHISPACA AS hc
            ON hc.NUMEFOLIO = a.NUMEFOLIO
           AND hc.NUMINGRES = a.NUMINGRES

        INNER JOIN [Contract].HealthAdministrator AS GrupoAtencion
            ON GrupoAtencion.ID = i.GENCAREGROUP

        LEFT JOIN dbo.INPROFSAL AS prof
            ON prof.CODPROSAL = hc.CODPROSAL

        LEFT JOIN dbo.INENTIDAD AS ent
            ON ent.CODENTIDA = i.CODENTIDA

        LEFT JOIN dbo.INESPECIA AS esp
            ON esp.CODESPECI = hc.CODESPTRA

        LEFT JOIN dbo.INUNIFUNC AS U
            ON U.UFUCODIGO = i.UFUACTPAC

        LEFT JOIN dbo.INDIAGNOS AS diag
            ON diag.CODDIAGNO = hc.CODDIAGNO

        OUTER APPLY
        (
            SELECT TOP (1)
                re.CODTIPEST
            FROM dbo.CHREGESTA AS re
            WHERE re.NUMINGRES = i.NUMINGRES
              AND re.REGESTADO = 1
            ORDER BY re.CODTIPEST DESC
        ) AS reg

        LEFT JOIN dbo.CHTIPESTA AS est
            ON est.CODTIPEST = reg.CODTIPEST

        WHERE i.IESTADOIN IN (' ', 'B')
          AND a.FECINFORM >= @FechaInicial
          AND a.FECINFORM < DATEADD(DAY, 1, @FechaFinal)
          AND a.CODCONCEC IS NOT NULL
          AND i.NUMINGRES IS NOT NULL
          AND NULLIF(LTRIM(RTRIM(i.IPCODPACI)), '') IS NOT NULL
          AND NULLIF(LTRIM(RTRIM(i.CODENTIDA)), '') IS NOT NULL
          AND NULLIF(LTRIM(RTRIM(i.CODCENATE)), '') IS NOT NULL
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
        CreationUser,
        CreationDate,
        RequestDate,
        AssignedAuthorizer,
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
        TreatingSpecialtyName,
        DiagnosisCode,
        DiagnosisName
    )

    SELECT
        CAST(4 AS tinyint)          AS SubjectType,
        NULL                        AS SubjectCode,
        F.SubjectRecordId,
        NULL                        AS ServiceType,
        F.PatientCode,
        F.EntityCode,
        F.AdmissionNumber,
        F.Folio,
        F.CareCenterCode,
        F.FunctionalUnitCode,
        F.CareGroupId,
        CAST(1 AS tinyint)          AS Status,
        NULL                        AS RequestedQuantity,
        NULL                        AS AuthorizedQuantity,
        CAST(1 AS bit)              AS IsSusceptible,
        CAST(1 AS bit)              AS FromMedicalRecord,
        CAST(0 AS bit)              AS FromSurgicalReport,
        CAST(0 AS bit)              AS FromExtramural,
        NULL                        AS ClinicalJustification,
        NULL                        AS CancellationJustification,
        F.CreationUser,
        F.CreationDate,
        F.RequestDate,
        NULL                        AS AssignedAuthorizer,
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
        F.TreatingPhysicianCode,
        F.TreatingPhysicianName,
        F.TreatingSpecialtyCode,
        F.TreatingSpecialtyName,
        F.DiagnosisCode,
        F.DiagnosisName

    FROM UrgenciasFuente AS F

    WHERE F.NumeroFila = 1
      AND NOT EXISTS
      (
          SELECT 1
          FROM [Authorization].[AuthorizationControl] AS AC
          WHERE AC.SubjectType     = 4
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
