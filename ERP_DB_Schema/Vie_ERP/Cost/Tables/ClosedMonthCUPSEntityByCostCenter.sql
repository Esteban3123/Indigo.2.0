CREATE TABLE [Cost].[ClosedMonthCUPSEntityByCostCenter] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId] INT             NOT NULL,
    [CostCenterId]  INT             NOT NULL,
    [CUPSEntityId]  INT             NOT NULL,
    [Quantity]      INT             NOT NULL,
    [TotalSales]    DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthCUPSEntityByCostCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthCUPSEntityByCostCenter_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthCUPSEntityByCostCenter_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthCUPSEntityByCostCenter_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);


GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntityByCostCenter] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntityByCostCenter_ClosedMonth];


GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntityByCostCenter] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntityByCostCenter_CostCenter];


GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntityByCostCenter] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntityByCostCenter_CUPSEntity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Venta total', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByCostCenter', @level2type = N'COLUMN', @level2name = N'TotalSales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad facturada del servicio por el centro de costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByCostCenter', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByCostCenter', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del mes cerrado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByCostCenter', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntityByCostCenter', @level2type = N'COLUMN', @level2name = N'Id';

