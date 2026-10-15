CREATE TABLE [Treasury].[VoucherTransaction] (
    [Id]                        INT                                                                            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)                                                                   NOT NULL,
    [IdThirdParty]              INT                                                                            NULL,
    [IdMainAccount]             INT                                                                            NOT NULL,
    [IdCostCenter]              INT                                                                            NULL,
    [VoucherClass]              INT                                                                            CONSTRAINT [DF_VoucherTransaction_VoucherClass] DEFAULT ((1)) NOT NULL,
    [ExpenseType]               TINYINT                                                                        CONSTRAINT [DF_VoucherTransaction_AffectBank] DEFAULT ((0)) NOT NULL,
    [Detail]                    VARCHAR (MAX)                                                                  CONSTRAINT [DF_VoucherTransaction_Detail] DEFAULT ('-') NOT NULL,
    [DocumentDate]              DATETIME                                                                       NOT NULL,
    [IdCashRegister]            INT                                                                            NULL,
    [IdEntityBankAccount]       INT                                                                            NULL,
    [Value]                     DECIMAL (18, 2)                                                                NOT NULL,
    [PaymentMethod]             TINYINT                                                                        NULL,
    [NoteNumber]                VARCHAR (50)                                                                   NULL,
    [IdChecks]                  INT                                                                            NULL,
    [CheckNumber]               BIGINT                                                                         NULL,
    [TransactionDate]           DATETIME                                                                       NULL,
    [TaxByMil]                  BIT                                                                            CONSTRAINT [DF_VoucherTransaction_TaxByMil] DEFAULT ((0)) NOT NULL,
    [TaxByMilValue]             DECIMAL (18, 2)                                                                NULL,
    [CashRegisterExpense]       BIT                                                                            CONSTRAINT [DF_VoucherTransaction_CashRegisterExpense] DEFAULT ((0)) NOT NULL,
    [RefundCashRegisterExpense] BIT                                                                            CONSTRAINT [DF_VoucherTransaction_RefundCashRegisterExpense] DEFAULT ((0)) NOT NULL,
    [SchedulePaymentId]         INT                                                                            NULL,
    [BeneficiaryIdentification] VARCHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identity_Ofuscado", 0)')     NULL,
    [Beneficiary]               VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "Beneficiary_Ofuscado", 0)') NULL,
    [TransactionRelationship]   BIT                                                                            CONSTRAINT [DF_VoucherTransaction_TransactionRelationship] DEFAULT ((0)) NULL,
    [CheckReconciled]           BIT                                                                            CONSTRAINT [DF_VoucherTransaction_CheckReconciled] DEFAULT ((0)) NULL,
    [IdPaymentOrder]            INT                                                                            NULL,
    [Printed]                   BIT                                                                            CONSTRAINT [DF_VoucherTransaction_Printed] DEFAULT ((0)) NULL,
    [RTEValue]                  DECIMAL (18, 2)                                                                CONSTRAINT [DF_VoucherTransaction_RTEValue] DEFAULT ((0.0)) NULL,
    [IVAValue]                  DECIMAL (18, 2)                                                                CONSTRAINT [DF_VoucherTransaction_IVAValue] DEFAULT ((0.0)) NULL,
    [ICAValue]                  DECIMAL (18, 2)                                                                CONSTRAINT [DF_VoucherTransaction_ICAValue] DEFAULT ((0.0)) NULL,
    [OtherValue]                DECIMAL (18, 2)                                                                CONSTRAINT [DF_VoucherTransaction_OtherValue] DEFAULT ((0.0)) NULL,
    [BankAccountNumber]         VARCHAR (50) MASKED WITH (FUNCTION = 'partial(0, "Account_Ofuscado", 0)')      NULL,
    [BankName]                  VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "Bank_Ofuscado", 0)')        NULL,
    [DetailsInterfaceBudget]    VARCHAR (100)                                                                  NULL,
    [IdUnitOperative]           INT                                                                            NOT NULL,
    [IdRefund]                  INT                                                                            NULL,
    [Status]                    TINYINT                                                                        NOT NULL,
    [CreationUser]              VARCHAR (20)                                                                   NOT NULL,
    [CreationDate]              DATETIME                                                                       NOT NULL,
    [ModificationUser]          VARCHAR (20)                                                                   NULL,
    [ModificationDate]          DATETIME                                                                       NULL,
    [ConfirmationUser]          VARCHAR (20)                                                                   NULL,
    [ConfirmationDate]          DATETIME                                                                       NULL,
    [AnnulmentUser]             VARCHAR (20)                                                                   NULL,
    [AnnulmentDate]             DATETIME                                                                       NULL,
    [ReversedUser]              VARCHAR (20)                                                                   NULL,
    [ReversedDate]              DATETIME                                                                       NULL,
    [TimeStamp]                 ROWVERSION                                                                     NOT NULL,
    [EmailSent]                 BIT                                                                            CONSTRAINT [DF_VoucherTransaction_EmailSent] DEFAULT ((0)) NOT NULL,
    [IdCheckCashingStatus]      TINYINT                                                                        NULL,
    [HandlesDocumentSupport]    BIT                                                                            CONSTRAINT [DF__VoucherTr__Handl__452597E3] DEFAULT ((0)) NOT NULL,
    [CurrencyId]                INT                                                                            CONSTRAINT [DF__VoucherTr__Curre__77F10ED2] DEFAULT ((1)) NULL,
    [SupplierBankAccountId]     INT                                                                            NULL,
    CONSTRAINT [PK_VoucherTransaction__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_VoucherTransaction] CHECK (case when [Status]=(2) AND [ConfirmationUser] IS NULL then (0) else (1) end=(1)),
    CONSTRAINT [FK_VoucherTransaction_CashRegisters] FOREIGN KEY ([IdCashRegister]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_Checkbooks] FOREIGN KEY ([IdChecks]) REFERENCES [Treasury].[Checkbooks] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_EntityBankAccounts] FOREIGN KEY ([IdEntityBankAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_MainAccounts] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_OperatingUnit] FOREIGN KEY ([IdUnitOperative]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_Refunds] FOREIGN KEY ([IdRefund]) REFERENCES [Treasury].[Refunds] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_SchedulePayment] FOREIGN KEY ([SchedulePaymentId]) REFERENCES [Treasury].[SchedulePayment] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_SupplierBankAccount] FOREIGN KEY ([SupplierBankAccountId]) REFERENCES [Common].[SupplierBankAccount] ([Id]),
    CONSTRAINT [FK_VoucherTransaction_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_VoucherTransaction__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [CK_VoucherTransaction];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_CashRegisters];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_Checkbooks];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_CostCenter];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_Currency];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_EntityBankAccounts];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_MainAccounts];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_OperatingUnit];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_Refunds];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_SchedulePayment];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_SupplierBankAccount];


GO
ALTER TABLE [Treasury].[VoucherTransaction] NOCHECK CONSTRAINT [FK_VoucherTransaction_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta bancaria del proveedor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransaction', @level2type = N'COLUMN', @level2name = N'IdUnitOperative';

