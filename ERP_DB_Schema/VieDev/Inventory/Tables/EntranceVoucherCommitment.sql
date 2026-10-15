CREATE TABLE [Inventory].[EntranceVoucherCommitment] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EntranceVoucherId]  INT             NOT NULL,
    [CommitmentDetailId] INT             NOT NULL,
    [Value]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_EntranceVoucherCommitment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntranceVoucherCommitment_CommitmentDetail] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_EntranceVoucherCommitment_EntranceVoucher] FOREIGN KEY ([EntranceVoucherId]) REFERENCES [Inventory].[EntranceVoucher] ([Id])
);


GO
ALTER TABLE [Inventory].[EntranceVoucherCommitment] NOCHECK CONSTRAINT [FK_EntranceVoucherCommitment_CommitmentDetail];


GO
ALTER TABLE [Inventory].[EntranceVoucherCommitment] NOCHECK CONSTRAINT [FK_EntranceVoucherCommitment_EntranceVoucher];

