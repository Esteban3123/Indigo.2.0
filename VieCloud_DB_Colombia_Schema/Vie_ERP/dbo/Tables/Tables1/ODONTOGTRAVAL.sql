CREATE TABLE [dbo].[ODONTOGTRAVAL] (
    [ID]          INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCTRDIEVAL] INT         NOT NULL,
    [CONSECTRA]   INT         NOT NULL,
    [TIPO]        VARCHAR (1) NOT NULL,
    CONSTRAINT [PK_ODONTOGTRAVAL__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGTRAVAL_ODONTOGDIEVAL] FOREIGN KEY ([IDCTRDIEVAL]) REFERENCES [dbo].[ODONTOGDIEVAL] ([ID]),
    CONSTRAINT [FK_ODONTOGTRAVAL_ODOPARTRA] FOREIGN KEY ([CONSECTRA]) REFERENCES [dbo].[ODOPARTRA] ([CONSECTRA])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tratamiento en valoración odontológica: 0=NoAplica, 1=CPOCariado (diente cariado), 2=CPOPerdido (diente perdido), 3=CPOObturado (diente obturado/restaurado), 4=CEOCariado (diente temporal cariado), 5=CEOExtraído (diente temporal extraído), 6=CEOObturado (diente temporal obturado). Varchar(1), clasificación epidemiológica dental CPO/CEO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el tipo tratamiento valoración NoAplica = 0, CPOCariado = 1, CPOPerdido = 2, CPOObturado = 3, CEOCariado = 4, CEOExtraido = 5,
CEOObturado = 6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo autonumérico que identifica unívocamente cada tratamiento odontológico registrado. Clave foránea a tabla ODOPARTRA. INT, referencia a catálogo de tratamientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'CONSECTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de los tratamientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'CONSECTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'CONSECTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del control/evaluación odontológica (FK a ODONTOGDIEVAL). Vincula el tratamiento valorado a su evaluación diagnóstica matriz. INT, referencia a diagnóstico o control odontológico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'IDCTRDIEVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Id control valores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'IDCTRDIEVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'IDCTRDIEVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único (IDENTITY) que identifica cada registro de tratamiento-valoración en odontología. Clave primaria de la tabla. INT, autonumérico secuencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los trabajos o tratamientos realizados en cada evaluación odontológica, indicando el tipo de procedimiento ejecutado por diente o zona tratada dentro de un contrato o plan de tratamiento dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRAVAL';
