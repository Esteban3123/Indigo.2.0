CREATE TABLE [ClinicalParameters].[ClinicalHistoryFormatsFunctionalUnit] (
    [Id]                       INT       IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats] INT       NOT NULL,
    [UFUCODIGO]                CHAR (10) NOT NULL,
    CONSTRAINT [PK_ClinicalHistoryFormatsFunctionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (centro de atención, servicio clínico o área de salud) donde se aplica el formato de historia clínica. Identificador único de la unidad funcional (UFU), tipo CHAR(10), clave para búsquedas por área de atención, servicio o centro.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código unidad funcional', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con la tabla ClinicalHistoryFormats (FK). Vincula el formato de historia clínica específico (plantilla, template o estructura de registro) con la unidad funcional. Permite asociar múltiples unidades a un mismo formato de historia.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo y único del registro (INT IDENTITY). Clave primaria que identifica cada asociación entre formato de historia clínica y unidad funcional. Generado automáticamente por SQL Server.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo del registro', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los formatos de historia clínica con las unidades funcionales habilitadas para usarlos. Permite configurar qué plantillas o formularios clínicos están disponibles en cada servicio o unidad de atención.', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit';
