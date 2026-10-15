CREATE TABLE [Inventory].[RequestType] (
    [Id]          TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    [TimeStamp]   ROWVERSION   NOT NULL,
    CONSTRAINT [PK_RequestType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

