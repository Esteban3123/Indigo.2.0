CREATE TABLE [dbo].[ADPARCONPRI] (
    [ID]           INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]    CHAR (10) NOT NULL,
    [CODENTIDA]    CHAR (9)  NULL,
    [LUN]          BIT       NULL,
    [MAR]          BIT       NULL,
    [MIER]         BIT       NULL,
    [JUE]          BIT       NULL,
    [VIER]         BIT       NULL,
    [SAB]          BIT       NULL,
    [DOM]          BIT       NULL,
    [TRIAGECUATRO] INT       NULL,
    [TRIAGECINCO]  INT       NULL,
    [HORAS]        INT       NOT NULL,
    CONSTRAINT [PK_ADPARCONPRI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [Centro Atencion] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo de espera (INT), cantidad de horas permitidas para atención en contratación prioritaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'HORAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horas de espera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'HORAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'HORAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cupos de triage V (INT), cantidad de citas/atenciones para urgencias de prioridad cinco (menor urgencia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'TRIAGECINCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Triage cinco', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'TRIAGECINCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'TRIAGECINCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cupos de triage IV (INT), cantidad de citas/atenciones prioritarias para urgencias de prioridad cuatro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'TRIAGECUATRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teiage Cuatro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'TRIAGECUATRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'TRIAGECUATRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad operativa domingo (BIT), indicador de apertura/funcionamiento en día domingo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'DOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Domingo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'DOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'DOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad operativa sábado (BIT), indicador de apertura/funcionamiento en día sábado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'SAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Sabado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'SAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'SAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad operativa viernes (BIT), indicador de apertura/funcionamiento en día viernes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'VIER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Viernes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'VIER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'VIER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad operativa jueves (BIT), indicador de apertura/funcionamiento en día jueves', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'JUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Jueves', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'JUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'JUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad operativa miércoles (BIT), indicador de apertura/funcionamiento en día miércoles', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'MIER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Miercoles', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'MIER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'MIER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad operativa martes (BIT), indicador de apertura/funcionamiento en día martes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'MAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Martes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'MAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'MAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad operativa lunes (BIT), indicador de apertura/funcionamiento en día lunes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'LUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Disponibilidad para dia Lunes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'LUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'LUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad, asegurador o contratante (FK→INENTIDAD), identificación de la organización de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (FK→ADCENATEN), identificador de la unidad funcional, sede o punto de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY), consecutivo automático de la tabla de parámetros de contratación prioritaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración de contrato o convenio por centro de atención para consulta prioritaria o urgencias: define los días hábiles de atención, los tiempos máximos permitidos (en horas) para triage nivel 4 y 5, y la cantidad de horas de referencia por entidad aseguradora y sede.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCONPRI';
