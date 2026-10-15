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
ALTER TABLE [FixedAsset].[FixedAssetItem] NOCHECK CONSTRAINT [FK_Equipment_EquipmentType];


GO
ALTER TABLE [FixedAsset].[FixedAssetItem] NOCHECK CONSTRAINT [FK_FixedAssetEquipment_FixedAssetEquipmentCatalog];


GO
ALTER TABLE [FixedAsset].[FixedAssetItem] NOCHECK CONSTRAINT [FK_FixedAssetEquipment_GeneralLedgerIVA];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FixedAssetItem__Code]
    ON [FixedAsset].[FixedAssetItem]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del catalogo de bienes y servicios', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CatalogOfPropertyandServicesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Amortiza | 1 = Si | 0 = No |', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Amortizes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el articulo se deprecia por tiempo de uso de la ubicacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'DepreciateByTimeUse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si permite depreciar    Si permite depreciar se debe de llenar obligatoriamente el detalle de esta tabla como minimo con el libro oficial', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'AllowDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor razonable del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'FairValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica las observaciones del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el ultimo Costo del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'LastCostItem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del IVA', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del Catalogo del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del Tipo de Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'ItemTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Descripcion del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItem', @level2type = N'COLUMN', @level2name = N'Id';

