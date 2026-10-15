CREATE TABLE [Cost].[CostActivityStepInventory] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityStepId]   INT             NOT NULL,
    [CostInventoryGroupId] INT             NOT NULL,
    [Quantity]             DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_CostActivityStepInventory__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStepInventory_CostActivityStep] FOREIGN KEY ([CostActivityStepId]) REFERENCES [Cost].[CostActivityStep] ([Id]),
    CONSTRAINT [FK_CostActivityStepInventory_CostInventoryGroup] FOREIGN KEY ([CostInventoryGroupId]) REFERENCES [Cost].[CostInventoryGroup] ([Id])
);


GO
ALTER TABLE [Cost].[CostActivityStepInventory] NOCHECK CONSTRAINT [FK_CostActivityStepInventory_CostActivityStep];


GO
ALTER TABLE [Cost].[CostActivityStepInventory] NOCHECK CONSTRAINT [FK_CostActivityStepInventory_CostInventoryGroup];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CostActivityStepInventory__CostActivityStepId__CostInventoryGroupId]
    ON [Cost].[CostActivityStepInventory]([CostActivityStepId] ASC, [CostInventoryGroupId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Grupo de Productos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'CostInventoryGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Paso de la Actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepInventory', @level2type = N'COLUMN', @level2name = N'Id';

