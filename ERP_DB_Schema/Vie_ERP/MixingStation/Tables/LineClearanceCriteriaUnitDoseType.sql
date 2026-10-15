CREATE TABLE [MixingStation].[LineClearanceCriteriaUnitDoseType] (
    [Id]                      INT IDENTITY (1, 1) NOT NULL,
    [LineClearanceCriteriaId] INT NOT NULL,
    [UnitDoseTypeId]          INT NOT NULL,
    CONSTRAINT [PK_LineClearanceCriteriaUnitDoseType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LineClearanceCriteriaUnitDoseType_LineClearanceCriteria] FOREIGN KEY ([LineClearanceCriteriaId]) REFERENCES [MixingStation].[LineClearanceCriteria] ([Id]),
    CONSTRAINT [FK_LineClearanceCriteriaUnitDoseType_UniDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);


GO
ALTER TABLE [MixingStation].[LineClearanceCriteriaUnitDoseType] NOCHECK CONSTRAINT [FK_LineClearanceCriteriaUnitDoseType_LineClearanceCriteria];


GO
ALTER TABLE [MixingStation].[LineClearanceCriteriaUnitDoseType] NOCHECK CONSTRAINT [FK_LineClearanceCriteriaUnitDoseType_UniDoseType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Unidad Tipo de dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id criterio de despeje de linea', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType', @level2type = N'COLUMN', @level2name = N'LineClearanceCriteriaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'LineClearanceCriteriaUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';

