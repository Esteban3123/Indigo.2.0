CREATE TABLE [Maintenance].[AccesoryDetail] (
    [Id]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccessory]     INT NOT NULL,
    [IdEquipmentType] INT NOT NULL,
    CONSTRAINT [PK_AccesoryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccesoryDetail_Accessory] FOREIGN KEY ([IdAccessory]) REFERENCES [Maintenance].[Accessory] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AccesoryDetail_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipo o activo fijo relacionado con el accesorio. Clave foránea que vincula a FixedAsset.FixedAssetItemType. Define qué categoría de equipo (ej: electromedicina, mobiliario, informática) es compatible con este accesorio.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de Accesorio Relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del accesorio vinculado. Clave foránea que referencia Maintenance.Accessory. Establece relación de pertenencia del accesorio con sus tipos de equipos asociados. Se elimina en cascada si el accesorio se borra.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail', @level2type = N'COLUMN', @level2name = N'IdAccessory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Accesorio relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail', @level2type = N'COLUMN', @level2name = N'IdAccessory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail', @level2type = N'COLUMN', @level2name = N'IdAccessory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) de la tabla de relación. Clave primaria que identifica unívocamente cada asociación accesorio-tipo de equipo en el catálogo de mantenimiento y control de activos.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de relacion de accesorios y equipos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de accesorios asociados a tipos de equipos de mantenimiento. Registra qué accesorios corresponden a cada tipo de equipo, permitiendo gestionar la relación entre accesorios y equipos en el módulo de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'AccesoryDetail';
