CREATE TABLE [dbo].[ParamEducationFormatsVariablesC] (
    [Id]                            INT           IDENTITY (1, 1) NOT NULL,
    [IdParamEducationFormatsGroups] INT           NOT NULL,
    [Code]                          VARCHAR (3)   NOT NULL,
    [Name]                          VARCHAR (150) NULL,
    [ToolTip]                       VARCHAR (150) NULL,
    [Type]                          INT           NOT NULL,
    [ManejoDecimal]                 BIT           NULL,
    [CreationUser]                  CHAR (20)     NOT NULL,
    [CreationDate]                  DATETIME      NOT NULL,
    [ModificationUser]              CHAR (20)     NULL,
    [ModificationDate]              DATETIME      NULL,
    CONSTRAINT [PK_ParamEducationFormatsVariablesC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParamEducationFormatsVariablesC_ParamEducationFormatsGroups] FOREIGN KEY ([IdParamEducationFormatsGroups]) REFERENCES [dbo].[ParamEducationFormatsGroups] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación; timestamp de auditoría que registra cuándo se actualizó la variable de formato educativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de la modificación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (CHAR 20) que realiza la última modificación; auditoría de quién actualiza la variable de formato educativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el usuario quien modifica ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro; timestamp de auditoría que identifica cuándo se registró la variable de formato educativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de creación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (CHAR 20) que crea el registro; auditoría de quién origina la variable de formato educativo en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el usuario de creación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que especifica si el tipo numérico (Type=3) admite decimales; controla precisión en variables numéricas de formato educativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ManejoDecimal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si maneja decimal el numérico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ManejoDecimal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ManejoDecimal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dato de la variable: 1-Boolean, 2-String, 3-Numérico, 4-Lista, 5-Fecha (dd/mm/yyyy), 6-Fecha (mm/yyyy); determina el formato de entrada y validación en formularios educativos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Boolean  2 - String  3 - Numérico  4 - Lista  5 - Fecha (dd/mm/yyyy)  6 - Fecha (mm/yyyy)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto de ayuda o descripción contextual (VARCHAR 150) que se muestra al usuario en interfaces de educación y capacitación para explicar la variable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ToolTip';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Tooltip de la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ToolTip';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ToolTip';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 150) de la variable de formato educativo; etiqueta legible para usuarios en módulos de educación, capacitación y material de instrucción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Nombre de la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de 3 caracteres (VARCHAR 3) que identifica la variable dentro del grupo; utilizado como referencia corta en sistemas educativos, capacitación y formación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que vincula con ParamEducationFormatsGroups; identifica el grupo de formato educativo al que pertenece la variable. Relación obligatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Tiene relación con la tabla ParamEducationFormatsGroups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsGroups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY), consecutivo autoincrementable de la variable de formato educativo en la tabla ParamEducationFormatsVariablesC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variables configuradas para los formatos de educación al paciente. Cada variable pertenece a un grupo de formato educativo y define el tipo de dato, nombre visible, ayuda contextual y si maneja decimales, permitiendo personalizar los campos que se registran en las actividades de educación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC';
