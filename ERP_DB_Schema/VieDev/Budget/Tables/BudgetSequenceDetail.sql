CREATE TABLE [Budget].[BudgetSequenceDetail] (
    [Id]                INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseBudgetC] INT    NOT NULL,
    [IdSequense]        INT    NOT NULL,
    [IdOperatingUnit]   INT    NULL,
    [Next]              BIGINT CONSTRAINT [DF_SequenseBudgetD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseBudgetD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseBudgetD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseBudgetD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseBudgetD_SequenseBudgetC] FOREIGN KEY ([IdSequenseBudgetC]) REFERENCES [Budget].[BudgetSequence] ([Id])
);


GO
ALTER TABLE [Budget].[BudgetSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBudgetD_OperatingUnit];


GO
ALTER TABLE [Budget].[BudgetSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBudgetD_Sequense];


GO
ALTER TABLE [Budget].[BudgetSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBudgetD_SequenseBudgetC];

