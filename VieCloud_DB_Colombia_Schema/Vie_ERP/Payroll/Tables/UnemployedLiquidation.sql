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



GO



GO



GO



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) para calcular intereses de cesantías en la liquidación de nómina; permite determinar si los intereses se pagan junto con la nómina (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPaidWithPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Propiedad para calcular interes de cesantias en la liquidacion de nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPaidWithPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPaidWithPayroll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cálculo de cesantías: 1=Por fórmula interna del sistema, 2=Por suma de provisiones acumuladas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'CalculationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Cálculo  1. Por fórmula interna  2. Por suma de Provisiones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'CalculationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'CalculationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del grupo o lote de liquidación de cesantías; vincula a la tabla Group', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total deducido de la cesantía (embargos, retenciones, descuentos); NUMERIC(18,2)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Deducido (Para embargos)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario básico del empleado usado como base para el cálculo de cesantías; NUMERIC(18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'BasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario Básico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'BasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de resolución del ministerio para cesantía parcial (VARCHAR 50); vinculado a autorización oficial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'N° Resolución del ministerio  (cesantía parcial)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo o causal de retiro en caso de cesantía parcial; texto descriptivo del evento (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedRetirementReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de retiro de cesantía  (cesantía parcial)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedRetirementReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedRetirementReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de autorización de la cesantía parcial por ente regulador (DATE); NULL si es cesantía total', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'AuthorizationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de autorizacion (cesantía parcial)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'AuthorizationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'AuthorizationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de liquidación confirmada (BIT): 0=No, 1=Sí; OBSOLETO (use Status para confirmación global)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmLiquidated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquidacion confirmada: 0-> NO, 1-> SI  Este campo ya no se usa debido a que la confirmación ya es global y se usa el campo Status para identificar cuando ha sido confirmado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmLiquidated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmLiquidated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de confirmación de la liquidación de cesantías por responsable autorizado (DATE)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de confirmación de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (BIT): 1=Confirmado/Autorizado, 0=Sin confirmar; indica aprobación de la liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro: 1- Confirmado, 0 - Sin confirmar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha efectiva de pago de los intereses generados sobre la cesantía (DATE)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPayDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de pago de intereses de desempleados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPayDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPayDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha efectiva de pago del capital de cesantías al empleado (DATE)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedPayDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha pago de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedPayDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedPayDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en la que se realizó el cálculo y liquidación de cesantías (DATE)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedLiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha liquidación de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedLiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedLiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total de intereses generados sobre la cesantía liquidada; NUMERIC(18,2)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interes total de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa o porcentaje de interés aplicado al cálculo de intereses de cesantía; NUMERIC(18,2)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de interes de censatías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInterestPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total de cesantía pagado al empleado en efectivo; NUMERIC(18,2)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedPaidTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cesantías total pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedPaidTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedPaidTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total liquidado por cesantía antes de deducciones; NUMERIC(18,2)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'TotalUnemployed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'TotalUnemployed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'TotalUnemployed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de meses promedio considerados en el cálculo del promedio salarial para cesantía; INT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'AverageMothNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de meses promedio ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'AverageMothNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'AverageMothNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Promedio del Ingreso Base de Cotización (IBC) sin sanciones o castigos; NUMERIC(18,2)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'IBCAverageNotSanction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Promedio IBC no sancionado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'IBCAverageNotSanction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'IBCAverageNotSanction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Promedio del Ingreso Base de Cotización (IBC) total en el período de liquidación; NUMERIC(18,2)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'IBCAverage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Promedio IBC Total en periodo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'IBCAverage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'IBCAverage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador para restar días de sanción del cálculo (BIT): 0=No restar, 1=Sí restar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'DeductSanctionsDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Restar Días de sanción... 0 - NO, 1- SI', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'DeductSanctionsDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'DeductSanctionsDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de días de sanción, suspensión o castigo que afectan el cálculo de cesantía; INT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'SanctionTotalDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total días de sanción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'SanctionTotalDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'SanctionTotalDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de días efectivamente trabajados en el período de liquidación de cesantía; INT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'WorkedTotalDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Días trabajados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'WorkedTotalDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'WorkedTotalDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del período de liquidación de cesantía (fecha de retiro o fin de contrato); DATE', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedEndingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Fin Periodo Cesantia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedEndingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedEndingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del período de liquidación de cesantía; DATE', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial Periodo Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación de cesantía (BIT): 0=Total (fin de vínculo), 1=Parcial (evento regulado)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedLiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cesantias Tipo Liquidación  0-> Total   1->Parcial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedLiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'UnemployedLiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año fiscal o período anual de la liquidación de cesantía; INT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del contrato de fondo de cesantías; vincula a tabla FundContract; NULL si no aplica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'FundContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del contrato de fondo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'FundContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'FundContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del contrato de empleado; vincula a tabla Contract; clave para rastrear relación laboral', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del empleado; vincula a tabla Employee; identifica al trabajador titular de la cesantía', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) del registro de liquidación de cesantía; clave primaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquidaciones de cesantías de empleados: registra el cálculo, pago e intereses de cesantías por contrato y año, incluyendo días trabajados, salario base, promedios de ingreso base de cotización (IBC), montos liquidados, fechas de pago y estado de confirmación o autorización del proceso.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'UnemployedLiquidation';
