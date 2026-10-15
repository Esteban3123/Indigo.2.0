CREATE TABLE [Inventory].[ConsignmentCostListDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [ConsignmentCostListId] INT             NOT NULL,
    [ProductId]             INT             NOT NULL,
    [CostNew]               NUMERIC (12, 2) NULL,
    [CreationUser]          VARCHAR (20)    NOT NULL,
    [CreationDate]          DATETIME        NOT NULL,
    [ModificationUser]      VARCHAR (20)    NULL,
    [ModificationDate]      DATETIME        NULL,
    CONSTRAINT [PK_ConsignmentCostListDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsignmentCostListDetail_ConsignmentCostList] FOREIGN KEY ([ConsignmentCostListId]) REFERENCES [Inventory].[ConsignmentCostList] ([Id]),
    CONSTRAINT [FK_ConsignmentCostListDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[ConsignmentCostListDetail] NOCHECK CONSTRAINT [FK_ConsignmentCostListDetail_ConsignmentCostList];


GO
ALTER TABLE [Inventory].[ConsignmentCostListDetail] NOCHECK CONSTRAINT [FK_ConsignmentCostListDetail_InventoryProduct];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo nuevo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetail', @level2type = N'COLUMN', @level2name = N'CostNew';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla encabezado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentCostListId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentCostListDetail', @level2type = N'COLUMN', @level2name = N'Id';

