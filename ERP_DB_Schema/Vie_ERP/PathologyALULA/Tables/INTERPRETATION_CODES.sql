CREATE TABLE [PathologyALULA].[INTERPRETATION_CODES] (
    [diagnosis_result_id]     INT NOT NULL,
    [diagnosis_coding_method] INT NOT NULL,
    [diagnosis_code]          INT NOT NULL,
    CONSTRAINT [PK_INTERPRETATION_CODES] PRIMARY KEY CLUSTERED ([diagnosis_result_id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES', @level2type = N'COLUMN', @level2name = N'diagnosis_code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de codificación de diagnóstico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES', @level2type = N'COLUMN', @level2name = N'diagnosis_coding_method';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'INTERPRETATION_CODES', @level2type = N'COLUMN', @level2name = N'diagnosis_result_id';

