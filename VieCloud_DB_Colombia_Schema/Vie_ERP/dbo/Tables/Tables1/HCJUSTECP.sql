CREATE TABLE [dbo].[HCJUSTECP] (
    [CODCONCEC] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMPLANTI] VARCHAR (100) NOT NULL,
    [CODSERIPS] CHAR (20)     NOT NULL,
    [DESAYUDIA] VARCHAR (MAX) NOT NULL,
    [DESJUSTEC] VARCHAR (MAX) NOT NULL,
    [DESINCAYU] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_HCJUSTECP] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_HCJUSTECP]
    ON [dbo].[HCJUSTECP]([CODSERIPS] ASC);


GO
ALTER INDEX [IX_HCJUSTECP]
    ON [dbo].[HCJUSTECP] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la incidencia o eventualidad relacionada con la ayuda diagnóstica: complicaciones, hallazgos adicionales, limitaciones técnicas o resultados inesperados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'DESINCAYU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Incidencia de ayuda diagnostica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'DESINCAYU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'DESINCAYU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación técnica y científica del procedimiento o servicio: fundamentación clínica, indicación médica, necesidad diagnóstica o terapéutica del acto médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'DESJUSTEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'justificacion tecnica cientifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'DESJUSTEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'DESJUSTEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción completa de las ayudas diagnósticas solicitadas: exámenes de laboratorio, imagenología, estudios especializados o pruebas diagnósticas complementarias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'DESAYUDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descirpcion de las ayudas diagnosticas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'DESAYUDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'DESAYUDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de procedimiento o servicio RIPS, identificador del procedimiento/examen/ayuda diagnóstica según nomenclatura de facturación y reportes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción de la plantilla de justificación técnica, referencia de documento o formato utilizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'NOMPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre o descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'NOMPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'NOMPLANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de consecutivo interno, identificador único (PK), secuencial para cada registro de justificación técnica en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Concecutivo interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantillas de justificación y ayuda para la codificación de servicios en historia clínica. Contiene textos predefinidos que orientan al profesional sobre cómo justificar, describir y codificar un procedimiento o servicio (CUPS) dentro de la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP';
