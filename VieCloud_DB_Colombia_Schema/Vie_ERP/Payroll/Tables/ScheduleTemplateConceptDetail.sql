CREATE TABLE [Payroll].[ScheduleTemplateConceptDetail] (
    [Id]                        INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ScheduleTemplateConceptId] INT     NOT NULL,
    [ConceptType]               TINYINT NOT NULL,
    [ConceptId]                 INT     NOT NULL,
    CONSTRAINT [PK_ScheduleTemplateConceptDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleTemplateConceptDetail_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ScheduleTemplateConceptDetail_ScheduleTemplateConcept] FOREIGN KEY ([ScheduleTemplateConceptId]) REFERENCES [Payroll].[ScheduleTemplateConcept] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea que referencia Concept en Payroll. Identificador del concepto específico de nómina (descuento, deducción, auxilio, bonificación, etc.) usado en la programación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fk Id Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de concepto: 0=Ordinario (días laborales normales), 1=Feriado (días festivos). Clasificación del concepto de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de concepto 0 - Ordinario 1- Feriado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea que referencia ScheduleTemplateConcept. Vincula el concepto de nómina al template de programación horaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK  Id ScheduleTemplateConcept', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de la fila en ScheduleTemplateConceptDetail. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de conceptos asociados a una plantilla de horario de nómina. Cada fila vincula un concepto salarial o de liquidación (por tipo e identificador) a una plantilla de programación de turnos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail';
