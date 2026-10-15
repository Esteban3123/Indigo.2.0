CREATE TABLE [Treasury].[CashReceipts] (
    [Id]                              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                            VARCHAR (20)    NOT NULL,
    [IdThirdParty]                    INT             NOT NULL,
    [CollectType]                     TINYINT         NOT NULL,
    [IdMainAccount]                   INT             NOT NULL,
    [IdCostCenter]                    INT             NULL,
    [Detail]                          VARCHAR (2000)  NULL,
    [DocumentDate]                    DATETIME        NOT NULL,
    [IdCashRegister]                  INT             NULL,
    [IdBankAccount]                   INT             NULL,
    [PaymentResponsibles]             VARCHAR (100)   NOT NULL,
    [Value]                           DECIMAL (18, 2) NOT NULL,
    [IdRefund]                        INT             NULL,
    [OperatingUnitId]                 INT             NOT NULL,
    [Status]                          TINYINT         NOT NULL,
    [CreationUser]                    VARCHAR (20)    NOT NULL,
    [CreationDate]                    DATETIME        NOT NULL,
    [ModificationUser]                VARCHAR (20)    NULL,
    [ModificationDate]                DATETIME        NULL,
    [ConfirmationUser]                VARCHAR (20)    NULL,
    [ConfirmationDate]                DATETIME        NULL,
    [AnnulmentUser]                   VARCHAR (20)    NULL,
    [AnnulmentDate]                   DATETIME        NULL,
    [ReversedUser]                    VARCHAR (20)    NULL,
    [ReversedDate]                    DATETIME        NULL,
    [TimeStamp]                       ROWVERSION      NOT NULL,
    [PaymentResponsiblesThirdPartyId] INT             NULL,
    [EntityName]                      VARCHAR (250)   NULL,
    [CurrencyId]                      INT             NULL,
    CONSTRAINT [PK_CashReceipts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceipt_CashRegisters] FOREIGN KEY ([IdCashRegister]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_CashReceipt_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CashReceipt_EntityBankAccounts] FOREIGN KEY ([IdBankAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_CashReceipt_MainAccounts] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CashReceipt_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_CashReceipts_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_CashReceipts_ThirdPartyResponsiblePayment] FOREIGN KEY ([PaymentResponsiblesThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipt_CashRegisters];


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipt_CostCenter];


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipt_EntityBankAccounts];


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipt_MainAccounts];


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipt_ThirdParty];


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipts_OperatingUnit];


GO
ALTER TABLE [Treasury].[CashReceipts] NOCHECK CONSTRAINT [FK_CashReceipts_ThirdPartyResponsiblePayment];


GO
CREATE NONCLUSTERED INDEX [idx_CashReceipts_Status_Includes]
    ON [Treasury].[CashReceipts]([Status] ASC)
    INCLUDE([Code], [DocumentDate]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre del Proceso o entidad (tabla) que genera el rc', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceipts', @level2type = N'COLUMN', @level2name = N'EntityName';

