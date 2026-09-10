CREATE TABLE [dbo].[HCACIBASD] (
    [CODCONCEC]  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDACIDBASE] INT           NOT NULL,
    [COBALACIBA] CHAR (3)      NULL,
    [VALOR]      VARCHAR (100) NULL,
    CONSTRAINT [PK_HCACIBASD] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [IDACIDBASE] ASC),
    CONSTRAINT [FK_HCACIBASD_HCACIBASD] FOREIGN KEY ([IDACIDBASE]) REFERENCES [dbo].[HCACIBASE] ([IDACIDBASE])
);


GO
ALTER TABLE [dbo].[HCACIBASD] NOCHECK CONSTRAINT [FK_HCACIBASD_HCACIBASD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o parámetro del modo de ventilación mecánica (tipo, configuración, parámetros respiratorios) registrado en historia clínica de cuidados intensivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modo de Ventilacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o abreviación (3 caracteres) del modo de ventilación aplicado al paciente en unidad de cuidados intensivos o soporte ventilatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'COBALACIBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modo de Ventilacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'COBALACIBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'COBALACIBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) que relaciona el registro con la tabla HCACIBASE, vinculando los detalles de ventilación a la atención en cuidados intensivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'IDACIDBASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacionado con IDACIDBASE de la tabla HCACIBASE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'IDACIDBASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'IDACIDBASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo único (PK) que identifica cada registro detallado de modo de ventilación en historia clínica de cuidados intensivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de valores de balance ácido-base asociados a una atención o estudio clínico. Guarda los componentes individuales (parámetros) del análisis ácido-base del paciente, como pH, bicarbonato, pCO2, entre otros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASD';
