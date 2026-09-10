CREATE TABLE [Security].[UserOperatingUnit] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [IdUser]          INT           NOT NULL,
    [IdContainer]     INT           NOT NULL,
    [IdOperatingUnit] INT           NOT NULL,
    [Status]          BIT           CONSTRAINT [DF_UserOperatingUnit_Status] DEFAULT ((1)) NOT NULL,
    [ReportPath]      VARCHAR (200) NULL,
    CONSTRAINT [PK_UserOperatingUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserOperatingUnit_Containers] FOREIGN KEY ([IdContainer]) REFERENCES [Security].[Containers] ([Id]),
    CONSTRAINT [FK_UserOperatingUnit_User] FOREIGN KEY ([IdUser]) REFERENCES [Security].[User] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id usuario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserOperatingUnit', @level2type = N'COLUMN', @level2name = N'IdUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del contenedor de seguridad', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserOperatingUnit', @level2type = N'COLUMN', @level2name = N'IdContainer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserOperatingUnit', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro 1 = Activo , 0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserOperatingUnit', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta del reporte personalizado', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'UserOperatingUnit', @level2type = N'COLUMN', @level2name = N'ReportPath';

