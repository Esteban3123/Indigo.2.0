CREATE TABLE [dbo].[ADNIVELED] (
    [NIVECODIGO] CHAR (3)    NOT NULL,
    [NIVEDESCRI] CHAR (100)  NULL,
    [TIPONIVEL]  VARCHAR (3) NULL,
    [ESTADO]     TINYINT     NULL,
    CONSTRAINT [PK_ADNIVELED] PRIMARY KEY CLUSTERED ([NIVECODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (TINYINT): 1=Activo, 2=Inactivo. Indica si el nivel educativo está vigente en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Activo: 1->Si 2->No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de nivel educativo (VARCHAR 3). Clasificación o categoría del nivel de formación (ej: básico, medio, superior).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'TIPONIVEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Nivel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'TIPONIVEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'TIPONIVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del nivel educativo (CHAR 100). Nombre o detalle del grado de escolaridad (primaria, secundaria, técnico, profesional, postgrado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'NIVEDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion nivel educativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'NIVEDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'NIVEDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del nivel educativo (PK, CHAR 3). Identificador único del nivel de formación académica del paciente o profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'NIVECODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo nivel educativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'NIVECODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED', @level2type = N'COLUMN', @level2name = N'NIVECODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de niveles de atención o clasificación jerárquica de servicios de salud (por ejemplo: nivel 1, 2 o 3 de complejidad). Permite categorizar los servicios o unidades según su nivel de complejidad o tipo dentro del sistema de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADNIVELED';
