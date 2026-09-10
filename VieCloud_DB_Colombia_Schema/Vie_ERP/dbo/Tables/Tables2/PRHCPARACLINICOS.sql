CREATE TABLE [dbo].[PRHCPARACLINICOS] (
    [ID]           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]   INT NOT NULL,
    [IDHCTABPARAC] INT NOT NULL,
    [ACTIVO]       BIT NOT NULL,
    CONSTRAINT [PK_PRHCPARACLINICOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCPARACLINICOS_HCTABPARAC] FOREIGN KEY ([IDHCTABPARAC]) REFERENCES [dbo].[HCTABPARAC] ([ID]),
    CONSTRAINT [FK_PRHCPARACLINICOS_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo del registro de paraclinicos: 0=No activo, 1=Activo. Indica si el paraclinicos (examen, laboratorio, imagen) está vigente en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'ACTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'estado activo : 0.no 1.si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'ACTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'ACTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (clave foránea) de la cabecera de tabla de paraclinicos (HCTABPARAC). Referencia al registro maestro de exámenes, laboratorios e imágenes diagnósticas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'IDHCTABPARAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de cabecera de la tabla paraclinicos (HCTABPARAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'IDHCTABPARAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'IDHCTABPARAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (clave foránea) de la cabecera del modelo de historia clínica y sus parámetros (PRMODELOHC). Vincula el paraclinico al modelo de HC parametrizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cabecera de la historia clinica parametros (MODELOHC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) del registro de asociación paraclinicos en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los paraclinicos (exámenes de laboratorio, imágenes diagnósticas y otros estudios complementarios) asociados a cada modelo de historia clínica, indicando cuáles están activos para ser usados en la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCPARACLINICOS';
