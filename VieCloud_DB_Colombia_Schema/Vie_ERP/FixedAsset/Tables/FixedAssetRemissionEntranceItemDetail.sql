CREATE TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetail] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionEntranceItemId] INT          NOT NULL,
    [Plate]                   VARCHAR (50) NOT NULL,
    [Serie]                   VARCHAR (50) NOT NULL,
    [ResponsibleId]           INT          NOT NULL,
    [LocationId]              INT          NOT NULL,
    [AdquisitionDate]         DATE         NOT NULL,
    [Depreciate]              BIT          NOT NULL,
    [HandlesWarranty]         BIT          NOT NULL,
    [WarrantyExpirationDate]  DATE         NULL,
    [StatusAssetId]           INT          NOT NULL,
    CONSTRAINT [PK_InputRemissionEquipmentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetRemissionEntranceItemDetail_FixedAssetStatusAsset] FOREIGN KEY ([StatusAssetId]) REFERENCES [FixedAsset].[FixedAssetStatusAsset] ([Id]),
    CONSTRAINT [FK_InputRemissionEquipmentDetail_InputRemissionEquipmentDetail] FOREIGN KEY ([RemissionEntranceItemId]) REFERENCES [FixedAsset].[FixedAssetRemissionEntranceItem] ([Id]),
    CONSTRAINT [FK_InputRemissionEquipmentDetail_Location] FOREIGN KEY ([LocationId]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id]),
    CONSTRAINT [FK_InputRemissionEquipmentDetail_Responsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetail] NOCHECK CONSTRAINT [FK_FixedAssetRemissionEntranceItemDetail_FixedAssetStatusAsset];




GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetail] NOCHECK CONSTRAINT [FK_FixedAssetRemissionEntranceItemDetail_FixedAssetStatusAsset];


GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del estado del activo fijo (referencia a FixedAssetStatusAsset). Indica condición actual: activo, inactivo, depreciado, fuera de servicio, desechado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'StatusAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Estado del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'StatusAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'StatusAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento de la garantía del activo fijo. Campo DATE nullable; vacío si no aplica cobertura de garantía o sin fecha definida.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de vencimiento de la garantia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (1=SÍ, 0=NO) que especifica si el artículo/equipo posee y gestiona garantía del proveedor o fabricante.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'HandlesWarranty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el item maneja garantia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'HandlesWarranty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'HandlesWarranty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (1=SÍ depreciar, 0=NO depreciar) que controla depreciación contable del activo. Subordinado al atributo AllowDepreciate del artículo: si no permite depreciar aquí debe ser 0; si permite, el usuario decide opcionalmente.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deprecia (1 - SI, 0 - NO)    Nota: este campo depende del AllowDeprecate que se encuentra en el articulo ya que si alla esta marcado como no permite depreciar aca tambien debe ir siempre 0, pero si alla esta marcado como que si permite depreciar entonces aca es opcional por el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha DATE de adquisición, compra o entrada en servicio del activo fijo. Base para cálculo de depreciación y vida útil.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Fecha de Adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la localización/sucursal/centro de atención donde se encuentra físicamente el activo fijo (referencia a FixedAssetLocation).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'LocationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Localización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'LocationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'LocationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del responsable, custodio o propietario del activo fijo en la organización (referencia a FixedAssetResponsible). Persona a cargo del equipo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'ResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Responsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'ResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'ResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de serie único del fabricante o artículo. Identifica unidad específica para garantía, control de inventario y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la serie del articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Serie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Placa o código de identificación interno asignado por la institución al activo fijo. Identificador visible para localización física y registros.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Plate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Placa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Plate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Plate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle/rengln del comprobante de entrada de remisión (referencia a FixedAssetRemissionEntranceItem). Vincula el bien con su acta de ingreso.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle de comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (IDENTITY, INT) único de cada detalle de activo en remisión. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada activo fijo individual recibido en una remisión de entrada: registra la placa, serie, responsable, ubicación, fecha de adquisición, si aplica depreciación, si tiene garantía vigente y el estado actual del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetail';
