CREATE TABLE [dbo].[INIVERIES] (
    [CODNIVRIE] VARCHAR (20)  NOT NULL,
    [DESNIVRIE] VARCHAR (100) NOT NULL,
    [ESTNIVRIE] BIT           NOT NULL,
    CONSTRAINT [PK_INIVERIES] PRIMARY KEY CLUSTERED ([CODNIVRIE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del nivel de riesgo; indica si el nivel está vigente o desactivado en el sistema, BIT (1=activo, 0=inactivo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES', @level2type = N'COLUMN', @level2name = N'ESTNIVRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del nivel de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES', @level2type = N'COLUMN', @level2name = N'ESTNIVRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES', @level2type = N'COLUMN', @level2name = N'ESTNIVRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del nivel de riesgo; texto que detalla la clasificación de riesgo (ej: bajo, medio, alto, crítico), VARCHAR(100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES', @level2type = N'COLUMN', @level2name = N'DESNIVRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Nivel de Riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES', @level2type = N'COLUMN', @level2name = N'DESNIVRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES', @level2type = N'COLUMN', @level2name = N'DESNIVRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del nivel de riesgo (clave primaria); identificador del nivel de riesgo clínico o administrativo, VARCHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES', @level2type = N'COLUMN', @level2name = N'CODNIVRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del nivel de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES', @level2type = N'COLUMN', @level2name = N'CODNIVRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES', @level2type = N'COLUMN', @level2name = N'CODNIVRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Niveles de riesgo o categorías de clasificación utilizadas en el sistema. Guarda los distintos niveles definidos para clasificar elementos según su importancia, prioridad o riesgo dentro del proceso de atención o gestión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INIVERIES';
