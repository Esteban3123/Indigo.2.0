CREATE TABLE [Payroll].[UnemployedLiquidation] (
    [Id]                                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]                        INT             NOT NULL,
    [ContractId]                        INT             NOT NULL,
    [FundContractId]                    INT             NULL,
    [Year]                              INT             NOT NULL,
    [UnemployedLiquidationType]         BIT             NOT NULL,
    [UnemployedInitialDate]             DATE            NULL,
    [UnemployedEndingDate]              DATE            NULL,
    [WorkedTotalDays]                   INT             NULL,
    [SanctionTotalDays]                 INT             NULL,
    [DeductSanctionsDays]               BIT             NULL,
    [IBCAverage]                        NUMERIC (18, 2) NULL,
    [IBCAverageNotSanction]             NUMERIC (18, 2) NULL,
    [AverageMothNumber]                 INT             NULL,
    [TotalUnemployed]                   NUMERIC (18, 2) NULL,
    [UnemployedPaidTotal]               NUMERIC (18, 2) NULL,
    [UnemployedInterestPercentage]      NUMERIC (18, 2) NULL,
    [UnemployedInterestTotal]           NUMERIC (18, 2) NULL,
    [UnemployedLiquidationDate]         DATE            NULL,
    [UnemployedPayDate]                 DATE            NULL,
    [UnemployedInterestPayDate]         DATE            NULL,
    [Status]                            BIT             NULL,
    [ConfirmationDate]                  DATE            NULL,
    [ConfirmLiquidated]                 BIT             NOT NULL,
    [AuthorizationDate]                 DATE            NULL,
    [UnemployedRetirementReason]        VARCHAR (MAX)   NULL,
    [ResolutionNumber]                  VARCHAR (50)    NULL,
    [BasicSalary]                       NUMERIC (18)    NULL,
    [TotalDeducted]                     NUMERIC (18)    CONSTRAINT [DF_UnemployedLiquidation_TotalDeducted] DEFAULT ((0)) NULL,
    [GroupId]                           INT             NULL,
    [CalculationType]                   TINYINT         NULL,
    [UnemployedInterestPaidWithPayroll] BIT             CONSTRAINT [DF_UnemployedLiquidation_UnemployedInterestPaidWithPayroll] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UnemployedLiquidation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UnemployedLiquidation_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_UnemployedLiquidation_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_UnemployedLiquidation_FundContract] FOREIGN KEY ([FundContractId]) REFERENCES [Payroll].[FundContract] ([Id]),
    CONSTRAINT [FK_UnemployedLiquidation_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[UnemployedLiquidation] NOCHECK CONSTRAINT [FK_UnemployedLiquidation_Contract];


GO
ALTER TABLE [Payroll].[UnemployedLiquidation] NOCHECK CONSTRAINT [FK_UnemployedLiquidation_Employee];


GO
ALTER TABLE [Payroll].[UnemployedLiquidation] NOCHECK CONSTRAINT [FK_UnemployedLiquidation_FundContract];


GO
ALTER TABLE [Payroll].[UnemployedLiquidation] NOCHECK CONSTRAINT [FK_UnemployedLiquidation_Group];


GO
CREATE NONCLUSTERED INDEX [IX_UnemployedLiquidation__FundContractId]
    ON [Payroll].[UnemployedLiquidation]([FundContractId] ASC);


GO
CREATE NONCLUSTERED INDEX [iEmployeeId_Payroll_UnemployedLiquidation_EF36902C]
    ON [Payroll].[UnemployedLiquidation]([EmployeeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UnemployedLiquidation__EmployeeId]
    ON [Payroll].[UnemployedLiquidation]([ContractId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Propiedad para calcular interes de cesantias en la liquidacion de nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPaidWithPayroll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Cálculo  1. Por fórmula interna  2. Por suma de Provisiones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'CalculationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Deducido (Para embargos)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario Básico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'N° Resolución del ministerio  (cesantía parcial)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de retiro de cesantía  (cesantía parcial)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedRetirementReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de autorizacion (cesantía parcial)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'AuthorizationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidacion confirmada: 0-> NO, 1-> SI  Este campo ya no se usa debido a que la confirmación ya es global y se usa el campo Status para identificar cuando ha sido confirmado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmLiquidated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de confirmación de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro: 1- Confirmado, 0 - Sin confirmar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de pago de intereses de desempleados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPayDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha pago de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedPayDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha liquidación de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedLiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interes total de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de interes de censatías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cesantías total pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedPaidTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'TotalUnemployed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de meses promedio ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'AverageMothNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Promedio IBC no sancionado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'IBCAverageNotSanction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Promedio IBC Total en periodo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'IBCAverage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Restar Días de sanción... 0 - NO, 1- SI', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'DeductSanctionsDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total días de sanción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'SanctionTotalDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Días trabajados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'WorkedTotalDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Fin Periodo Cesantia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedEndingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial Periodo Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cesantias Tipo Liquidación  0-> Total   1->Parcial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedLiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del contrato de fondo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'FundContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'Id';

