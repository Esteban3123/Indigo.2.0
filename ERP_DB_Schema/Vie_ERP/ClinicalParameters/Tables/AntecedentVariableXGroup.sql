CREATE TABLE [ClinicalParameters].[AntecedentVariableXGroup] (
    [Id]                        INT IDENTITY (1, 1) NOT NULL,
    [IdClinicalHistoryFormats]  INT NOT NULL,
    [IdAntecedentGroupXFormats] INT NOT NULL,
    [IdHistoryVariables]        INT NOT NULL,
    [Mandatory]                 BIT NULL,
    CONSTRAINT [PK_AntecedentVariableXGroup] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro obligatorio', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'Mandatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo de HistoryVariables', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdHistoryVariables';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla AntecedentGroupXFormat', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdAntecedentGroupXFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla ClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'AntecedentVariableXGroup', @level2type = N'COLUMN', @level2name = N'Id';

