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

