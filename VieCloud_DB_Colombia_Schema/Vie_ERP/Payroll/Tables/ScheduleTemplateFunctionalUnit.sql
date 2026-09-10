CREATE TABLE [Payroll].[ScheduleTemplateFunctionalUnit] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ScheduleTemplateId] INT NOT NULL,
    [FunctionalUnitId]   INT NOT NULL,
    CONSTRAINT [PK_ScheduleTemplateFunctionalUni] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ScheduleTemplateFunctionalUni_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ScheduleTemplateFunctionalUni_ScheduleTemplate] FOREIGN KEY ([ScheduleTemplateId]) REFERENCES [Payroll].[ScheduleTemplate] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la Unidad Funcional (centro de atención, departamento, área) asociada a la plantilla de horario. Referencia FK a Payroll.FunctionalUnit(Id). Permite filtrar horarios por ubicación o unidad organizacional.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Plantilla de Horario utilizada para la liquidación y cálculo de nómina. Referencia FK a Payroll.ScheduleTemplate(Id). Vincula cada unidad funcional con su esquema de turnos y jornadas de trabajo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del item de la plantilla con la que se liquido el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autonumérica (IDENTITY) que identifica únicamente cada registro de asociación entre plantilla de horario y unidad funcional. Tipo INT, no replicable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona cada plantilla de horario (turno o esquema de trabajo) con las unidades funcionales (servicios o áreas) a las que aplica, permitiendo definir qué turnos rigen en cada unidad funcional de la nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleTemplateFunctionalUnit';
