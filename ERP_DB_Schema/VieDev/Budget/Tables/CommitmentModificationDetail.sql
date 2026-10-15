CREATE TABLE [Budget].[CommitmentModificationDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CommitmentModificationId] INT             NOT NULL,
    [CommitmentDetailId]       INT             NOT NULL,
    [Nature]                   TINYINT         NOT NULL,
    [Value]                    NUMERIC (18, 2) NOT NULL,
    [IsLogBase]                BIT             NOT NULL,
    CONSTRAINT [PK_CommitmentModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CommitmentModificationDetail_CommitmentDetail] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_CommitmentModificationDetail_CommitmentModification] FOREIGN KEY ([CommitmentModificationId]) REFERENCES [Budget].[CommitmentModification] ([Id])
);


GO
ALTER TABLE [Budget].[CommitmentModificationDetail] NOCHECK CONSTRAINT [FK_CommitmentModificationDetail_CommitmentDetail];


GO
ALTER TABLE [Budget].[CommitmentModificationDetail] NOCHECK CONSTRAINT [FK_CommitmentModificationDetail_CommitmentModification];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle del compromiso.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentModificationDetail', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';

