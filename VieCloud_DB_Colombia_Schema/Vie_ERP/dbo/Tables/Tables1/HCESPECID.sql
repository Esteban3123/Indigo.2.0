CREATE TABLE [dbo].[HCESPECID] (
    [CODCONCEC]   INT                                                                                    NOT NULL,
    [CODCAMPO]    CHAR (3)                                                                               NOT NULL,
    [DESEXAME]    VARCHAR (250) MASKED WITH (FUNCTION = 'partial(0, "ExaminationPhysical_Ofuscado", 0)') NOT NULL,
    [OBLIGATORIO] BIT                                                                                    NOT NULL,
    CONSTRAINT [PK_HCESPECID] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODCAMPO] ASC),
    CONSTRAINT [FK_HCESPECID_HCESPECIC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCESPECIC] ([CODCONCEC])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCESPECID].[DESEXAME]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (0/1) que especifica si el campo de examen físico es obligatorio u opcional en la consulta de urgencias. Permite validación de datos completos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica Si el campo es obligatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del examen físico realizado en urgencias. Texto enmascarado (PII). Incluye hallazgos clínicos, signos vitales, exploración física. Búsqueda: examen, exploración, hallazgos, urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'DESEXAME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Examen fisico urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'DESEXAME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'DESEXAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (CHAR 3) que identifica cada campo o componente del examen físico en la consulta de urgencias. Identificador único junto a CODCONCEC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'CODCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del campo del Examen Fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'CODCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'CODCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autoincremental que referencia la cabecera de especificidad del examen clínico (FK a HCESPECIC). Agrupa campos del examen físico por consulta de urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campos especiales del examen físico por especialidad médica: define qué ítems o hallazgos deben registrarse en la historia clínica según la especialidad, indicando si son obligatorios o no.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID';
