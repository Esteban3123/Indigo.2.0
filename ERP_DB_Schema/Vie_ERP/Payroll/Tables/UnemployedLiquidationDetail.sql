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
ALTER TABLE [Payroll].[UnemployedLiquidationDetail] NOCHECK CONSTRAINT [FK_UnemployedLiquidationDetail_UnemployedLiquidation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de sanciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'SanctionsDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ValI BC no sanción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ValIBCNotSanction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interés de desempleo promedio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'UnemployedInterestAverage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Promedio Cesantías mes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'UnemployedAverage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor IBC mes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ValIBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días trabajados mes ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'WorkedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario Base Mes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Salary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de liquidación de desempleados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'UnemployedLiquidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';

