CREATE TABLE [SelfService].[RolesForms] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [FormId]           INT          NOT NULL,
    [RoleId]           INT          NOT NULL,
    [Status]           BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) NULL,
    [CreationDate]     DATETIME     NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_RolesForms] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RolesForms_Form1] FOREIGN KEY ([FormId]) REFERENCES [SelfService].[Form] ([Id]),
    CONSTRAINT [FK_RolesForms_Role1] FOREIGN KEY ([RoleId]) REFERENCES [SelfService].[Role] ([Id])
);

