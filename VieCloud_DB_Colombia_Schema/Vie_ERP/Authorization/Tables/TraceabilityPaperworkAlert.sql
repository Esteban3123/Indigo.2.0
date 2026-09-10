CREATE TABLE [Authorization].[TraceabilityPaperworkAlert] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TraceabilityPaperworkId] INT           NOT NULL,
    [Comments]                VARCHAR (MAX) NOT NULL,
    [Status]                  BIT           NOT NULL,
    [CreationUser]            VARCHAR (20)  NOT NULL,
    [CreationDate]            DATETIME      NOT NULL,
    [ModificationUser]        VARCHAR (20)  NULL,
    [ModificationDate]        DATETIME      NULL,
    CONSTRAINT [PK_TraceabilityPaperworkAlert] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TraceabilityPaperworkAlert_TraceabilityPaperwork] FOREIGN KEY ([TraceabilityPaperworkId]) REFERENCES [Authorization].[TraceabilityPaperwork] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación de la alerta de trazabilidad; DATETIME; auditoría de cambios en alertas de trámites', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación de la alerta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación de la alerta; PII_Auditoría; identificación del responsable del cambio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quien modificó la alerta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación de la alerta de trazabilidad; DATETIME; marca temporal de generación del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación de la alerta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó la alerta de trazabilidad; PII_Auditoría; identificación del responsable de la creación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario quien creó la alerta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Activo, 0=Suspendido o Inactivo; BIT; indica vigencia de la alerta en el trámite de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro: Activo o Suspendido', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas o comentarios detallados sobre la alerta de trazabilidad; VARCHAR MAX; información libre para justificación o seguimiento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la cabecera del trámite de trazabilidad; FK a Authorization.TraceabilityPaperwork; vincula la alerta al expediente de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del trámite', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de alerta de trazabilidad; PK; clave primaria del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alertas o novedades registradas sobre trámites de trazabilidad en procesos de autorización. Permite hacer seguimiento de observaciones, comentarios y el estado de cada alerta generada durante la gestión documental de autorizaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAlert';
