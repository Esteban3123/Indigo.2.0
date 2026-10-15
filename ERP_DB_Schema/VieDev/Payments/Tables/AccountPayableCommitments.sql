CREATE TABLE [Payments].[AccountPayableCommitments] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountPayableId]   INT             NOT NULL,
    [CommitmentDetailId] INT             NOT NULL,
    [Value]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_AccountPayableCommitments__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableCommitments_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_AccountPayableCommitments_CommitmentDetail] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayableCommitments] NOCHECK CONSTRAINT [FK_AccountPayableCommitments_AccountPayable];


GO
ALTER TABLE [Payments].[AccountPayableCommitments] NOCHECK CONSTRAINT [FK_AccountPayableCommitments_CommitmentDetail];

