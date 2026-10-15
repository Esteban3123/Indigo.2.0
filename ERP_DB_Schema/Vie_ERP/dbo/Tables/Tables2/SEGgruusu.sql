CREATE TABLE [dbo].[SEGgruusu] (
    [codgrupou] CHAR (3)  NOT NULL,
    [descrigru] CHAR (60) NOT NULL,
    CONSTRAINT [PK_SEGgrupus] PRIMARY KEY CLUSTERED ([codgrupou] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGgruusu', @level2type = N'COLUMN', @level2name = N'descrigru';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Grupo de Usuarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGgruusu', @level2type = N'COLUMN', @level2name = N'codgrupou';

