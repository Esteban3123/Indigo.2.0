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
ALTER TABLE [Cost].[ClosedMonthPayroll] NOCHECK CONSTRAINT [FK_ClosedMonthPayroll_ClosedMonth];


GO
ALTER TABLE [Cost].[ClosedMonthPayroll] NOCHECK CONSTRAINT [FK_ClosedMonthPayroll_CostCenter];


GO
ALTER TABLE [Cost].[ClosedMonthPayroll] NOCHECK CONSTRAINT [FK_ClosedMonthPayroll_Position];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo promedio del cargo, este se calcula usando el valor total del cargo / la sumatoria de horas laboradas del cargo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'AverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor total pagado durante el periodo para el cargo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'ValueTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sumatoria de horas laboradas del cargo en el periodo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'TotalHoursWorked';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cargo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del mes cerrado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthPayroll', @level2type = N'COLUMN', @level2name = N'Id';

