

-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-06-09
-- Description:	Procedimiento que se encarga de guardar, actualizar la gestión de orden médica
-- =============================================
CREATE PROCEDURE [Authorization].[SP_SaveManagementMedicalOrder] 
    @ListManagementMedicalOrderXml AS XML,
    @CodeUser AS VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE
        @Message VARCHAR(MAX),
        @SubXml XML,
        @Code_Output INT = 0,
        @Message_Output VARCHAR(MAX);

    CREATE TABLE #ManagementMedicalOrder
    (
        RowHeaderId int IDENTITY(1,1) NOT NULL,
        Id int NOT NULL,
        OperatingUnitId int NOT NULL,
        EntityName varchar(50) NOT NULL,
        EntityId int NOT NULL,
        CareCenterCode varchar(20) NOT NULL,
        FunctionalUnitCode varchar(20) NOT NULL,
        AdmissionNumber varchar(20) NOT NULL,
        Folio varchar(20) NOT NULL,
        PatientCode varchar(20) NOT NULL,
        ProfessionalCode varchar(20) NOT NULL,
        RequestDate datetime NOT NULL,
        RequestQuantity int NOT NULL,
        [Type] tinyint NOT NULL,
        ItemCode varchar(20) NOT NULL,
        CancellationReasonsId int NULL,
        CancellationReasonsObservations varchar(max) NULL,
        [Status] tinyint NOT NULL
    );

    CREATE TABLE #ViewListRequests
    (
        EntityName varchar(50) NOT NULL,
        EntityId int NOT NULL,
        ItemCodeOriginal varchar(20) NULL,
        ServiceCode varchar(20) NULL,
        TraceabilityPaperworkId int NULL,
        AdmissionNumber varchar(20) NULL,
        Folio varchar(20) NULL,
        [Type] tinyint NULL,
        PatientCode varchar(20) NULL,
        CareCenterCode varchar(20) NULL,
        RequestDate datetime NULL,
        Quantity int NULL,
        FunctionalUnitCode varchar(20) NULL,
        TraceabilityPaperworkStatus tinyint NULL,
        AssignUser varchar(max) NULL,
        AuthorizationSourceId int NULL,
        IsManual bit NULL,
        CareGroupId int NULL,
        HealthAdministratorId int NULL,
        ProfessionalCode varchar(20) NULL,
        ServiceId int NULL,
        ContractDescriptionId int NULL
    );

    BEGIN TRY

        INSERT INTO #ManagementMedicalOrder
        (
            Id,
            OperatingUnitId,
            EntityName,
            EntityId,
            CareCenterCode,
            FunctionalUnitCode,
            AdmissionNumber,
            Folio,
            PatientCode,
            ProfessionalCode,
            RequestDate,
            RequestQuantity,
            [Type],
            ItemCode,
            CancellationReasonsId,
            CancellationReasonsObservations,
            [Status]
        )
        SELECT
            t.x.value('Id[1]','int'),
            t.x.value('OperatingUnitId[1]','int'),
            t.x.value('EntityName[1]','varchar(50)'),
            t.x.value('EntityId[1]','int'),
            t.x.value('CareCenterCode[1]','varchar(20)'),
            t.x.value('FunctionalUnitCode[1]','varchar(20)'),
            t.x.value('AdmissionNumber[1]','varchar(20)'),
            t.x.value('Folio[1]','varchar(20)'),
            t.x.value('PatientCode[1]','varchar(20)'),
            t.x.value('ProfessionalCode[1]','varchar(20)'),
            t.x.value('RequestDate[1]','datetime'),
            t.x.value('RequestQuantity[1]','int'),
            t.x.value('Type[1]','tinyint'),
            t.x.value('ItemCode[1]','varchar(20)'),
            t.x.value('CancellationReasonsId[1]','int'),
            t.x.value('CancellationReasonsObservations[1]','varchar(max)'),
            t.x.value('Status[1]','tinyint')
        FROM @ListManagementMedicalOrderXml.nodes('/ManagementMedicalOrder') t(x);

        CREATE CLUSTERED INDEX IX_tmp_ManagementMedicalOrder_Entity_Item
            ON #ManagementMedicalOrder (EntityName, EntityId, ItemCode, RowHeaderId);

        CREATE INDEX IX_tmp_ManagementMedicalOrder_Status_Cancel
            ON #ManagementMedicalOrder ([Status], CancellationReasonsId)
            INCLUDE (EntityName, EntityId, ItemCode, PatientCode, AdmissionNumber, Folio, [Type]);

        /***********************************************  VALIDACIONES ***********************************************/

        IF NOT EXISTS (SELECT 1 FROM #ManagementMedicalOrder)
        BEGIN
            SELECT 999 AS CodeResult,
                   'No se encontraron solicitudes a procesar.' AS MessageResult;
            RETURN;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM #ManagementMedicalOrder tmmo
            JOIN [Authorization].ManagementMedicalOrder mmo
                ON tmmo.EntityId = mmo.EntityId
               AND tmmo.EntityName = mmo.EntityName
               AND tmmo.ItemCode = mmo.ItemCode
        )
        BEGIN
            SELECT @Message = STUFF
            (
                (
                    SELECT DISTINCT
                        CHAR(13) + CHAR(10) +
                        CONCAT(
                            ' - Paciente: ', tmmo.PatientCode,
                            ' - Ingreso: ', tmmo.AdmissionNumber,
                            ' - Folio: ', tmmo.Folio,
                            ' - ', IIF(tmmo.Type = 1, 'Servicio: ', 'Producto: '),
                            tmmo.ItemCode
                        )
                    FROM #ManagementMedicalOrder tmmo
                    JOIN [Authorization].ManagementMedicalOrder mmo
                        ON tmmo.EntityId = mmo.EntityId
                       AND tmmo.EntityName = mmo.EntityName
                       AND tmmo.ItemCode = mmo.ItemCode
                    FOR XML PATH(N''), TYPE
                ).value(N'.[1]', N'nvarchar(max)')
            , 1, 2, N'');

            SELECT 999 AS CodeResult,
                   'Las siguientes solicitudes ya fueron procesadas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') AS MessageResult;
            RETURN;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM #ManagementMedicalOrder tmmo
            WHERE tmmo.Status NOT IN (1, 3)
        )
        BEGIN
            SELECT @Message = STUFF
            (
                (
                    SELECT DISTINCT
                        CHAR(13) + CHAR(10) +
                        CONCAT(
                            ' - Paciente: ', tmmo.PatientCode,
                            ' - Ingreso: ', tmmo.AdmissionNumber,
                            ' - Folio: ', tmmo.Folio,
                            ' - ', IIF(tmmo.Type = 1, 'Servicio: ', 'Producto: '),
                            tmmo.ItemCode
                        )
                    FROM #ManagementMedicalOrder tmmo
                    WHERE tmmo.Status NOT IN (1, 3)
                    FOR XML PATH(N''), TYPE
                ).value(N'.[1]', N'nvarchar(max)')
            , 1, 2, N'');

            SELECT 999 AS CodeResult,
                   'Las siguientes solicitudes no tienen un estado válido: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') AS MessageResult;
            RETURN;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM #ManagementMedicalOrder tmmo
            LEFT JOIN [Authorization].CancellationReasons cr
                ON tmmo.CancellationReasonsId = cr.Id
            WHERE tmmo.Status = 1
              AND cr.Id IS NULL
        )
        BEGIN
            SELECT @Message = STUFF
            (
                (
                    SELECT DISTINCT
                        CHAR(13) + CHAR(10) +
                        CONCAT(
                            ' - Paciente: ', tmmo.PatientCode,
                            ' - Ingreso: ', tmmo.AdmissionNumber,
                            ' - Folio: ', tmmo.Folio,
                            ' - ', IIF(tmmo.Type = 1, 'Servicio: ', 'Producto: '),
                            tmmo.ItemCode
                        )
                    FROM #ManagementMedicalOrder tmmo
                    LEFT JOIN [Authorization].CancellationReasons cr
                        ON tmmo.CancellationReasonsId = cr.Id
                    WHERE tmmo.Status = 1
                      AND cr.Id IS NULL
                    FOR XML PATH(N''), TYPE
                ).value(N'.[1]', N'nvarchar(max)')
            , 1, 2, N'');

            SELECT 999 AS CodeResult,
                   'Las siguientes solicitudes de cancelación no tienen asociada una razón de cancelación: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') AS MessageResult;
            RETURN;
        END;

        ---------------------------------------------------------------------------------------------------------------

        DELETE tmmo
        FROM #ManagementMedicalOrder tmmo
        JOIN
        (
            SELECT
                tmmo.EntityName,
                tmmo.EntityId,
                tmmo.ItemCode,
                MIN(tmmo.RowHeaderId) AS RowHeaderId
            FROM #ManagementMedicalOrder tmmo
            GROUP BY tmmo.EntityName, tmmo.EntityId, tmmo.ItemCode
            HAVING COUNT(1) > 1
        ) tmmod
            ON tmmo.EntityName = tmmod.EntityName
           AND tmmo.EntityId = tmmod.EntityId
           AND tmmo.ItemCode = tmmod.ItemCode
           AND tmmo.RowHeaderId <> tmmod.RowHeaderId;

        ---------------------------------------------------------------------------------------------------------------

        IF EXISTS
        (
            SELECT 1
            FROM #ManagementMedicalOrder tmmo
            GROUP BY tmmo.EntityName, tmmo.EntityId, tmmo.ItemCode
            HAVING COUNT(1) > 1
        )
        BEGIN
            SELECT @Message = STUFF
            (
                (
                    SELECT DISTINCT
                        CHAR(13) + CHAR(10) +
                        CONCAT(
                            ' - Paciente: ', tmmo.PatientCode,
                            ' - Ingreso: ', tmmo.AdmissionNumber,
                            ' - Folio: ', tmmo.Folio,
                            ' - ', IIF(tmmo.Type = 1, 'Servicio: ', 'Producto: '),
                            tmmo.ItemCode
                        )
                    FROM #ManagementMedicalOrder tmmo
                    GROUP BY
                        tmmo.EntityName,
                        tmmo.EntityId,
                        tmmo.PatientCode,
                        tmmo.AdmissionNumber,
                        tmmo.Folio,
                        tmmo.Type,
                        tmmo.ItemCode
                    HAVING COUNT(1) > 1
                    FOR XML PATH(N''), TYPE
                ).value(N'.[1]', N'nvarchar(max)')
            , 1, 2, N'');

            SELECT 999 AS CodeResult,
                   'Las siguientes solicitudes se encuentran duplicadas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') AS MessageResult;
            RETURN;
        END;

        /**************************************** MATERIALIZACION DE LA VISTA ****************************************/

        IF EXISTS
        (
            SELECT 1
            FROM #ManagementMedicalOrder
            WHERE CancellationReasonsId IS NOT NULL
        )
        BEGIN
            INSERT INTO #ViewListRequests
            (
                EntityName,
                EntityId,
                ItemCodeOriginal,
                ServiceCode,
                TraceabilityPaperworkId,
                AdmissionNumber,
                Folio,
                [Type],
                PatientCode,
                CareCenterCode,
                RequestDate,
                Quantity,
                FunctionalUnitCode,
                TraceabilityPaperworkStatus,
                AssignUser,
                AuthorizationSourceId,
                IsManual,
                CareGroupId,
                HealthAdministratorId,
                ProfessionalCode,
                ServiceId,
                ContractDescriptionId
            )
            SELECT
                v.EntityName,
                v.EntityId,
                v.ItemCodeOriginal,
                v.ServiceCode,
                v.TraceabilityPaperworkId,
                v.AdmissionNumber,
                v.Folio,
                v.[Type],
                v.PatientCode,
                v.CareCenterCode,
                v.RequestDate,
                v.Quantity,
                v.FunctionalUnitCode,
                v.TraceabilityPaperworkStatus,
                v.AssignUser,
                v.AuthorizationSourceId,
                v.IsManual,
                v.CareGroupId,
                v.HealthAdministratorId,
                v.ProfessionalCode,
                v.ServiceId,
                v.ContractDescriptionId
            FROM [Authorization].[ViewListRequests] v
            JOIN #ManagementMedicalOrder tmmo
                ON tmmo.EntityName = v.EntityName
               AND tmmo.EntityId = v.EntityId
               AND (tmmo.ItemCode = v.ItemCodeOriginal OR tmmo.ItemCode = v.ServiceCode)
            WHERE tmmo.CancellationReasonsId IS NOT NULL;

            CREATE CLUSTERED INDEX IX_tmp_ViewListRequests_Entity_Item
                ON #ViewListRequests (EntityName, EntityId, ItemCodeOriginal);

            CREATE INDEX IX_tmp_ViewListRequests_Entity_Service
                ON #ViewListRequests (EntityName, EntityId, ServiceCode);
        END;

        /************************************************** ALERTAS **************************************************/

        IF EXISTS
        (
            SELECT 1
            FROM #ManagementMedicalOrder tmmo
            JOIN #ViewListRequests v
                ON tmmo.EntityName = v.EntityName
               AND tmmo.EntityId = v.EntityId
               AND tmmo.ItemCode = v.ItemCodeOriginal
            WHERE tmmo.CancellationReasonsId IS NOT NULL
        )
        BEGIN
            SELECT @SubXml = CONVERT
            (
                XML,
                (
                    SELECT *
                    FROM
                    (
                        SELECT
                            tmmo.RowHeaderId,
                            v.TraceabilityPaperworkId AS Id,
                            v.AdmissionNumber,
                            v.Folio,
                            v.ItemCodeOriginal AS ServiceCode,
                            v.Type,
                            v.PatientCode,
                            v.CareCenterCode,
                            v.RequestDate,
                            v.Quantity AS RequestQuantity,
                            v.FunctionalUnitCode,
                            v.EntityId,
                            v.EntityName,
                            v.TraceabilityPaperworkStatus AS Status,
                            tp.CancellationReasonsId,
                            tp.CancellationReasonsObservations,
                            tp.CancellationUserCode,
                            v.AssignUser AS AssignUserCode,
                            v.AuthorizationSourceId,
                            v.IsManual,
                            v.CareGroupId,
                            v.HealthAdministratorId,
                            v.ProfessionalCode,
                            tp.CareCenterTargetCode,
                            tp.FunctionalUnitTargetId,
                            v.ServiceId,
                            v.ContractDescriptionId
                        FROM #ManagementMedicalOrder tmmo
                        JOIN #ViewListRequests v
                            ON tmmo.EntityName = v.EntityName
                           AND tmmo.EntityId = v.EntityId
                           AND tmmo.ItemCode = v.ItemCodeOriginal
                        LEFT JOIN [Authorization].TraceabilityPaperwork tp
                            ON v.TraceabilityPaperworkId = tp.Id
                        WHERE tmmo.CancellationReasonsId IS NOT NULL
                    ) TraceabilityPaperwork
                    JOIN
                    (
                        SELECT
                            tmmo.RowHeaderId,
                            v.TraceabilityPaperworkId,
                            CONCAT(cr.Code, ' - ', cr.Name, ': ', tmmo.CancellationReasonsObservations) AS Comments,
                            1 AS Status,
                            @CodeUser AS CreationUser
                        FROM #ManagementMedicalOrder tmmo
                        JOIN #ViewListRequests v
                            ON tmmo.EntityName = v.EntityName
                           AND tmmo.EntityId = v.EntityId
                           AND tmmo.ItemCode = v.ItemCodeOriginal
                        JOIN [Authorization].CancellationReasons cr
                            ON tmmo.CancellationReasonsId = cr.Id
                        WHERE tmmo.CancellationReasonsId IS NOT NULL
                    ) TraceabilityPaperworkAlert
                        ON TraceabilityPaperwork.RowHeaderId = TraceabilityPaperworkAlert.RowHeaderId
                    FOR XML AUTO, TYPE, ELEMENTS
                )
            );

            EXEC [Authorization].[SP_SaveTraceabilityPaperwork_Output]
                @SubXml, @Code_Output OUT, @Message_Output OUT, NULL, NULL;

            IF @Code_Output <> 0
            BEGIN
                SELECT 999 AS CodeResult,
                       CONCAT('Error al guardar las alertas de las solicitudes: ', @Message_Output) AS MessageResult;
                RETURN;
            END;
        END;

        /**************************************/

        IF EXISTS
        (
            SELECT 1
            FROM #ManagementMedicalOrder tmmo
            JOIN #ViewListRequests v
                ON tmmo.EntityName = v.EntityName
               AND tmmo.EntityId = v.EntityId
               AND tmmo.ItemCode = v.ServiceCode
            WHERE tmmo.CancellationReasonsId IS NOT NULL
        )
        BEGIN
            SELECT @SubXml = CONVERT
            (
                XML,
                (
                    SELECT *
                    FROM
                    (
                        SELECT
                            tmmo.RowHeaderId,
                            v.TraceabilityPaperworkId AS Id,
                            v.AdmissionNumber,
                            v.Folio,
                            v.ItemCodeOriginal AS ServiceCode,
                            v.Type,
                            v.PatientCode,
                            v.CareCenterCode,
                            v.RequestDate,
                            v.Quantity AS RequestQuantity,
                            v.FunctionalUnitCode,
                            v.EntityId,
                            v.EntityName,
                            v.TraceabilityPaperworkStatus AS Status,
                            tp.CancellationReasonsId,
                            tp.CancellationReasonsObservations,
                            tp.CancellationUserCode,
                            v.AssignUser AS AssignUserCode,
                            v.AuthorizationSourceId,
                            v.IsManual,
                            v.CareGroupId,
                            v.HealthAdministratorId,
                            v.ProfessionalCode,
                            tp.CareCenterTargetCode,
                            tp.FunctionalUnitTargetId,
                            v.ServiceId,
                            v.ContractDescriptionId
                        FROM #ManagementMedicalOrder tmmo
                        JOIN #ViewListRequests v
                            ON tmmo.EntityName = v.EntityName
                           AND tmmo.EntityId = v.EntityId
                           AND tmmo.ItemCode = v.ItemCodeOriginal
                        LEFT JOIN [Authorization].TraceabilityPaperwork tp
                            ON v.TraceabilityPaperworkId = tp.Id
                        WHERE tmmo.CancellationReasonsId IS NOT NULL
                    ) TraceabilityPaperwork
                    JOIN
                    (
                        SELECT
                            tmmo.RowHeaderId,
                            v.TraceabilityPaperworkId,
                            CONCAT(cr.Code, ' - ', cr.Name, ': ', tmmo.CancellationReasonsObservations) AS Comments,
                            1 AS Status,
                            @CodeUser AS CreationUser
                        FROM #ManagementMedicalOrder tmmo
                        JOIN #ViewListRequests v
                            ON tmmo.EntityName = v.EntityName
                           AND tmmo.EntityId = v.EntityId
                           AND tmmo.ItemCode = v.ItemCodeOriginal
                        JOIN [Authorization].CancellationReasons cr
                            ON tmmo.CancellationReasonsId = cr.Id
                        WHERE tmmo.CancellationReasonsId IS NOT NULL
                    ) TraceabilityPaperworkAlert
                        ON TraceabilityPaperwork.RowHeaderId = TraceabilityPaperworkAlert.RowHeaderId
                    FOR XML AUTO, TYPE, ELEMENTS
                )
            );

            EXEC [Authorization].[SP_SaveTraceabilityPaperwork_Output]
                @SubXml, @Code_Output OUT, @Message_Output OUT, NULL, NULL;

            IF @Code_Output <> 0
            BEGIN
                SELECT 999 AS CodeResult,
                       CONCAT('Error al guardar las alertas de las solicitudes: ', @Message_Output) AS MessageResult;
                RETURN;
            END;
        END;

        /************************************************** PROCESO **************************************************/

        INSERT INTO [Authorization].[ManagementMedicalOrder]
        (
            [OperatingUnitId],
            [EntityName],
            [EntityId],
            [Status],
            [CareCenterCode],
            [FunctionalUnitCode],
            [AdmissionNumber],
            [Folio],
            [PatientCode],
            [ProfessionalCode],
            [RequestDate],
            [RequestQuantity],
            [Type],
            [ItemCode],
            [CancellationReasonsId],
            [CancellationReasonsObservations],
            [CreationUser],
            [CreationDate],
            [CancellationUser],
            [CancellationDate]
        )
        SELECT
            tmmo.OperatingUnitId,
            tmmo.EntityName,
            tmmo.EntityId,
            IIF(tmmo.Status = 1, IIF(cr.ApplyWithdrawal = 1, 1, 2), tmmo.Status),
            tmmo.CareCenterCode,
            tmmo.FunctionalUnitCode,
            tmmo.AdmissionNumber,
            tmmo.Folio,
            tmmo.PatientCode,
            tmmo.ProfessionalCode,
            tmmo.RequestDate,
            tmmo.RequestQuantity,
            tmmo.Type,
            tmmo.ItemCode,
            IIF(tmmo.Status = 1, tmmo.CancellationReasonsId, NULL),
            IIF(tmmo.Status = 1, tmmo.CancellationReasonsObservations, NULL),
            @CodeUser,
            [Common].[GETDATE](),
            IIF(tmmo.Status = 1, @CodeUser, NULL),
            IIF(tmmo.Status = 1, [Common].[GETDATE](), NULL)
        FROM #ManagementMedicalOrder tmmo
        LEFT JOIN [Authorization].ManagementMedicalOrder mmo
            ON tmmo.EntityId = mmo.EntityId
           AND tmmo.EntityName = mmo.EntityName
           AND tmmo.ItemCode = mmo.ItemCode
        LEFT JOIN [Authorization].CancellationReasons cr
            ON tmmo.CancellationReasonsId = cr.Id
        WHERE mmo.Id IS NULL;

        SELECT 0 AS CodeResult,
               'Se guardaron correctamente las gestiones de ordenes medicas' AS MessageResult;
    END TRY
    BEGIN CATCH
        SELECT 999 AS CodeResult,
               'SP_SaveManagementMedicalOrder: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult;
    END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda y actualiza la gestión de órdenes médicas en el módulo de autorizaciones. Recibe un listado de órdenes médicas en formato XML (procedimientos, medicamentos, exámenes u otros ítems clínicos solicitados por un profesional para un paciente en un ingreso específico), las deserializa en una tabla temporal optimizada con índices, y las persiste o actualiza en la tabla [Authorization].[ManagementMedicalOrder]. Utiliza una materialización de la vista de solicitudes para evitar múltiples lecturas costosas, validando estados, cancelaciones y consistencia de los ítems antes de confirmar cada registro.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveManagementMedicalOrder';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveManagementMedicalOrder';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste de forma masiva la gestión (creación o cancelación) de órdenes médicas a partir de un XML, validando duplicados, estados y razones de cancelación, y registrando alertas de trazabilidad cuando corresponde.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveManagementMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@ListManagementMedicalOrderXml debe contener nodos /ManagementMedicalOrder con la estructura esperada; @CodeUser debe identificar al usuario que registra la operación (queda en CreationUser y, si aplica, CancellationUser); Authorization.CancellationReasons debe tener registrados los motivos referenciados cuando Status=1; Las vistas Authorization.ViewListRequest y ViewListRequests deben estar disponibles para construir las alertas de trazabilidad', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveManagementMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan solicitudes con Status 1 (cancelación) o 3 (otro estado válido permitido); Cada combinación (EntityName, EntityId, ItemCode) debe ser única en la entrada y no debe existir previamente en ManagementMedicalOrder; Una cancelación (Status=1) requiere obligatoriamente una razón de cancelación válida en CancellationReasons; Si CancellationReasons.ApplyWithdrawal=1, la cancelación se materializa como Status=1; si no, se traduce a Status=2; Los campos CancellationReasonsId, CancellationReasonsObservations, CancellationUser y CancellationDate solo se completan cuando la solicitud entra como Status=1; en otro caso quedan NULL; El INSERT se realiza únicamente para registros que aún no existen en ManagementMedicalOrder (mmo.Id IS NULL); CreationUser y CreationDate se fijan a @CodeUser y Common.GETDATE() respectivamente; Cualquier fallo del SP de trazabilidad (Code_Output<>0) aborta el proceso antes del INSERT principal; Errores no controlados son capturados por TRY/CATCH y devueltos con CodeResult=999', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveManagementMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'gestión de orden médica; paciente; ingreso/admisión; folio; profesional; razón de cancelación; retiro/desistimiento (ApplyWithdrawal); trazabilidad de trámite de autorización; alertas de solicitudes; servicio/producto', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveManagementMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Authorization.ManagementMedicalOrder: Inserta una fila por cada solicitud del XML que no exista previamente (LEFT JOIN mmo.Id IS NULL); si Status=1 calcula el Status final como 1 cuando CancellationReasons.ApplyWithdrawal=1 o 2 en caso contrario, y registra CancellationUser/CancellationDate=Common.GETDATE(); en otro caso conserva el Status original y deja campos de cancelación en NULL; [CALL] Authorization.TraceabilityPaperwork: Cuando hay solicitudes con CancellationReasonsId NOT NULL que cruzan con ViewListRequest o ViewListRequests por ItemCodeOriginal, invoca SP_SaveTraceabilityPaperwork_Output con un XML que incluye un comentario ''Code - Name: Observaciones'' y Status=1 para registrar la alerta de trazabilidad; [RETURN_RESULT] resultset: Devuelve un único resultset con columnas CodeResult y MessageResult: 0 cuando todo se guarda correctamente, 999 ante cualquier validación fallida, error del SP de trazabilidad o excepción capturada', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveManagementMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El XML no produce filas en la tabla temporal de detalles → Retorna CodeResult=999 con mensaje ''No se encontraron solicitudes a procesar.'' y termina; si Existe en Authorization.ManagementMedicalOrder un registro con misma EntityId+EntityName+ItemCode que alguno del XML → Retorna 999 listando las solicitudes ya procesadas y termina sin insertar; si Alguna solicitud trae Status NOT IN (1,3) → Retorna 999 indicando estado inválido y termina; si Status=1 (cancelación) y CancellationReasonsId no existe en Authorization.CancellationReasons → Retorna 999 indicando que falta razón de cancelación y termina; si Tras deduplicar por (EntityName,EntityId,ItemCode) aún quedan duplicados al agrupar por (EntityName,EntityId,PatientCode,AdmissionNumber,Folio,Type,ItemCode) → Retorna 999 listando las solicitudes duplicadas y termina; si Existen filas con CancellationReasonsId NOT NULL que cruzan con ViewListRequest/ViewListRequests por ItemCodeOriginal → Construye XML con datos de trazabilidad y comentario ''Code - Name: Observaciones'' y ejecuta SP_SaveTraceabilityPaperwork_Output; si retorna code<>0 devuelve 999 y termina else Continúa al INSERT principal; si En el INSERT, tmmo.Status = 1 → Si CancellationReasons.ApplyWithdrawal=1 entonces Status insertado=1 (cancelado/retiro), de lo contrario Status=2; además se llenan CancellationReasonsId, CancellationReasonsObservations, CancellationUser=@CodeUser y CancellationDate=Common.GETDATE() else Se conserva tmmo.Status original y los campos de cancelación se insertan en NULL', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveManagementMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Authorization.SP_SaveTraceabilityPaperwork_Output', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveManagementMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.ManagementMedicalOrder; Authorization.CancellationReasons; Authorization.ViewListRequest; Authorization.ViewListRequests; Authorization.TraceabilityPaperwork', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveManagementMedicalOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveManagementMedicalOrder';
-- GO
