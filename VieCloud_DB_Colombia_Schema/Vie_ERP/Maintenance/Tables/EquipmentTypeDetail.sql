CREATE TABLE [Maintenance].[EquipmentTypeDetail] (
    [Id]              TINYINT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentType] TINYINT NOT NULL,
    [IdInventoryType] TINYINT NOT NULL,
    CONSTRAINT [PK_EquipmentTypeDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentTypeDetail_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [Maintenance].[EquipmentType] ([Id]),
    CONSTRAINT [FK_EquipmentTypeDetail_InventoryType] FOREIGN KEY ([IdInventoryType]) REFERENCES [Maintenance].[InventoryType] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de inventario (TINYINT, FK a InventoryType) asociado al tipo de equipo; clasifica la naturaleza del recurso inventariable (repuestos, consumibles, activos fijos, equipos médicos)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail', @level2type = N'COLUMN', @level2name = N'IdInventoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de inventario relacionado al tipo de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail', @level2type = N'COLUMN', @level2name = N'IdInventoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail', @level2type = N'COLUMN', @level2name = N'IdInventoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipo (TINYINT, FK a EquipmentType) vinculado al tipo de inventario; define la categoría de equipamiento (diagnóstico, quirúrgico, laboratorio, infraestructura)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de equipo relacionado del tipo de inventario.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (TINYINT, PK, IDENTITY 1,1) de la relación entre tipo de equipo y tipo de inventario; clave primaria de la tabla de detalle', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la relación entre tipos de equipos médicos o técnicos y los tipos de inventario asociados. Permite clasificar qué categorías de inventario corresponden a cada tipo de equipo en el módulo de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentTypeDetail';
