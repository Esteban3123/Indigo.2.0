CREATE TABLE [Budget].[RecognitionModificationDetail] (
    [Id]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RecognitionModificationId] INT          NOT NULL,
    [RecognitionDetailId]       INT          NOT NULL,
    [Nature]                    TINYINT      NOT NULL,
    [Value]                     NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_RecognitionModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RecognitionModificationDetail_RecognitionDetail] FOREIGN KEY ([RecognitionDetailId]) REFERENCES [Budget].[RecognitionDetail] ([Id]),
    CONSTRAINT [FK_RecognitionModificationDetail_RecognitionModification] FOREIGN KEY ([RecognitionModificationId]) REFERENCES [Budget].[RecognitionModification] ([Id])
);


GO
ALTER TABLE [Budget].[RecognitionModificationDetail] NOCHECK CONSTRAINT [FK_RecognitionModificationDetail_RecognitionDetail];


GO
ALTER TABLE [Budget].[RecognitionModificationDetail] NOCHECK CONSTRAINT [FK_RecognitionModificationDetail_RecognitionModification];

