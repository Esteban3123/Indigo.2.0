CREATE TABLE [Taxes].[TaxesLiquidationDetail] (
    [Id]                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TaxesLiquidationId] INT            NOT NULL,
    [TaxesPropertyId]    INT            NOT NULL,
    [Appraisal]          DECIMAL (18)   NOT NULL,
    [ThirdPartyId]       INT            NOT NULL,
    [RateOwner]          DECIMAL (5, 2) NOT NULL,
    [PercentageOwner]    DECIMAL (5, 2) NOT NULL,
    [TotalValueTax]      DECIMAL (18)   NOT NULL,
    [ValueOwner]         DECIMAL (18)   NOT NULL,
    [TaxesInvoiceId]     INT            NULL,
    CONSTRAINT [PK_TaxesLiquidationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesLiquidationDetail_TaxesInvoice] FOREIGN KEY ([TaxesInvoiceId]) REFERENCES [Taxes].[TaxesInvoice] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationDetail_TaxesLiquidation] FOREIGN KEY ([TaxesLiquidationId]) REFERENCES [Taxes].[TaxesLiquidation] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationDetail_TaxesProperty] FOREIGN KEY ([TaxesPropertyId]) REFERENCES [Taxes].[TaxesProperty] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Taxes].[TaxesLiquidationDetail] NOCHECK CONSTRAINT [FK_TaxesLiquidationDetail_TaxesInvoice];


GO
ALTER TABLE [Taxes].[TaxesLiquidationDetail] NOCHECK CONSTRAINT [FK_TaxesLiquidationDetail_TaxesLiquidation];


GO
ALTER TABLE [Taxes].[TaxesLiquidationDetail] NOCHECK CONSTRAINT [FK_TaxesLiquidationDetail_TaxesProperty];


GO
ALTER TABLE [Taxes].[TaxesLiquidationDetail] NOCHECK CONSTRAINT [FK_TaxesLiquidationDetail_ThirdParty];

