CREATE TABLE [Payments].[PaymentsNoteDetails] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPaymentsNote]               INT             NOT NULL,
    [IdAccountPayableConceptNotes] INT             NOT NULL,
    [IdAccount]                    INT             NOT NULL,
    [IdThirdParty]                 INT             NOT NULL,
    [IdCostCenter]                 INT             NULL,
    [BaseValue]                    DECIMAL (18, 2) NULL,
    [BillingValue]                 DECIMAL (18, 2) NULL,
    [Value]                        DECIMAL (18, 2) NOT NULL,
    [IdRetentionConcept]           INT             NULL,
    [Nature]                       TINYINT         NOT NULL,
    [Comments]                     VARCHAR (500)   NULL,
    [Percentage]                   DECIMAL (6, 3)  NULL,
    [DiscountableIVA]              BIT             NULL,
    [IdGeneralLedgerIVA]           INT             NULL,
    [IVAValue]                     DECIMAL (18, 2) NULL,
    [TotalConceptValue]            DECIMAL (18, 2) NULL,
    [TaxRegistration]              TINYINT         NULL,
    CONSTRAINT [PK_PaymentsNoteDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentsNoteDetail_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PaymentsNoteDetail_MainAccount] FOREIGN KEY ([IdAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PaymentsNoteDetail_PaymentsNote] FOREIGN KEY ([IdPaymentsNote]) REFERENCES [Payments].[PaymentNotes] ([Id]),
    CONSTRAINT [FK_PaymentsNoteDetail_RetentionConcept] FOREIGN KEY ([IdRetentionConcept]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_PaymentsNoteDetail_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_PaymentsNoteDetails_AccountPayableConceptNotes] FOREIGN KEY ([IdAccountPayableConceptNotes]) REFERENCES [Payments].[AccountPayableConceptNotes] ([Id]),
    CONSTRAINT [FK_PaymentsNoteDetails_IdGeneralLedgerIVA] FOREIGN KEY ([IdGeneralLedgerIVA]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);


GO
ALTER TABLE [Payments].[PaymentsNoteDetails] NOCHECK CONSTRAINT [FK_PaymentsNoteDetail_CostCenter];


GO
ALTER TABLE [Payments].[PaymentsNoteDetails] NOCHECK CONSTRAINT [FK_PaymentsNoteDetail_MainAccount];


GO
ALTER TABLE [Payments].[PaymentsNoteDetails] NOCHECK CONSTRAINT [FK_PaymentsNoteDetail_PaymentsNote];


GO
ALTER TABLE [Payments].[PaymentsNoteDetails] NOCHECK CONSTRAINT [FK_PaymentsNoteDetail_RetentionConcept];


GO
ALTER TABLE [Payments].[PaymentsNoteDetails] NOCHECK CONSTRAINT [FK_PaymentsNoteDetail_ThirdParty];


GO
ALTER TABLE [Payments].[PaymentsNoteDetails] NOCHECK CONSTRAINT [FK_PaymentsNoteDetails_AccountPayableConceptNotes];


GO
ALTER TABLE [Payments].[PaymentsNoteDetails] NOCHECK CONSTRAINT [FK_PaymentsNoteDetails_IdGeneralLedgerIVA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro fiscal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'TaxRegistration';

