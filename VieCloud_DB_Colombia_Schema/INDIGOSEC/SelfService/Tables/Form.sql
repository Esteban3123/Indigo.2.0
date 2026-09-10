CREATE TABLE [SelfService].[Form] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [Type]             TINYINT       NOT NULL,
    [Url]              VARCHAR (300) NOT NULL,
    [IconCls]          VARCHAR (100) NOT NULL,
    [FormFolderId]     INT           NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [FormFolderCode]   VARCHAR (20)  NULL,
    CONSTRAINT [PK_Form] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Form_FormFolder] FOREIGN KEY ([FormFolderId]) REFERENCES [SelfService].[FormFolder] ([Id])
);

