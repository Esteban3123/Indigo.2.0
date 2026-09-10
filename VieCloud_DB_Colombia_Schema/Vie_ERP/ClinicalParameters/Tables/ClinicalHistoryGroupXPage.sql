CREATE TABLE [ClinicalParameters].[ClinicalHistoryGroupXPage] (
    [Id]                       INT IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats] INT NOT NULL,
    [IdHistoryGroups]          INT NOT NULL,
    [OrderGroup]               INT NULL,
    CONSTRAINT [PK_ClinicalHistoryGroupXPage] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial (INT) que define el orden de visualización de los grupos de historia clínica en las pestañas dinámicas del formulario. Permite parametrizar la secuencia de presentación.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'OrderGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para indicar el orden en el cual se parametrizan los grupos de las pestañas dinamicas', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'OrderGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'OrderGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK) que referencia la tabla HistoryGroups. Identifica el grupo de historia clínica (secciones como antecedentes, medicamentos, alergias, etc.) asociado a esta página del formulario.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'IdHistoryGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla HistoryGroups', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'IdHistoryGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'IdHistoryGroups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK) que referencia la tabla ClinicalHistoryFormats. Vincula el formato/plantilla de historia clínica dinámico con los grupos de datos clínicos que contiene.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (INT IDENTITY). Clave primaria que distingue cada relación entre un formato de historia clínica, un grupo de historia y su orden de visualización en la interfaz.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los grupos de campos clínicos con las páginas o formatos de historia clínica, definiendo el orden en que cada grupo aparece dentro de un formulario de historia clínica.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryGroupXPage';
