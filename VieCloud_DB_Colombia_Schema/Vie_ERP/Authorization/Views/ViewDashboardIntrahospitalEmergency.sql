CREATE VIEW [Authorization].[ViewDashboardIntrahospitalEmergency]
AS
    SELECT
        CAST(
            CASE
                WHEN EXISTS (
                    SELECT 1
                    FROM [Authorization].[AuthorizationControl] Pending 
                    WHERE Pending.AdmissionNumber = AC.AdmissionNumber
                      AND Pending.PatientCode = AC.PatientCode
                      AND Pending.SubjectType IN (1, 2, 3, 5)
                      AND Pending.Status IN (1, 2)
                      AND Pending.Id <> AC.Id
                )
                THEN 1
                ELSE 0
            END
        AS bit) AS Alerta,
        LTRIM(RTRIM(AC.AdmissionNumber)) AS Ingreso,
        AC.Id AS NoSolicitud,
        ISNULL(AC.RequestDate, AC.CreationDate) AS FechaSolicitud,
        LTRIM(RTRIM(ISNULL(AC.PatientIdTypeName, ''))) AS TipoIdentificacion,
        LTRIM(RTRIM(AC.PatientCode)) AS Identificacion,
        LTRIM(RTRIM(ISNULL(AC.PatientName, ''))) AS Paciente,
        AC.AdmissionDate AS FechaIngreso,
        LTRIM(RTRIM(ISNULL(AC.CareGroupName, ''))) AS GrupoAtencion,
        LTRIM(RTRIM(ISNULL(AC.PayerName, ''))) AS EntidadAdministradora,
        LTRIM(RTRIM(ISNULL(AC.FunctionalUnitCode, ''))) AS UnidadFuncionalPaciente,
        LTRIM(RTRIM(ISNULL(AC.FunctionalUnitName, ''))) AS UnidadFuncionalActual,
        ISNULL(ESTA.BedNumber, '')                                         AS Cama,
        LTRIM(RTRIM(COALESCE(AC.StayTypeName, ESTA.StayTypeName, ''))) AS TipoCama,
        AC.PatientAge AS Edad,
        LTRIM(RTRIM(ISNULL(AC.TreatingPhysicianName, ''))) AS MedicoTratante,
        LTRIM(RTRIM(ISNULL(AC.TreatingSpecialtyName, ''))) AS EspecialidadTratante,
        LTRIM(RTRIM(ISNULL(AC.DiagnosisCode, ''))) AS CodDiagnostico,
        LTRIM(RTRIM(ISNULL(AC.DiagnosisName, ''))) AS Diagnostico,
        AC.SubjectRecordId,
        AC.SubjectType,
        AC.Status,
        AC.CareCenterCode,
        LTRIM(RTRIM(ISNULL(AC.FunctionalUnitCode, ''))) AS FunctionalUnitCode,
        AC.AssignedAuthorizer,
        AC.CreationUser,
        LTRIM(RTRIM(ISNULL(AC.EntityCode, '')))                          AS CodigoEntidad,
        ISNULL(AC.AdmissionDate, AC.CreationDate) AS FechaFiltro,
        AA.Id                                     AS AnnexId,
        LTRIM(RTRIM(ISNULL(AC.Folio, '')))        AS FolioSolicitud,
        ISNULL(PAC.IPDIRECCI, '')                 AS DireccionPaciente,
        ISNULL(PAC.IPTELEFON, '')                 AS TelefonoPaciente,
        ISNULL(AC.RequestedQuantity, 1)            AS Cantidad
    FROM [Authorization].[AuthorizationControl] AC 
    OUTER APPLY (
        SELECT TOP 1 Id
        FROM [Authorization].[AuthorizationAnnexes] 
        WHERE AuthorizationControlId = AC.Id
        ORDER BY CreationDate DESC
    ) AA
    OUTER APPLY (
        SELECT TOP 1
            LTRIM(RTRIM(CAM.NUMCAMHOS)) AS BedNumber,
            LTRIM(RTRIM(TIP.DESTIPEST))  AS StayTypeName
        FROM dbo.CHREGESTA CHR 
        LEFT JOIN dbo.CHCAMASHO CAM  ON CAM.CODICAMAS = CHR.CODICAMAS
        LEFT JOIN dbo.CHTIPESTA TIP  ON TIP.CODTIPEST = CHR.CODTIPEST
        WHERE CHR.NUMINGRES = AC.AdmissionNumber
          AND CHR.IPCODPACI = AC.PatientCode
          AND CHR.REGESTADO = 1
        ORDER BY CHR.FECINIEST DESC
    ) ESTA
    INNER JOIN INPACIENT PAC ON AC.PatientCode = PAC.IPCODPACI
    WHERE AC.SubjectType = 4
      AND AC.Status IN (1,2)
      AND AC.SubjectRecordId IS NOT NULL;
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dashboard de autorizaciones intrahospitalarias V2 - tab Urgencias. Expone pacientes de urgencias con orden o indicacion de hospitalizacion desde Authorization.AuthorizationControl y calcula alerta por pendientes del mismo ingreso/paciente en solicitudes, radicados o estancias.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewDashboardIntrahospitalEmergency';
GO
