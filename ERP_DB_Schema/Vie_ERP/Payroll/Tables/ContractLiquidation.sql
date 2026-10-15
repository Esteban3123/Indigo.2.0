CREATE TABLE [Payroll].[ContractLiquidation] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]         INT             NOT NULL,
    [ContractId]         INT             NOT NULL,
    [RetirementDate]     DATE            NOT NULL,
    [RetirementReasonId] TINYINT         NOT NULL,
    [Status]             NCHAR (1)       NOT NULL,
    [TotalAccrued]       DECIMAL (18, 2) NOT NULL,
    [TotalDeducted]      DECIMAL (18, 2) NOT NULL,
    [TotalPaid]          DECIMAL (18, 2) NOT NULL,
    [ResolutionNumber]   VARCHAR (50)    NULL,
    [ResolutionDate]     DATE            NULL,
    CONSTRAINT [PK_ContractLiquidation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractLiquidation_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_ContractLiquidation_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_ContractLiquidation_RetirementReason] FOREIGN KEY ([RetirementReasonId]) REFERENCES [Payroll].[RetirementReason] ([Id])
);


GO
ALTER TABLE [Payroll].[ContractLiquidation] NOCHECK CONSTRAINT [FK_ContractLiquidation_Contract];


GO
ALTER TABLE [Payroll].[ContractLiquidation] NOCHECK CONSTRAINT [FK_ContractLiquidation_Employee];


GO
ALTER TABLE [Payroll].[ContractLiquidation] NOCHECK CONSTRAINT [FK_ContractLiquidation_RetirementReason];


GO
CREATE NONCLUSTERED INDEX [IX_ContractLiquidation__RetirementReasonId]
    ON [Payroll].[ContractLiquidation]([RetirementReasonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ContractLiquidation__ContractId]
    ON [Payroll].[ContractLiquidation]([ContractId] ASC);


GO
CREATE NONCLUSTERED INDEX [iEmployeeId_Payroll_ContractLiquidation_487B499E]
    ON [Payroll].[ContractLiquidation]([EmployeeId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Resolución', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'ResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Resolución', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'TotalPaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'TotalAccrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Confirmado "C" o sin confirmar ""', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Razón de Retiro (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'RetirementReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Retiro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'RetirementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Contrato (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Empleado (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la liquidacion de contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'Id';

