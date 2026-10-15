CREATE TABLE [Contract].[ContractPackageProduct] (
    [Id]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractPackageId] INT             NOT NULL,
    [ProductId]         INT             NOT NULL,
    [Description]       VARCHAR (200)   NOT NULL,
    [Quantity]          INT             NOT NULL,
    [ApplyCondition]    BIT             NOT NULL,
    [CreationUser]      VARCHAR (20)    NOT NULL,
    [CreationDate]      DATETIME        NOT NULL,
    [ModificationUser]  VARCHAR (20)    NULL,
    [ModificationDate]  DATETIME        NULL,
    [UnitValue]         NUMERIC (20, 2) CONSTRAINT [DF__ContractP__UnitV__69D7F9A5] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ContractPackageProduct] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractPackageProduct_ContractPackage] FOREIGN KEY ([ContractPackageId]) REFERENCES [Contract].[ContractPackage] ([Id]),
    CONSTRAINT [FK_ContractPackageProduct_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Contract].[ContractPackageProduct] NOCHECK CONSTRAINT [FK_ContractPackageProduct_ContractPackage];


GO
ALTER TABLE [Contract].[ContractPackageProduct] NOCHECK CONSTRAINT [FK_ContractPackageProduct_InventoryProduct];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor unitario asignado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageProduct', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageProduct', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageProduct', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageProduct', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageProduct', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano donde 0 - No aplica condición, 1 - Aplica condición.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageProduct', @level2type = N'COLUMN', @level2name = N'ApplyCondition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la cantidad de un producto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageProduct', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información fundamental del paquete de contrato del producto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageProduct', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del producto.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageProduct', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera, del paquete de contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageProduct', @level2type = N'COLUMN', @level2name = N'ContractPackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Idetificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractPackageProduct', @level2type = N'COLUMN', @level2name = N'Id';

