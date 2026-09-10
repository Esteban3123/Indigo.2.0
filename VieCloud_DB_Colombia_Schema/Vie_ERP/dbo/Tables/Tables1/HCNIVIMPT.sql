CREATE TABLE [dbo].[HCNIVIMPT] (
    [CODNIVIMP] CHAR (2)   NOT NULL,
    [DESNIVIMP] CHAR (100) NOT NULL,
    [COLNIVIMP] CHAR (50)  NOT NULL,
    CONSTRAINT [PK_HCNIVIMPT] PRIMARY KEY CLUSTERED ([CODNIVIMP] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código hexadecimal del color de visualización para el nivel de importancia. Formato RGB hex (ej: #FF0000). Tipo: CHAR(50). Usado para codificar colores en interfaz gráfica de notas clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT', @level2type = N'COLUMN', @level2name = N'COLNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Hexadecimal del Color', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT', @level2type = N'COLUMN', @level2name = N'COLNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT', @level2type = N'COLUMN', @level2name = N'COLNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del nivel de importancia de la nota clínica. Etiqueta legible para clasificar prioridad o relevancia (ej: Crítico, Alto, Normal, Bajo). Tipo: CHAR(100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT', @level2type = N'COLUMN', @level2name = N'DESNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT', @level2type = N'COLUMN', @level2name = N'DESNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT', @level2type = N'COLUMN', @level2name = N'DESNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de nivel de importancia para notas clínicas, historia clínica. Clave primaria de 2 caracteres. Clasifica prioridad/urgencia de anotaciones en HCE. Tipo: CHAR(2). PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Nivel de Importancia para las Notas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de niveles de importancia o prioridad utilizados en la historia clínica. Permite clasificar registros clínicos según su grado de relevancia o urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPT';
