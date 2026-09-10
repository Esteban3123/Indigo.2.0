CREATE VIEW [Authorization].[ViewDashboardIntrahospitalEstancias]
AS
    SELECT
        AC.Id,
        CAST(
            CASE
                WHEN EXISTS (
                    SELECT 1
                    FROM [Authorization].[AuthorizationControl] Pend 
                    WHERE Pend.AdmissionNumber = AC.AdmissionNumber
                      AND Pend.PatientCode    = AC.PatientCode
                      AND Pend.SubjectType    = 5
                      AND Pend.Status        IN (1, 2)
                      -- Excluir la propia fila (2026-07-27). Sin este predicado toda estancia
                      -- en estado 1/2 se contaba a sí misma y la columna Alerta salía encendida
                      -- siempre en esta pestaña. Las otras 5 vistas del dashboard ya lo tenían.
                      AND Pend.Id            <> AC.Id
                )
                THEN 1
                ELSE 0
            END
        AS bit) AS Alerta,
        LTRIM(RTRIM(AC.AdmissionNumber))                                 AS Ingreso,
        LTRIM(RTRIM(ISNULL(AC.Folio, '')))                               AS FolioSolicitud,
        ISNULL(AC.RequestDate, AC.CreationDate)                          AS FechaSolicitud,
        LTRIM(RTRIM(ISNULL(AC.PatientIdTypeName, '')))                       AS TipoIdentificacion,
        LTRIM(RTRIM(ISNULL(AC.PatientIdType, '')))                       AS TipoIdentificacion2,
        LTRIM(RTRIM(AC.PatientCode))                                     AS Identificacion,
        LTRIM(RTRIM(ISNULL(AC.PatientName, '')))                         AS Paciente,
        AC.AdmissionDate                                                 AS FechaIngreso,
        LTRIM(RTRIM(ISNULL(AC.CareGroupName, '')))                       AS GrupoAtencion,
        LTRIM(RTRIM(ISNULL(AC.PayerName, '')))                           AS EntidadResponsable,
        LTRIM(RTRIM(ISNULL(AC.EntityCode, '')))                          AS CodigoEntidad,
        LTRIM(RTRIM(ISNULL(AC.FunctionalUnitName, '')))                  AS UnidadFuncional,
        ISNULL(CAST(AC.Bed AS VARCHAR(10)), '')                          AS IdCama,
        ISNULL(CAST(AC.BedDescription AS VARCHAR(10)), '')                          AS Cama,
        LTRIM(RTRIM(ISNULL(AC.StayTypeName, AC.StayTypeCode)))           AS TipoCama,
        ISNULL(CAST(AC.PatientAge AS VARCHAR(10)), '')                   AS Edad,
        LTRIM(RTRIM(ISNULL(AC.TreatingPhysicianName, '')))               AS MedicoTratante,
        LTRIM(RTRIM(ISNULL(AC.TreatingSpecialtyName, '')))               AS EspecialidadTratante,
        LTRIM(RTRIM(ISNULL(AC.DiagnosisCode, '')))                       AS CodDiagnostico,
        LTRIM(RTRIM(ISNULL(AC.DiagnosisName, '')))                       AS Diagnostico,
        AC.Status,
        ISNULL(AC.RequestedQuantity, 1)                                  AS Cantidad,
        AC.CareCenterCode,
        LTRIM(RTRIM(ISNULL(AC.FunctionalUnitCode, '')))                  AS FunctionalUnitCode,
        AC.AssignedAuthorizer,
        AC.CreationUser,
        ISNULL(AC.AdmissionDate, AC.CreationDate)                        AS FechaFiltro,
        dbo.Edad(PAC.IPFECNACI, GETDATE())                               AS EdadCompleta,
        PAC.IPDIRECCI                                                    AS DireccionPaciente,
        PAC.IPTELEFON                                                    AS TelefonoPaciente
    FROM [Authorization].[AuthorizationControl] AC 
    INNER JOIN INPACIENT PAC ON AC.PatientCode = PAC.IPCODPACI
    WHERE AC.SubjectType = 5
      AND AC.Status NOT IN (7, 8);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dashboard autorizaciones intrahospitalarias V2 - tab Estancias. Expone autorizaciones de tipo estancia (SubjectType=5) en cualquier estado salvo Cancelado (7) y Rechazado (8), enriquecidas con INPACIENT para la edad completa (dbo.Edad). TipoIdentificacion=nombre del tipo de documento, TipoIdentificacion2=código; IdCama=AC.Bed, Cama=descripción. FechaFiltro basada en AdmissionDate para coherencia con el ciclo de hospitalización. Alerta activa cuando el ingreso tiene controles de estancia en estado 1/2 (incluido el propio registro).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewDashboardIntrahospitalEstancias';
GO
