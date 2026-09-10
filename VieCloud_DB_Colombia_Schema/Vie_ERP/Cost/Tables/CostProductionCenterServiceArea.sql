CREATE TABLE [Cost].[CostProductionCenterServiceArea] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionCenterId] INT NOT NULL,
    [FunctionalUnitId]   INT NOT NULL,
    CONSTRAINT [PK_CostProductionCenterServiceArea] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostProductionCenterServiceArea_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_CostProductionCenterServiceArea_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional o área de servicio del ERP con la que se realiza interfaz (FK a Payroll.FunctionalUnit). Representa el departamento, servicio o unidad operativa (ej: urgencia, laboratorio, imagenología, farmacia) que consume costos del centro de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la area del servicio del erp con el que se haga interfaz', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de producción o centro de costo (FK a Cost.CostProductionCenter). Referencia la unidad generadora de costos que presta servicios a la unidad funcional asociada.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la relación entre centro de producción y área de servicio (clave primaria). Registra el vínculo que asocia un centro de producción con su unidad funcional de destino para costeo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion del area de servicio', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los centros de producción (centros de costo) con sus áreas de servicio o unidades funcionales, permitiendo asociar qué unidades funcionales pertenecen a cada centro de producción para efectos de costeo y distribución de gastos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenterServiceArea';
