CREATE TABLE [dbo].[HCLMEDELI] (
    [CODMEDELI] CHAR (2)  NOT NULL,
    [DESMEDELI] CHAR (40) NOT NULL,
    CONSTRAINT [PK_HCLVIAELI] PRIMARY KEY CLUSTERED ([CODMEDELI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Tipo de Liquido Administrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLMEDELI', @level2type = N'COLUMN', @level2name = N'DESMEDELI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Liqiuido Administrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLMEDELI', @level2type = N'COLUMN', @level2name = N'CODMEDELI';

