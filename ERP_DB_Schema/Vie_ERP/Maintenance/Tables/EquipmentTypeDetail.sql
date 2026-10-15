CREATE TABLE [Maintenance].[EquipmentTypeDetail] (
    [Id]              TINYINT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentType] TINYINT NOT NULL,
    [IdInventoryType] TINYINT NOT NULL,
    CONSTRAINT [PK_EquipmentTypeDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypeDetail_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [Maintenance].[EquipmentType] ([Id]),
    CONSTRAINT [FK_EquipmentTypeDetail_InventoryType] FOREIGN KEY ([IdInventoryType]) REFERENCES [Maintenance].[InventoryType] ([Id])
);


GO
ALTER TABLE [Maintenance].[EquipmentTypeDetail] NOCHECK CONSTRAINT [FK_EquipmentTypeDetail_EquipmentType];


GO
ALTER TABLE [Maintenance].[EquipmentTypeDetail] NOCHECK CONSTRAINT [FK_EquipmentTypeDetail_InventoryType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de inventario relacionado al tipo de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail', @level2type = N'COLUMN', @level2name = N'IdInventoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de equipo relacionado del tipo de inventario.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail', @level2type = N'COLUMN', @level2name = N'Id';

