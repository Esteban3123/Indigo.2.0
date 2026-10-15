CREATE TABLE [Common].[SupplierDetailType] (
    [Id]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SupplierId]     INT NOT NULL,
    [SupplierTypeId] INT NOT NULL,
    CONSTRAINT [PK_SupplierDetailType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SupplierDetailType_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_SupplierDetailType_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id])
);


GO
ALTER TABLE [Common].[SupplierDetailType] NOCHECK CONSTRAINT [FK_SupplierDetailType_Supplier];


GO
ALTER TABLE [Common].[SupplierDetailType] NOCHECK CONSTRAINT [FK_SupplierDetailType_SupplierType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de proveedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierDetailType', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierDetailType', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SupplierDetailType', @level2type = N'COLUMN', @level2name = N'Id';

