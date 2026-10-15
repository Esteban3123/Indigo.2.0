CREATE TABLE [Maintenance].[AccesoryDetail] (
    [Id]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccessory]     INT NOT NULL,
    [IdEquipmentType] INT NOT NULL,
    CONSTRAINT [PK_AccesoryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccesoryDetail_Accessory] FOREIGN KEY ([IdAccessory]) REFERENCES [Maintenance].[Accessory] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AccesoryDetail_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id])
);


GO
ALTER TABLE [Maintenance].[AccesoryDetail] NOCHECK CONSTRAINT [FK_AccesoryDetail_Accessory];


GO
ALTER TABLE [Maintenance].[AccesoryDetail] NOCHECK CONSTRAINT [FK_AccesoryDetail_EquipmentType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de Accesorio Relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Accesorio relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail', @level2type = N'COLUMN', @level2name = N'IdAccessory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de relacion de accesorios y equipos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail', @level2type = N'COLUMN', @level2name = N'Id';

