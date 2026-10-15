CREATE TABLE [Payroll].[GroupEventConcept] (
    [Id]           INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupId]      INT     NOT NULL,
    [TypeDay]      TINYINT NOT NULL,
    [TypeSchedule] TINYINT NOT NULL,
    [ConceptId]    INT     NOT NULL,
    CONSTRAINT [PK_GroupEventConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupEventConcept_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_GroupEventConcept_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[GroupEventConcept] NOCHECK CONSTRAINT [FK_GroupEventConcept_Concept];


GO
ALTER TABLE [Payroll].[GroupEventConcept] NOCHECK CONSTRAINT [FK_GroupEventConcept_Group];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Tabla de concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de horario 1 - Normal 2 - Nocturno', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'TypeSchedule';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de dia 1 - Ordinario 2- Feriado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'TypeDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK con la tabla grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id llave primaria de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'Id';

