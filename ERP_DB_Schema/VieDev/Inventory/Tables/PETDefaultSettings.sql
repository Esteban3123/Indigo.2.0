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
ALTER TABLE [Inventory].[PETDefaultSettings] NOCHECK CONSTRAINT [FK_PETDefaultSettings_BillingGroup];


GO
ALTER TABLE [Inventory].[PETDefaultSettings] NOCHECK CONSTRAINT [FK_PETDefaultSettings_InventoryMeasurementUnit];


GO
ALTER TABLE [Inventory].[PETDefaultSettings] NOCHECK CONSTRAINT [FK_PETDefaultSettings_InventoryRiskLevel];


GO
ALTER TABLE [Inventory].[PETDefaultSettings] NOCHECK CONSTRAINT [FK_PETDefaultSettings_Manufacturer];


GO
ALTER TABLE [Inventory].[PETDefaultSettings] NOCHECK CONSTRAINT [FK_PETDefaultSettings_PackagingUnit];


GO
ALTER TABLE [Inventory].[PETDefaultSettings] NOCHECK CONSTRAINT [FK_PETDefaultSettings_ProductGroup];


GO
ALTER TABLE [Inventory].[PETDefaultSettings] NOCHECK CONSTRAINT [FK_PETDefaultSettings_ProductSubGroup];


GO
ALTER TABLE [Inventory].[PETDefaultSettings] NOCHECK CONSTRAINT [FK_PETDefaultSettings_ProductType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo de facturación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'BillingGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Fabricante', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ManufacturerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Unidad de embalaje', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'PackagingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del SubGrupo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Alternativo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'CodeAlternative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de producto, solo pueden ser tipo de productos insumo ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'ProductTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Nivel de riesgo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'RiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PETDefaultSettings', @level2type = N'COLUMN', @level2name = N'Id';

