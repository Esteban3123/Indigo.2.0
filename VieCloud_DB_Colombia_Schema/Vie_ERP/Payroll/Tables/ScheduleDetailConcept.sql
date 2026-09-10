CREATE TABLE [Payroll].[ScheduleDetailConcept] (
    [Id]                   INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ScheduleDetailHourId] INT     NULL,
    [ConceptType]          TINYINT NOT NULL,
    [ConceptId]            INT     NOT NULL,
    CONSTRAINT [PK_ScheduleDetailConcept__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleDetailConcept_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ScheduleDetailConcept_ScheduleDetailHour] FOREIGN KEY ([ScheduleDetailHourId]) REFERENCES [Payroll].[ScheduleDetailHour] ([Id]) ON DELETE CASCADE
);




GO



GO





GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_ScheduleDetailConcept__ScheduleDetailHourId]
    ON [Payroll].[ScheduleDetailConcept]([ScheduleDetailHourId] ASC)
    INCLUDE ([ConceptType], [ConceptId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Concepto (FK a Payroll.Concept). Referencias el concepto de nómina asociado: salario, bonificación, descuento, aporte, auxilio, o valor aplicable al detalle de horario.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fk Id Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Concepto (TINYINT). Clasificación: 0=Ordinario (día laboral regular), 1=Feriado (día festivo, descanso, o jornada especial). Determina el tratamiento del valor en cálculos de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de concepto 0 - Ordinario 1- Feriado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Detalle de Horario (FK a Payroll.ScheduleDetailHour). Referencia la jornada, turno o franja horaria específica vinculada a este concepto de nómina. Permite cascada de eliminación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ScheduleDetailHourId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Schedule Detail Hour Id (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ScheduleDetailHourId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ScheduleDetailHourId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador Único Autonumérico (INT IDENTITY). Clave primaria clustered de la tabla, generado automáticamente para cada registro de concepto en el detalle de horario de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';


GO
CREATE NONCLUSTERED INDEX [IX_ScheduleDetailConcept_ScheduleDetailHourId]
    ON [Payroll].[ScheduleDetailConcept]([ScheduleDetailHourId] ASC)
    INCLUDE([Id], [ConceptId]) WITH (FILLFACTOR = 90, PAD_INDEX = ON);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asociación entre el detalle de horas de un turno o horario de nómina y los conceptos de liquidación (como recargos, horas extras, bonificaciones) que aplican a ese bloque horario.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept';
