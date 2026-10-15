CREATE TABLE [dbo].[CHOFACRIE] (
    [CODFACRIE] CHAR (3)  NOT NULL,
    [DESFACRIE] CHAR (40) NOT NULL,
    CONSTRAINT [PK_CHOFACRIE] PRIMARY KEY CLUSTERED ([CODFACRIE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Factor de Riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOFACRIE', @level2type = N'COLUMN', @level2name = N'DESFACRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Factor de Riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOFACRIE', @level2type = N'COLUMN', @level2name = N'CODFACRIE';

