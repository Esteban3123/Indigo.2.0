CREATE TABLE [Payroll].[UnemployedLiquidationDetail] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UnemployedLiquidationId]   INT             NOT NULL,
    [Year]                      INT             NOT NULL,
    [Month]                     TINYINT         NOT NULL,
    [Salary]                    NUMERIC (18, 2) NOT NULL,
    [WorkedDays]                TINYINT         NOT NULL,
    [ValIBC]                    NUMERIC (18, 2) NOT NULL,
    [UnemployedAverage]         NUMERIC (18, 2) NOT NULL,
    [UnemployedInterestAverage] NUMERIC (18, 2) NOT NULL,
    [ValIBCNotSanction]         NUMERIC (18, 2) NOT NULL,
    [SanctionsDays]             INT             NOT NULL,
    CONSTRAINT [PK_UnemployedLiquidationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UnemployedLiquidationDetail_UnemployedLiquidation] FOREIGN KEY ([UnemployedLiquidationId]) REFERENCES [Payroll].[UnemployedLiquidation] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días sancionados o descuentados en el mes (INT); reduce base de cálculo de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'SanctionsDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Días de sanciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'SanctionsDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'SanctionsDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor IBC sin sanciones aplicadas (NUMERIC 18,2); IBC depurado de descuentos disciplinarios', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ValIBCNotSanction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ValI BC no sanción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ValIBCNotSanction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ValIBCNotSanction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interés sobre el promedio de desempleo/cesantías (NUMERIC 18,2); rendimiento financiero acumulado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'UnemployedInterestAverage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interés de desempleo promedio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'UnemployedInterestAverage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'UnemployedInterestAverage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Promedio de cesantías/desempleo del mes (NUMERIC 18,2); acumulado mensual para liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'UnemployedAverage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Promedio Cesantías mes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'UnemployedAverage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'UnemployedAverage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor IBC (Ingreso Base de Cotización) del mes (NUMERIC 18,2); base para aportes a desempleo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ValIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor IBC mes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ValIBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ValIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días trabajados en el mes (TINYINT); base para prorratas y cálculos de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'WorkedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Días trabajados mes ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'WorkedDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'WorkedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario base mensual (NUMERIC 18,2); monto bruto devengado en el mes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Salary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario Base Mes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Salary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Salary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes (TINYINT, 1-12) del período de cálculo de la liquidación de desempleo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año (TINYINT) del período de cálculo de la liquidación de desempleo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia UnemployedLiquidation; identifica la liquidación de desempleo/cesantías padre', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'UnemployedLiquidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de liquidación de desempleados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'UnemployedLiquidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'UnemployedLiquidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de cada detalle de liquidación de desempleo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle mensual del cálculo de liquidación de cesantías por empleado. Registra, mes a mes, los valores de salario, días trabajados, base de cotización (IBC) y promedios de cesantías e intereses utilizados para liquidar las cesantías de cada trabajador.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail';
