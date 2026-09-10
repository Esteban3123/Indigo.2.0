CREATE TABLE [dbo].[CHOTIPBAC] (
    [CODTIPBAC] CHAR (3)  NOT NULL,
    [DESTIPBAC] CHAR (40) NOT NULL,
    CONSTRAINT [PK_CHOTIPBAC] PRIMARY KEY CLUSTERED ([CODTIPBAC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de bacteria causante de infección intrahospitalaria (nosocomial). Nombre o denominación científica/clínica de la cepa bacteriana identificada. Tipo VARCHAR(40), búsqueda por: microorganismo, agente causal, patógeno, bacteria nosocomial, infección hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOTIPBAC', @level2type = N'COLUMN', @level2name = N'DESTIPBAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Tipo de Bacteria - Infeccion Intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOTIPBAC', @level2type = N'COLUMN', @level2name = N'DESTIPBAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOTIPBAC', @level2type = N'COLUMN', @level2name = N'DESTIPBAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único alfanumérico (3 caracteres) del tipo de bacteria causante de infección intrahospitalaria. Identificador de microorganismo en estudios microbiológicos, cultivos y reportes de infecciones nosocomiales. PK, tipo CHAR(3), búsqueda por: código bacteria, tipo microorganismo, agente infeccioso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOTIPBAC', @level2type = N'COLUMN', @level2name = N'CODTIPBAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Bacteria - Infeccion Intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOTIPBAC', @level2type = N'COLUMN', @level2name = N'CODTIPBAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOTIPBAC', @level2type = N'COLUMN', @level2name = N'CODTIPBAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de bacterias o agentes bacteriológicos utilizados en resultados de microbiología y cultivos clínicos. Permite clasificar los gérmenes identificados en exámenes de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOTIPBAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOTIPBAC';
