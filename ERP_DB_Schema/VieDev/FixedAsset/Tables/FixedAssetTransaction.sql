CREATE TABLE [FixedAsset].[FixedAssetTransaction] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]            INT             NOT NULL,
    [Code]                       VARCHAR (20)    NOT NULL,
    [DocumentDate]               DATE            NOT NULL,
    [GenerateAccountPayable]     BIT             NOT NULL,
    [ThirdPartyId]               INT             NOT NULL,
    [SupplierId]                 INT             NULL,
    [SupplierDistributionLineId] INT             NULL,
    [SupplierTypeId]             INT             NULL,
    [DayPeriod]                  INT             NOT NULL,
    [CreditMainAccountId]        INT             NOT NULL,
    [CostCenterId]               INT             NOT NULL,
    [InvoiceNumber]              VARCHAR (100)   NULL,
    [InvoiceDate]                DATE            NULL,
    [Observation]                VARCHAR (1000)  NULL,
    [Value]                      DECIMAL (18, 2) NOT NULL,
    [ValueDiscount]              DECIMAL (18, 2) NOT NULL,
    [ValueTax]                   DECIMAL (18, 2) NOT NULL,
    [WithholdingTax]             DECIMAL (18, 2) NOT NULL,
    [WithholdingICA]             DECIMAL (18, 2) NOT NULL,
    [RetentionSource]            DECIMAL (18, 2) NOT NULL,
    [RetentionOther]             DECIMAL (18, 2) NOT NULL,
    [DeductionOther]             DECIMAL (18, 2) NOT NULL,
    [TotalValue]                 DECIMAL (18, 2) NOT NULL,
    [Status]                     TINYINT         NOT NULL,
    [CreationUser]               VARCHAR (20)    NOT NULL,
    [CreationDate]               DATETIME        NOT NULL,
    [ModificationUser]           VARCHAR (20)    NULL,
    [ModificationDate]           DATETIME        NULL,
    [ConfirmationUser]           VARCHAR (20)    NULL,
    [ConfirmationDate]           DATETIME        NULL,
    [AnnulmentUser]              VARCHAR (20)    NULL,
    [AnnulmentDate]              DATETIME        NULL,
    [TimeStamp]                  ROWVERSION      NOT NULL,
    [CurrencyId]                 INT             NULL,
    [TaxRegistration]            TINYINT         DEFAULT ((4)) NOT NULL,
    CONSTRAINT [PK_FixedAssetTransaction__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetTransaction_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_FixedAssetTransaction_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_FixedAssetTransaction_MainAccounts] FOREIGN KEY ([CreditMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetTransaction_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_FixedAssetTransaction_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_FixedAssetTransaction_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id]),
    CONSTRAINT [FK_FixedAssetTransaction_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_FixedAssetTransaction__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [FixedAsset].[FixedAssetTransaction] NOCHECK CONSTRAINT [FK_FixedAssetTransaction_CostCenter];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransaction] NOCHECK CONSTRAINT [FK_FixedAssetTransaction_Currency];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransaction] NOCHECK CONSTRAINT [FK_FixedAssetTransaction_MainAccounts];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransaction] NOCHECK CONSTRAINT [FK_FixedAssetTransaction_Supplier];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransaction] NOCHECK CONSTRAINT [FK_FixedAssetTransaction_SuppliersDistributionLines];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransaction] NOCHECK CONSTRAINT [FK_FixedAssetTransaction_SupplierType];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransaction] NOCHECK CONSTRAINT [FK_FixedAssetTransaction_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransaction', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta que se va a acreditar en el movimiento contable de la transaccion    1 - Si la transaccion genera cuenta por pagar la cuenta que se va a acreditar es la que tiene asociada la linea de distribucion y esta no puede ser cambiada por el usuario    2 - Si la transaccion no genera cuenta por pagar la cuenta que se va a acreditar es la cuenta que se llama cuenta por servicios que se encuentra en parametros de activos fijos, esta cuenta solo se sugiere ya que el usuario podria cambiarla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransaction', @level2type = N'COLUMN', @level2name = N'CreditMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la transaccion genera una cuenta por pagar  1 - Si  0 - No', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransaction', @level2type = N'COLUMN', @level2name = N'GenerateAccountPayable';

