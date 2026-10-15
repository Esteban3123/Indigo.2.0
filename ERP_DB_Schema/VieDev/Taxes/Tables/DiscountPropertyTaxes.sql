CREATE TABLE [Taxes].[DiscountPropertyTaxes] (
    [Id]            INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MainAccountId] INT            NOT NULL,
    [DeadLine]      DATE           NOT NULL,
    [Percentage]    NUMERIC (5, 2) NOT NULL,
    CONSTRAINT [PK_DiscountPropertyTaxes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DiscountPropertyTaxes_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Taxes].[DiscountPropertyTaxes] NOCHECK CONSTRAINT [FK_DiscountPropertyTaxes_MainAccounts];

