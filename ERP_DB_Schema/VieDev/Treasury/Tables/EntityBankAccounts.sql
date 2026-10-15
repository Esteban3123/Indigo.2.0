CREATE TABLE [Treasury].[EntityBankAccounts] (
    [Id]                          INT                                                                       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (20)                                                              NOT NULL,
    [IdBank]                      INT                                                                       NOT NULL,
    [IdCity]                      INT                                                                       NOT NULL,
    [Type]                        TINYINT                                                                   NOT NULL,
    [Prefix]                      VARCHAR (4)                                                               NOT NULL,
    [Number]                      VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "Number_Ofuscado", 0)') NOT NULL,
    [InitialBalance]              DECIMAL (18, 2)                                                           NOT NULL,
    [CurrentBalance]              DECIMAL (18, 2)                                                           NOT NULL,
    [Rate]                        DECIMAL (18)                                                              NOT NULL,
    [Quota]                       DECIMAL (18)                                                              NOT NULL,
    [InitialDate]                 DATE                                                                      NOT NULL,
    [IdMainAccount]               INT                                                                       NOT NULL,
    [IdCostCenter]                INT                                                                       NULL,
    [ThirdPartyId]                INT                                                                       NULL,
    [FMGCounterpartMainAccountId] INT                                                                       NOT NULL,
    [FMGCounterpartThirdPartyId]  INT                                                                       NULL,
    [FMGCounterpartCostCenterId]  INT                                                                       NULL,
    [FMGExpenseMainAccountId]     INT                                                                       NOT NULL,
    [FMGExpenseThirdPartyId]      INT                                                                       NULL,
    [FMGExpenseCostCenterId]      INT                                                                       NULL,
    [FinancialSourceId]           INT                                                                       NULL,
    [Status]                      BIT                                                                       CONSTRAINT [DF_EntityBankAccount_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                VARCHAR (20)                                                              NOT NULL,
    [CreationDate]                DATETIME                                                                  NOT NULL,
    [ModificationUser]            VARCHAR (20)                                                              NULL,
    [ModificationDate]            DATETIME                                                                  NULL,
    [TimeStamp]                   ROWVERSION                                                                NOT NULL,
    [CurrencyId]                  INT                                                                       CONSTRAINT [DF__EntityBan__Curre__74207DEE] DEFAULT ((1)) NULL,
    [BalanceLastRevaluation]      DECIMAL (18, 2)                                                           CONSTRAINT [DF_EntityBankAccounts_BalanceLastRevaluation] DEFAULT ((0)) NOT NULL,
    [PeriodLastRevaluation]       INT                                                                       CONSTRAINT [DF_EntityBankAccounts_PeriodLastRevaluation] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EntityBankAccounts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntityBankAccount_Bank] FOREIGN KEY ([IdBank]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_EntityBankAccount_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_EntityBankAccount_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_EntityBankAccount_MainAccount] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_EntityBankAccounts_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_EntityBankAccounts_FinancialSource] FOREIGN KEY ([FinancialSourceId]) REFERENCES [Budget].[FinancialSource] ([Id]),
    CONSTRAINT [FK_EntityBankAccounts_MainAccounts] FOREIGN KEY ([FMGCounterpartMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_EntityBankAccounts_MainAccounts1] FOREIGN KEY ([FMGExpenseMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_EntityBankAccounts_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccount_Bank];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccount_City];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccount_CostCenter];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccount_MainAccount];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccounts_Currency];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccounts_FinancialSource];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccounts_MainAccounts];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccounts_MainAccounts1];


GO
ALTER TABLE [Treasury].[EntityBankAccounts] NOCHECK CONSTRAINT [FK_EntityBankAccounts_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo de la última revalorización.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccounts', @level2type = N'COLUMN', @level2name = N'BalanceLastRevaluation';

