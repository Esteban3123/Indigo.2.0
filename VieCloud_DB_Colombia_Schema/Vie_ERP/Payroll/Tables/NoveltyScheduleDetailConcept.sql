CREATE TABLE [Payroll].[NoveltyScheduleDetailConcept] (
    [Id]                          INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NoveltyScheduleDetailHourId] INT     NOT NULL,
    [ConceptType]                 TINYINT NOT NULL,
    [ConceptId]                   INT     NOT NULL,
    CONSTRAINT [PK_NoveltyScheduleDetailConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NoveltyScheduleDetailConcept_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_NoveltyScheduleDetailConcept_NoveltyScheduleDetailHour] FOREIGN KEY ([NoveltyScheduleDetailHourId]) REFERENCES [Payroll].[NoveltyScheduleDetailHour] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Concepto (FK a Payroll.Concept). Referencia al concepto de nómina asociado (descuento, bonificación, aporte, etc.). Tipo: INT. Búsquedas: concepto de pago, rubro salarial, deducción, aporte.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fk Id Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Concepto. Valores: 0=Ordinario (días laborales regulares), 1=Feriado (días festivos/descanso). Tipo: TINYINT. Define cómo se calcula el pago del concepto según el tipo de jornada.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de concepto 0 - Ordinario 1- Feriado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Detalle de Horario de Novedad (FK a Payroll.NoveltyScheduleDetailHour). Referencia a la distribución horaria específica de la novedad (licencia, incapacidad, vacación, permiso). Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'NoveltyScheduleDetailHourId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Schedule Detail Hour Id (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'NoveltyScheduleDetailHourId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'NoveltyScheduleDetailHourId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY 1,1) de la relación entre horario de novedad y concepto de nómina. Clave primaria. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de conceptos de nómina asociados a cada hora de un cronograma de novedades. Relaciona cada franja horaria de novedad con el tipo y código de concepto salarial o prestacional que le aplica.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept';
