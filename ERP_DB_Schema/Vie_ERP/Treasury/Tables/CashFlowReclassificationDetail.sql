CREATE TABLE [Treasury].[CashFlowReclassificationDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCashFlowReclassification] INT             NOT NULL,
    [DocumentId]                 INT             NOT NULL,
    [CreditValue]                DECIMAL (18, 2) NULL,
    [DebitValue]                 DECIMAL (18, 2) NULL,
    [PreviousCashFlowConcept]    INT             NULL,
    [CurrentCashFlowConcept]     INT             NULL,
    CONSTRAINT [PK_CashFlowReclassificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashFlowReclassificationDetail_CashFlowReclassification] FOREIGN KEY ([IdCashFlowReclassification]) REFERENCES [Treasury].[CashFlowReclassification] ([Id]),
    CONSTRAINT [FK_CashFlowReclassificationDetail_CurrentCashFlowConcept] FOREIGN KEY ([CurrentCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_CashFlowReclassificationDetail_PreviousCashFlowConcept] FOREIGN KEY ([PreviousCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id])
);


GO
ALTER TABLE [Treasury].[CashFlowReclassificationDetail] NOCHECK CONSTRAINT [FK_CashFlowReclassificationDetail_CashFlowReclassification];


GO
ALTER TABLE [Treasury].[CashFlowReclassificationDetail] NOCHECK CONSTRAINT [FK_CashFlowReclassificationDetail_CurrentCashFlowConcept];


GO
ALTER TABLE [Treasury].[CashFlowReclassificationDetail] NOCHECK CONSTRAINT [FK_CashFlowReclassificationDetail_PreviousCashFlowConcept];


GO
CREATE NONCLUSTERED INDEX [IX_CashFlowReclassificationDetail_PreviousCashFlowConcept]
    ON [Treasury].[CashFlowReclassificationDetail]([PreviousCashFlowConcept] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CashFlowReclassificationDetail_IdCashFlowReclassification]
    ON [Treasury].[CashFlowReclassificationDetail]([IdCashFlowReclassification] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CashFlowReclassificationDetail_CurrentCashFlowConcept]
    ON [Treasury].[CashFlowReclassificationDetail]([CurrentCashFlowConcept] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de flujo de efectivo actual', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'CurrentCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de flujo de efectivo anterior', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'PreviousCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor débito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor crédito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de documento origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'DocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de reclasificación de concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowReclassification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'Id';

