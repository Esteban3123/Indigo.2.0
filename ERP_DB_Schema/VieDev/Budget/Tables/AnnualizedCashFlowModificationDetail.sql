CREATE TABLE [Budget].[AnnualizedCashFlowModificationDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PACModificationId]    INT             NOT NULL,
    [AnnualizedCashFlowId] INT             NOT NULL,
    [Nature]               TINYINT         NOT NULL,
    [Value]                NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_PACModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnualizedCashFlowModificationDetail_AnnualizedCashFlow] FOREIGN KEY ([AnnualizedCashFlowId]) REFERENCES [Budget].[AnnualizedCashFlow] ([Id]),
    CONSTRAINT [FK_PACModificationDetail_PACModification] FOREIGN KEY ([PACModificationId]) REFERENCES [Budget].[AnnualizedCashFlowModification] ([Id]),
    CONSTRAINT [UQ_AnnualizedCashFlowModificationDetail__PACModificationId__AnnualizedCashFlowId] UNIQUE NONCLUSTERED ([PACModificationId] ASC, [AnnualizedCashFlowId] ASC)
);


GO
ALTER TABLE [Budget].[AnnualizedCashFlowModificationDetail] NOCHECK CONSTRAINT [FK_AnnualizedCashFlowModificationDetail_AnnualizedCashFlow];


GO
ALTER TABLE [Budget].[AnnualizedCashFlowModificationDetail] NOCHECK CONSTRAINT [FK_PACModificationDetail_PACModification];

