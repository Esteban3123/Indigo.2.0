CREATE TABLE [ClinicalParameters].[ClinicalHistoryFormatsFunctionalUnit] (
    [Id]                       INT       IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats] INT       NOT NULL,
    [UFUCODIGO]                CHAR (10) NOT NULL,
    CONSTRAINT [PK_ClinicalHistoryFormatsFunctionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad funcional', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla ClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo del registro', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';

