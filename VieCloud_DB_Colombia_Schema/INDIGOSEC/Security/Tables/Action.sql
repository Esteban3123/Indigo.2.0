CREATE TABLE [Security].[Action] (
    [Id]    INT          NOT NULL,
    [Name]  VARCHAR (60) NOT NULL,
    [State] BIT          NOT NULL,
    CONSTRAINT [PK_Action] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de acciones de formulario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Llave principal', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de accion', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de accion', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Action', @level2type = N'COLUMN', @level2name = N'State';

