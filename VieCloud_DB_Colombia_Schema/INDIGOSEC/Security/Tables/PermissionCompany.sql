CREATE TABLE [Security].[PermissionCompany] (
    [Id]                     INT IDENTITY (1, 1) NOT NULL,
    [IdUser]                 INT NOT NULL,
    [IdContainer]            INT NOT NULL,
    [IdOperatingUnitDefault] INT NOT NULL,
    [Permission]             BIT NOT NULL,
    [Administrator]          BIT NULL,
    CONSTRAINT [PK_PermissionCompany] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PermissionCompany_Containers] FOREIGN KEY ([IdContainer]) REFERENCES [Security].[Containers] ([Id]),
    CONSTRAINT [FK_PermissionCompany_User] FOREIGN KEY ([IdUser]) REFERENCES [Security].[User] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PermissionCompany]
    ON [Security].[PermissionCompany]([IdUser] ASC, [IdContainer] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario es o no administrador en compañia', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PermissionCompany', @level2type = N'COLUMN', @level2name = N'Administrator';

