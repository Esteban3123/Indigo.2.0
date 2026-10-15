CREATE TABLE [Glosas].[GlosaMovementGlosaConciliation] (
    [Id]                              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GlosaMovementGlosaId]            INT             NOT NULL,
    [ConciliationCId]                 INT             NOT NULL,
    [ResponseHierarchyConciliationId] INT             NULL,
    [ValueAcceptedIPSconciliation]    DECIMAL (18, 2) NOT NULL,
    [ValueAcceptedEAPBconciliation]   DECIMAL (18, 2) NOT NULL,
    [RationaleConciliation]           VARCHAR (MAX)   NULL,
    [RationaleDateConciliation]       DATETIME        NOT NULL,
    [State]                           TINYINT         NOT NULL,
    CONSTRAINT [PK_GlosaMovementGlosaConciliation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosaMovementGlosaConciliation_ConciliationC] FOREIGN KEY ([ConciliationCId]) REFERENCES [Glosas].[ConciliationC] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosaConciliation_GlosaMovementGlosa] FOREIGN KEY ([GlosaMovementGlosaId]) REFERENCES [Glosas].[GlosaMovementGlosa] ([Id]),
    CONSTRAINT [FK_GlosaMovementGlosaConciliation_GlosasResponseHierarchyConciliation] FOREIGN KEY ([ResponseHierarchyConciliationId]) REFERENCES [Glosas].[GlosasResponseHierarchy] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosaMovementGlosaConciliation] NOCHECK CONSTRAINT [FK_GlosaMovementGlosaConciliation_ConciliationC];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosaConciliation] NOCHECK CONSTRAINT [FK_GlosaMovementGlosaConciliation_GlosaMovementGlosa];


GO
ALTER TABLE [Glosas].[GlosaMovementGlosaConciliation] NOCHECK CONSTRAINT [FK_GlosaMovementGlosaConciliation_GlosasResponseHierarchyConciliation];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_GlosaMovementGlosaConciliation]
    ON [Glosas].[GlosaMovementGlosaConciliation]([GlosaMovementGlosaId] ASC, [ConciliationCId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado antes de iniciar la conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'RationaleDateConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'razon de conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'RationaleConciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor aceptado EAPB en conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ValueAcceptedEAPBconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor aceptado IPS en conciliacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ValueAcceptedIPSconciliation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id relacion del Concepto de Aceptación Jerarquíca para Conciliación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ResponseHierarchyConciliationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera de conciliación', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'ConciliationCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Relacion del Movimiento de Glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'GlosaMovementGlosaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de movimientos de glosas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosaMovementGlosaConciliation', @level2type = N'COLUMN', @level2name = N'Id';

