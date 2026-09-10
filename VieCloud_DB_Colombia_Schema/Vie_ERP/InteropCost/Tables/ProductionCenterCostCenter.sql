CREATE TABLE [InteropCost].[ProductionCenterCostCenter] (
    [Id]                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostCenterId]       INT          NOT NULL,
    [CostCenterCode]     VARCHAR (50) NOT NULL,
    [ProductionCenterId] INT          NOT NULL,
    CONSTRAINT [PK_ProductionCenterCostCenter__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionCenterCostCenter_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id]),
    CONSTRAINT [UQ_ProductionCenterCostCenter__CostCenterId] UNIQUE NONCLUSTERED ([CostCenterId] ASC)
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Centro de Producción o Unidad Funcional (FK a ProductionCenter). Referencia la ubicación física donde se generan costos operativos, servicios clínicos o administrativos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único del Centro de Costo (VARCHAR 50). Identificador legible asignado para clasificación contable, presupuestaria y de facturación (RIPS, glosa, contrato).', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo centro de costo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del Centro de Costo (INT, FK). Clave que vincula el centro de producción con su estructura contable y presupuestaria para asignación de gastos operativos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del Centro de Costo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico autonumerable (IDENTITY) de la relación ProductionCenter-CostCenter. Clave primaria de la tabla para trazabilidad de asociaciones de centros.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los centros de producción con sus centros de costo correspondientes, permitiendo asociar cada unidad productiva a su clasificación contable y financiera dentro del sistema.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenterCostCenter';
