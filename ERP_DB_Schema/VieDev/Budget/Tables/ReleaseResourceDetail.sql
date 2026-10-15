CREATE TABLE [Budget].[ReleaseResourceDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ReleaseResourceId]          INT             NOT NULL,
    [ObligationDetailId]         INT             NULL,
    [CommimentDetailId]          INT             NULL,
    [ObligationModificationId]   INT             NULL,
    [CommitmentModificationId]   INT             NULL,
    [AvailabilityModificationId] INT             NULL,
    [Value]                      NUMERIC (18, 2) NULL,
    CONSTRAINT [PK_ReleaseResourceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReleaseResourceDetail_AvailabilityModification] FOREIGN KEY ([AvailabilityModificationId]) REFERENCES [Budget].[AvailabilityModification] ([Id]),
    CONSTRAINT [FK_ReleaseResourceDetail_CommitmentDetail] FOREIGN KEY ([CommimentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_ReleaseResourceDetail_CommitmentModification] FOREIGN KEY ([CommitmentModificationId]) REFERENCES [Budget].[CommitmentModification] ([Id]),
    CONSTRAINT [FK_ReleaseResourceDetail_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id]),
    CONSTRAINT [FK_ReleaseResourceDetail_ObligationModification] FOREIGN KEY ([ObligationModificationId]) REFERENCES [Budget].[ObligationModification] ([Id]),
    CONSTRAINT [FK_ReleaseResourceDetail_ReleaseResource] FOREIGN KEY ([ReleaseResourceId]) REFERENCES [Budget].[ReleaseResource] ([Id])
);


GO
ALTER TABLE [Budget].[ReleaseResourceDetail] NOCHECK CONSTRAINT [FK_ReleaseResourceDetail_AvailabilityModification];


GO
ALTER TABLE [Budget].[ReleaseResourceDetail] NOCHECK CONSTRAINT [FK_ReleaseResourceDetail_CommitmentDetail];


GO
ALTER TABLE [Budget].[ReleaseResourceDetail] NOCHECK CONSTRAINT [FK_ReleaseResourceDetail_CommitmentModification];


GO
ALTER TABLE [Budget].[ReleaseResourceDetail] NOCHECK CONSTRAINT [FK_ReleaseResourceDetail_ObligationDetail];


GO
ALTER TABLE [Budget].[ReleaseResourceDetail] NOCHECK CONSTRAINT [FK_ReleaseResourceDetail_ObligationModification];


GO
ALTER TABLE [Budget].[ReleaseResourceDetail] NOCHECK CONSTRAINT [FK_ReleaseResourceDetail_ReleaseResource];

