CREATE TABLE [Inventory].[PETDefaultSettings] (
    [Id]                INT          IDENTITY (1, 1) NOT NULL,
    [RiskLevelId]       INT          NOT NULL,
    [ProductTypeId]     INT          NOT NULL,
    [CodeAlternative]   VARCHAR (30) NOT NULL,
    [ProductGroupId]    INT          NOT NULL,
    [ProductSubGroupId] INT          NOT NULL,
    [MeasurementUnitId] INT          NOT NULL,
    [PackagingUnitId]   INT          NOT NULL,
    [ManufacturerId]    INT          NOT NULL,
    [BillingGroupId]    INT          NOT NULL,
    [CreationUser]      VARCHAR (20) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL,
    [ModificationUser]  VARCHAR (20) NULL,
    [ModificationDate]  DATETIME     NULL,
    CONSTRAINT [PK_PETDefaultSettings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PETDefaultSettings_BillingGroup] FOREIGN KEY ([BillingGroupId]) REFERENCES [Billing].[BillingGroup] ([Id]),
    CONSTRAINT [FK_PETDefaultSettings_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_PETDefaultSettings_InventoryRiskLevel] FOREIGN KEY ([RiskLevelId]) REFERENCES [Inventory].[InventoryRiskLevel] ([Id]),
    CONSTRAINT [FK_PETDefaultSettings_Manufacturer] FOREIGN KEY ([ManufacturerId]) REFERENCES [Inventory].[Manufacturer] ([Id]),
    CONSTRAINT [FK_PETDefaultSettings_PackagingUnit] FOREIGN KEY ([PackagingUnitId]) REFERENCES [Inventory].[PackagingUnit] ([Id]),
    CONSTRAINT [FK_PETDefaultSettings_ProductGroup] FOREIGN KEY ([ProductGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id]),
    CONSTRAINT [FK_PETDefaultSettings_ProductSubGroup] FOREIGN KEY ([ProductSubGroupId]) REFERENCES [Inventory].[ProductSubGroup] ([Id]),
    CONSTRAINT [FK_PETDefaultSettings_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [Inventory].[ProductType] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME, NULLABLE); marca temporal del último cambio en la configuración predeterminada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó el registro (VARCHAR 20, NULLABLE); auditoría de cambios en la configuración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME); marca temporal de cuándo se registró la configuración predeterminada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (VARCHAR 20); auditoría de origen del registro de configuración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de facturación (FK a BillingGroup); agrupa el insumo para propósitos de cobro, RIPS o glosas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de facturación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'BillingGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del fabricante (FK a Manufacturer); proveedor o laboratorio productor del insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ManufacturerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Fabricante', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ManufacturerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ManufacturerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de embalaje (FK a PackagingUnit); presentación o formato de empaque del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'PackagingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Unidad de embalaje', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'PackagingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'PackagingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida (FK a InventoryMeasurementUnit); unidad en que se cuantifica el insumo (mg, ml, unidades, etc.)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del subgrupo del producto (FK a ProductSubGroup); subclasificación o categoría secundaria del insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del SubGrupo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de producto (FK a ProductGroup); agrupa insumos por familia o categoría principal', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alternativo del producto (VARCHAR 30); denominación auxiliar, referencia secundaria o SKU alternativo del insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'CodeAlternative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Alternativo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'CodeAlternative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'CodeAlternative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de producto, restringido a insumos médicos/farmacéuticos (FK a ProductType); especifica categoría de insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de producto, solo pueden ser tipo de productos insumo ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ProductTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del nivel de riesgo del producto (FK a InventoryRiskLevel); clasifica el riesgo sanitario del insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'RiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Nivel de riesgo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'RiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'RiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable (INT IDENTITY) de la configuración predeterminada del producto de inventario PET', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración predeterminada para el registro de medicamentos PET (productos especiales de trazabilidad) en inventario: define los valores por defecto de nivel de riesgo, tipo de producto, unidades de medida, empaque, fabricante y grupo de facturación que se asignan automáticamente al crear un ítem PET.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings';
