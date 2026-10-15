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
ALTER TABLE [Inventory].[ProductGroupFunctionalUnit] NOCHECK CONSTRAINT [FK_ProductGroupFunctionalUnit_DiscountAccount];


GO
ALTER TABLE [Inventory].[ProductGroupFunctionalUnit] NOCHECK CONSTRAINT [FK_ProductGroupFunctionalUnit_FunctionalUnit];


GO
ALTER TABLE [Inventory].[ProductGroupFunctionalUnit] NOCHECK CONSTRAINT [FK_ProductGroupFunctionalUnit_MainAccounts];


GO
ALTER TABLE [Inventory].[ProductGroupFunctionalUnit] NOCHECK CONSTRAINT [FK_ProductGroupFunctionalUnit_MainAccounts1];


GO
ALTER TABLE [Inventory].[ProductGroupFunctionalUnit] NOCHECK CONSTRAINT [FK_ProductGroupFunctionalUnit_ProductGroup];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable, donde se registra el valor de los movimientos de descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'DiscountAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de cuenta de ventas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'SalesAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable del costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'CostAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de parametros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroupFunctionalUnit', @level2type = N'COLUMN', @level2name = N'Id';

