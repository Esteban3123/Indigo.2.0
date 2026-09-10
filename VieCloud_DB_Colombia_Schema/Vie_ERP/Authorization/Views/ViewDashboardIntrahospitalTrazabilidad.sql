CREATE VIEW [Authorization].[ViewDashboardIntrahospitalTrazabilidad]
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
                      AND Pend.SubjectType   IN (1, 2, 3, 4, 5)
                      AND Pend.Status        IN (1, 2)
                      AND Pend.Id            <> AC.Id
                )
                THEN 1
                ELSE 0
            END
        AS bit) AS Alerta,
        ISNULL(AC.RequestDate, AC.CreationDate)                          AS FechaSolicitud,
        LTRIM(RTRIM(AC.AdmissionNumber))                                 AS Ingreso,
        ISNULL(CAST(AC.Bed AS VARCHAR(10)), '')                          AS Cama,
        LTRIM(RTRIM(ISNULL(AC.StayTypeName, AC.StayTypeCode)))           AS TipoCama,
        LTRIM(RTRIM(ISNULL(AC.PatientName, '')))                         AS Paciente,
        LTRIM(RTRIM(AC.PatientCode))                                     AS Identificacion,
        LTRIM(RTRIM(ISNULL(AC.PatientIdTypeName, '')))                   AS TipoIdentificacion,
        ISNULL(CAST(AC.PatientAge AS VARCHAR(10)), '')                   AS Edad,
        LTRIM(RTRIM(ISNULL(AC.ServiceBillingGroup, '')))                 AS TipoServicio,
        LTRIM(RTRIM(ISNULL(AC.SubjectCode, '')))                         AS CodigoServicio,
        LTRIM(RTRIM(ISNULL(AC.ServiceDescription, '')))                  AS DescripcionServicio,
        LTRIM(RTRIM(ISNULL(AC.RelatedDescription, '')))                  AS DescripcionRelacionada,
        AC.RequestedQuantity                                             AS Cantidad,
        LTRIM(RTRIM(ISNULL(AC.FunctionalUnitName, '')))                  AS UnidadFuncional,
        LTRIM(RTRIM(ISNULL(AC.DiagnosisCode, '')))                       AS CodDiagnostico,
        LTRIM(RTRIM(ISNULL(AC.DiagnosisName, '')))                       AS Diagnostico,
        LTRIM(RTRIM(ISNULL(AC.RequestingPhysicianName, '')))             AS MedicoSolicitante,
        LTRIM(RTRIM(ISNULL(AC.TreatingSpecialtyName, '')))               AS EspecialidadMedico,
        LTRIM(RTRIM(ISNULL(AC.Folio, '')))                               AS FolioSolicitud,
        LTRIM(RTRIM(ISNULL(AC.CareGroupName, '')))                       AS GrupoAtencion,
        LTRIM(RTRIM(ISNULL(AC.PayerName, '')))                           AS EntidadResponsable,
        LTRIM(RTRIM(ISNULL(AC.EntityCode, '')))                          AS CodigoEntidad,
        AC.IsCovered                                                     AS Cotizacion,
        LTRIM(RTRIM(ISNULL(AC.AssignedAuthorizer, '')))                  AS Asignacion,
        AC.ParametrizedTime                                              AS TiempoParametrizado,
        AC.SemaphoreDeadline                                             AS FechaVencimiento,
        AC.Status,
        CASE AC.Status
            WHEN 1 THEN 'No solicitado'
            WHEN 2 THEN 'En trámite'
            WHEN 3 THEN 'Radicado'
            WHEN 4 THEN 'Autorizado'
            WHEN 5 THEN 'Entregado'
            WHEN 6 THEN 'Confirmado'
            WHEN 7 THEN 'Cancelado'
            WHEN 8 THEN 'Rechazado'
            ELSE ''
        END                                                              AS DescripcionEstado,
        AC.CareCenterCode,
        LTRIM(RTRIM(ISNULL(AC.FunctionalUnitCode, '')))                  AS FunctionalUnitCode,
        AC.AssignedAuthorizer,
        AC.CreationUser,
        ISNULL(AC.RequestDate, AC.CreationDate)                          AS FechaFiltro
    FROM [Authorization].[AuthorizationControl] AC ;
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dashboard autorizaciones intrahospitalarias V2 - tab Trazabilidad. Expone todos los controles en cualquier estado para consulta historica. Incluye DescripcionEstado legible y soporta busqueda libre por ingreso, paciente o identificacion desde el XPO criteria.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewDashboardIntrahospitalTrazabilidad';
GO
