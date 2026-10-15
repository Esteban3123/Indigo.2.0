CREATE TABLE [Budget].[ObligationModificationDetail] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ObligationModificationId] INT          NOT NULL,
    [ObligationDetailId]       INT          NOT NULL,
    [ExpiredDate]              DATETIME     NOT NULL,
    [Nature]                   TINYINT      NOT NULL,
    [Value]                    NUMERIC (18) NOT NULL,
    [IsLogBase]                BIT          NOT NULL,
    CONSTRAINT [PK_ObligationModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ObligationModificationDetail_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id]),
    CONSTRAINT [FK_ObligationModificationDetail_ObligationModification] FOREIGN KEY ([ObligationModificationId]) REFERENCES [Budget].[ObligationModification] ([Id])
);


GO
ALTER TABLE [Budget].[ObligationModificationDetail] NOCHECK CONSTRAINT [FK_ObligationModificationDetail_ObligationDetail];


GO
ALTER TABLE [Budget].[ObligationModificationDetail] NOCHECK CONSTRAINT [FK_ObligationModificationDetail_ObligationModification];

