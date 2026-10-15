CREATE TABLE [Payments].[AdvancePayments] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)    NOT NULL,
    [IdSupplier]           INT             NOT NULL,
    [IdAccount]            INT             NOT NULL,
    [IdCostCenter]         INT             NULL,
    [IdThirdParty]         INT             NULL,
    [StateAdvancePayments] TINYINT         NOT NULL,
    [DocumentDate]         DATETIME        NOT NULL,
    [Comments]             VARCHAR (MAX)   NULL,
    [Value]                DECIMAL (18, 2) NOT NULL,
    [DebitValue]           DECIMAL (18, 2) NULL,
    [CreditValue]          DECIMAL (18, 2) NULL,
    [Balance]              DECIMAL (18, 2) NOT NULL,
    [EntityId]             INT             NULL,
    [EntityCode]           VARCHAR (20)    NULL,
    [EntityName]           VARCHAR (250)   NULL,
    [Status]               TINYINT         CONSTRAINT [DF_AdvancePayments_Status] DEFAULT ((2)) NOT NULL,
    [CreationUser]         VARCHAR (20)    NOT NULL,
    [CreationDate]         DATETIME        NOT NULL,
    [ModificationUser]     VARCHAR (20)    NULL,
    [ModificationDate]     DATETIME        NULL,
    [ConfirmationUser]     VARCHAR (20)    NULL,
    [ConfirmationDate]     DATETIME        NULL,
    [AnnulmentUser]        VARCHAR (20)    NULL,
    [AnnulmentDate]        DATETIME        NULL,
    [TimeStamp]            ROWVERSION      NOT NULL,
    [CurrencyId]           INT             NULL,
    [TRMValue]             NUMERIC (20, 5) CONSTRAINT [DF__AdvancePa__TRMVa__1A31183D] DEFAULT ((0)) NOT NULL,
    [ValueCurrencyHeader]  NUMERIC (20, 5) CONSTRAINT [DF__AdvancePa__Value__1B253C76] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AdvancePayments__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdvancePayments_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_AdvancePayments_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_MoneyAdvance_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_MoneyAdvance_MainAccount] FOREIGN KEY ([IdAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_MoneyAdvance_ThirdParty] FOREIGN KEY ([IdSupplier]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [Payments].[AdvancePayments] NOCHECK CONSTRAINT [FK_AdvancePayments_Currency];


GO
ALTER TABLE [Payments].[AdvancePayments] NOCHECK CONSTRAINT [FK_AdvancePayments_ThirdParty];


GO
ALTER TABLE [Payments].[AdvancePayments] NOCHECK CONSTRAINT [FK_MoneyAdvance_CostCenter];


GO
ALTER TABLE [Payments].[AdvancePayments] NOCHECK CONSTRAINT [FK_MoneyAdvance_MainAccount];


GO
ALTER TABLE [Payments].[AdvancePayments] NOCHECK CONSTRAINT [FK_MoneyAdvance_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la cabecera con la moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePayments', @level2type = N'COLUMN', @level2name = N'ValueCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TRM De la transaccion de la moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePayments', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePayments', @level2type = N'COLUMN', @level2name = N'IdThirdParty';

