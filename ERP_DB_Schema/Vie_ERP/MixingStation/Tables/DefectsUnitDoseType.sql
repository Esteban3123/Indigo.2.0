CREATE TABLE [MixingStation].[DefectsUnitDoseType] (
    [Id]                           INT IDENTITY (1, 1) NOT NULL,
    [Id_DefectsClassificationItem] INT NOT NULL,
    [Id_UnitDoseType]              INT NOT NULL,
    CONSTRAINT [PK_DefectsUnitDoseType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DefectsUnitDoseType_DefectClassificationItem] FOREIGN KEY ([Id_DefectsClassificationItem]) REFERENCES [MixingStation].[DefectClassificationItem] ([Id]),
    CONSTRAINT [FK_DefectsUnitDoseType_UniDoseType] FOREIGN KEY ([Id_UnitDoseType]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);


GO
ALTER TABLE [MixingStation].[DefectsUnitDoseType] NOCHECK CONSTRAINT [FK_DefectsUnitDoseType_DefectClassificationItem];


GO
ALTER TABLE [MixingStation].[DefectsUnitDoseType] NOCHECK CONSTRAINT [FK_DefectsUnitDoseType_UniDoseType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Unidad Tipo de dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_UnitDoseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Defecto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_DefectsClassificationItem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DefectsUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';

