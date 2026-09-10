CREATE TABLE [Payroll].[ScheduleTemplateConcept] (
    [Id]                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ScheduleTemplateId] INT            NOT NULL,
    [Consecutive]        TINYINT        NOT NULL,
    [NextDay]            BIT            NOT NULL,
    [InitialTime]        TIME (0)       NOT NULL,
    [EndingTime]         TIME (0)       NOT NULL,
    [NumberHour]         NUMERIC (5, 2) NOT NULL,
    [State]              BIT            NOT NULL,
    CONSTRAINT [PK_ScheduleTemplateConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleTemplateConcept_ScheduleTemplate] FOREIGN KEY ([ScheduleTemplateId]) REFERENCES [Payroll].[ScheduleTemplate] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del concepto de la plantilla de cuadro de turnos. Bit (1=Activo, 0=Inactivo).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del concepto de la plantilla de cuadro de turnos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de horas trabajadas en el rango horario del concepto. Decimal (5,2), incluye fraccionarios.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'NumberHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de horas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'NumberHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'NumberHour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora final (HH:MM:SS) en la que cesa la aplicación del concepto de turno en la plantilla.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'EndingTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora final en la que aplica el concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'EndingTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'EndingTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora inicial (HH:MM:SS) en la que inicia la aplicación del concepto de turno en la plantilla.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'InitialTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicial en la que aplica el concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'InitialTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'InitialTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el tramo horario del concepto continúa al día siguiente de la fecha de plantilla. Bit (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'NextDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si el item pertenece al dia Siguiente o no de la fecha aplicada a la plantilla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'NextDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'NextDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de orden secuencial (1-255) que determina la secuencia de ejecución de conceptos en la plantilla.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'consecutivo para saber el orden de los items', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) al Id de la plantilla de cuadro de turnos padre en Payroll.ScheduleTemplate.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la plantilla del cuadro de turnos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del concepto de turno dentro de la plantilla de cuadro de turnos. INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de la plantilla del cuadro de turnos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conceptos de horario definidos dentro de una plantilla de turnos de nómina. Cada registro representa un bloque horario (turno o franja) asociado a una plantilla, indicando hora de inicio, hora de fin, duración en horas y si el bloque cruza al día siguiente.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConcept';
