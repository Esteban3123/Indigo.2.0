CREATE TABLE [Authorization].[TraceabilityPaperworkAnnexes] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TraceabilityPaperworkId] INT           NOT NULL,
    [HealthAdministratorId]   INT           NOT NULL,
    [TypeRequestServices]     TINYINT       NOT NULL,
    [PriorityAttention]       TINYINT       NOT NULL,
    [Justification]           VARCHAR (MAX) NULL,
    [Folio]                   VARCHAR (20)  NULL,
    [Consecutive]             DECIMAL (18)  NOT NULL,
    [CreationDate]            DATETIME      NOT NULL,
    [CreationUser]            VARCHAR (20)  NOT NULL,
    [DiagnosticCode]          VARCHAR (20)  NULL,
    CONSTRAINT [PK_TraceabilityPaperworkAnnexes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TraceabilityPaperworkAnnexes_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_TraceabilityPaperworkAnnexes_TraceabilityPaperwork] FOREIGN KEY ([TraceabilityPaperworkId]) REFERENCES [Authorization].[TraceabilityPaperwork] ([Id])
);


GO
ALTER TABLE [Authorization].[TraceabilityPaperworkAnnexes] NOCHECK CONSTRAINT [FK_TraceabilityPaperworkAnnexes_HealthAdministrator];


GO
ALTER TABLE [Authorization].[TraceabilityPaperworkAnnexes] NOCHECK CONSTRAINT [FK_TraceabilityPaperworkAnnexes_TraceabilityPaperwork];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de diagnostico, se asigna cuando no hay folio para la generación de los anexos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuari quien creó el anexo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del anexo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del reporte de anexo, este consecutivo se registra con el código 11 en la tabla Common.Consecutive', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. del folio del ingreso al cual se saco la justificación clinica, puede ir vacío cuando el item no tiene un ingreso y debe seleccionar un diagnostico', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Folio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificación clínica', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Justification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad de la atención:  1 - Prioritaria  2 - No Prioritaria', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'PriorityAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de servicios solicitados:  1 - Posterior a la atención inicial  2 - Servicios electivos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'TypeRequestServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del trámite', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Id';

