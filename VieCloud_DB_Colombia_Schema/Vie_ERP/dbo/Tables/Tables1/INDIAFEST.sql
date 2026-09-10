CREATE TABLE [dbo].[INDIAFEST] (
    [DIAFESTIV] DATETIME     NOT NULL,
    [DIADESCRI] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INDIAFES] PRIMARY KEY CLUSTERED ([DIAFESTIV] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Auditoría; identificador numérico (NUMERIC 18) que registra quién o qué proceso creó/modificó el registro de día festivo para trazabilidad y control interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del Día Festivo; nombre o denominación del festivo (ej: Navidad, Año Nuevo, Corpus Christi) almacenado en texto de hasta 40 caracteres para identificación legible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST', @level2type = N'COLUMN', @level2name = N'DIADESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Dia Festivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST', @level2type = N'COLUMN', @level2name = N'DIADESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST', @level2type = N'COLUMN', @level2name = N'DIADESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del Día Festivo; fecha y hora (DATETIME) que define el día festivo en el calendario, usada como clave primaria para evitar duplicados y para cálculos de disponibilidad de servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST', @level2type = N'COLUMN', @level2name = N'DIAFESTIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Dia Festivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST', @level2type = N'COLUMN', @level2name = N'DIAFESTIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST', @level2type = N'COLUMN', @level2name = N'DIAFESTIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calendario de días festivos y feriados oficiales. Registra las fechas no laborables para controlar disponibilidad de agendamiento, liquidación de turnos y cálculo de tiempos de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAFEST';
