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

