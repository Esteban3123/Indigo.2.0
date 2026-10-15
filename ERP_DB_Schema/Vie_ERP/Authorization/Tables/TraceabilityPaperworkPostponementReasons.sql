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
ALTER TABLE [Authorization].[TraceabilityPaperworkPostponementReasons] NOCHECK CONSTRAINT [FK_TraceabilityPaperworkPostponementReasons_PostponementReasons];


GO
ALTER TABLE [Authorization].[TraceabilityPaperworkPostponementReasons] NOCHECK CONSTRAINT [FK_TraceabilityPaperworkPostponementReasons_TraceabilityPaperwork];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la postergación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado anterior del registro de la cabecera', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'StatusPrevious';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de la postergación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'PostponementObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la postergación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'PostponementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del motivo de postergación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'PostponementReasonsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkPostponementReasons', @level2type = N'COLUMN', @level2name = N'Id';

