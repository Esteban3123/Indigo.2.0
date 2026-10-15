CREATE TABLE [Budget].[AnnualizedCashFlowTransferDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PACTransferId]        INT             NOT NULL,
    [AnnualizedCashFlowId] INT             NOT NULL,
    [Nature]               TINYINT         NOT NULL,
    [Value]                NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_PACTransferDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnualizedCashFlowTransferDetail_AnnualizedCashFlow] FOREIGN KEY ([AnnualizedCashFlowId]) REFERENCES [Budget].[AnnualizedCashFlow] ([Id]),
    CONSTRAINT [FK_PACTransferDetail_PACTransfer] FOREIGN KEY ([PACTransferId]) REFERENCES [Budget].[AnnualizedCashFlowTransfer] ([Id]),
    CONSTRAINT [UQ_AnnualizedCashFlowTransferDetail__PACTransferId__AnnualizedCashFlowId] UNIQUE NONCLUSTERED ([PACTransferId] ASC, [AnnualizedCashFlowId] ASC)
);


GO
ALTER TABLE [Budget].[AnnualizedCashFlowTransferDetail] NOCHECK CONSTRAINT [FK_AnnualizedCashFlowTransferDetail_AnnualizedCashFlow];


GO
ALTER TABLE [Budget].[AnnualizedCashFlowTransferDetail] NOCHECK CONSTRAINT [FK_PACTransferDetail_PACTransfer];

