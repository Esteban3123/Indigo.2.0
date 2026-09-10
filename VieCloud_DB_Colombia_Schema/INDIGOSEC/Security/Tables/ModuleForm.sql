CREATE TABLE [Security].[ModuleForm] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [IdModule]  INT NOT NULL,
    [IdTitle]   INT NOT NULL,
    [IdForm]    INT NOT NULL,
    [FormOrder] INT NOT NULL,
    CONSTRAINT [PK_ModuleForm] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ModuleForm_Form] FOREIGN KEY ([IdForm]) REFERENCES [Security].[Form] ([Id]),
    CONSTRAINT [FK_ModuleForm_Module] FOREIGN KEY ([IdModule]) REFERENCES [Security].[Module] ([Id]),
    CONSTRAINT [FK_ModuleForm_Title] FOREIGN KEY ([IdTitle]) REFERENCES [Security].[Title] ([Id])
);

