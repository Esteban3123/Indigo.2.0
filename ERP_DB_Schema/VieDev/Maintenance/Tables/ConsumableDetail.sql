CREATE TABLE [Maintenance].[ConsumableDetail] (
    [Id]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdConsumable]    INT NOT NULL,
    [IdEquipmentType] INT NOT NULL,
    CONSTRAINT [PK_ConsumableDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsumableDetail_Consumable] FOREIGN KEY ([IdConsumable]) REFERENCES [Maintenance].[Consumable] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ConsumableDetail_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id])
);


GO
ALTER TABLE [Maintenance].[ConsumableDetail] NOCHECK CONSTRAINT [FK_ConsumableDetail_Consumable];


GO
ALTER TABLE [Maintenance].[ConsumableDetail] NOCHECK CONSTRAINT [FK_ConsumableDetail_EquipmentType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de equipo relacionado al consumible', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del consumible relacionado al tipo de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail', @level2type = N'COLUMN', @level2name = N'IdConsumable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la relacion entre consumibles y tipos de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ConsumableDetail', @level2type = N'COLUMN', @level2name = N'Id';

