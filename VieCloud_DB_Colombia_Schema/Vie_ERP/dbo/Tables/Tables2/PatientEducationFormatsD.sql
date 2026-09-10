CREATE TABLE [dbo].[PatientEducationFormatsD] (
    [Id]                                INT           IDENTITY (1, 1) NOT NULL,
    [IdPatientEducationFormatsC]        INT           NOT NULL,
    [IdParamEducationFormatsGroups]     INT           NOT NULL,
    [IdParamEducationFormatsVariablesC] INT           NOT NULL,
    [Value]                             VARCHAR (MAX) NOT NULL,
    [IdParamEducationFormatsVariablesD] INT           NULL,
    CONSTRAINT [PK_PatientEducationFormatsD] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea opcional (nullable) a ParamEducationFormatsVariablesD; referencia sub-variables o detalles adicionales del formato educativo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla ParamEducationFormatsVariablesD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido o valor específico guardado del formato educativo para el paciente (VARCHAR MAX); almacena texto, instrucciones, recomendaciones o datos de la sesión educativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el valor del formato de educación para el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea a ParamEducationFormatsVariablesC; identifica la variable o campo paramétrico maestro del formato educativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla ParamEducationFormatsVariablesC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea a ParamEducationFormatsGroups; referencia el grupo o categoría paramétrica a la que pertenece el formato de educación (ej: prevención, nutrición, medicamentos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla ParamEducationFormatsGroups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsGroups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea a PatientEducationFormatsC; identifica el encabezado o contexto principal del formato educativo asignado al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdPatientEducationFormatsC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla PatientEducationFormatsC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdPatientEducationFormatsC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'IdPatientEducationFormatsC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único (INT IDENTITY) de cada detalle de formato educativo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los formularios de educación al paciente: almacena los valores registrados para cada variable o campo dentro de un grupo de un formulario educativo aplicado al paciente durante su atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PatientEducationFormatsD';
