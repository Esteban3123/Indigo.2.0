CREATE TABLE [Inventory].[TechnicalSheet] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ATCId]              INT           NOT NULL,
    [DiagnosticId]       INT           NOT NULL,
    [TechnicalSheetType] TINYINT       NOT NULL,
    [Comment]            VARCHAR (300) NULL,
    CONSTRAINT [PK_TechnicalSheet] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TechnicalSheet_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_TechnicalSheet_Diagnostic] FOREIGN KEY ([DiagnosticId]) REFERENCES [Inventory].[Diagnostic] ([Id])
);


GO
ALTER TABLE [Inventory].[TechnicalSheet] NOCHECK CONSTRAINT [FK_TechnicalSheet_ATC];


GO
ALTER TABLE [Inventory].[TechnicalSheet] NOCHECK CONSTRAINT [FK_TechnicalSheet_Diagnostic];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de ficha tecnica  1 - Indicaciones  2 - Contraindicaciones  3 - Precauciones  4 - Reacciones Adversas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'TechnicalSheetType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'DiagnosticId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del atc al cual esta relacionado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la indicación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'Id';

