CREATE TABLE [Budget].[BudgetModificationDetail] (
    [Id]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ModificationId] INT             NOT NULL,
    [BudgetId]       INT             NOT NULL,
    [RevenueTypeId]  INT             NOT NULL,
    [Nature]         TINYINT         NOT NULL,
    [Value]          NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_ModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ModificationDetail_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_ModificationDetail_Modification] FOREIGN KEY ([ModificationId]) REFERENCES [Budget].[BudgetModification] ([Id]),
    CONSTRAINT [FK_ModificationDetail_RevenueType] FOREIGN KEY ([RevenueTypeId]) REFERENCES [Budget].[RevenueType] ([Id])
);


GO
ALTER TABLE [Budget].[BudgetModificationDetail] NOCHECK CONSTRAINT [FK_ModificationDetail_Budget];


GO
ALTER TABLE [Budget].[BudgetModificationDetail] NOCHECK CONSTRAINT [FK_ModificationDetail_Modification];


GO
ALTER TABLE [Budget].[BudgetModificationDetail] NOCHECK CONSTRAINT [FK_ModificationDetail_RevenueType];

