CREATE TABLE [Inventory].[HighRiskDrugs] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [DCIId]                INT           NOT NULL,
    [InventoryRiskLevelId] INT           NOT NULL,
    [Observation]          VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_HighRiskDrugs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_DCI] FOREIGN KEY ([DCIId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [fk_InventoryRiskLevelId] FOREIGN KEY ([InventoryRiskLevelId]) REFERENCES [Inventory].[InventoryRiskLevel] ([Id])
);


GO
ALTER TABLE [Inventory].[HighRiskDrugs] NOCHECK CONSTRAINT [fk_DCI];


GO
ALTER TABLE [Inventory].[HighRiskDrugs] NOCHECK CONSTRAINT [fk_InventoryRiskLevelId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla: [Inventory].[InventoryRiskLevel]  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'InventoryRiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'DCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'HighRiskDrugs', @level2type = N'COLUMN', @level2name = N'Id';

