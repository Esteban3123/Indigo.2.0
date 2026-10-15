CREATE TABLE [Payroll].[CostDistributions] (
    [Id]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [JournalVoucherTypeId]      INT          NULL,
    [GroupId]                   INT          NOT NULL,
    [EmployeeId]                INT          NOT NULL,
    [ContractId]                INT          NOT NULL,
    [ConceptId]                 INT          NOT NULL,
    [CompanyId]                 INT          NULL,
    [CompanyNit]                VARCHAR (50) NULL,
    [EmployeeNit]               VARCHAR (50) NULL,
    [PayrollDateLiquidated]     DATE         NOT NULL,
    [AccountingStatementNumber] VARCHAR (50) NOT NULL,
    [AccruedAccount]            VARCHAR (20) NULL,
    [DeductedAccount]           VARCHAR (20) NULL,
    [NumberHours]               INT          NULL,
    [TotalConceptValue]         NUMERIC (18) NULL,
    [CostCenterId]              INT          NOT NULL,
    [SubCostCenterId]           INT          NULL,
    [DeductedValue]             NUMERIC (18) NULL,
    [AccruedValue]              NUMERIC (18) NULL,
    [InabilityCollect]          NUMERIC (18) NULL,
    [SpendingInability]         NUMERIC (18) NULL,
    [Status]                    BIT          NOT NULL,
    CONSTRAINT [PK_CostDistributions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributions_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_CostDistributions_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_CostDistributions_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CostDistributions_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_CostDistributions_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_CostDistributions_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id])
);


GO
ALTER TABLE [Payroll].[CostDistributions] NOCHECK CONSTRAINT [FK_CostDistributions_Concept];


GO
ALTER TABLE [Payroll].[CostDistributions] NOCHECK CONSTRAINT [FK_CostDistributions_Contract];


GO
ALTER TABLE [Payroll].[CostDistributions] NOCHECK CONSTRAINT [FK_CostDistributions_CostCenter];


GO
ALTER TABLE [Payroll].[CostDistributions] NOCHECK CONSTRAINT [FK_CostDistributions_Employee];


GO
ALTER TABLE [Payroll].[CostDistributions] NOCHECK CONSTRAINT [FK_CostDistributions_Group];


GO
ALTER TABLE [Payroll].[CostDistributions] NOCHECK CONSTRAINT [FK_CostDistributions_JournalVoucherTypes];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado: 0: Borrador, 1: Confirmado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gasto de Incapacidad ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'SpendingInability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Incapacidad x Cobrar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'InabilityCollect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'AccruedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'DeductedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del SubCentro de Costo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'SubCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Total del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'TotalConceptValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Horas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'NumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable Credito', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'DeductedAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable Debito', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'AccruedAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Comprobante Contable', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'AccountingStatementNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Fin de Liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'PayrollDateLiquidated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'EmployeeNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit de la Compañía', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'CompanyNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Compañia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de Comprobante. Si la Nómina es Nativa, se llena en campo. Si es con otro ERP, se deja vacío', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistributions', @level2type = N'COLUMN', @level2name = N'Id';

