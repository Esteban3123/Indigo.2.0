CREATE TABLE [Budget].[SuspensionCancellationDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SuspensionCancellationId] INT             NOT NULL,
    [SuspensionDetailId]       INT             NOT NULL,
    [Value]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_SuspensionAnnularDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SuspensionAnnularDetail_SuspensionAnnular] FOREIGN KEY ([SuspensionCancellationId]) REFERENCES [Budget].[SuspensionCancellation] ([Id]),
    CONSTRAINT [FK_SuspensionAnnularDetail_SuspensionDetail] FOREIGN KEY ([SuspensionDetailId]) REFERENCES [Budget].[SuspensionDetail] ([Id])
);


GO
ALTER TABLE [Budget].[SuspensionCancellationDetail] NOCHECK CONSTRAINT [FK_SuspensionAnnularDetail_SuspensionAnnular];


GO
ALTER TABLE [Budget].[SuspensionCancellationDetail] NOCHECK CONSTRAINT [FK_SuspensionAnnularDetail_SuspensionDetail];

