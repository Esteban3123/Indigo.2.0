CREATE TABLE [Payroll].[CostDistribution] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupId]              INT           NOT NULL,
    [PayrollEndDate]       DATE          NOT NULL,
    [LiquidationDetailId]  INT           NOT NULL,
    [JournalVoucherTypeId] INT           NOT NULL,
    [MainAccountId]        INT           NULL,
    [ThirdPartyId]         INT           NULL,
    [CostCenterId]         INT           NULL,
    [DebitValue]           DECIMAL (18)  CONSTRAINT [DF_CostDistribution_DebitValue] DEFAULT ((0)) NULL,
    [CreditValue]          DECIMAL (18)  CONSTRAINT [DF_CostDistribution_CreditValue] DEFAULT ((0)) NULL,
    [EmployeeId]           INT           NULL,
    [NumberHours]          INT           CONSTRAINT [DF_CostDistribution_NumberHours] DEFAULT ((0)) NULL,
    [Detail]               VARCHAR (500) NULL,
    CONSTRAINT [PK_CostDistribution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistribution_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CostDistribution_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_CostDistribution_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_CostDistribution_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CostDistribution_LiquidationDetail] FOREIGN KEY ([LiquidationDetailId]) REFERENCES [Payroll].[LiquidationDetail] ([Id]),
    CONSTRAINT [FK_CostDistribution_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CostDistribution_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_CostCenter];


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_Employee];


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_Group];


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_JournalVoucherTypes];


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_LiquidationDetail];


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_MainAccounts];


GO
ALTER TABLE [Payroll].[CostDistribution] NOCHECK CONSTRAINT [FK_CostDistribution_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_Payroll_CostDistribution]
    ON [Payroll].[CostDistribution]([MainAccountId] ASC, [CostCenterId] ASC)
    INCLUDE([CreditValue], [DebitValue], [LiquidationDetailId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Horas laboradas (Distribución de Gastos)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'NumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Credito', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Debito', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Tercero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la Cuenta Contable', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Tipo de Comprobante del cual corresponda el registro procesado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del detalle de la liquidación Procesada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'LiquidationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Nomina Contabilizada', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'PayrollEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Grupo Contabilizado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'CostDistribution', @level2type = N'COLUMN', @level2name = N'Id';

