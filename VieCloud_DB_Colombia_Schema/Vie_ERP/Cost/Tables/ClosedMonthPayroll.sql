CREATE TABLE [Cost].[ClosedMonthPayroll] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId]    INT             NOT NULL,
    [CostCenterId]     INT             NOT NULL,
    [PositionId]       INT             NOT NULL,
    [TotalHoursWorked] INT             NOT NULL,
    [ValueTotal]       NUMERIC (18, 2) NOT NULL,
    [AverageCost]      NUMERIC (24, 6) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthPayroll] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthPayroll_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthPayroll_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthPayroll_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo promedio unitario por hora del cargo (NUMERIC 24,6), calculado como ValueTotal ÷ TotalHoursWorked, usado para análisis de eficiencia y proyecciones', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'AverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo promedio del cargo, este se calcula usando el valor total del cargo / la sumatoria de horas laboradas del cargo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'AverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'AverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total pagado en el mes para el cargo (NUMERIC 18,2), incluye salario base, prestaciones y deducciones, reflejo del gasto de nómina del período', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'ValueTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor total pagado durante el periodo para el cargo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'ValueTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'ValueTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sumatoria total de horas laboradas en el período por el cargo, base para cálculo de costo promedio y provisiones', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'TotalHoursWorked';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sumatoria de horas laboradas del cargo en el periodo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'TotalHoursWorked';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'TotalHoursWorked';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cargo (FK a Payroll.Position), puesto o rol del profesional de salud o personal administrativo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cargo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo (FK a Payroll.CostCenter), unidad funcional o departamento donde se genera el gasto de nómina', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del mes cerrado (FK a Cost.ClosedMonth), referencia al período de liquidación de nómina', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del mes cerrado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de nómina cerrada por mes, cargo y centro de costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del cierre mensual de nómina por centro de costo y cargo: guarda las horas trabajadas, el valor total liquidado y el costo promedio por hora para cada combinación de periodo cerrado, centro de costo y posición/cargo, permitiendo el análisis y distribución del gasto de personal en costos operacionales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll';
