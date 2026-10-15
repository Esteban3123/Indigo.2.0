CREATE TABLE [dbo].[SEGrolesu] (
    [codigorol] CHAR (4)  NOT NULL,
    [descrirol] CHAR (60) NOT NULL,
    CONSTRAINT [PK_SEGrolesu] PRIMARY KEY CLUSTERED ([codigorol] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Rol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGrolesu', @level2type = N'COLUMN', @level2name = N'descrirol';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Rol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGrolesu', @level2type = N'COLUMN', @level2name = N'codigorol';

