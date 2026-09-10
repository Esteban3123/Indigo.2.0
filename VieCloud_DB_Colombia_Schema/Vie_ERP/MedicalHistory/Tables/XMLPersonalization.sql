CREATE TABLE [MedicalHistory].[XMLPersonalization] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [IdModel]         INT             NOT NULL,
    [IdGroup]         INT             NOT NULL,
    [OriginModelName] VARCHAR (30)    NOT NULL,
    [OriginGroupName] VARCHAR (30)    NULL,
    [XML]             VARBINARY (MAX) NOT NULL,
    [UserCreation]    CHAR (20)       NOT NULL,
    [DateCreation]    DATETIME        NOT NULL,
    [UserModify]      CHAR (20)       NULL,
    [DateModify]      DATETIME        NULL,
    CONSTRAINT [PK_XMLPersonalization] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación de la personalización/customización del formulario de historia clínica. DATETIME, nullable.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'DateModify';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se modifica la customizacion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'DateModify';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'DateModify';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (login/identificación) que realizó la última modificación de la customización. CHAR(20), nullable.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'UserModify';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifica la customizacion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'UserModify';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'UserModify';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación de la personalización/customización del modelo de historia clínica. DATETIME, no nullable.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se creo la customizacion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (login/identificación) que creó la customización del formulario. CHAR(20), no nullable.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creo la customizacion', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido XML con la personalización de variables, campos y parámetros clínicos por grupo. VARBINARY(MAX), serializado.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'XML';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'XML con la customizacion de las variables por grupo', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'XML';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'XML';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del grupo origen desde tabla [ClinicalParameters].[HistoryGroups]. Referencia a clasificación de grupos de variables clínicas. VARCHAR(30).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'OriginGroupName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la tabla Origen del grupo:  [ClinicalParameters].[HistoryGroups] - [ClinicalParameters].[HistoryGroups]', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'OriginGroupName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'OriginGroupName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del modelo/formato de historia clínica origen desde [ClinicalParameters].[ClinicalHistoryFormats]. VARCHAR(30), no nullable.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'OriginModelName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la tabla origen del modelo de HC: [ClinicalParameters].[ClinicalHistoryFormats]', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'OriginModelName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'OriginModelName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (clave foránea) del grupo de variables clínicas desde [ClinicalParameters].[HistoryGroups]. INT, no nullable.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'IdGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id entidad origen: [ClinicalParameters].[HistoryGroups] - [ClinicalParameters].[HistoryGroups]', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'IdGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'IdGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (clave foránea) del modelo/formato de historia clínica desde [ClinicalParameters].[ClinicalHistoryFormats]. INT, no nullable.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'IdModel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id entidad origen: ClinicalParameters.ClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'IdModel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'IdModel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (consecutivo autoincrementable) de registro de personalización. INT IDENTITY, clave primaria.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuraciones personalizadas de formularios y modelos de historia clínica almacenadas en formato XML. Registra las preferencias de visualización y estructura definidas por grupos de usuarios para cada modelo de historia clínica, junto con la trazabilidad de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'XMLPersonalization';
