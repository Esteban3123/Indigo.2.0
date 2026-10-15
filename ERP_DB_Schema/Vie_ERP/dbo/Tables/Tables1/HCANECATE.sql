CREATE TABLE [dbo].[HCANECATE] (
    [CODCATEGO] CHAR (2)     NOT NULL,
    [NOMCATEGO] VARCHAR (60) NOT NULL,
    CONSTRAINT [PK_HCANECATE] PRIMARY KEY CLUSTERED ([CODCATEGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Categoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANECATE', @level2type = N'COLUMN', @level2name = N'NOMCATEGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Categoria en Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANECATE', @level2type = N'COLUMN', @level2name = N'CODCATEGO';

