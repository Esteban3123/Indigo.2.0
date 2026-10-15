CREATE TABLE [dbo].[INIVERIES] (
    [CODNIVRIE] VARCHAR (20)  NOT NULL,
    [DESNIVRIE] VARCHAR (100) NOT NULL,
    [ESTNIVRIE] BIT           NOT NULL,
    CONSTRAINT [PK_INIVERIES] PRIMARY KEY CLUSTERED ([CODNIVRIE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del nivel de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES', @level2type = N'COLUMN', @level2name = N'ESTNIVRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Nivel de Riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES', @level2type = N'COLUMN', @level2name = N'DESNIVRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del nivel de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES', @level2type = N'COLUMN', @level2name = N'CODNIVRIE';

