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
ALTER TABLE [Payroll].[NoveltyScheduleDetailConcept] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetailConcept_Concept];


GO
ALTER TABLE [Payroll].[NoveltyScheduleDetailConcept] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetailConcept_NoveltyScheduleDetailHour];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fk Id Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de concepto 0 - Ordinario 1- Feriado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Schedule Detail Hour Id (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'NoveltyScheduleDetailHourId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';

