CREATE TABLE [dbo].[HCTLIQELI] (
    [CODLIQELI] CHAR (4)   NOT NULL,
    [DESLIQELI] CHAR (100) NOT NULL,
    [DEFTIPOLI] CHAR (1)   NOT NULL,
    CONSTRAINT [PK_HCTLIQELI] PRIMARY KEY CLUSTERED ([CODLIQELI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Definición binaria del tipo de líquido: 0=N/A, 1=Residuo Gástrico (contenido estomacal drenado), 2=Diuresis (orina); parámetro clínico para balance hidroelectrolítico y monitoreo post-quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTLIQELI', @level2type = N'COLUMN', @level2name = N'DEFTIPOLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el tipo de liquido eliminado  0. N/A  1. Residuo Gastrico  2. Diuresis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTLIQELI', @level2type = N'COLUMN', @level2name = N'DEFTIPOLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTLIQELI', @level2type = N'COLUMN', @level2name = N'DEFTIPOLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del líquido eliminado (orina, heces, drenaje, vómito); identificador legible para balance hídrico y control de ingesta-excreta en paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTLIQELI', @level2type = N'COLUMN', @level2name = N'DESLIQELI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Liquido Eliminado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTLIQELI', @level2type = N'COLUMN', @level2name = N'DESLIQELI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTLIQELI', @level2type = N'COLUMN', @level2name = N'DESLIQELI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único (4 caracteres) del tipo de líquido eliminado; clave primaria para clasificación de excreciones y drenajes en registro clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTLIQELI', @level2type = N'COLUMN', @level2name = N'CODLIQELI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Liquido Eliminado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTLIQELI', @level2type = N'COLUMN', @level2name = N'CODLIQELI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTLIQELI', @level2type = N'COLUMN', @level2name = N'CODLIQELI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de liquidación de elementos o insumos utilizados en historia clínica. Define las categorías o modalidades bajo las cuales se liquidan los elementos, materiales o insumos en los procesos de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTLIQELI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTLIQELI';
