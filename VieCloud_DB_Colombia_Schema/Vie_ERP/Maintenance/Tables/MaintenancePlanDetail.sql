CREATE TABLE [Maintenance].[MaintenancePlanDetail] (
    [Id]                                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenancePlanId]                         INT NOT NULL,
    [EquipmentTypePartsAccesoriesConsumiblesId] INT NOT NULL,
    CONSTRAINT [PK_MaintenancePlanDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenancePlanDetail_MaintenancePlan] FOREIGN KEY ([MaintenancePlanId]) REFERENCES [Maintenance].[MaintenancePlan] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipo, pieza, accesorio o consumible que forma parte del detalle de mantenimiento. Referencia a catálogo de equipamiento biomédico y sus componentes.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail', @level2type = N'COLUMN', @level2name = N'EquipmentTypePartsAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Tipo de equipo Piezas Accesorios Consumibles', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail', @level2type = N'COLUMN', @level2name = N'EquipmentTypePartsAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail', @level2type = N'COLUMN', @level2name = N'EquipmentTypePartsAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del plan de mantenimiento padre. Clave foránea que vincula este detalle con el plan de mantenimiento específico al cual pertenecen las actividades de servicio técnico del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail', @level2type = N'COLUMN', @level2name = N'MaintenancePlanId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el plan de mantenimiento que a su vez tiene el tipo de equipo que se le esta haciendo la actividad', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail', @level2type = N'COLUMN', @level2name = N'MaintenancePlanId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail', @level2type = N'COLUMN', @level2name = N'MaintenancePlanId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la fila de detalle del plan de mantenimiento. Clave primaria agrupada en cluster de la tabla MaintenancePlanDetail.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del plan de mantenimiento: registra la relación entre un plan de mantenimiento y los tipos de equipos, partes, accesorios o consumibles incluidos en dicho plan.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanDetail';
