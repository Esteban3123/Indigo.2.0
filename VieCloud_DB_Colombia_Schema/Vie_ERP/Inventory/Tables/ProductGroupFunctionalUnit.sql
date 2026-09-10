CREATE TABLE [Inventory].[ProductGroupFunctionalUnit] (
    [Id]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductGroupId]    INT NOT NULL,
    [FunctionalUnitId]  INT NOT NULL,
    [CostAccountId]     INT NOT NULL,
    [SalesAccountId]    INT NOT NULL,
    [DiscountAccountId] INT NULL,
    CONSTRAINT [PK_ProductGroupFunctionalUnit__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductGroupFunctionalUnit_DiscountAccount] FOREIGN KEY ([DiscountAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroupFunctionalUnit_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ProductGroupFunctionalUnit_MainAccounts] FOREIGN KEY ([CostAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroupFunctionalUnit_MainAccounts1] FOREIGN KEY ([SalesAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroupFunctionalUnit_ProductGroup] FOREIGN KEY ([ProductGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id])
);




GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProductGroupFunctionalUnit__ProductGroupId__FunctionalUnitId__SalesAccountId]
    ON [Inventory].[ProductGroupFunctionalUnit]([ProductGroupId] ASC, [FunctionalUnitId] ASC, [SalesAccountId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductGroupFunctionalUnit_UniqueCostAccount]
    ON [Inventory].[ProductGroupFunctionalUnit]([ProductGroupId] ASC, [FunctionalUnitId] ASC, [CostAccountId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProductGroupFunctionalUnit__FunctionalUnitId__ProductGroupId]
    ON [Inventory].[ProductGroupFunctionalUnit]([FunctionalUnitId] ASC, [ProductGroupId] ASC);


GO
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-10-21
-- Description:	Impedir que se eliminen detalles si se encuentra parametrizado que la cuenta contable de costo y venta se tomará del grupo de productos
-- =============================================
CREATE TRIGGER [Inventory].[tgg_ProductGroupFunctionalUnitDelete]
   ON [Inventory].[ProductGroupFunctionalUnit]
   AFTER DELETE
AS 
BEGIN
	SET NOCOUNT ON

    -- Si se va a eliminar debe existir otro correo con formato valido
	IF EXISTS
	(
		SELECT 1
		FROM Inventory.SettingInventory
		WHERE AssociateCostMainAccount = 2
	)
	BEGIN
		THROW 51000, 'Error generado por control de eliminacion de detalles desde trigger cuando se encuentra parametrizado que la cuenta contable de costo y venta se tomará del grupo de productos', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable (MainAccounts) donde se registran movimientos de descuentos aplicados a grupos de productos. Referencia a Contabilidad General para auditoría de rebajas y promociones. Nulo si no aplica descuento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'DiscountAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable, donde se registra el valor de los movimientos de descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'DiscountAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'DiscountAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable (MainAccounts) para registrar ingresos por ventas del grupo de productos en la unidad funcional. Vinculación a Contabilidad General para control de revenue.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'SalesAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de cuenta de ventas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'SalesAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'SalesAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable (MainAccounts) donde se registra el costo asociado al grupo de productos. Referencia a Contabilidad General para valorización de inventario y margen de ganancia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'CostAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable del costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'CostAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'CostAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad funcional (centro de atención, departamento, servicio) a la que se asigna el grupo de productos. Vinculación a estructura organizacional para control de inventario por dependencia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo/familia de productos parametrizado (cabecera de productos). Referencia a configuración maestro que agrupa artículos para control contable y operativo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de parametros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria de la tabla. Identificador único de la asignación de grupo de productos a unidad funcional con sus cuentas contables asociadas (costo, venta, descuento).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona grupos de productos con unidades funcionales, asignando las cuentas contables de costo, ventas y descuentos que aplican a cada combinación. Permite controlar cómo se registran contablemente los movimientos de inventario según el grupo de producto y la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit';
