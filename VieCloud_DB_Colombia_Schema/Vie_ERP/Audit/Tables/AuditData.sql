CREATE TABLE [Audit].[AuditData] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [EntityName]   VARCHAR (250) NOT NULL,
    [EntityId]     INT           NOT NULL,
    [JSONData]     VARCHAR (MAX) NULL,
    [CreationUser] VARCHAR (20)  NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_AuditData] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de auditoría (DATETIME). Marca temporal del evento auditado en el sistema.', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que generó el cambio auditado (VARCHAR 20). Identificación del profesional o sistema que realizó la acción.', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Serialización JSON con los cambios realizados al registro (VARCHAR MAX). Contiene estructura de datos antes/después o diferencias del evento auditado.', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'JSONData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Texto en formato Json con los cambios hechos al registro', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'JSONData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'JSONData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro auditado en su tabla origen (INT). Clave que vincula la auditoría al registro específico (paciente, factura, orden, etc.).', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro al cual se guarda la auditoria', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la tabla origen donde se realizó el cambio auditado (VARCHAR 250). Ej: Patients, Admissions, Billing, Procedures, Providers.', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la tabla a la cual se le hace auditoria', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de auditoría (INT IDENTITY). Clave primaria secuencial del evento de cambio registrado.', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría que guarda el historial de cambios realizados sobre cualquier entidad del sistema (pacientes, ingresos, órdenes, etc.), incluyendo los datos modificados en formato JSON, el usuario que hizo el cambio y la fecha exacta de la operación.', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Audit', @level1type = N'TABLE', @level1name = N'AuditData';
