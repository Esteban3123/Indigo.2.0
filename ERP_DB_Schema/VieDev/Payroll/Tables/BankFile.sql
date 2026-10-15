CREATE TABLE [Payroll].[BankFile] (
    [Id]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20) NOT NULL,
    [OperatingUnitId]        INT          NOT NULL,
    [CompanyId]              INT          NOT NULL,
    [LiquidationDate]        DATE         NOT NULL,
    [EntityBankAccountId]    INT          NOT NULL,
    [ExpenseConceptId]       INT          NOT NULL,
    [VoucherTransactionId]   INT          NULL,
    [Value]                  NUMERIC (18) NOT NULL,
    [Status]                 TINYINT      NOT NULL,
    [CreationUser]           VARCHAR (20) NOT NULL,
    [CreationDate]           DATETIME     NOT NULL,
    [ModificationUser]       VARCHAR (20) NULL,
    [ModificationDate]       DATETIME     NULL,
    [ConfirmationUser]       VARCHAR (20) NULL,
    [ConfirmationDate]       DATETIME     NULL,
    [AnnulmentUser]          VARCHAR (20) NULL,
    [AnnulmentDate]          DATETIME     NULL,
    [TimeStamp]              ROWVERSION   NOT NULL,
    [Process]                TINYINT      CONSTRAINT [DF__BankFile__Proces__43E8263E] DEFAULT ((2)) NOT NULL,
    [PeriodIncentivePayment] TINYINT      NULL,
    [YearIncentivePayment]   INT          NULL,
    [IncentivePaymentId]     INT          NULL,
    CONSTRAINT [PK_BankFile] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankFile_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Payroll].[Company] ([Id]),
    CONSTRAINT [FK_BankFile_EntityBankAccounts] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_BankFile_ExpenseConcepts] FOREIGN KEY ([ExpenseConceptId]) REFERENCES [Treasury].[ExpenseConcepts] ([Id]),
    CONSTRAINT [FK_BankFile_IncentivePayment] FOREIGN KEY ([IncentivePaymentId]) REFERENCES [Payroll].[IncentivePayment] ([Id]),
    CONSTRAINT [FK_BankFile_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_BankFile_VoucherTransaction] FOREIGN KEY ([VoucherTransactionId]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);


GO
ALTER TABLE [Payroll].[BankFile] NOCHECK CONSTRAINT [FK_BankFile_Company];


GO
ALTER TABLE [Payroll].[BankFile] NOCHECK CONSTRAINT [FK_BankFile_EntityBankAccounts];


GO
ALTER TABLE [Payroll].[BankFile] NOCHECK CONSTRAINT [FK_BankFile_ExpenseConcepts];


GO
ALTER TABLE [Payroll].[BankFile] NOCHECK CONSTRAINT [FK_BankFile_IncentivePayment];


GO
ALTER TABLE [Payroll].[BankFile] NOCHECK CONSTRAINT [FK_BankFile_OperatingUnit];


GO
ALTER TABLE [Payroll].[BankFile] NOCHECK CONSTRAINT [FK_BankFile_VoucherTransaction];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del incentivo de pago', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'IncentivePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pago de incentivo anual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'YearIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pago de incentivos por período', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'PeriodIncentivePayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Proceso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Process';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado      1. Registrado      2. Confirmado      3. Anulado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFile', @level2type = N'COLUMN', @level2name = N'Status';

