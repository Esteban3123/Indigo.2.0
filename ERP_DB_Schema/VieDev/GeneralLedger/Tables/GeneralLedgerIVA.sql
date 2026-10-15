CREATE TABLE [GeneralLedger].[GeneralLedgerIVA] (
    [Id]                           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)   NOT NULL,
    [Name]                         VARCHAR (200)  NOT NULL,
    [Percentage]                   NUMERIC (5, 2) NOT NULL,
    [Status]                       BIT            NOT NULL,
    [CreationUser]                 VARCHAR (20)   CONSTRAINT [DF_GeneralLedgerIVA_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                 DATETIME       CONSTRAINT [DF_GeneralLedgerIVA_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]             VARCHAR (20)   NULL,
    [ModificationDate]             DATETIME       NULL,
    [TimeStamp]                    ROWVERSION     NOT NULL,
    [IdAccountPurchaseService]     INT            NULL,
    [IdAccountSale]                INT            NULL,
    [IdAccountDebitControlFiscal]  INT            NULL,
    [IdAccountCreditControlFiscal] INT            NULL,
    [ApplyTaxDevolution]           BIT            CONSTRAINT [DF__GeneralLe__Apply__7F9D1D92] DEFAULT ((0)) NOT NULL,
    [PaymentMethodTypes]           VARCHAR (500)  NULL,
    CONSTRAINT [PK_GeneralLedgerIVA] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GeneralLedgerIVA_AccountCreditControlFiscal] FOREIGN KEY ([IdAccountCreditControlFiscal]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_GeneralLedgerIVA_AccountDebitControlFiscal] FOREIGN KEY ([IdAccountDebitControlFiscal]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_GeneralLedgerIVA_AccountPurchaseService] FOREIGN KEY ([IdAccountPurchaseService]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_GeneralLedgerIVA_AccountSale] FOREIGN KEY ([IdAccountSale]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerIVA] NOCHECK CONSTRAINT [FK_GeneralLedgerIVA_AccountCreditControlFiscal];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerIVA] NOCHECK CONSTRAINT [FK_GeneralLedgerIVA_AccountDebitControlFiscal];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerIVA] NOCHECK CONSTRAINT [FK_GeneralLedgerIVA_AccountPurchaseService];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerIVA] NOCHECK CONSTRAINT [FK_GeneralLedgerIVA_AccountSale];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Tipos de metodos de pago,                   en este campo van concatenado los tipo por comas Ej:                   1,2,3,4..', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerIVA', @level2type = N'COLUMN', @level2name = N'PaymentMethodTypes';

