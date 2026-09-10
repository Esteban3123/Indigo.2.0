CREATE TABLE [Security].[Countries] (
    [Id]       TINYINT       IDENTITY (1, 1) NOT NULL,
    [Code]     VARCHAR (3)   NOT NULL,
    [Name]     VARCHAR (200) NOT NULL,
    [Flagcode] VARCHAR (10)  NULL,
    CONSTRAINT [PK_Countries_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Countries', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del pais segun INE', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Countries', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del pais segun INE', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Countries', @level2type = N'COLUMN', @level2name = N'Name';

