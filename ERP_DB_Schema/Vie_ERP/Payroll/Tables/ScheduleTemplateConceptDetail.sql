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
ALTER TABLE [Payroll].[ScheduleTemplateConceptDetail] NOCHECK CONSTRAINT [FK_ScheduleTemplateConceptDetail_Concept];


GO
ALTER TABLE [Payroll].[ScheduleTemplateConceptDetail] NOCHECK CONSTRAINT [FK_ScheduleTemplateConceptDetail_ScheduleTemplateConcept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fk Id Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de concepto 0 - Ordinario 1- Feriado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK  Id ScheduleTemplateConcept', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateConceptDetail', @level2type = N'COLUMN', @level2name = N'Id';

