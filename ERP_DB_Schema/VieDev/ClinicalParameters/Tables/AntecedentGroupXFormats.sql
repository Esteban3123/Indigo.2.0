CREATE TABLE [ClinicalParameters].[AntecedentGroupXFormats] (
    [Id]                       INT IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats] INT NOT NULL,
    [CodeAntecedent]           INT NOT NULL,
    [OrderGroup]               INT NULL,
    CONSTRAINT [PK_AntecedentXFormats] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo vinculado al orden en el cual es agregado el grupo en la historia clinica intrahospitalaria parametrizada', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'OrderGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del antecedente', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'CodeAntecedent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla ClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentGroupXFormats', @level2type = N'COLUMN', @level2name = N'Id';

