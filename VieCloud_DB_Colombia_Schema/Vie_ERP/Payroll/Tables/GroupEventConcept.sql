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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia tabla Payroll.Concept. Referencia el concepto de nómina (salario, prima, descuento, auxilio, bono) aplicable al grupo en este tipo de día y horario. INT.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Tabla de concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de jornada/horario: 1=Normal (diurno, jornada regular), 2=Nocturno (turno nocturno, requiere prima). TINYINT. Usado para cálculo de prestaciones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'TypeSchedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de horario 1 - Normal 2 - Nocturno', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'TypeSchedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'TypeSchedule';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de día laboral: 1=Ordinario (día regular de trabajo), 2=Feriado (día festivo, descanso remunerado). TINYINT.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'TypeDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de dia 1 - Ordinario 2- Feriado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'TypeDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'TypeDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia tabla Payroll.Group. Identifica el grupo de empleados o centro de atención al cual se aplica este evento de concepto de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK con la tabla grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) de la asociación entre grupo de empleados, tipo de día, horario y concepto de nómina. INT IDENTITY, autoincremental.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id llave primaria de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona grupos de nómina con conceptos de pago según el tipo de día (hábil, festivo, etc.) y el tipo de horario o turno. Permite definir qué conceptos salariales o de liquidación aplican a cada grupo de empleados dependiendo de la jornada.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'GroupEventConcept';
