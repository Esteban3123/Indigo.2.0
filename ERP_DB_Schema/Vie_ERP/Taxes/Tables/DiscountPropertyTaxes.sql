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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje ', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'DiscountPropertyTaxes', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha limite', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'DiscountPropertyTaxes', @level2type = N'COLUMN', @level2name = N'DeadLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MainAccountId', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'DiscountPropertyTaxes', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'DiscountPropertyTaxes', @level2type = N'COLUMN', @level2name = N'Id';

