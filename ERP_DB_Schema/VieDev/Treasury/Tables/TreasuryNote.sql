CREATE TABLE [Treasury].[TreasuryNote] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)    NOT NULL,
    [NoteDate]             DATETIME        CONSTRAINT [DF_TreasuryNote_NoteDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [NoteType]             TINYINT         NOT NULL,
    [OperatingUnitId]      INT             CONSTRAINT [DF_TreasuryNote_OperatingUnitId] DEFAULT ((9)) NOT NULL,
    [CashRegisterId]       INT             NULL,
    [EntityBankAccountId]  INT             NULL,
    [VoucherTransactionId] INT             NULL,
    [CashReceiptId]        INT             NULL,
    [MainAccountId]        INT             NULL,
    [CostCenterId]         INT             NULL,
    [Description]          VARCHAR (300)   NOT NULL,
    [Nature]               TINYINT         NULL,
    [Value]                DECIMAL (18, 2) NULL,
    [Status]               TINYINT         NOT NULL,
    [CreationUser]         VARCHAR (20)    NOT NULL,
    [CreationDate]         DATETIME        NOT NULL,
    [ModificationUser]     VARCHAR (20)    NULL,
    [ModificationDate]     DATETIME        NULL,
    [ConfirmationUser]     VARCHAR (20)    NULL,
    [ConfirmationDate]     DATETIME        NULL,
    [AnnulmentUser]        VARCHAR (20)    NULL,
    [AnnulmentDate]        DATETIME        NULL,
    [TimeStamp]            ROWVERSION      NOT NULL,
    [ConsignmentId]        INT             NULL,
    [CrossingAccountId]    INT             NULL,
    [CurrencyId]           INT             CONSTRAINT [DF__TreasuryN__Curre__460F9A7B] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TreasuryNote] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TreasuryNote_CashReceipts] FOREIGN KEY ([CashReceiptId]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_TreasuryNote_CashRegisters] FOREIGN KEY ([CashRegisterId]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_TreasuryNote_Consignment] FOREIGN KEY ([ConsignmentId]) REFERENCES [Treasury].[Consignment] ([Id]),
    CONSTRAINT [FK_TreasuryNote_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_TreasuryNote_CrossingAccount] FOREIGN KEY ([CrossingAccountId]) REFERENCES [Treasury].[CrossingAccount] ([Id]),
    CONSTRAINT [FK_TreasuryNote_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_TreasuryNote_EntityBankAccounts] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_TreasuryNote_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_TreasuryNote_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_TreasuryNote_VoucherTransaction] FOREIGN KEY ([VoucherTransactionId]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_CashReceipts];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_CashRegisters];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_Consignment];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_CostCenter];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_CrossingAccount];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_Currency];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_EntityBankAccounts];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_MainAccounts];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_OperatingUnit];


GO
ALTER TABLE [Treasury].[TreasuryNote] NOCHECK CONSTRAINT [FK_TreasuryNote_VoucherTransaction];


GO
CREATE NONCLUSTERED INDEX [IX_TreasuryNote_NoteType]
    ON [Treasury].[TreasuryNote]([NoteType] ASC)
    INCLUDE([Code], [NoteDate], [Description], [Status], [CreationUser], [ConsignmentId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la nota   1 - Debito  2 - Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena informacion sobre la nota de tesoreria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta principal.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de la nota   1 - Nota a Cuenta Bancaria  2 - Nota a Caja  3 - Anulación de Comprobante de Egreso  4 - Anulación de Recibo de caja  5 - Anulación de Consignacion  6- Reversión cruce CxC vs CxP  7- Devolucion de Recibo de Caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryNote', @level2type = N'COLUMN', @level2name = N'NoteType';

