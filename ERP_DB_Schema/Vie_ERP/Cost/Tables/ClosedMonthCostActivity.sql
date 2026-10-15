CREATE TABLE [Cost].[ClosedMonthCostActivity] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId]          INT             NOT NULL,
    [CostActivityId]         INT             NOT NULL,
    [CUPSEntityId]           INT             NOT NULL,
    [CostProductionCenterId] INT             NOT NULL,
    [CostCenterId]           INT             NULL,
    [FixedAssetValue]        DECIMAL (24, 6) NOT NULL,
    [PayrollValue]           DECIMAL (24, 6) NOT NULL,
    [InventoryValue]         DECIMAL (24, 6) NOT NULL,
    [AddictionalValue]       DECIMAL (24, 6) NOT NULL,
    [UnitValue]              DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthCostActivity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthCostActivity_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthCostActivity_CostActivity] FOREIGN KEY ([CostActivityId]) REFERENCES [Cost].[CostActivity] ([Id]),
    CONSTRAINT [FK_ClosedMonthCostActivity_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthCostActivity_CostProductionCenter] FOREIGN KEY ([CostProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthCostActivity_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);


GO
ALTER TABLE [Cost].[ClosedMonthCostActivity] NOCHECK CONSTRAINT [FK_ClosedMonthCostActivity_ClosedMonth];


GO
ALTER TABLE [Cost].[ClosedMonthCostActivity] NOCHECK CONSTRAINT [FK_ClosedMonthCostActivity_CostActivity];


GO
ALTER TABLE [Cost].[ClosedMonthCostActivity] NOCHECK CONSTRAINT [FK_ClosedMonthCostActivity_CostCenter];


GO
ALTER TABLE [Cost].[ClosedMonthCostActivity] NOCHECK CONSTRAINT [FK_ClosedMonthCostActivity_CostProductionCenter];


GO
ALTER TABLE [Cost].[ClosedMonthCostActivity] NOCHECK CONSTRAINT [FK_ClosedMonthCostActivity_CUPSEntity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo promedio de la actividad al sumar los costos unitarios de los detalles (Activos Fijos, Nomina, Inventario, Adicionales)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo calculado de costos Adicionales', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'AddictionalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo calculado de Inventario', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'InventoryValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo calculado de Inventario', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'PayrollValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo calculado de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'FixedAssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Centro de Producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CostProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio relacionado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del costo por actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CostActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del mes cerrado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'Id';

