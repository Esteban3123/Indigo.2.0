CREATE TABLE [dbo].[ParamEducationFormatsXVariables] (
    [Id]                                INT IDENTITY (1, 1) NOT NULL,
    [IdParamEducationFormatsC]          INT NOT NULL,
    [IdParamEducationFormatsVariablesC] INT NOT NULL,
    [Required]                          BIT NOT NULL,
    [Status]                            BIT NOT NULL,
    CONSTRAINT [PK_ParamEducationFormatsXVariables] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParamEducationFormatsXVariables_ParamEducationFormatsC] FOREIGN KEY ([IdParamEducationFormatsC]) REFERENCES [dbo].[ParamEducationFormatsC] ([Id]),
    CONSTRAINT [FK_ParamEducationFormatsXVariables_ParamEducationFormatsVariablesC] FOREIGN KEY ([IdParamEducationFormatsVariablesC]) REFERENCES [dbo].[ParamEducationFormatsVariablesC] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Activo, 0=Inactivo) que guarda el estado de activación o vigencia de la relación entre formato y variable educativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  estado  1 = Si  0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) que especifica si la variable del formato educativo es obligatoria o requerida para completar el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'Required';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Requerido  1  = Si       0  = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'Required';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'Required';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el identificador de la variable/campo del formato educativo en ParamEducationFormatsVariablesC; identifica qué parámetro o variable educativa se asigna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda los id de parametros educacion formatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el identificador del formato educativo en ParamEducationFormatsC; identifica qué formato educativo o de capacitación aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la identidad  "ID" de los parametros educacion formatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY), consecutivo correlativo de la tabla de relación entre formatos educativos y sus variables.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre formatos de educación al paciente y las variables que los componen, indicando cuáles variables son obligatorias y si la relación está activa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables';
