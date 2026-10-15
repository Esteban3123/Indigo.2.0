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
ALTER TABLE [Payroll].[ScheduleDetailConcept] NOCHECK CONSTRAINT [FK_ScheduleDetailConcept_Concept];


GO
ALTER TABLE [Payroll].[ScheduleDetailConcept] NOCHECK CONSTRAINT [FK_ScheduleDetailConcept_ScheduleDetailHour];


GO
CREATE NONCLUSTERED INDEX [IX_ScheduleDetailConcept__ScheduleDetailHourId]
    ON [Payroll].[ScheduleDetailConcept]([ScheduleDetailHourId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fk Id Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de concepto 0 - Ordinario 1- Feriado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Schedule Detail Hour Id (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'ScheduleDetailHourId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';

