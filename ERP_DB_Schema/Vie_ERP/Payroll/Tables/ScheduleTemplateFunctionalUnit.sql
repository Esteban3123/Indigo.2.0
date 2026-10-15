CREATE TABLE [Payroll].[ScheduleTemplateFunctionalUnit] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ScheduleTemplateId] INT NOT NULL,
    [FunctionalUnitId]   INT NOT NULL,
    CONSTRAINT [PK_ScheduleTemplateFunctionalUni] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleTemplateFunctionalUni_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ScheduleTemplateFunctionalUni_ScheduleTemplate] FOREIGN KEY ([ScheduleTemplateId]) REFERENCES [Payroll].[ScheduleTemplate] ([Id])
);


GO
ALTER TABLE [Payroll].[ScheduleTemplateFunctionalUnit] NOCHECK CONSTRAINT [FK_ScheduleTemplateFunctionalUni_FunctionalUnit];


GO
ALTER TABLE [Payroll].[ScheduleTemplateFunctionalUnit] NOCHECK CONSTRAINT [FK_ScheduleTemplateFunctionalUni_ScheduleTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del item de la plantilla con la que se liquido el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';

