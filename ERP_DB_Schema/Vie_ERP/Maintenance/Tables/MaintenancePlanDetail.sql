CREATE TABLE [Maintenance].[MaintenancePlanDetail] (
    [Id]                                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenancePlanId]                         INT NOT NULL,
    [EquipmentTypePartsAccesoriesConsumiblesId] INT NOT NULL,
    CONSTRAINT [PK_MaintenancePlanDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenancePlanDetail_MaintenancePlan] FOREIGN KEY ([MaintenancePlanId]) REFERENCES [Maintenance].[MaintenancePlan] ([Id])
);


GO
ALTER TABLE [Maintenance].[MaintenancePlanDetail] NOCHECK CONSTRAINT [FK_MaintenancePlanDetail_MaintenancePlan];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Tipo de equipo Piezas Accesorios Consumibles', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail', @level2type = N'COLUMN', @level2name = N'EquipmentTypePartsAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el plan de mantenimiento que a su vez tiene el tipo de equipo que se le esta haciendo la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail', @level2type = N'COLUMN', @level2name = N'MaintenancePlanId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail', @level2type = N'COLUMN', @level2name = N'Id';

