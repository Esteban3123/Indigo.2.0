CREATE TABLE [Inventory].[SettingInventoryFunctionalUnit] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SettingInventoryId] INT NOT NULL,
    [FunctionalUnitId]   INT NOT NULL,
    [CostAccountId]      INT NOT NULL,
    [SalesAccountId]     INT NOT NULL,
    CONSTRAINT [PK_SettingInventoryFunctionalUnit__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingInventoryFunctionalUnit_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_SettingInventoryFunctionalUnit_MainAccounts] FOREIGN KEY ([CostAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingInventoryFunctionalUnit_MainAccounts1] FOREIGN KEY ([SalesAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingInventoryFunctionalUnit_SettingInventory] FOREIGN KEY ([SettingInventoryId]) REFERENCES [Inventory].[SettingInventory] ([Id])
);




GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SettingInventoryFunctionalUnit__FunctionalUnitId__SettingInventoryId]
    ON [Inventory].[SettingInventoryFunctionalUnit]([FunctionalUnitId] ASC, [SettingInventoryId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de ventas (ingresos por venta de inventario). Referencia a MainAccounts en contabilidad general para registrar ingresos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'SalesAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID cuenta de ventas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'SalesAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'SalesAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable de costo (costo de venta/costo de inventario). Referencia a MainAccounts para contabilizar el costo de bienes vendidos o consumidos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'CostAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable del costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'CostAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'CostAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional (centro de atención, servicio, área operativa). Referencia a FunctionalUnit para asociar configuración de inventario por departamento o punto de servicio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro padre de configuración/parametrización de inventario. Referencia a SettingInventory que agrupa todas las reglas y políticas de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de parametros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'SettingInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la asociación entre parámetros de inventario, unidad funcional y cuentas contables (clave primaria clustered).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona configuraciones de inventario con unidades funcionales, definiendo las cuentas contables de costo y ventas asociadas a cada combinación. Permite controlar qué cuentas se afectan contablemente cuando se registran movimientos de inventario por unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'SettingInventoryFunctionalUnit';

GO
CREATE NONCLUSTERED INDEX [IX_SettingInventoryFunctionalUnit_SettingInventoryId]
    ON [Inventory].[SettingInventoryFunctionalUnit]([SettingInventoryId] ASC)
    INCLUDE([FunctionalUnitId], [CostAccountId], [SalesAccountId]);
