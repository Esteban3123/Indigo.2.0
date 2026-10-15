CREATE TABLE [Cost].[ClosedMonthInventory] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId]        INT             NOT NULL,
    [CostCenterId]         INT             NOT NULL,
    [CostInventoryGroupId] INT             NOT NULL,
    [AverageCost]          NUMERIC (24, 6) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthInventory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthInventory_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthInventory_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthInventory_CostInventoryGroup] FOREIGN KEY ([CostInventoryGroupId]) REFERENCES [Cost].[CostInventoryGroup] ([Id])
);


GO
ALTER TABLE [Cost].[ClosedMonthInventory] NOCHECK CONSTRAINT [FK_ClosedMonthInventory_ClosedMonth];


GO
ALTER TABLE [Cost].[ClosedMonthInventory] NOCHECK CONSTRAINT [FK_ClosedMonthInventory_CostCenter];


GO
ALTER TABLE [Cost].[ClosedMonthInventory] NOCHECK CONSTRAINT [FK_ClosedMonthInventory_CostInventoryGroup];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo promedio del grupo de artículos, este se calcula mediante el promedio ponderado del costo promedio de cierre de mes de los productos usando las equivalencias establecidas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'AverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de producto', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'CostInventoryGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del mes cerrado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'Id';

