CREATE TABLE [AccountManagement].[UserNovelties] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [AssignedUserId] INT            NOT NULL,
    [NoveltyDate]    DATETIME       DEFAULT (getdate()) NOT NULL,
    [Description]    NVARCHAR (300) NOT NULL,
    [IsUserActive]   BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserNovelties_UsersAssignment] FOREIGN KEY ([AssignedUserId]) REFERENCES [AccountManagement].[UsersAssignment] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del usuario debido a la novedad, 1 = activo, 0 = inactivo', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'IsUserActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la novedad', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la creación de la novedad', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'NoveltyDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del usuario asignado a quien se le está creando una novedad', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'AssignedUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Id';

