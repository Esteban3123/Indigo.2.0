CREATE TABLE [SelfService].[RolFormAction] (
    [Id]           INT IDENTITY (1, 1) NOT NULL,
    [RoleId]       INT NOT NULL,
    [FormActionId] INT NOT NULL,
    [Status]       BIT CONSTRAINT [DF_RolFormAction_Status] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RolFormAction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RolFormAction_FormAction] FOREIGN KEY ([FormActionId]) REFERENCES [SelfService].[FormAction] ([Id]),
    CONSTRAINT [FK_RolFormAction_Role] FOREIGN KEY ([RoleId]) REFERENCES [SelfService].[Role] ([Id])
);

