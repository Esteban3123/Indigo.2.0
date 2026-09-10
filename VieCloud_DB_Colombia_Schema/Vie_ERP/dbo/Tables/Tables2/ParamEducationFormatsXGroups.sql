CREATE TABLE [dbo].[ParamEducationFormatsXGroups] (
    [Id]                            INT IDENTITY (1, 1) NOT NULL,
    [IdParamEducationFormatsC]      INT NOT NULL,
    [IdParamEducationFormatsGroups] INT NOT NULL,
    CONSTRAINT [PK_ParamEducationFormatsXGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParamEducationFormatsXGroups_ParamEducationFormatsGroups] FOREIGN KEY ([IdParamEducationFormatsGroups]) REFERENCES [dbo].[ParamEducationFormatsGroups] ([Id]),
    CONSTRAINT [FK_ParamEducationFormatsXGroups_ParamEducationFormatsVariablesC] FOREIGN KEY ([IdParamEducationFormatsC]) REFERENCES [dbo].[ParamEducationFormatsC] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de grupo de formatos educativos (FK a ParamEducationFormatsGroups). Clave foránea que referencia la agrupación o categoría de formatos educativos, permite asociar múltiples formatos a un grupo específico de educación en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Tiene relación con la tabla ParamEducationFormatsGroups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsGroups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del formato educativo (FK a ParamEducationFormatsC). Clave foránea que referencia el parámetro o configuración de formato educativo específico, vincula el formato de educación a su grupo correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la identidad  "ID" de los parametros educacion formatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (Identity INT). Clave primaria secuencial que genera automáticamente el número consecutivo de cada asociación entre formatos educativos y sus grupos, garantiza unicidad en la tabla de relaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los formatos de educación parametrizados con los grupos a los que pertenecen, permitiendo asociar un formato educativo (como plantillas de educación al paciente) con uno o varios grupos de clasificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups';
