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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje fijo de retención en la fuente aplicado al empleado. NUMERIC(5,2). Utilizado en cálculo de impuesto sobre la renta (IRPF) y retenciones tributarias. Búsquedas: retención porcentaje, tasa retención, retención en la fuente.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'PercentageRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el pocentaje fijo de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'PercentageRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'PercentageRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la Unidad de Valor Tributario (UVT) vigente en el período de cálculo. NUMERIC(18). Parámetro normativo colombiano para cálculos de tributos, multas y sanciones. Búsquedas: UVT, valor tributario, unidad tributaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'UVTValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor del UVT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'UVTValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'UVTValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base imponible sobre la cual se calcula la retención en la fuente. NUMERIC(18). Corresponde al monto sujeto a retención después de aplicar deducciones y exenciones. Búsquedas: base retención, base impuesto, base tributaria.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'RetentionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la base de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'RetentionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'RetentionBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de meses computados en el período de cálculo del empleado. INT. Define el tiempo durante el cual se generan ingresos y deducciones. Búsquedas: meses cálculo, período meses, tiempo liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'CalculationMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de meses que se calculo al empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'CalculationMonth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'CalculationMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal calculado como: (TotalIncome - TotalDeduction - TotalIncomeExempt - ExemptIncome25). NUMERIC(18). Base para determinar la retención tributaria neta del empleado. Búsquedas: subtotal, renta gravable, base cálculo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'Subtotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el subtotal el cual se calcula de la siguiente forma  =(TotalIncome - TotalDeduction - TotalIncomeExempt - ExempIncome25)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'Subtotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'Subtotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Renta exenta del 25% conforme normas tributarias colombianas. NUMERIC(18). Porción de ingresos que no genera retención en la fuente por exención legal. Búsquedas: exención 25%, renta exenta, ingreso no gravable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'ExemptIncome25';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la renta excenta del 25%', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'ExemptIncome25';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'ExemptIncome25';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de rentas exentas del empleado en el período. NUMERIC(18). Suma de todos los ingresos que por ley no están sujetos a impuesto sobre la renta. Búsquedas: ingresos exentos, rentas exentas, ingresos no gravables.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'TotalIncomeExempt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de rentas exentas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'TotalIncomeExempt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'TotalIncomeExempt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de deducciones autorizadas del empleado. NUMERIC(18). Suma de descuentos legales (aportes a pensión, salud, etc.) que reduce la base tributaria. Búsquedas: deducciones, descuentos, aportes empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'TotalDeduction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Total de Deducciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'TotalDeduction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'TotalDeduction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de ingresos brutos del empleado en el período. NUMERIC(18). Suma de salario, prestaciones, bonificaciones y demás percepciones antes de deducciones. Búsquedas: ingreso bruto, salario total, remuneración.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'TotalIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Total de Ingresos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'TotalIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'TotalIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de vigencia del porcentaje fijo de retención. DATE. Indica cuándo deja de aplicarse esta tarifa de retención en la fuente. Búsquedas: fecha fin retención, vigencia final, término retención.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'ValidEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha final donde termina de regir el porcentaje fijo de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'ValidEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'ValidEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de vigencia del porcentaje fijo de retención. DATE. Indica desde cuándo comienza a aplicarse esta tarifa de retención en la fuente. Búsquedas: fecha inicio retención, vigencia inicial, comienzo retención.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'ValidStartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha inicial donde empieza a regir el porcentaje fijo de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'ValidStartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'ValidStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año calendario en el que se realizó el cálculo de retención. INT. Define el período fiscal y las normas tributarias aplicables. Búsquedas: año cálculo, año fiscal, período anual.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'YearCalculation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Año en el que se realizo el calculo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'YearCalculation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'YearCalculation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período bimestral de cálculo de retención: 1=Diciembre, 2=Junio. TINYINT. Define cuáles meses componen el ciclo de liquidación y retención. Búsquedas: período bimestral, ciclo retención, bimestre.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'Period';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Periodo que se calculo  1 - Diciembre  2 - Junio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'Period';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'Period';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del empleado (FK a Payroll.Employee). INT. Vincula la retención al registro del trabajador. Búsquedas: empleado, trabajador, cédula empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del empleado que van a distribuir', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable del registro de retención. INT. Clave primaria de la tabla FixedPercentageRetention. Búsquedas: identificador retención, ID registro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retenciones en la fuente por porcentaje fijo aplicadas a empleados en nómina. Registra el cálculo periódico de retención en la fuente a tarifa fija, incluyendo ingresos, deducciones, base gravable y el porcentaje de retención determinado para cada trabajador según el procedimiento 2 de retención.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FixedPercentageRetention';
