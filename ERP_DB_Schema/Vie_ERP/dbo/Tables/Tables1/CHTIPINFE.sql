CREATE TABLE [dbo].[CHTIPINFE] (
    [CODTIPINFE] CHAR (3)  NOT NULL,
    [DESTIPINFE] CHAR (40) NOT NULL,
    CONSTRAINT [PK_CHTIPINFE] PRIMARY KEY CLUSTERED ([CODTIPINFE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Tipo de Infecciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPINFE', @level2type = N'COLUMN', @level2name = N'DESTIPINFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Infecciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPINFE', @level2type = N'COLUMN', @level2name = N'CODTIPINFE';

