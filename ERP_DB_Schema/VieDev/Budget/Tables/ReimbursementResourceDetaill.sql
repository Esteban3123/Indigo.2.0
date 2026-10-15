CREATE TABLE [Budget].[ReimbursementResourceDetaill] (
    [Id]                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ReimbursementResourceId]    INT          NOT NULL,
    [PaymentOrderDetailId]       INT          NOT NULL,
    [ObligationModificationId]   INT          NULL,
    [CommitmentModificationId]   INT          NULL,
    [AvailabilityModificationId] INT          NULL,
    [BudgetModificationId]       INT          NULL,
    [Value]                      NUMERIC (18) CONSTRAINT [DF__Reimburse__Value__7AC7528F] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ReimbursementResourceDetaill] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReimbursementResourceDetaill_AvailabilityModification] FOREIGN KEY ([AvailabilityModificationId]) REFERENCES [Budget].[AvailabilityModification] ([Id]),
    CONSTRAINT [FK_ReimbursementResourceDetaill_BudgetModification] FOREIGN KEY ([BudgetModificationId]) REFERENCES [Budget].[BudgetModification] ([Id]),
    CONSTRAINT [FK_ReimbursementResourceDetaill_CommitmentModification] FOREIGN KEY ([CommitmentModificationId]) REFERENCES [Budget].[CommitmentModification] ([Id]),
    CONSTRAINT [FK_ReimbursementResourceDetaill_ObligationModification] FOREIGN KEY ([ObligationModificationId]) REFERENCES [Budget].[ObligationModification] ([Id]),
    CONSTRAINT [FK_ReimbursementResourceDetaill_PaymentOrderDetail] FOREIGN KEY ([PaymentOrderDetailId]) REFERENCES [Budget].[PaymentOrderDetail] ([Id]),
    CONSTRAINT [FK_ReimbursementResourceDetaill_ReimbursementResource] FOREIGN KEY ([ReimbursementResourceId]) REFERENCES [Budget].[ReimbursementResource] ([Id])
);


GO
ALTER TABLE [Budget].[ReimbursementResourceDetaill] NOCHECK CONSTRAINT [FK_ReimbursementResourceDetaill_AvailabilityModification];


GO
ALTER TABLE [Budget].[ReimbursementResourceDetaill] NOCHECK CONSTRAINT [FK_ReimbursementResourceDetaill_BudgetModification];


GO
ALTER TABLE [Budget].[ReimbursementResourceDetaill] NOCHECK CONSTRAINT [FK_ReimbursementResourceDetaill_CommitmentModification];


GO
ALTER TABLE [Budget].[ReimbursementResourceDetaill] NOCHECK CONSTRAINT [FK_ReimbursementResourceDetaill_ObligationModification];


GO
ALTER TABLE [Budget].[ReimbursementResourceDetaill] NOCHECK CONSTRAINT [FK_ReimbursementResourceDetaill_PaymentOrderDetail];


GO
ALTER TABLE [Budget].[ReimbursementResourceDetaill] NOCHECK CONSTRAINT [FK_ReimbursementResourceDetaill_ReimbursementResource];

