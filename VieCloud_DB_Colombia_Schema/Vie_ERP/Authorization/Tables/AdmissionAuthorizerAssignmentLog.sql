-- =============================================================================
-- Log append-only de asignacion automatica de ingresos hospitalarios a agentes
-- autorizadores (feature Distribucion de Usuarios de Autorizaciones). Ancla en
-- AdmissionNumber (NUMINGRES), NO en Authorization.AuthorizationControl.Id: un
-- mismo ingreso genera N filas de AuthorizationControl (una por SubjectType —
-- Servicio, Medicamento, Insumo, Urgencia, Estancia); lo que se asigna es el
-- ingreso completo, no una fila de control puntual. Ver
-- docs/PBI_BD_DISTRIBUCION_AUTORIZADORES_INGRESOS.md.
--
-- El pool de agentes habilitados y sus novedades vive en
-- Authorization.UsersAssignment / Authorization.UserNovelties (ya existentes,
-- gestionados desde FrmParametrosGestionAutorizacion). AssignedUserCode aqui
-- referencia de forma logica UsersAssignment.UserCode (no FK fisica, misma
-- convencion que UsersAssignment.UserCode hacia INDIGOSECV2).
-- =============================================================================

CREATE TABLE [Authorization].[AdmissionAuthorizerAssignmentLog] (
    [Id]                       INT IDENTITY(1,1) NOT NULL,
    [AdmissionNumber]          CHAR(10) NOT NULL,
    [AssignedUserCode]         VARCHAR(20) NOT NULL,
    [PreviousAssignedUserCode] VARCHAR(20) NULL,
    [Action]                   VARCHAR(20) NOT NULL CONSTRAINT [DF_AdmissionAuthorizerAssignmentLog_Action] DEFAULT (N'Automatica'),
    [IsCurrent]                BIT NOT NULL CONSTRAINT [DF_AdmissionAuthorizerAssignmentLog_IsCurrent] DEFAULT ((1)),
    [AssignmentDate]           DATETIME NOT NULL CONSTRAINT [DF_AdmissionAuthorizerAssignmentLog_AssignmentDate] DEFAULT (GETUTCDATE()),
    CONSTRAINT [PK_AdmissionAuthorizerAssignmentLog] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

-- Garantiza que exista a lo sumo UNA fila vigente por ingreso — es a la vez la
-- guarda de idempotencia de negocio que debe consultar el consumer antes de
-- asignar (si ya existe fila IsCurrent=1 para el AdmissionNumber, no reasigna).
CREATE UNIQUE NONCLUSTERED INDEX [UX_AdmissionAuthorizerAssignmentLog_AdmissionNumber_Current]
    ON [Authorization].[AdmissionAuthorizerAssignmentLog] ([AdmissionNumber] ASC)
    WHERE [IsCurrent] = 1
GO

-- Soporta el COUNT(*) por usuario que usa el algoritmo de "menor carga actual".
CREATE NONCLUSTERED INDEX [IX_AdmissionAuthorizerAssignmentLog_UserCode_Current]
    ON [Authorization].[AdmissionAuthorizerAssignmentLog] ([AssignedUserCode] ASC)
    WHERE [IsCurrent] = 1
GO

-- =============================================================================
-- Distribucion de Usuarios de Autorizaciones (2026-07-21) — propagacion hacia
-- AuthorizationControl.AssignedAuthorizer, caso simetrico al trigger
-- TR_AuthorizationControl_PropagateAdmissionAssignment (AuthorizationControl.sql):
-- cubre "el ingreso se asigna DESPUES de que ya existian filas de AuthorizationControl
-- sin asignar" (por ejemplo, el evento clinical.admission-registered.v1 llega con
-- retraso respecto a la creacion de la primera solicitud de autorizacion del mismo
-- ingreso). Al insertarse una fila nueva aqui (solo pasa con Outcome=Assigned en
-- AdmissionAssignmentWriter — las demas salidas del writer, AlreadyAssigned/
-- AutomaticAssignmentDisabled/NoEligibleUsers/DuplicateSkipped, no insertan nada,
-- asi que este trigger no dispara para esos casos), se hace backfill de las filas de
-- AuthorizationControl de ese AdmissionNumber que SIGAN sin asignar (AssignedAuthorizer
-- IS NULL) — nunca pisa una asignacion manual ya existente.
-- =============================================================================
CREATE TRIGGER [Authorization].[TR_AdmissionAuthorizerAssignmentLog_BackfillAuthorizationControl]
    ON [Authorization].[AdmissionAuthorizerAssignmentLog]
    AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE ac
            SET ac.[AssignedAuthorizer] = i.[AssignedUserCode]
        FROM [Authorization].[AuthorizationControl] ac
        INNER JOIN inserted i ON i.[AdmissionNumber] = ac.[AdmissionNumber]
        WHERE i.[IsCurrent] = 1
          AND ac.[AssignedAuthorizer] IS NULL;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- =============================================================================
-- Extended properties
-- =============================================================================

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asignación; clave primaria IDENTITY; INT.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso hospitalario (dbo.ADINGRESO.NUMINGRES); referencia lógica, sin FK física (mismo criterio de aislamiento que Authorization.AuthorizationControl.AdmissionNumber). Ancla de la asignación: todo el ingreso, no una fila de control puntual; CHAR(10) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del agente autorizador asignado; referencia lógica a Authorization.UsersAssignment.UserCode (no FK física); VARCHAR(20) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'AssignedUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'AssignedUserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario previamente asignado, solo con valor cuando la fila representa una reasignación (Action=Manual); VARCHAR(20) NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'PreviousAssignedUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'PreviousAssignedUserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la asignación: ''Automatica'' (consumer de distribución, algoritmo de menor carga) o ''Manual'' (reasignación desde el dashboard, FrmAssignUser). VARCHAR(20) NOT NULL DEFAULT ''Automatica''.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si esta fila es la asignación vigente para el AdmissionNumber (solo puede haber una vigente por ingreso, garantizado por UX_AdmissionAuthorizerAssignmentLog_AdmissionNumber_Current). BIT NOT NULL DEFAULT 1.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'IsCurrent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'IsCurrent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que se registró la asignación; DATETIME NOT NULL DEFAULT GETUTCDATE().', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'AssignmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'AssignmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Log append-only de asignación automática (y, a futuro, manual) de ingresos hospitalarios a agentes autorizadores. Ancla en AdmissionNumber, no en AuthorizationControl.Id. Alimentado por Indigo.AzClinicalAuthorizationControl (consumer AuthorizationAdmissionDistributionConsumer) al procesar clinical-admission-facts. Fuente de verdad de la asignación; se propaga hacia Authorization.AuthorizationControl.AssignedAuthorizer (columna ya leída directo por las 6 vistas Authorization.ViewDashboardIntrahospital*) vía 2 triggers simétricos: TR_AdmissionAuthorizerAssignmentLog_BackfillAuthorizationControl (este archivo, dispara al insertar aquí) y TR_AuthorizationControl_PropagateAdmissionAssignment (AuthorizationControl.sql, dispara al insertar ahí) — ninguno pisa una asignación manual (solo actúan si AssignedAuthorizer es NULL). Sin cambios a las vistas ni coordinación con cliente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog';
GO
