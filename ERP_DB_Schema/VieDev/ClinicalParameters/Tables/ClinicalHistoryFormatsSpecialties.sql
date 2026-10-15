CREATE TABLE [ClinicalParameters].[ClinicalHistoryFormatsSpecialties] (
    [Id]                       INT      IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats] INT      NOT NULL,
    [CODESPECI]                CHAR (3) NOT NULL,
    CONSTRAINT [PK_ClinicalHistoryFormatsSpecialties] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsSpecialties', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tablaClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsSpecialties', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'ClinicalHistoryFormatsSpecialties', @level2type = N'COLUMN', @level2name = N'Id';

