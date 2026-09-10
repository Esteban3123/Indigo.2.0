CREATE VIEW [Authorization].[ViewDashboardIntrahospitalAutorizado]
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
                      AND Pend.SubjectType   IN (1, 2, 3)
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
        AC.CareCenterCode,
        LTRIM(RTRIM(ISNULL(AC.FunctionalUnitCode, '')))                  AS FunctionalUnitCode,
        AC.AssignedAuthorizer,
        AC.CreationUser,
        ISNULL(AC.AuthorizedDate, AC.CreationDate)                       AS FechaFiltro,
        -- Semáforo SLA (Opción B, 2026-07-03): 1=Verde (<50% del SLA), 2=Amarillo (>50%),
        -- 3=Rojo (vencido), NULL=sin semáforo. En esta vista Status=4 → fase DeliveryService,
        -- fecha base AuthorizedDate.
        -- Guardas agregadas 2026-07-27:
        --   * slaTime > 0        : un SLA parametrizado en cero NO es un SLA configurado.
        --                          Request/Radicated/DeliveryService son NOT NULL, así que
        --                          GetControlSlaTime devolvía 0, el guard IS NOT NULL pasaba
        --                          y la celda salía ROJA de inmediato (falso positivo).
        --   * slaUnit IN (1,2,3) : con unidad fuera de rango GetRequestElapsedTime devuelve 0
        --                          por su ISNULL(...,0) y la celda salía VERDE aunque el plazo
        --                          estuviera vencido hace días (falso negativo, el peligroso).
        CASE
            WHEN calc.slaTime > 0
             AND calc.slaUnit IN (1, 2, 3)
             AND calc.fechaBase IS NOT NULL
            THEN [Authorization].fnGetColor(
                     calc.slaTime,
                     [Authorization].GetRequestElapsedTime(calc.fechaBase, calc.slaUnit, GETDATE()))
        END                                                              AS ColorRequest,
        ISNULL(PAC.IPDIRECCI, '')                                        AS DireccionPaciente,
        ISNULL(PAC.IPTELEFON, '')                                        AS TelefonoPaciente
    FROM [Authorization].[AuthorizationControl] AC 
    -- SLA del portafolio HOSPITALARIO (TypePortfolio=2) para el ítem del control:
    -- CUPS (SubjectType=1) o producto de inventario (SubjectType=2/3), con override por
    -- caregroup. TOP(1) replica el FirstOrDefault del PortfolioConfigurationResolver.
    OUTER APPLY (
        SELECT TOP (1)
            ISNULL(csae.Request,             csa.Request)             AS Request,
            ISNULL(csae.RequestUnit,         csa.RequestUnit)         AS RequestUnit,
            ISNULL(csae.Radicated,           csa.Radicated)           AS Radicated,
            ISNULL(csae.RadicatedUnit,       csa.RadicatedUnit)       AS RadicatedUnit,
            ISNULL(csae.DeliveryService,     csa.DeliveryService)     AS DeliveryService,
            ISNULL(csae.DeliveryServiceUnit, csa.DeliveryServiceUnit) AS DeliveryServiceUnit
        FROM (
            SELECT csa1.Id
            FROM [Authorization].[AuthorizationPortfolio] ap 
            JOIN [Authorization].[AuthorizationPortfolioCareCenter] apcc 
                ON apcc.AuthorizationPortfolioId = ap.Id
            JOIN [Authorization].[AuthorizationPortfolioCUPSEntity] apce 
                ON apce.AuthorizationPortfolioId = ap.Id
            JOIN [Contract].[CUPSEntity] ce 
                ON ce.Id = apce.CUPSEntityId
            JOIN [Authorization].[ConfigurationServicesAmbulatory] csa1 
                ON csa1.AuthorizationPortfolioCUPSEntityId = apce.Id
            WHERE AC.SubjectType = 1
              AND ap.TypePortfolio = 2
              AND ap.Status = 1
              AND apcc.CareCenterCode = AC.CareCenterCode
              AND ce.Code = AC.SubjectCode

            UNION ALL

            SELECT csa2.Id
            FROM [Authorization].[AuthorizationPortfolio] ap 
            JOIN [Authorization].[AuthorizationPortfolioCareCenter] apcc 
                ON apcc.AuthorizationPortfolioId = ap.Id
            JOIN [Authorization].[AuthorizationPortfolioInventoryProduct] apip 
                ON apip.AuthorizationPortfolioId = ap.Id
            JOIN [Inventory].[InventoryProduct] ip 
                ON ip.Id = apip.InventoryProductId
            JOIN [Authorization].[ConfigurationServicesAmbulatory] csa2 
                ON csa2.AuthorizationPortfolioInventoryProductId = apip.Id
            WHERE AC.SubjectType IN (2, 3)
              AND ap.TypePortfolio = 2
              AND ap.Status = 1
              AND apcc.CareCenterCode = AC.CareCenterCode
              AND ip.Code = AC.SubjectCode
        ) k
        JOIN [Authorization].[ConfigurationServicesAmbulatory] csa 
            ON csa.Id = k.Id
        LEFT JOIN [Authorization].[ConfigurationServicesAmbulatoryExceptions] csae 
            ON csae.ConfigurationServicesAmbulatoryId = csa.Id
        -- OJO: si AC.CareGroupId viene en 0 (valor que no existe en Contract.CareGroup) este
        -- join nunca encuentra pareja y las excepciones por grupo de atención quedan inertes.
        -- El origen está en quien puebla AuthorizationControl.CareGroupId, no en esta vista.
           AND csae.CareGroupId = NULLIF(AC.CareGroupId, 0)
        -- Desempate explícito (2026-07-27). Antes era ORDER BY csa.Id, es decir ganaba la
        -- configuración MÁS ANTIGUA: con un ítem cargado dos veces en el portafolio
        -- (ej. CUPS 250001 en el portafolio 55 → apce 7413 y 7423) editar la configuración
        -- nueva no se reflejaba nunca en el dashboard. Ahora gana la excepción por caregroup
        -- si existe y, entre configuraciones duplicadas, la más reciente.
        ORDER BY CASE WHEN csae.Id IS NOT NULL THEN 0 ELSE 1 END,
                 csa.Id DESC,
                 csae.Id
    ) sla
    CROSS APPLY (
        SELECT
            [Authorization].GetControlSlaTime(
                AC.Status, sla.Request, sla.Radicated, sla.DeliveryService
            ) AS slaTime,
            [Authorization].GetControlSlaUnit(
                AC.Status, sla.RequestUnit, sla.RadicatedUnit, sla.DeliveryServiceUnit
            ) AS slaUnit,
            CASE AC.Status
                WHEN 3 THEN AC.FiledDate
                WHEN 4 THEN AC.AuthorizedDate
                ELSE ISNULL(AC.RequestDate, AC.CreationDate)
            END AS fechaBase
    ) calc
    INNER JOIN INPACIENT PAC ON AC.PatientCode = PAC.IPCODPACI
    WHERE AC.SubjectType IN (1, 2, 3, 4)
      AND AC.Status = 4;
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dashboard autorizaciones intrahospitalarias V2 - tab Autorizado. Expone controles en estado Autorizado (4) para SubjectType 1-4 (excluye Estancias/5, que se gestionan en su propia pestaña). Alerta activa si el mismo ingreso tiene otros items pendientes de tramite. Incluye ColorRequest: semáforo SLA (1=Verde, 2=Amarillo, 3=Rojo, NULL=sin semáforo) de la fase DeliveryService desde AuthorizedDate, calculado con GetControlSlaTime/GetControlSlaUnit + GetRequestElapsedTime + fnGetColor contra el portafolio hospitalario con excepciones por caregroup.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewDashboardIntrahospitalAutorizado';
GO
