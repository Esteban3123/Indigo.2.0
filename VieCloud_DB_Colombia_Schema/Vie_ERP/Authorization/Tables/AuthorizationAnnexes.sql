-- =============================================================================
-- Anexos de autorización ligados a un AuthorizationControl.
-- Equivalente intrahospitalario de TraceabilityPaperworkAnnexes (ambulatorio).
-- FK lógica a AuthorizationControl(Id) — mismo dominio Authorization.
-- =============================================================================

CREATE TABLE [Authorization].[AuthorizationAnnexes] (
    [Id]                      INT IDENTITY(1,1) NOT NULL,
    [AuthorizationControlId]  INT NOT NULL,
    [HealthAdministratorId]   INT NULL,
    [TypeRequestServices]     TINYINT NOT NULL,       -- 1=Servicio, 2=Producto
    [PriorityAttention]       TINYINT NULL,
    [Justification]           VARCHAR(MAX) NULL,
    [Folio]                   VARCHAR(20) NULL,
    [Consecutive]             DECIMAL(18, 0) NOT NULL,
    [DiagnosticCode]          VARCHAR(20) NULL,
    [AnnexBatchId]            UNIQUEIDENTIFIER NULL,  -- compartido entre anexos del mismo lote masivo
    [CreationUser]            VARCHAR(20) NOT NULL,
    [CreationDate]            DATETIME NOT NULL CONSTRAINT [DF_AuthorizationAnnexes_CreationDate] DEFAULT (GETUTCDATE()),
    CONSTRAINT [PK_AuthorizationAnnexes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationAnnexes_AuthorizationControl]
        FOREIGN KEY ([AuthorizationControlId])
        REFERENCES [Authorization].[AuthorizationControl] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_AuthorizationAnnexes_ControlId]
    ON [Authorization].[AuthorizationAnnexes] ([AuthorizationControlId] ASC)
    INCLUDE ([CreationDate])
GO

-- =============================================================================
-- Extended properties
-- =============================================================================

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de anexo de autorización intrahospitalaria; clave primaria IDENTITY; INT', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del control de autorización intrahospitalaria al que pertenece este anexo; FK a Authorization.AuthorizationControl; INT NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'AuthorizationControlId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del control de autorización al que pertenece el anexo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'AuthorizationControlId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'AuthorizationControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad administradora de salud (EPS/aseguradora) ante la cual se tramita el anexo; FK lógica a Contract.HealthAdministrator; INT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad administradora de salud', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de solicitud del anexo intrahospitalario: 1=Servicio (CUPS), 2=Producto (medicamento/insumo); TINYINT NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'TypeRequestServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de solicitud: 1=Servicio, 2=Producto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'TypeRequestServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'TypeRequestServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de prioridad de atención: 1=Prioritaria, 2=No Prioritaria; TINYINT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'PriorityAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad de la atención: 1=Prioritaria, 2=No Prioritaria', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'PriorityAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'PriorityAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica del anexo; argumentación médica presentada ante la administradora para sustentar la autorización del servicio o producto; VARCHAR(MAX) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica del anexo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'Justification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio del ingreso del cual se extrajo la justificación clínica; puede estar vacío si el diagnóstico se selecciona manualmente; VARCHAR(20) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'Folio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de folio del ingreso del cual se tomó la justificación clínica; vacío cuando se selecciona diagnóstico manual', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'Folio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'Folio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo del anexo de autorización; se genera con código 11 en Common.Consecutive; DECIMAL(18,0) NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del anexo, generado con código 11 en Common.Consecutive', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 asociado al anexo; se asigna cuando no hay folio de ingreso disponible y el diagnóstico se selecciona manualmente; VARCHAR(20) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código diagnóstico CIE-10, asignado cuando no hay folio y se selecciona diagnóstico manual', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de lote de generación masiva de anexos; UNIQUEIDENTIFIER NULL; compartido entre todos los anexos creados en la misma operación masiva para permitir su agrupación y anulación conjunta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'AnnexBatchId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de lote compartido entre anexos generados en la misma operación masiva', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'AnnexBatchId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'AnnexBatchId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que creó el registro del anexo; VARCHAR(20) NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creó el anexo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de creación del anexo; DATETIME NOT NULL DEFAULT GETUTCDATE()', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora UTC de creación del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anexos de autorización intrahospitalaria ligados a un AuthorizationControl. Equivalente intrahospitalario de TraceabilityPaperworkAnnexes (ambulatorio). Registra cada documento de solicitud presentado ante la administradora de salud, incluyendo el tipo de solicitud (servicio o producto), la prioridad, el folio de ingreso o diagnóstico de respaldo, la justificación clínica y el lote de generación masiva.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationAnnexes';
GO
