CREATE TABLE [MixingStation].[NPTConfiguration] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InitialWeight] DECIMAL (18, 2) NOT NULL,
    [EndWeight]     DECIMAL (18, 2) NOT NULL,
    [ProductId]     INT             NOT NULL,
    CONSTRAINT [PK_NPTConfiguration] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NPTConfiguration_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [MixingStation].[NPTConfiguration] NOCHECK CONSTRAINT [FK_NPTConfiguration_InventoryProduct];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id de producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Peso final', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'EndWeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Peso inicial', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'InitialWeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'NPTConfiguration', @level2type = N'COLUMN', @level2name = N'Id';

