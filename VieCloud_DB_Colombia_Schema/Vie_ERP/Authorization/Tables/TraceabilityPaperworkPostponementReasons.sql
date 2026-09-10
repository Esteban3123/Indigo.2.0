CREATE TABLE [Authorization].[TraceabilityPaperworkPostponementReasons] (
    [Id]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TraceabilityPaperworkId]  INT           NOT NULL,
    [PostponementReasonsId]    INT           NOT NULL,
    [PostponementDate]         DATETIME      NOT NULL,
    [PostponementObservations] VARCHAR (500) NULL,
    [StatusPrevious]           TINYINT       NOT NULL,
    [Status]                   BIT           NOT NULL,
    [CreationUser]             VARCHAR (20)  NOT NULL,
    [CreationDate]             DATETIME      NOT NULL,
    [ModificationUser]         VARCHAR (20)  NULL,
    [ModificationDate]         DATETIME      NULL,
    CONSTRAINT [PK_TraceabilityPaperworkPostponementReasons] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TraceabilityPaperworkPostponementReasons_PostponementReasons] FOREIGN KEY ([PostponementReasonsId]) REFERENCES [Authorization].[PostponementReasons] ([Id]),
    CONSTRAINT [FK_TraceabilityPaperworkPostponementReasons_TraceabilityPaperwork] FOREIGN KEY ([TraceabilityPaperworkId]) REFERENCES [Authorization].[TraceabilityPaperwork] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de postergación; DATETIME, auditoría de cambios en autorización/trámite.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación; VARCHAR(20), identificación PII del profesional/administrador que editó el registro.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de postergación; DATETIME, marca de auditoría inicial en autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro; VARCHAR(20), identificación PII del profesional/administrador que registró la postergación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la postergación (activo/inactivo); BIT (1=activo, 0=inactivo), controla vigencia del aplazamiento de trámite/autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la postergación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado anterior del registro de cabecera/trámite; TINYINT, auditoria de cambio de estado previo en autorización/paperwork.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'StatusPrevious';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado anterior del registro de la cabecera', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'StatusPrevious';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'StatusPrevious';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas, comentarios o detalles adicionales de la razón de postergación; VARCHAR(500), observaciones del aplazamiento de autorización/trámite.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'PostponementObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de la postergación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'PostponementObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'PostponementObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se registró o efectuó la postergación/aplazamiento; DATETIME, marca temporal del diferimiento en autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'PostponementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la postergación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'PostponementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'PostponementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la razón/motivo de postergación; INT, FK a Authorization.PostponementReasons, vincula causa del aplazamiento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'PostponementReasonsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del motivo de postergación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'PostponementReasonsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'PostponementReasonsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de trámite/autorización postergada; INT, FK a Authorization.TraceabilityPaperwork, referencia al expediente/solicitud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de postergación; INT IDENTITY, clave primaria del histórico de aplazamientos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'Id';


GO
CREATE NONCLUSTERED INDEX [IDX_TraceabilityPaperworkPostponementReasons_TraceabilityPaperworkId]
    ON [Authorization].[TraceabilityPaperworkPostponementReasons]([TraceabilityPaperworkId] ASC, [Id] ASC)
    INCLUDE([PostponementReasonsId], [PostponementDate], [CreationDate], [CreationUser]);


GO
CREATE NONCLUSTERED INDEX [IDX_TPPR_TraceabilityPaperworkId]
    ON [Authorization].[TraceabilityPaperworkPostponementReasons]([TraceabilityPaperworkId] ASC)
    INCLUDE([Id], [PostponementReasonsId], [PostponementDate], [CreationDate], [CreationUser]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los motivos por los cuales se pospuso o aplazó el trámite de un expediente o papelería en el proceso de autorización. Permite hacer seguimiento a los aplazamientos de gestión documental, incluyendo la razón, la fecha y las observaciones del posponimiento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons';
