CREATE TABLE [FixedAsset].[FixedAssetPhysicalAssetAccessory] (
    [Id]              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PhysicalAssetId] INT             NOT NULL,
    [Description]     VARCHAR (500)   NOT NULL,
    [Serie]           VARCHAR (50)    NULL,
    [Quantity]        INT             NOT NULL,
    [Value]           DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetPhysicalAssetAccessory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPhysicalAssetAccessory_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario o total del accesorio del activo fijo en moneda local; tipo DECIMAL(18,2) para cálculos de depreciación y valorización de patrimonio', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del accesorio asociado al activo físico; tipo INT, usado para inventario y conteo de componentes', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de serie o lote del accesorio del activo fijo; tipo VARCHAR(50) opcional, identificador único del fabricante para trazabilidad', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Serie', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Serie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del accesorio o componente del activo físico; tipo VARCHAR(500), nombre comercial o especificaciones técnicas del repuesto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del activo físico padre; clave foránea INT que referencia FixedAssetPhysicalAsset(Id), vincula accesorios a su equipo o bien principal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de accesorio; clave primaria INT IDENTITY, identifica cada componente o repuesto agregado al activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Accesorios o componentes asociados a un activo fijo físico. Registra los ítems complementarios (como cables, soportes, periféricos u otras piezas) que forman parte de un bien, incluyendo su descripción, número de serie, cantidad y valor.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetAccessory';
