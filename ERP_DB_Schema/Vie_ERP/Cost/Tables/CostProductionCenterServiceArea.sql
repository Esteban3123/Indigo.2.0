CREATE TABLE [Cost].[CostProductionCenterServiceArea] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionCenterId] INT NOT NULL,
    [FunctionalUnitId]   INT NOT NULL,
    CONSTRAINT [PK_CostProductionCenterServiceArea] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostProductionCenterServiceArea_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_CostProductionCenterServiceArea_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);


GO
ALTER TABLE [Cost].[CostProductionCenterServiceArea] NOCHECK CONSTRAINT [FK_CostProductionCenterServiceArea_CostProductionCenter];


GO
ALTER TABLE [Cost].[CostProductionCenterServiceArea] NOCHECK CONSTRAINT [FK_CostProductionCenterServiceArea_FunctionalUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la area del servicio del erp con el que se haga interfaz', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion del area de servicio', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'Id';

