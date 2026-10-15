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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del concepto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Naturaleza', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del traslado de la cabecera', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'PaymentTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Id';

