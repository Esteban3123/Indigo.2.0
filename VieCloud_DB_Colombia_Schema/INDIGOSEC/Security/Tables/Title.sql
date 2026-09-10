CREATE TABLE [Security].[Title] (
    [Id]    INT          NOT NULL,
    [Name]  VARCHAR (60) NOT NULL,
    [State] BIT          NOT NULL,
    CONSTRAINT [PK_Title] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de Titulos Agrupadores', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave Principal', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Title', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre Titulo', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Title', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado Titulo', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Title', @level2type = N'COLUMN', @level2name = N'State';

