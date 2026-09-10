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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario o nota adicional en texto libre (VARCHAR 300) sobre la ficha técnica, indicaciones, contraindicaciones, precauciones o reacciones adversas del medicamento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ficha técnica: clasificador TINYINT que indica 1=Indicaciones, 2=Contraindicaciones, 3=Precauciones, 4=Reacciones Adversas del fármaco según código ATC.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'TechnicalSheetType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de ficha tecnica  1 - Indicaciones  2 - Contraindicaciones  3 - Precauciones  4 - Reacciones Adversas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'TechnicalSheetType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'TechnicalSheetType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del diagnóstico clínico relacionado en tabla Inventory.Diagnostic; referencia cruzada a condición de salud, enfermedad o patología.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'DiagnosticId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'DiagnosticId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'DiagnosticId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del código ATC (Anatomical Therapeutic Chemical) del medicamento o fármaco en tabla Inventory.ATC; clasificación farmacoterapéutica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del atc al cual esta relacionado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la ficha técnica completa; clave primaria que agrupa indicaciones, contraindicaciones, precauciones y reacciones adversas de un medicamento-diagnóstico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la indicación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha técnica de medicamentos del inventario: relaciona cada código ATC (clasificación farmacológica) con un diagnóstico (CIE-10), el tipo de ficha técnica y observaciones adicionales. Permite asociar medicamentos a sus indicaciones clínicas autorizadas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TechnicalSheet';
