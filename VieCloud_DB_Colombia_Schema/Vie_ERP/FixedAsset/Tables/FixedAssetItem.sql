CREATE TABLE [FixedAsset].[FixedAssetItem] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                           VARCHAR (20)    NOT NULL,
    [Description]                    VARCHAR (300)   NOT NULL,
    [ItemTypeId]                     INT             NOT NULL,
    [ItemCatalogId]                  INT             NOT NULL,
    [IVAId]                          INT             NOT NULL,
    [LastCostItem]                   NUMERIC (18)    NOT NULL,
    [Observations]                   VARCHAR (300)   NULL,
    [FairValue]                      DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetItem_FairValue] DEFAULT ((0)) NOT NULL,
    [AllowDepreciate]                BIT             CONSTRAINT [DF_FixedAssetItem_AllowDepreciate] DEFAULT ((1)) NOT NULL,
    [Status]                         BIT             NOT NULL,
    [CreationUser]                   VARCHAR (20)    NOT NULL,
    [CreationDate]                   DATETIME        NOT NULL,
    [ModificationUser]               VARCHAR (20)    NULL,
    [ModificationDate]               DATETIME        NULL,
    [TimeStamp]                      ROWVERSION      NOT NULL,
    [DepreciateByTimeUse]            BIT             CONSTRAINT [DF_FixedAssetItem_DepreciateByTimeUse] DEFAULT ((0)) NOT NULL,
    [Amortizes]                      BIT             CONSTRAINT [DF__FixedAsse__Amort__7866DB75] DEFAULT ((0)) NOT NULL,
    [CatalogOfPropertyandServicesId] INT             NULL,
    CONSTRAINT [PK_FixedAssetItem__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Equipment_EquipmentType] FOREIGN KEY ([ItemTypeId]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipment_FixedAssetEquipmentCatalog] FOREIGN KEY ([ItemCatalogId]) REFERENCES [FixedAsset].[FixedAssetItemCatalog] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipment_GeneralLedgerIVA] FOREIGN KEY ([IVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_FixedAssetItem_FixedAssetCatalogOfPropertyandServices] FOREIGN KEY ([CatalogOfPropertyandServicesId]) REFERENCES [FixedAsset].[FixedAssetCatalogOfPropertyandServices] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetItem] NOCHECK CONSTRAINT [FK_FixedAssetEquipment_FixedAssetEquipmentCatalog];




GO



GO
ALTER TABLE [FixedAsset].[FixedAssetItem] NOCHECK CONSTRAINT [FK_FixedAssetEquipment_FixedAssetEquipmentCatalog];


GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FixedAssetItem__Code]
    ON [FixedAsset].[FixedAssetItem]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del catálogo de bienes y servicios INCOP, referencia FK a FixedAssetCatalogOfPropertyandServices. Permite clasificación normativa de activos fijos según clasificación estatal (INT, nullable).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CatalogOfPropertyandServicesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del catalogo de bienes y servicios', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CatalogOfPropertyandServicesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CatalogOfPropertyandServicesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de amortización del activo fijo (BIT: 1=Sí amortiza, 0=No amortiza). Determina si el bien es intangible o depreciable por amortización contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Amortizes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Amortiza | 1 = Si | 0 = No |', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Amortizes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Amortizes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de depreciación por tiempo o uso físico (BIT: 1=Sí deprecia por uso, 0=No). Especifica si la depreciación se calcula según ubicación/tiempo de uso operativo del activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'DepreciateByTimeUse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el articulo se deprecia por tiempo de uso de la ubicacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'DepreciateByTimeUse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'DepreciateByTimeUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP) que registra el instante exacto de creación, modificación o evento en el activo. Usado para auditoría y control de concurrencia en SQL Server.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro del activo fijo. Nulo si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Login o usuario (VARCHAR 20) que realizó la última modificación del activo. Nulo si no hay cambios posteriores a creación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro del activo fijo en el módulo de Activos Fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Login o usuario (VARCHAR 20) que creó el registro inicial del activo fijo en el sistema.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del activo fijo (BIT: 1=Activo/Vigente, 0=Inactivo/Retirado). Controla disponibilidad para depreciación y reporting.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de permiso de depreciación (BIT: 1=Permite depreciar, 0=No deprecia). Si es 1, obliga llenar obligatoriamente detalles de depreciación en tabla de libros contables.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'AllowDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si permite depreciar    Si permite depreciar se debe de llenar obligatoriamente el detalle de esta tabla como minimo con el libro oficial', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'AllowDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'AllowDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor razonable o valor justo (DECIMAL 18,2) del activo fijo para propósitos de revalorización contable. Default=0.00.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'FairValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor razonable del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'FairValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'FairValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas o comentarios adicionales (VARCHAR 300) sobre el activo fijo: condición, ubicación, mantenimiento. Opcional.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica las observaciones del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Último costo registrado (NUMERIC 18) del artículo/bien de activo fijo. Base para cálculo de depreciación contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'LastCostItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el ultimo Costo del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'LastCostItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'LastCostItem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del régimen de IVA (INT, FK a GeneralLedgerIVA). Define tratamiento fiscal del activo y retención en la fuente.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del IVA', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del catálogo de artículos/bienes (INT, FK a FixedAssetItemCatalog). Clasifica el tipo de bien según estructura interna del activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del Catalogo del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de artículo/bien (INT, FK a FixedAssetItemType). Determina categoría: equipo, mueble, inmueble, vehículo, etc.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ItemTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del Tipo de Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ItemTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ItemTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción completa (VARCHAR 300) del artículo/bien de activo fijo: marca, modelo, especificaciones técnicas.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Descripcion del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) del activo fijo, asignado según política interna. Sinónimo: código patrimonial, número de inventario, placa del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK Identity) del artículo/bien en la tabla FixedAssetItem. Clave primaria para referenciar el activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de artículos o bienes que pueden ser registrados como activos fijos en la organización. Contiene la información base de cada ítem: código, descripción, tipo, costos, valor razonable y configuración de depreciación o amortización.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem';
