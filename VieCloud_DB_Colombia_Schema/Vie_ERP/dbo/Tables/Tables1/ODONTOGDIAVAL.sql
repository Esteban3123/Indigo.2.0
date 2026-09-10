CREATE TABLE [dbo].[ODONTOGDIAVAL] (
    [ID]          INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCTRDIEVAL] INT         NOT NULL,
    [CONSECDIA]   INT         NOT NULL,
    [TIPO]        VARCHAR (1) NOT NULL,
    CONSTRAINT [PK_ODONTOGDIAVAL__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGDIAVAL_ODONTOGDIEVAL] FOREIGN KEY ([IDCTRDIEVAL]) REFERENCES [dbo].[ODONTOGDIEVAL] ([ID]),
    CONSTRAINT [FK_ODONTOGDIAVAL_ODOPARDIA] FOREIGN KEY ([CONSECDIA]) REFERENCES [dbo].[ODOPARDIA] ([CONSECDIA])
);








GO



GO





GO



GO





GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de diagnóstico odontológico en valoración (VARCHAR(1)): NoAplica=0, Valoración=1, Tratamiento=2. Clasifica la naturaleza del diagnóstico dental registrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el tipo de diagnóstico valoración NoAplica = 0
, Valoracion = 1, Tratamiento = 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'TIPO';












GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo del diagnóstico odontológico (FK a ODOPARDIA.CONSECDIA). Referencia al diagnóstico padre en la tabla de parámetros de diagnósticos odontológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'CONSECDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla ODOPARDIA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'CONSECDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'CONSECDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la evaluación diagnóstica odontológica (FK a ODONTOGDIEVAL.ID). Enlace a la evaluación clínica odontológica que contiene este diagnóstico valorado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'IDCTRDIEVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla ODONTOGDIEVAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'IDCTRDIEVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'IDCTRDIEVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (INT IDENTITY, PK). Número secuencial único de la valoración diagnóstica odontológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de valores diarios del odontograma por diagnóstico: guarda el detalle de cada diagnóstico o hallazgo odontológico evaluado en una cita o control, indicando el tipo de condición o tratamiento detectado en cada pieza dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIAVAL';
