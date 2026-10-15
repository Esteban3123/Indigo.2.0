CREATE TABLE [Treasury].[CashRegisters] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)    NOT NULL,
    [Name]                   VARCHAR (100)   NOT NULL,
    [Type]                   TINYINT         NOT NULL,
    [FilingUnitId]           INT             NULL,
    [Prefix]                 VARCHAR (4)     NOT NULL,
    [InitialBalance]         DECIMAL (18, 2) NOT NULL,
    [InitialDate]            DATETIME        NOT NULL,
    [RefundDate]             DATETIME        NULL,
    [AmountMax]              DECIMAL (18)    NOT NULL,
    [AmountMin]              DECIMAL (18)    NOT NULL,
    [CurrentBalance]         DECIMAL (18, 2) NOT NULL,
    [IdMainAccount]          INT             NOT NULL,
    [IdCostCenter]           INT             NULL,
    [ThirdPartyId]           INT             NULL,
    [IsMovement]             BIT             NOT NULL,
    [Status]                 BIT             CONSTRAINT [DF_CashRegister_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]           VARCHAR (20)    NOT NULL,
    [CreationDate]           DATETIME        NOT NULL,
    [ModificationUser]       VARCHAR (20)    NULL,
    [ModificationDate]       DATETIME        NULL,
    [TimeStamp]              ROWVERSION      NOT NULL,
    [CurrencyId]             INT             NULL,
    [BalanceLastRevaluation] DECIMAL (18, 2) CONSTRAINT [DF__CashRegis__Balan__1CA37094] DEFAULT ((0)) NOT NULL,
    [PeriodLastRevaluation]  INT             CONSTRAINT [DF__CashRegis__Perio__1D9794CD] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CashRegisters__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashRegister_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CashRegister_MainAccount] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CashRegisters_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_CashRegisters_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_CashRegisters_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Treasury].[CashRegisters] NOCHECK CONSTRAINT [FK_CashRegister_CostCenter];


GO
ALTER TABLE [Treasury].[CashRegisters] NOCHECK CONSTRAINT [FK_CashRegister_MainAccount];


GO
ALTER TABLE [Treasury].[CashRegisters] NOCHECK CONSTRAINT [FK_CashRegisters_Currency];


GO
ALTER TABLE [Treasury].[CashRegisters] NOCHECK CONSTRAINT [FK_CashRegisters_FilingUnit];


GO
ALTER TABLE [Treasury].[CashRegisters] NOCHECK CONSTRAINT [FK_CashRegisters_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo de la última revalorización.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisters', @level2type = N'COLUMN', @level2name = N'BalanceLastRevaluation';

