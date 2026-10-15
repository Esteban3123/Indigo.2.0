CREATE TABLE [Payroll].[BankFileDetail] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankFileId]                INT           NOT NULL,
    [LiquidationId]             INT           NULL,
    [GroupId]                   INT           NOT NULL,
    [PositionId]                INT           NOT NULL,
    [FunctionalUnitId]          INT           NOT NULL,
    [ContractId]                INT           NOT NULL,
    [EmployeeId]                INT           NOT NULL,
    [EmployeeBankId]            INT           NOT NULL,
    [EmployeeBankTypeAccount]   INT           NOT NULL,
    [EmployeeBankAccountNumber] VARCHAR (100) NOT NULL,
    [BasicSalary]               NUMERIC (18)  NOT NULL,
    [TotalAccrued]              NUMERIC (18)  NOT NULL,
    [TotalDeducted]             NUMERIC (18)  NOT NULL,
    [TotalPaid]                 NUMERIC (18)  NOT NULL,
    CONSTRAINT [PK_BankFileDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankFileDetail_Bank] FOREIGN KEY ([EmployeeBankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_BankFileDetail_BankFile] FOREIGN KEY ([BankFileId]) REFERENCES [Payroll].[BankFile] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_BankFileDetail_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Liquidation] FOREIGN KEY ([LiquidationId]) REFERENCES [Payroll].[Liquidation] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id])
);


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_Bank];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_BankFile];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_Contract];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_Employee];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_FunctionalUnit];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_Group];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_Liquidation];


GO
ALTER TABLE [Payroll].[BankFileDetail] NOCHECK CONSTRAINT [FK_BankFileDetail_Position];


GO
-- Unique index for payroll (nomina): prevents same liquidation in the same bank file
CREATE UNIQUE NONCLUSTERED INDEX [UQ_BankFileDetail__BankFileId__LiquidationId]
    ON [Payroll].[BankFileDetail]([BankFileId] ASC, [LiquidationId] ASC)
    WHERE [LiquidationId] IS NOT NULL;


GO
-- Unique index for incentive payments (primas): prevents same contract in the same bank file when no liquidation exists
CREATE UNIQUE NONCLUSTERED INDEX [UQ_BankFileDetail__BankFileId__ContractId__IncentivePayment]
    ON [Payroll].[BankFileDetail]([BankFileId] ASC, [ContractId] ASC)
    WHERE [LiquidationId] IS NULL;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'TotalPaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'TotalDeducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'TotalAccrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario Basico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeBankAccountNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeBankTypeAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del banco del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeBankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cargo del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'LiquidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'BankFileId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'Id';

