CREATE TABLE [Security].[FormAction] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [IdForm]   INT NOT NULL,
    [IdAction] INT NOT NULL,
    CONSTRAINT [PK_FormAction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormAction_Action] FOREIGN KEY ([IdAction]) REFERENCES [Security].[Action] ([Id]),
    CONSTRAINT [FK_FormAction_Form] FOREIGN KEY ([IdForm]) REFERENCES [Security].[Form] ([Id]),
    CONSTRAINT [UC_FormAction] UNIQUE NONCLUSTERED ([IdForm] ASC, [IdAction] ASC)
);

