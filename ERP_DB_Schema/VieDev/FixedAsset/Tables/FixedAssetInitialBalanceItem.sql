CREATE TABLE [FixedAsset].[FixedAssetInitialBalanceItem] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetInitialBalanceId] INT             NOT NULL,
    [ItemId]                     INT             NOT NULL,
    [Serie]                      VARCHAR (50)    NOT NULL,
    [Plate]                      VARCHAR (50)    NOT NULL,
    [LocationId]                 INT             NOT NULL,
    [ResponsibleId]              INT             NOT NULL,
    [SupplierId]                 INT             NULL,
    [HistoricalValue]            DECIMAL (18, 2) NOT NULL,
    [FairValue]                  DECIMAL (18, 2) NOT NULL,
    [TrademarkId]                INT             NOT NULL,
    [Model]                      VARCHAR (100)   NOT NULL,
    [PolicyId]                   INT             NULL,
    [HandlesWarranty]            BIT             NOT NULL,
    [WarrantyExpirationDate]     DATE            NULL,
    [AdquisitionDate]            DATE            NOT NULL,
    [Depreciate]                 BIT             NOT NULL,
    [StatusAssetId]              INT             NOT NULL,
    [Status]                     BIT             NOT NULL,
    [AdquisitionType]            TINYINT         CONSTRAINT [DF_FixedAssetInitialBalanceItem_AdquisitionType] DEFAULT ((1)) NOT NULL,
    [ValidMinorAmount]           BIT             CONSTRAINT [DF__FixedAsse__Valid__2835EA28] DEFAULT ((1)) NULL,
    [Amortize]                   BIT             CONSTRAINT [DF_FixedAssetInitialBalanceItem_Amortize] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetInitialBalanceItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetInitialBalance] FOREIGN KEY ([FixedAssetInitialBalanceId]) REFERENCES [FixedAsset].[FixedAssetInitialBalance] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetItem] FOREIGN KEY ([ItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetLocation] FOREIGN KEY ([LocationId]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetPoliza] FOREIGN KEY ([PolicyId]) REFERENCES [FixedAsset].[FixedAssetPolicy] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetResponsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetStatusAsset] FOREIGN KEY ([StatusAssetId]) REFERENCES [FixedAsset].[FixedAssetStatusAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetTrademark] FOREIGN KEY ([TrademarkId]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItem] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetInitialBalance];


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItem] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItem] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetLocation];


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItem] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetPoliza];


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItem] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetResponsible];


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItem] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetStatusAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItem] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetTrademark];


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItem] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItem_Supplier];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Amortiza | 1 = Si | 0 = No | ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Amortize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida menor Cuantía | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'ValidMinorAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Adquisición  Especirfica el Tipo de Adquisicion   1 - Compra Directa  3 - Comodato  4 - Donadacion  5 - Traspaso de Bienes  6 - Otro Concepto  7 - Leasing Financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'AdquisitionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado del activo  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del activo, Se diligencia con la información de la tabla FixedAssetStatusAsset', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'StatusAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deprecia (1 - SI, 0 - NO)    Nota: este campo depende del AllowDeprecate que se encuentra en el articulo ya que si alla esta marcado como no permite depreciar aca tambien debe ir siempre 0, pero si alla esta marcado como que si permite depreciar entonces aca es opcional por el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Depreciate';

