CREATE TABLE [Security].[ModuleTitle] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [IdModule] INT NOT NULL,
    [IdTitle]  INT NOT NULL,
    [Order]    INT NULL,
    CONSTRAINT [PK_ModuleTitle] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ModuleTitle_Module] FOREIGN KEY ([IdModule]) REFERENCES [Security].[Module] ([Id]),
    CONSTRAINT [FK_ModuleTitle_Title] FOREIGN KEY ([IdTitle]) REFERENCES [Security].[Title] ([Id])
);

