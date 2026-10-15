CREATE TABLE [Common].[SupplierBankAccount] (
    [Id]             INT                                                                   IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SupplierId]     INT                                                                   NOT NULL,
    [BankId]         INT                                                                   NOT NULL,
    [Type]           TINYINT                                                               NOT NULL,
    [Number]         VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "BankAccount", 0)') NOT NULL,
    [PaymentDefault] BIT                                                                   NOT NULL,
    [CurrencyId]     INT                                                                   NULL,
    [State]          TINYINT                                                               CONSTRAINT [DF__SupplierB__State__53F470D5] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SupplierBankAccount] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SupplierBankAccount_Bank] FOREIGN KEY ([BankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_SupplierBankAccount_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_SupplierBankAccount_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [Common].[SupplierBankAccount] NOCHECK CONSTRAINT [FK_SupplierBankAccount_Bank];


GO
ALTER TABLE [Common].[SupplierBankAccount] NOCHECK CONSTRAINT [FK_SupplierBankAccount_Currency];


GO
ALTER TABLE [Common].[SupplierBankAccount] NOCHECK CONSTRAINT [FK_SupplierBankAccount_Supplier];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1-Activo  2-Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Moneda de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierBankAccount', @level2type = N'COLUMN', @level2name = N'CurrencyId';

