CREATE TABLE [Payments].[PaymentTransferOtherConcept] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PaymentTransferId]           INT             NOT NULL,
    [AccountPayableConceptNoteId] INT             NOT NULL,
    [MainAccountId]               INT             NOT NULL,
    [ThirdPartyId]                INT             NULL,
    [CostCenterId]                INT             NULL,
    [Nature]                      TINYINT         NOT NULL,
    [Value]                       NUMERIC (20, 2) NOT NULL,
    CONSTRAINT [PK_PaymentTransferOtherConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentTransferOtherConcept_AccountPayableConceptNotes] FOREIGN KEY ([AccountPayableConceptNoteId]) REFERENCES [Payments].[AccountPayableConceptNotes] ([Id]),
    CONSTRAINT [FK_PaymentTransferOtherConcept_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PaymentTransferOtherConcept_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PaymentTransferOtherConcept_PaymentTransfer] FOREIGN KEY ([PaymentTransferId]) REFERENCES [Payments].[PaymentTransfer] ([Id]),
    CONSTRAINT [FK_PaymentTransferOtherConcept_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payments].[PaymentTransferOtherConcept] NOCHECK CONSTRAINT [FK_PaymentTransferOtherConcept_AccountPayableConceptNotes];


GO
ALTER TABLE [Payments].[PaymentTransferOtherConcept] NOCHECK CONSTRAINT [FK_PaymentTransferOtherConcept_CostCenter];


GO
ALTER TABLE [Payments].[PaymentTransferOtherConcept] NOCHECK CONSTRAINT [FK_PaymentTransferOtherConcept_MainAccounts];


GO
ALTER TABLE [Payments].[PaymentTransferOtherConcept] NOCHECK CONSTRAINT [FK_PaymentTransferOtherConcept_PaymentTransfer];


GO
ALTER TABLE [Payments].[PaymentTransferOtherConcept] NOCHECK CONSTRAINT [FK_PaymentTransferOtherConcept_ThirdParty];

