CREATE TABLE [Payroll].[NoveltyScheduleDetailHour] (
    [Id]                        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NoveltyScheduleDetailId]   INT      NOT NULL,
    [DateTimeInitial]           DATETIME NOT NULL,
    [DateTimeEnding]            DATETIME NOT NULL,
    [TotalNumberHours]          INT      NOT NULL,
    [NextDay]                   INT      NOT NULL,
    [AppliedLiquidationConcept] BIT      NOT NULL,
    [State]                     BIT      NOT NULL,
    CONSTRAINT [PK_NoveltyScheduleDetailHour] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NoveltyScheduleDetailHour_NoveltyScheduleDetail] FOREIGN KEY ([NoveltyScheduleDetailId]) REFERENCES [Payroll].[NoveltyScheduleDetail] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=activo (vigente en nómina), 0=inactivo (descartado o histórico)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de liquidación aplicado: 0=Ordinario, 1=Feriado; determina el cálculo de pago en nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'AppliedLiquidationConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto que aplica 0 - Ordinario 1 - Feriado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'AppliedLiquidationConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'AppliedLiquidationConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el registro corresponde al día siguiente (1=sí, 0=no), usado en novedades que trascienden a jornada siguiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NextDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el Item es de un dia siguiente o no 1-si 0-No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NextDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NextDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de horas acumuladas entre la fecha-hora inicial y final del rango de novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total de horas del rango de horas de initial y end', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final del rango de registro de horas, marca el término de la jornada o bloque de novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeEnding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora final del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeEnding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeEnding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial del rango de registro de horas, marca el comienzo de la jornada o bloque de novedad', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeInitial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora inicial del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeInitial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeInitial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea que referencia a NoveltyScheduleDetail, identifica el detalle de novedad de horario asociado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NoveltyScheduleDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Schedule Detail Id (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NoveltyScheduleDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NoveltyScheduleDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único de la tabla NoveltyScheduleDetailHour (clave primaria)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el detalle de horas de cada novedad de turno/horario en nómina, indicando el rango horario de inicio y fin, la cantidad de horas totales, si aplica al día siguiente y si el concepto de liquidación ya fue aplicado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour';
