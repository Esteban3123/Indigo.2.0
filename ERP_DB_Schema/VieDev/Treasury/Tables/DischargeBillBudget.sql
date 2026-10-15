CREATE TABLE [Treasury].[DischargeBillBudget] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DischargeBillId]    INT             NOT NULL,
    [ObligationDetailId] INT             NOT NULL,
    [Value]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_DischargeBillBudget__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DischargeBillBudget_DischargeBill] FOREIGN KEY ([DischargeBillId]) REFERENCES [Treasury].[DischargeBill] ([Id]),
    CONSTRAINT [FK_DischargeBillBudget_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id])
);


GO
ALTER TABLE [Treasury].[DischargeBillBudget] NOCHECK CONSTRAINT [FK_DischargeBillBudget_DischargeBill];


GO
ALTER TABLE [Treasury].[DischargeBillBudget] NOCHECK CONSTRAINT [FK_DischargeBillBudget_ObligationDetail];

