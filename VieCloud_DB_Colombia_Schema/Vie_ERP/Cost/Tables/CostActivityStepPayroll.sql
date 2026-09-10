CREATE TABLE [Cost].[CostActivityStepPayroll] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityStepId] INT             NOT NULL,
    [PayrollPositionId]  INT             NOT NULL,
    [Hours]              DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_CostActivityStepPayroll__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStepPayroll_CostActivityStep] FOREIGN KEY ([CostActivityStepId]) REFERENCES [Cost].[CostActivityStep] ([Id]),
    CONSTRAINT [FK_CostActivityStepPayroll_PayrollPosition] FOREIGN KEY ([PayrollPositionId]) REFERENCES [Payroll].[Position] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CostActivityStepPayroll__CostActivityStepId__PayrollPositionId]
    ON [Cost].[CostActivityStepPayroll]([CostActivityStepId] ASC, [PayrollPositionId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horas requeridas para ejecutar el paso de actividad; cantidad decimal de tiempo (horas.decimales) asignadas al cargo de nómina en esta etapa de costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'Hours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horas requeridas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'Hours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'Hours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Cargo de Nómina (FK a Payroll.Position); referencia a la posición, puesto o rol laboral asociado al cálculo de costo y asignación de horas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'PayrollPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Cargo de Nomina', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'PayrollPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'PayrollPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Paso de la Actividad de Costo (FK a Cost.CostActivityStep); referencia a la etapa o fase específica de la actividad para la cual se asignan horas y recursos de nómina', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Paso de la Actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asignación de nómina a paso de actividad; clave primaria (IDENTITY, tipo INT) para rastrear la relación entre cargos y etapas de costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horas de nómina asignadas a cada paso de una actividad de costos. Registra qué cargo o posición de nómina participa en cada etapa del proceso productivo y cuántas horas de trabajo le corresponden, permitiendo calcular el costo de mano de obra por actividad.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll';
