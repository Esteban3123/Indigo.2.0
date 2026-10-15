CREATE TABLE [Inventory].[RelatedSupplieMedicine] (
    [Id]       INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ATCId]    INT     NOT NULL,
    [ItemType] TINYINT NOT NULL,
    [SourceId] INT     NOT NULL,
    CONSTRAINT [PK_RelatedSupplieMedicine] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RelatedSupplieMedicine_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id])
);


GO
ALTER TABLE [Inventory].[RelatedSupplieMedicine] NOCHECK CONSTRAINT [FK_RelatedSupplieMedicine_ATC];

