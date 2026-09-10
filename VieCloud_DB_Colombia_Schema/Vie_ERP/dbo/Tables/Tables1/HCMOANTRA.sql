CREATE TABLE [dbo].[HCMOANTRA] (
    [CODANUTRA] INT          NOT NULL,
    [DESANUTRA] VARCHAR (80) NOT NULL,
    [ESTANUTRA] BIT          NOT NULL,
    CONSTRAINT [PK_HCMOANTRA] PRIMARY KEY CLUSTERED ([CODANUTRA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de activación del motivo de anulación (bit: 0=inactivo, 1=activo). Controla si el motivo está disponible para usar en cancelaciones de transcripciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA', @level2type = N'COLUMN', @level2name = N'ESTANUTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA', @level2type = N'COLUMN', @level2name = N'ESTANUTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA', @level2type = N'COLUMN', @level2name = N'ESTANUTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del motivo de anulación o cancelación de la transcripción de historia clínica. Texto que explica la razón por la cual se anuló el registro transcrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA', @level2type = N'COLUMN', @level2name = N'DESANUTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del motivo de anulacion de la transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA', @level2type = N'COLUMN', @level2name = N'DESANUTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA', @level2type = N'COLUMN', @level2name = N'DESANUTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador del motivo de anulación de transcripción. Clave primaria numérica que clasifica las razones de cancelación de registros transcritos en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA', @level2type = N'COLUMN', @level2name = N'CODANUTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Motivo de Anulacion de La Transcripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA', @level2type = N'COLUMN', @level2name = N'CODANUTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA', @level2type = N'COLUMN', @level2name = N'CODANUTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de tipos o categorías de antecedentes de historia clínica (por ejemplo, antecedentes personales, familiares, quirúrgicos, alérgicos, etc.). Define los grupos de antecedentes disponibles para registrar en la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANTRA';
