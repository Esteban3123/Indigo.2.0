CREATE TABLE [Inventory].[EntranceVoucherDevolutionObligationBudget] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EntranceVoucherDevolutionId] INT             NOT NULL,
    [ObligationDetailId]          INT             NOT NULL,
    [Value]                       DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_EntranceVoucherDevolutionObligationBudget] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntranceVoucherDevolutionObligationBudget_EntranceVoucherDevolution] FOREIGN KEY ([EntranceVoucherDevolutionId]) REFERENCES [Inventory].[EntranceVoucherDevolution] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolutionObligationBudget_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[EntranceVoucherDevolutionObligationBudget] NOCHECK CONSTRAINT [FK_EntranceVoucherDevolutionObligationBudget_EntranceVoucherDevolution];


GO
ALTER TABLE [Inventory].[EntranceVoucherDevolutionObligationBudget] NOCHECK CONSTRAINT [FK_EntranceVoucherDevolutionObligationBudget_ObligationDetail];

