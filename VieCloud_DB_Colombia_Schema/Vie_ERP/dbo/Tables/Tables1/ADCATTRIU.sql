CREATE TABLE [dbo].[ADCATTRIU] (
    [TRIACATEG] CHAR (5)     NOT NULL,
    [TRIANOMCA] NCHAR (250)  NOT NULL,
    [TRIANIVEL] CHAR (1)     NOT NULL,
    [TRIACRITE] NCHAR (100)  NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    [ESTADO]    INT          NULL,
    CONSTRAINT [PK_ADCATTRIU] PRIMARY KEY CLUSTERED ([TRIACATEG] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de clasificación de triage: 1=Activo, 2=Inactivo. Indica si la categoría diagnóstica está habilitada para uso en atención de urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1->Activo 2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría o identificador único de auditoría formal. Rastreo de cambios y conformidad en la tabla de categorización de triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio de clasificación sindromática para triage de urgencias. Categorías diagnósticas: 1=Abdominales, 2=Cardiovasculares, 3=Genitourinarios, 4=Musculoesqueléticos, 5=Neurológicos, 6=Oculares, 7=Otorrino, 8=Patología Dolor, 9=Patología Ocular, 10=Patología Psiquiatría, 11=Patología Piel, 12=Patología Trauma, 13=Respiratorios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIACRITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos de Dx Sindromatico:  1: Abdominales  2: Cardiovasculares  3: GenitoUrinarios  4: MuscoloEsqueleticos  5: Neurologicos  6: Oculares  7: Otorrino  8: Patologia Dolor  9: Patologia Ocular  10: Patologia Psiquiatria  11: Patologia Piel  12: Patologia Trauma  13: Respiratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIACRITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIACRITE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel jerárquico de la clasificación de triage. Define la profundidad o estratificación de la categoría diagnóstica sindromática en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIANIVEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de la Clasificacion del Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIANIVEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIANIVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción narrativa de la categoría diagnóstica sindromática. Nombre o etiqueta textual del diagnóstico para búsqueda y documentación clínica en triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIANOMCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Dx Sindromatico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIANOMCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIANOMCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único de la categoría diagnóstica sindromática de triage. Clave primaria que vincula diagnósticos a protocolos de urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIACATEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Dx Sindromatico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIACATEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU', @level2type = N'COLUMN', @level2name = N'TRIACATEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categorías de atributos o criterios de auditoría de admisión. Guarda la clasificación jerárquica de criterios utilizados para evaluar y auditar procesos de ingreso y atención, con su nombre, nivel y estado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCATTRIU';
