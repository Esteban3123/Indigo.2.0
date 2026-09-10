CREATE TABLE [dbo].[ODOPERDIE] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONTROL]   INT           NOT NULL,
    [NUMDIENTE]   TINYINT       NOT NULL,
    [MOVILIDAD]   CHAR (1)      NULL,
    [NOTA]        VARCHAR (MAX) NULL,
    [IMPLANTE]    BIT           NULL,
    [DIENAUSENTE] BIT           NULL,
    CONSTRAINT [PK_ODOPERDIE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODOPERDIE_ODOPERICTR] FOREIGN KEY ([IDCONTROL]) REFERENCES [dbo].[ODOPERCTR] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que especifica si el diente está ausente, perdido, extraído o congenitalmente falta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'DIENAUSENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me especifica si No tiene el Diente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'DIENAUSENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'DIENAUSENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que especifica si el diente es un implante dental o restauración implantosoportada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'IMPLANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me especifica si tiene Implante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'IMPLANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'IMPLANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto libre (VARCHAR MAX) para observaciones, hallazgos o anotaciones clínicas específicas del diente evaluado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'NOTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nota del diente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'NOTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'NOTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado de movilidad dental (CHAR 1): I=movilidad ligera, II=movilidad moderada, III=movilidad severa; null si sin movimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'MOVILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Movilidad    1:  I  2:  II  3:  III', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'MOVILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'MOVILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del diente (1-32, TINYINT); identificador dental según nomenclatura odontológica FDI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del diente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'NUMDIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que relaciona con tabla ODOPERCTR; referencia al control/examen odontológico (periodontograma) del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de control de periodontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'IDCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de cada registro de análisis dental por diente en odontología operatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del estado periodontal por diente: movilidad, presencia de implante y ausencia dental, asociado a un control odontológico específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPERDIE';
