CREATE TABLE [Maintenance].[EquipmentTypePartsAccesoriesConsumibles] (
    [Id]                           INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EquipmentTypeId]              TINYINT NOT NULL,
    [PartsAccesoriesConsumiblesId] INT     NOT NULL,
    CONSTRAINT [PK_EquipmentTypePartsAccesoriesConsumibles__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypePartsAccesoriesConsumibles_EquipmentTypePartsAccesoriesConsumibles] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [Maintenance].[EquipmentType] ([Id]),
    CONSTRAINT [FK_EquipmentTypePartsAccesoriesConsumibles_PartsAccesoriesConsumables] FOREIGN KEY ([PartsAccesoriesConsumiblesId]) REFERENCES [Maintenance].[PartsAccesoriesConsumables] ([Id])
);


GO
ALTER TABLE [Maintenance].[EquipmentTypePartsAccesoriesConsumibles] NOCHECK CONSTRAINT [FK_EquipmentTypePartsAccesoriesConsumibles_EquipmentTypePartsAccesoriesConsumibles];


GO
ALTER TABLE [Maintenance].[EquipmentTypePartsAccesoriesConsumibles] NOCHECK CONSTRAINT [FK_EquipmentTypePartsAccesoriesConsumibles_PartsAccesoriesConsumables];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de las Piezas Accesorios Consumibles', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';

