CREATE TABLE [MixingStation].[Transportation] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)  NOT NULL,
    [TransportationType]        TINYINT       NOT NULL,
    [FixedAssetPhysicalAssetId] INT           NULL,
    [TransportationCode]        VARCHAR (20)  NOT NULL,
    [TransportationName]        VARCHAR (300) NOT NULL,
    [SupplierId]                INT           NULL,
    [Plate]                     VARCHAR (50)  NOT NULL,
    [TrademarkId]               INT           NOT NULL,
    [Model]                     VARCHAR (100) NOT NULL,
    [Status]                    BIT           NOT NULL,
    [CreationUser]              VARCHAR (20)  NOT NULL,
    [CreationDate]              DATETIME      NOT NULL,
    [ModificationUser]          VARCHAR (20)  NULL,
    [ModificationDate]          DATETIME      NULL,
    [TimeStamp]                 ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Transportation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Transportation_FixedAssetPhysicalAsset] FOREIGN KEY ([FixedAssetPhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_Transportation_FixedAssetTrademark] FOREIGN KEY ([TrademarkId]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_Transportation_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [MixingStation].[Transportation] NOCHECK CONSTRAINT [FK_Transportation_FixedAssetPhysicalAsset];


GO
ALTER TABLE [MixingStation].[Transportation] NOCHECK CONSTRAINT [FK_Transportation_FixedAssetTrademark];


GO
ALTER TABLE [MixingStation].[Transportation] NOCHECK CONSTRAINT [FK_Transportation_Supplier];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de auxiliar:  1. Interno  2. Contratado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Transportation', @level2type = N'COLUMN', @level2name = N'TransportationType';

