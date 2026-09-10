CREATE TABLE [SelfService].[FormFolder] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [FormFolderId]     INT           NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_FormFolder] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormFolder_FormFolder] FOREIGN KEY ([FormFolderId]) REFERENCES [SelfService].[FormFolder] ([Id])
);

