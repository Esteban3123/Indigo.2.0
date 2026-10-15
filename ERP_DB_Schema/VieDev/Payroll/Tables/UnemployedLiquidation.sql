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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Propiedad para calcular interes de cesantias en la liquidacion de nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPaidWithPayroll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Cálculo  1. Por fórmula interna  2. Por suma de Provisiones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'CalculationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Liquidacion confirmada: 0-> NO, 1-> SI  Este campo ya no se usa debido a que la confirmación ya es global y se usa el campo Status para identificar cuando ha sido confirmado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmLiquidated';

