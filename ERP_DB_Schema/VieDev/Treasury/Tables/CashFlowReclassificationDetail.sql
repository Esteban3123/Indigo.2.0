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

