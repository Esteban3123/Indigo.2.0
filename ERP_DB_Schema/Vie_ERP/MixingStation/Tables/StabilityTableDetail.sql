CREATE TABLE [MixingStation].[StabilityTableDetail] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [StabilityTableId]     INT           NOT NULL,
    [ATCId]                INT           NOT NULL,
    [ProductId]            INT           NOT NULL,
    [UnitDoseTypeId]       INT           NOT NULL,
    [AllowableDoses]       INT           NOT NULL,
    [Observations]         VARCHAR (300) NULL,
    [HourStabilityProduct] INT           NULL,
    CONSTRAINT [PK_StabilityTableDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StabilityTableDetail_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_StabilityTableDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_StabilityTableDetail_StabilityTable] FOREIGN KEY ([StabilityTableId]) REFERENCES [MixingStation].[StabilityTable] ([Id]),
    CONSTRAINT [FK_StabilityTableDetail_UnitDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);


GO
ALTER TABLE [MixingStation].[StabilityTableDetail] NOCHECK CONSTRAINT [FK_StabilityTableDetail_ATC];


GO
ALTER TABLE [MixingStation].[StabilityTableDetail] NOCHECK CONSTRAINT [FK_StabilityTableDetail_InventoryProduct];


GO
ALTER TABLE [MixingStation].[StabilityTableDetail] NOCHECK CONSTRAINT [FK_StabilityTableDetail_StabilityTable];


GO
ALTER TABLE [MixingStation].[StabilityTableDetail] NOCHECK CONSTRAINT [FK_StabilityTableDetail_UnitDoseType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis permisibles', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'AllowableDoses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto que tenga asociado el medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'StabilityTableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetail', @level2type = N'COLUMN', @level2name = N'Id';

