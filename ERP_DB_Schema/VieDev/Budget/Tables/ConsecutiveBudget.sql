CREATE TABLE [Budget].[ConsecutiveBudget] (
    [Id]                INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ValidityId]        INT     NOT NULL,
    [FormId]            INT     NOT NULL,
    [NumberConsecutive] INT     NOT NULL,
    [ProcessType]       TINYINT NULL,
    [Used]              BIT     CONSTRAINT [DF_ConsecutiveBudget_Used] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ConsecutiveBudget] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsecutiveBudget_Validity] FOREIGN KEY ([ValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id])
);


GO
ALTER TABLE [Budget].[ConsecutiveBudget] NOCHECK CONSTRAINT [FK_ConsecutiveBudget_Validity];

