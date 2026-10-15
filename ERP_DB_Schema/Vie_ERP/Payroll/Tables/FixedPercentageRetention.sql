CREATE TABLE [Payroll].[FixedPercentageRetention] (
    [Id]                  INT            NOT NULL,
    [EmployeeId]          INT            NOT NULL,
    [Period]              TINYINT        NOT NULL,
    [YearCalculation]     INT            NOT NULL,
    [ValidStartDate]      DATE           NOT NULL,
    [ValidEndDate]        DATE           NOT NULL,
    [TotalIncome]         NUMERIC (18)   NOT NULL,
    [TotalDeduction]      NUMERIC (18)   NOT NULL,
    [TotalIncomeExempt]   NUMERIC (18)   NOT NULL,
    [ExemptIncome25]      NUMERIC (18)   NOT NULL,
    [Subtotal]            NUMERIC (18)   NOT NULL,
    [CalculationMonth]    INT            NOT NULL,
    [RetentionBase]       NUMERIC (18)   NOT NULL,
    [UVTValue]            NUMERIC (18)   NOT NULL,
    [PercentageRetention] NUMERIC (5, 2) NOT NULL,
    CONSTRAINT [PK_FixedPercentageRetention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedPercentageRetention_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [Payroll].[FixedPercentageRetention] NOCHECK CONSTRAINT [FK_FixedPercentageRetention_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el pocentaje fijo de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'PercentageRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor del UVT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'UVTValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la base de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'RetentionBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de meses que se calculo al empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'CalculationMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el subtotal el cual se calcula de la siguiente forma  =(TotalIncome - TotalDeduction - TotalIncomeExempt - ExempIncome25)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'Subtotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la renta excenta del 25%', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'ExemptIncome25';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total de rentas exentas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'TotalIncomeExempt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Total de Deducciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'TotalDeduction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Total de Ingresos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'TotalIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha final donde termina de regir el porcentaje fijo de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'ValidEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha inicial donde empieza a regir el porcentaje fijo de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'ValidStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Año en el que se realizo el calculo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'YearCalculation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Periodo que se calculo  1 - Diciembre  2 - Junio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'Period';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del empleado que van a distribuir', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'Id';

