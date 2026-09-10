CREATE TABLE [ClinicalParameters].[HistoryVariables] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [MinimumAge]       INT           NOT NULL,
    [MaximumAge]       INT           NOT NULL,
    [Sex]              INT           NOT NULL,
    [TypeVariable]     INT           NOT NULL,
    [Decimals]         BIT           NULL,
    [CodeScale]        INT           NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [Status]           BIT           NULL,
    CONSTRAINT [PK_HistoryVariables] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo de la variable histórica (BIT: 1=Activa, 0=Inactiva)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación de la variable histórica (DATETIME)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación de la variable histórica (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación de la variable histórica en el sistema (DATETIME)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó la variable histórica en el sistema (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la escala de medición asociada a la variable histórica (INT, FK)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'CodeScale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de escala de variables históricas', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'CodeScale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'CodeScale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si la variable histórica admite valores decimales (BIT: 1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Decimals';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene o no contiene decimales', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Decimals';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Decimals';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dato de la variable histórica: 1=Booleano, 2=Texto, 3=Numérico, 4=Lista, 5=Fecha(dd/MM/yyyy), 6=Fecha(MM/yyyy), 7=Escalas (INT)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'TypeVariable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Boolean  2 - String  3 - Númerico  4 - Lista  5 - Fecha  (dd/MM/yyyy)  6 - Fecha  (MM/yyyy)  7 - Escalas       ', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'TypeVariable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'TypeVariable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género de aplicación de la variable histórica: 1=Masculino, 2=Femenino, 3=Ambos (INT)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Sex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Genero de variables históricas', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Sex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Sex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida para aplicar la variable histórica en pacientes (INT, años)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad máxima de variables históricas', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima permitida para aplicar la variable histórica en pacientes (INT, años)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'MinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad mínima de variables históricas', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'MinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'MinimumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la variable histórica clínica (VARCHAR 100, ej: Frecuencia Cardíaca, Saturación O2, Presión Arterial)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de las variables históricas', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de la variable histórica (INT IDENTITY, clave primaria)', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de variables clínicas o signos vitales parametrizados para historia clínica, con sus rangos de edad, sexo aplicable, tipo de variable y escala de medición. Permite configurar qué parámetros clínicos (como temperatura, presión arterial, glucemia) se registran según el perfil del paciente.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariables';
