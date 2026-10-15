CREATE TABLE [Inventory].[ATCConcentrationByDCI] (
    [Id]                         INT             IDENTITY (1, 1) NOT NULL,
    [AtcId]                      INT             NOT NULL,
    [DCIId]                      INT             NOT NULL,
    [Concentration]              DECIMAL (18, 2) NOT NULL,
    [ConcentrationMeasureUnitId] INT             NOT NULL,
    CONSTRAINT [PK__ATCConce__3214EC07661A1BF3] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ATCConcentrationByDCI_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_ATCConcentrationByDCI_DCI] FOREIGN KEY ([DCIId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [FK_ATCConcentrationByDCI_UnitMeasure] FOREIGN KEY ([ConcentrationMeasureUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [IX_ATCConcentrationByDCI] UNIQUE NONCLUSTERED ([AtcId] ASC, [DCIId] ASC)
);


GO
ALTER TABLE [Inventory].[ATCConcentrationByDCI] NOCHECK CONSTRAINT [FK_ATCConcentrationByDCI_ATC];


GO
ALTER TABLE [Inventory].[ATCConcentrationByDCI] NOCHECK CONSTRAINT [FK_ATCConcentrationByDCI_DCI];


GO
ALTER TABLE [Inventory].[ATCConcentrationByDCI] NOCHECK CONSTRAINT [FK_ATCConcentrationByDCI_UnitMeasure];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida de la concentración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'ConcentrationMeasureUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentración del DCI que compone el medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de los DCI que componen el medicamento  Ejemplo:  Para el medicamento ACETAMINOFEN + CODEINA el DCI se encuentra combinado, por lo cual se guardan dos registros:  1. El DCI del ACETAMINOFEN   2. El DCI de la CODEINA ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'DCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del medicamento compuesto por más de un DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCConcentrationByDCI', @level2type = N'COLUMN', @level2name = N'Id';

