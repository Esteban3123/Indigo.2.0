CREATE TABLE [dbo].[HCPLADIAG] (
    [CODDIAGNO] CHAR (4)    NOT NULL,
    [CODCONSEC] VARCHAR (5) NOT NULL,
    [PLANTPRIN] BIT         NULL,
    [TipoFicha] INT         NULL,
    CONSTRAINT [PK_HCPLADIAG] PRIMARY KEY CLUSTERED ([CODDIAGNO] ASC, [CODCONSEC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de tipo de ficha clínica: 0=Ficha de notificación SIVIGILA (vigilancia epidemiológica), 1=Ficha distrital. Indica el origen/clasificación del diagnóstico notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'TipoFicha';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0 - Fichas notificacion SIVIGILA  1 - Fichas distritales  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'TipoFicha';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'TipoFicha';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que marca si esta es la plantilla/diagnóstico principal del paciente. Bandera para identificar el diagnóstico primario o principal en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'PLANTPRIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para marcar la plantilla principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'PLANTPRIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'PLANTPRIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo (VARCHAR 5) que identifica la secuencia o número de orden del diagnóstico dentro del registro de historia clínica. Clave compuesta con CODDIAGNO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (CHAR 4), identificador único del diagnóstico médico. Corresponde a clasificación CIE-10 o códigos diagnósticos del sistema de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del plan diagnóstico de la historia clínica: vincula diagnósticos (CIE-10) a una consulta o atención clínica, indicando si el diagnóstico es principal y el tipo de ficha clínica al que pertenece.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLADIAG';
