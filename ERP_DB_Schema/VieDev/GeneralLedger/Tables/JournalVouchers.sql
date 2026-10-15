CREATE TABLE [GeneralLedger].[JournalVouchers] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountingMovementId] INT           NOT NULL,
    [Consecutive]          BIGINT        NOT NULL,
    [LegalBookId]          INT           CONSTRAINT [DF_JournalVouchers_LegalBookId] DEFAULT ((1)) NOT NULL,
    [IdJournalVoucher]     INT           NOT NULL,
    [VoucherDate]          DATETIME      NOT NULL,
    [Imported]             BIT           CONSTRAINT [DF_JournalVouchers_Imported] DEFAULT ((0)) NOT NULL,
    [Status]               TINYINT       NOT NULL,
    [Detail]               VARCHAR (MAX) CONSTRAINT [DF_Accounting_Detail] DEFAULT ('-') NULL,
    [EntityCode]           VARCHAR (20)  NULL,
    [EntityId]             INT           NULL,
    [EntityName]           VARCHAR (250) NULL,
    [IsClosedYear]         TINYINT       CONSTRAINT [DF_Accounting_IsClosedYear] DEFAULT ((0)) NOT NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [ConfirmationUser]     VARCHAR (20)  NULL,
    [ConfirmationDate]     DATETIME      NULL,
    [AnnulmentUser]        VARCHAR (20)  NULL,
    [AnnulmentDate]        DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    [YearMovement]         INT           NOT NULL,
    CONSTRAINT [PK_JournalVouchers__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Accounting_DocumentType] FOREIGN KEY ([IdJournalVoucher]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_JournalVouchers_AccountingMovement] FOREIGN KEY ([AccountingMovementId]) REFERENCES [GeneralLedger].[AccountingMovement] ([Id]),
    CONSTRAINT [FK_JournalVouchers_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[JournalVouchers] NOCHECK CONSTRAINT [FK_Accounting_DocumentType];


GO
ALTER TABLE [GeneralLedger].[JournalVouchers] NOCHECK CONSTRAINT [FK_JournalVouchers_AccountingMovement];


GO
ALTER TABLE [GeneralLedger].[JournalVouchers] NOCHECK CONSTRAINT [FK_JournalVouchers_LegalBook];


GO
CREATE NONCLUSTERED INDEX [IX_JournalVouchers_Consecutive_IdJournalVoucher_LegalBookId_YearMovement]
    ON [GeneralLedger].[JournalVouchers]([Consecutive] ASC, [IdJournalVoucher] ASC, [LegalBookId] ASC, [YearMovement] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_JournalVouchers_AccountingMovementId_LegalBookId]
    ON [GeneralLedger].[JournalVouchers]([AccountingMovementId] ASC, [LegalBookId] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_JournalVouchers_Consecutive_LegalBookId_IdJournalVoucher_YearMovement]
    ON [GeneralLedger].[JournalVouchers]([Consecutive] ASC, [LegalBookId] ASC, [IdJournalVoucher] ASC, [YearMovement] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si el comprobante fue generado por el proceso de cierre de año  0 - No Aplica (Comprobante normal)  1 - Comprobante de saldo Inicial (Inserta en el mes 14 de los saldos)  2 - Comrpobante de cierre (Inserta en el mes 13 de los saldos)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'JournalVouchers', @level2type = N'COLUMN', @level2name = N'IsClosedYear';

