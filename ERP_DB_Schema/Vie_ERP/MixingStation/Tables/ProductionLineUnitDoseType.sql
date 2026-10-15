CREATE TABLE [MixingStation].[ProductionLineUnitDoseType] (
    [Id]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Id_ProductionLine] INT NOT NULL,
    [Id_UnitDoseType]   INT NOT NULL,
    CONSTRAINT [PK_ProductionLineUnitDoseType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionLineUnitDoseType_ProductionLine] FOREIGN KEY ([Id_ProductionLine]) REFERENCES [MixingStation].[ProductionLine] ([Id]),
    CONSTRAINT [FK_ProductionLineUnitDoseType_UniDoseType] FOREIGN KEY ([Id_UnitDoseType]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);


GO
ALTER TABLE [MixingStation].[ProductionLineUnitDoseType] NOCHECK CONSTRAINT [FK_ProductionLineUnitDoseType_ProductionLine];


GO
ALTER TABLE [MixingStation].[ProductionLineUnitDoseType] NOCHECK CONSTRAINT [FK_ProductionLineUnitDoseType_UniDoseType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Unidad Tipo de dosis', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_UnitDoseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Línea de producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id_ProductionLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineUnitDoseType', @level2type = N'COLUMN', @level2name = N'Id';

