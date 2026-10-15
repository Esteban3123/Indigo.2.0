CREATE TABLE [FixedAsset].[FixedAssetPhysicalAsset] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ItemId]                   INT             NOT NULL,
    [MainAccountId]            INT             NOT NULL,
    [Serie]                    VARCHAR (50)    NOT NULL,
    [Plate]                    VARCHAR (50)    NOT NULL,
    [LocationId]               INT             NOT NULL,
    [ResponsibleId]            INT             NOT NULL,
    [SupplierId]               INT             NULL,
    [HistoricalValue]          DECIMAL (18, 2) NOT NULL,
    [FairValue]                DECIMAL (18, 2) NOT NULL,
    [TrademarkId]              INT             NOT NULL,
    [Model]                    VARCHAR (100)   NOT NULL,
    [PolicyId]                 INT             NULL,
    [HandlesWarranty]          BIT             NOT NULL,
    [WarrantyExpirationDate]   DATE            NULL,
    [AdquisitionDate]          DATE            NOT NULL,
    [InstallationDate]         DATE            NULL,
    [Depreciate]               BIT             NOT NULL,
    [HasOutput]                BIT             CONSTRAINT [DF_FixedAssetPhysicalAsset_HasOutput] DEFAULT ((0)) NOT NULL,
    [OutputDate]               DATE            NULL,
    [OutputRefund]             BIT             CONSTRAINT [DF_FixedAssetPhysicalAsset_OutputRefund] DEFAULT ((0)) NOT NULL,
    [Observation]              VARCHAR (1000)  NULL,
    [StatusAssetId]            INT             NOT NULL,
    [Status]                   BIT             NOT NULL,
    [TimeStamp]                ROWVERSION      NOT NULL,
    [AdquisitionType]          TINYINT         CONSTRAINT [DF_FixedAssetPhysicalAsset_AdquisitionType] DEFAULT ((1)) NOT NULL,
    [NumberContractLeasing]    VARCHAR (50)    NULL,
    [InitialDateLeasing]       DATE            NULL,
    [EndDateLeasing]           DATE            NULL,
    [ApplyMinimunAmount]       BIT             CONSTRAINT [DF_FixedAssetPhysicalAsset_ApplyMinimunAmount] DEFAULT ((0)) NOT NULL,
    [AdquisitionTypeReal]      TINYINT         CONSTRAINT [DF_FixedAssetPhysicalAsset_AdquisitionType1] DEFAULT ((1)) NOT NULL,
    [PurchaseDate]             DATE            NULL,
    [EntryNumber]              VARCHAR (20)    NULL,
    [VoucherTransactionNumber] VARCHAR (20)    NULL,
    [HasReclassified]          BIT             CONSTRAINT [DF_FixedAssetPhysicalAsset_HasReclassified] DEFAULT ((0)) NOT NULL,
    [EndDateLeasingExecuted]   DATE            NULL,
    [RecoverableValue]         DECIMAL (18, 2) CONSTRAINT [DF_FixedAssetPhysicalAsset_RecoverableValue] DEFAULT ((0)) NOT NULL,
    [HasHighTech]              BIT             CONSTRAINT [DF_FixedAssetPhysicalAsset_HasHighTech] DEFAULT ((0)) NOT NULL,
    [NetHistoricalValue]       DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__NetHi__2DB9B954] DEFAULT ((0)) NOT NULL,
    [FinancialDiscount]        DECIMAL (18, 2) CONSTRAINT [DF__FixedAsse__Finan__2EADDD8D] DEFAULT ((0)) NOT NULL,
    [Amortize]                 BIT             CONSTRAINT [DF__FixedAsse__Amort__2E78D363] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetPhysicalAsset__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetItem] FOREIGN KEY ([ItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetLocation] FOREIGN KEY ([LocationId]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetPoliza] FOREIGN KEY ([PolicyId]) REFERENCES [FixedAsset].[FixedAssetPolicy] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetResponsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetStatusAsset] FOREIGN KEY ([StatusAssetId]) REFERENCES [FixedAsset].[FixedAssetStatusAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetTrademark] FOREIGN KEY ([TrademarkId]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAsset_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAsset] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAsset] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetLocation];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAsset] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetPoliza];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAsset] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetResponsible];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAsset] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetStatusAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAsset] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAsset_FixedAssetTrademark];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAsset] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAsset_MainAccounts];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAsset] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAsset_Supplier];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Amortiza | 1 = Si | 0 = No | ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Amortize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'FinancialDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor histórico neto(valor histórico - valor descuento financiero)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'NetHistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene alta tecnología | 1 = Si | 0 = No |', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HasHighTech';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Adquisición  Especirfica el Tipo de Adquisicion   1 - Compra Directa  3 - Comodato  4 - Donadacion  5 - Traspaso de Bienes  6 - Otro Concepto  7 - Leasing Financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'AdquisitionTypeReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Adquisicion   1 - Compra Directa  3 - Comodato  4 - Donacion  5 - Traspaso de Bienes  6 - Otro Concepto  7 - Leasing Financiero  8 - Comodato Tercerizado  9 - Renting Financiero  10 - Renting Operativo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'AdquisitionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado del activo  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deprecia (1 - SI, 0 - NO)    Nota: este campo depende del AllowDeprecate que se encuentra en el articulo ya que si alla esta marcado como no permite depreciar aca tambien debe ir siempre 0, pero si alla esta marcado como que si permite depreciar entonces aca es opcional por el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor Historico del activo  En desuso, ahora se usa el valor historico del activo por libro, puesto que este puede variar por libro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta de la cuenta contable del activo    de acuerdo con su tipo de adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAsset', @level2type = N'COLUMN', @level2name = N'MainAccountId';

