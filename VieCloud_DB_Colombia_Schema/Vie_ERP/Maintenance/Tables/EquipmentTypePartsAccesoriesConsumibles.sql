CREATE TABLE [Maintenance].[EquipmentTypePartsAccesoriesConsumibles] (
    [Id]                           INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EquipmentTypeId]              TINYINT NOT NULL,
    [PartsAccesoriesConsumiblesId] INT     NOT NULL,
    CONSTRAINT [PK_EquipmentTypePartsAccesoriesConsumibles__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypePartsAccesoriesConsumibles_EquipmentTypePartsAccesoriesConsumibles] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [Maintenance].[EquipmentType] ([Id]),
    CONSTRAINT [FK_EquipmentTypePartsAccesoriesConsumibles_PartsAccesoriesConsumables] FOREIGN KEY ([PartsAccesoriesConsumiblesId]) REFERENCES [Maintenance].[PartsAccesoriesConsumables] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la pieza, accesorio o consumible asociado al tipo de equipo. Referencia a Maintenance.PartsAccesoriesConsumables (FK). Tipo INT, vincula inventario de repuestos y materiales fungibles utilizados en mantenimiento preventivo y correctivo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de las Piezas Accesorios Consumibles', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'PartsAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipo (ej: respirador, monitor, anestesía, imagenología). Referencia a Maintenance.EquipmentType (FK). Tipo TINYINT, establece qué piezas, accesorios y consumibles son compatibles con cada categoría de equipamiento biomédico.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY INT). Clave primaria de la asociación entre tipos de equipo y sus piezas/accesorios/consumibles. Permite rastrear configuraciones de repuestos por equipamiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los tipos de equipos médicos o de mantenimiento con sus partes, accesorios y consumibles asociados. Permite saber qué repuestos, insumos o componentes corresponden a cada tipo de equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypePartsAccesoriesConsumibles';
