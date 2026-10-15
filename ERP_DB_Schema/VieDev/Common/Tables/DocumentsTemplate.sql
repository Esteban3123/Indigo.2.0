CREATE TABLE [Common].[DocumentsTemplate] (
    [Id]       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]     VARCHAR (20)  NOT NULL,
    [Name]     VARCHAR (200) NOT NULL,
    [Template] VARCHAR (MAX) NOT NULL,
    [Status]   BIT           NOT NULL,
    CONSTRAINT [PK_DocumentsTemplate] PRIMARY KEY CLUSTERED ([Id] ASC)
);

