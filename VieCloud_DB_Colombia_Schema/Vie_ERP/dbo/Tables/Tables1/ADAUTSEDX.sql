CREATE TABLE [dbo].[ADAUTSEDX] (
    [NUMCONSEC] NUMERIC (18) IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC] NUMERIC (18) NOT NULL,
    [CODDIAGNO] CHAR (4)     NOT NULL,
    [CODDIAPRI] BIT          NOT NULL,
    CONSTRAINT [PK_ADAUTSEDX] PRIMARY KEY CLUSTERED ([NUMCONSEC] ASC),
    CONSTRAINT [FK_ADAUTSEDX_ADAUTSERC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[ADAUTSERC] ([CODCONCEC]),
    CONSTRAINT [FK_ADAUTSEDX_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_ADAUTSEDX]
    ON [dbo].[ADAUTSEDX]([CODCONCEC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de diagnóstico principal (BIT): marca si el diagnóstico registrado es el diagnóstico principal o primario de la atención/ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnostico Principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (CHAR 4): identificador de la clasificación diagnóstica (CIE-10), referencia a tabla INDIAGNOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno de la cabecera de atención/ingreso: identificador único que vincula el diagnóstico a la consulta, urgencia o internación registrada en ADAUTSERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno de la Cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno autoincrementado de la tabla ADAUTSEDX: número secuencial único (IDENTITY) que identifica cada registro de diagnóstico asociado a una atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno de tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de diagnósticos asociados a autorizaciones de atención. Guarda los diagnósticos (CIE-10) vinculados a cada concepto o autorización de ingreso, indicando cuál es el diagnóstico principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTSEDX';
