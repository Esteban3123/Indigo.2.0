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
ALTER TABLE [Cost].[CostActivityStepPayroll] NOCHECK CONSTRAINT [FK_CostActivityStepPayroll_CostActivityStep];


GO
ALTER TABLE [Cost].[CostActivityStepPayroll] NOCHECK CONSTRAINT [FK_CostActivityStepPayroll_PayrollPosition];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CostActivityStepPayroll__CostActivityStepId__PayrollPositionId]
    ON [Cost].[CostActivityStepPayroll]([CostActivityStepId] ASC, [PayrollPositionId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas requeridas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'Hours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Cargo de Nomina', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'PayrollPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Paso de la Actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepPayroll', @level2type = N'COLUMN', @level2name = N'Id';

