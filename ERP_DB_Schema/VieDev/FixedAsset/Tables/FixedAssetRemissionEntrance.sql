CREATE TABLE [FixedAsset].[FixedAssetRemissionEntrance] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]            INT           CONSTRAINT [DF_FixedAssetRemissionEntrance_OperatingUnitId] DEFAULT ((14)) NOT NULL,
    [Code]                       VARCHAR (20)  NOT NULL,
    [RemisionDate]               DATE          NOT NULL,
    [AdquisitionType]            TINYINT       NOT NULL,
    [SupplierDistributionLineId] INT           NOT NULL,
    [RemisionNumber]             VARCHAR (20)  NOT NULL,
    [GetLocationResponsible]     TINYINT       NOT NULL,
    [LocationId]                 INT           NULL,
    [ResponsibleId]              INT           NULL,
    [Detail]                     VARCHAR (300) NOT NULL,
    [Status]                     TINYINT       NOT NULL,
    [CreationUser]               VARCHAR (20)  NOT NULL,
    [CreationDate]               DATETIME      NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    [ConfirmationUser]           VARCHAR (20)  NULL,
    [ConfirmationDate]           DATETIME      NULL,
    [AnnulmentUser]              VARCHAR (20)  NULL,
    [AnnulmentDate]              DATETIME      NULL,
    [TimeStamp]                  ROWVERSION    NULL,
    CONSTRAINT [PK_InputRemission] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetRemissionEntrance_FixedAssetLocation] FOREIGN KEY ([LocationId]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id]),
    CONSTRAINT [FK_FixedAssetRemissionEntrance_FixedAssetResponsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_FixedAssetRemissionEntrance_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_InputRemission_Supplier] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntrance] NOCHECK CONSTRAINT [FK_FixedAssetRemissionEntrance_FixedAssetLocation];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntrance] NOCHECK CONSTRAINT [FK_FixedAssetRemissionEntrance_FixedAssetResponsible];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntrance] NOCHECK CONSTRAINT [FK_FixedAssetRemissionEntrance_OperatingUnit];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntrance] NOCHECK CONSTRAINT [FK_InputRemission_Supplier];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica de donde se va a obtener la Ubicacion y el Responsable  1 - Responsable y Ubicacion General  2 - Responsable y Ubicacion Especifico    Nota: Cuando sea tipo General solo se pedira el responsable y la ubicacion en la cabecera y todos los detalles van a quedar con este responsable y ubicacion    Si es Especifico entonces la ubicacion y el responsable solo se pediran en los detalles de los articulos uno a uno', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntrance', @level2type = N'COLUMN', @level2name = N'GetLocationResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Adquisicion   1 - Compra Directa  3 - Comodato  4 - Donacion  5 - Traspaso de Bienes  6 - Otro Concepto  7 - Leasing Financiero  8 - Comodato Tercerizado  9 - Renting Financiero  10 - Renting Operativo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntrance', @level2type = N'COLUMN', @level2name = N'AdquisitionType';

