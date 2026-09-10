CREATE TABLE [dbo].[HCUNESPED] (
    [CODCONCEC]   INT           NOT NULL,
    [CODCAMPO]    CHAR (3)      NOT NULL,
    [DESEXAME]    VARCHAR (250) NOT NULL,
    [OBLIGATORIO] BIT           NOT NULL,
    CONSTRAINT [PK_HCUNESPED] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODCAMPO] ASC),
    CONSTRAINT [FK_HCUNESPED_HCUNESPEC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCUNESPEC] ([CODCONCEC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si el campo del examen físico es obligatorio u opcional. Valores: 1=Requerido, 0=Opcional. Controla validación en captura de datos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica Si el campo es obligatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del examen físico o hallazgo clínico (ej: presión arterial, frecuencia cardíaca, inspección, palpación). Detalla el nombre completo del componente evaluado en la exploración física del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'DESEXAME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del examen fisico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'DESEXAME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'DESEXAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (3 caracteres) que identifica unívocamente cada campo o elemento del examen físico dentro de una especificación. Referencia interna para mapeo de datos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'CODCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del campo del Examen Fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'CODCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'CODCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico autonumérico (clave foránea) que vincula a la cabecera de especificación de examen físico (HCUNESPEC). Agrupa múltiples campos relacionados a una misma evaluación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de campos y exámenes especiales en la historia clínica: define qué campos pertenecen a cada concepto clínico especial, su descripción y si son obligatorios de diligenciar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED';
