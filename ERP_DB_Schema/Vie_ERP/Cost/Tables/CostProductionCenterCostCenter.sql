CREATE TABLE [Cost].[CostProductionCenterCostCenter] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostCenterId]       INT NOT NULL,
    [ProductionCenterId] INT NOT NULL,
    CONSTRAINT [PK_CostProductionCenterCostCenter__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostProductionCenterCostCenter_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CostProductionCenterCostCenter_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [UQ_CostProductionCenterCostCenter__CostCenterId] UNIQUE NONCLUSTERED ([CostCenterId] ASC)
);


GO
ALTER TABLE [Cost].[CostProductionCenterCostCenter] NOCHECK CONSTRAINT [FK_CostProductionCenterCostCenter_CostCenter];


GO
ALTER TABLE [Cost].[CostProductionCenterCostCenter] NOCHECK CONSTRAINT [FK_CostProductionCenterCostCenter_CostProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo, solo se llena si se obtiene el centro de costo del grupo del servicio IPS ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'Id';

