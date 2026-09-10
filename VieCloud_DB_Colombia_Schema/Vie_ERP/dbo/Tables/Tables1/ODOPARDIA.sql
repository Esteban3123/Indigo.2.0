CREATE TABLE [dbo].[ODOPARDIA] (
    [CONSECDIA] INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODDIAGNO] CHAR (4)        NOT NULL,
    [INDICECPO] CHAR (1)        NOT NULL,
    [INDICECEO] CHAR (1)        NOT NULL,
    [IMAGENDIA] VARBINARY (MAX) NULL,
    [APLICADIA] CHAR (1)        NOT NULL,
    [COLORSDIA] NCHAR (40)      NULL,
    [OBSERVDIA] NVARCHAR (200)  NULL,
    [ESTADODIA] BIT             NOT NULL,
    CONSTRAINT [PK_ODOPARDIA] PRIMARY KEY CLUSTERED ([CONSECDIA] ASC),
    CONSTRAINT [FK_ODOPARDIA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del diagnóstico odontológico: True=Activo, False=Inactivo. Indica si el hallazgo diagnóstico está vigente o desactivado en la historia clínica dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'ESTADODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del diagnostico    True: Activo  False: Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'ESTADODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'ESTADODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas del diagnóstico parametrizado odontológico. Notas adicionales, hallazgos complementarios o comentarios del profesional sobre la condición dental identificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'OBSERVDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion del diagnostico parametrizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'OBSERVDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'OBSERVDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Color de la superficie dental diagnosticada. Parámetro descriptivo de tonalidad o pigmentación de la pieza o superficie dental afectada (ej: manchas, decoloración).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'COLORSDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Color de la Superficie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'COLORSDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'COLORSDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de aplicación del diagnóstico odontológico: 1=Diente (pieza dental completa), 2=Superficie (cara/sector específico del diente). Determina granularidad del hallazgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'APLICADIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Para determinar si aplica a  1: Diente  2: Superficie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'APLICADIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'APLICADIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Archivo de imagen digital asociado al diagnóstico odontológico. Fotografía intraoral, radiografía o imagen clínica que documenta la condición dental diagnosticada. Tipo: VARBINARY(MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'IMAGENDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagen relacionada al diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'IMAGENDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'IMAGENDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CEO (Cariados-Extraídos-Obturados) para diagnóstico en dentición temporal. Valores: N=No Aplica, C=Cariado, E=Extraído, O=Obturado. Métrica de salud dental infantil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'INDICECEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice CEO    N: No Aplica  C: Cariado  E: Extraido  O: Obturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'INDICECEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'INDICECEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice CPO (Cariados-Perdidos-Obturados) para diagnóstico en dentición permanente. Valores: N=No Aplica, C=Cariado, P=Perdido, O=Obturado. Indicador epidemiológico de caries dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'INDICECPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice CPO    N: No Aplica  C: Cariado  P: Perdido  O: Obturado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'INDICECPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'INDICECPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 odontológico. Clasificación internacional de enfermedad dental según estándar WHO. FK referencia a tabla INDIAGNOS. Búsqueda: diagnóstico, patología dental, CIE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnosticio CIE 10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) de registro en tabla ODOPARDIA. Clave primaria. Número secuencial para cada diagnóstico odontológico parametrizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'CONSECDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'CONSECDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA', @level2type = N'COLUMN', @level2name = N'CONSECDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de diagnósticos odontológicos utilizados en la historia clínica dental. Cada registro define un diagnóstico con sus índices epidemiológicos (CPO/CEO), representación visual y configuración de aplicación en la odontograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARDIA';
