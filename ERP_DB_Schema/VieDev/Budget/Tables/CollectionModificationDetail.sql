CREATE TABLE [Budget].[CollectionModificationDetail] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CollectionModificationId] INT          NOT NULL,
    [CollectionDetailId]       INT          NOT NULL,
    [Nature]                   TINYINT      NOT NULL,
    [Value]                    NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CollectionModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CollectionModificationDetail_CollectionDetail] FOREIGN KEY ([CollectionDetailId]) REFERENCES [Budget].[CollectionDetail] ([Id]),
    CONSTRAINT [FK_CollectionModificationDetail_CollectionModification] FOREIGN KEY ([CollectionModificationId]) REFERENCES [Budget].[CollectionModification] ([Id])
);


GO
ALTER TABLE [Budget].[CollectionModificationDetail] NOCHECK CONSTRAINT [FK_CollectionModificationDetail_CollectionDetail];


GO
ALTER TABLE [Budget].[CollectionModificationDetail] NOCHECK CONSTRAINT [FK_CollectionModificationDetail_CollectionModification];

