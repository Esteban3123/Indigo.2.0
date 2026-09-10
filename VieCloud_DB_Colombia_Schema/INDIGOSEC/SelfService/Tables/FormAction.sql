CREATE TABLE [SelfService].[FormAction] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [FormId]   INT NOT NULL,
    [ActionId] INT NOT NULL,
    CONSTRAINT [PK_FormAction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormAction_Action] FOREIGN KEY ([ActionId]) REFERENCES [SelfService].[Action] ([Id]),
    CONSTRAINT [FK_FormAction_Form] FOREIGN KEY ([FormId]) REFERENCES [SelfService].[Form] ([Id])
);

