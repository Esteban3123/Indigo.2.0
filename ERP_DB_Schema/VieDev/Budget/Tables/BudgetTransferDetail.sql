CREATE TABLE [Budget].[BudgetTransferDetail] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferId]    INT             NOT NULL,
    [BudgetId]      INT             NOT NULL,
    [RevenueTypeId] INT             NOT NULL,
    [Nature]        TINYINT         NOT NULL,
    [Value]         NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_TransferDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BudgetTransferDetail_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_TransferDetail_RevenueType] FOREIGN KEY ([RevenueTypeId]) REFERENCES [Budget].[RevenueType] ([Id]),
    CONSTRAINT [FK_TransferDetail_Transfer] FOREIGN KEY ([TransferId]) REFERENCES [Budget].[BudgetTransfer] ([Id])
);


GO
ALTER TABLE [Budget].[BudgetTransferDetail] NOCHECK CONSTRAINT [FK_BudgetTransferDetail_Budget];


GO
ALTER TABLE [Budget].[BudgetTransferDetail] NOCHECK CONSTRAINT [FK_TransferDetail_RevenueType];


GO
ALTER TABLE [Budget].[BudgetTransferDetail] NOCHECK CONSTRAINT [FK_TransferDetail_Transfer];

