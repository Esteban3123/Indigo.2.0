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
ALTER TABLE [Payroll].[NoveltyScheduleDetailHour] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetailHour_NoveltyScheduleDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto que aplica 0 - Ordinario 1 - Feriado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'AppliedLiquidationConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si el Item es de un dia siguiente o no 1-si 0-No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NextDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total de horas del rango de horas de initial y end', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora final del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeEnding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora inicial del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'DateTimeInitial';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Schedule Detail Id (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'NoveltyScheduleDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailHour', @level2type = N'COLUMN', @level2name = N'Id';

