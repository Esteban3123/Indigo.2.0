CREATE TABLE [FixedAsset].[FixedAssetEntryDevolutionDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetEntryDevolutionId] INT             NOT NULL,
    [FixedAssetEntryItemId]       INT             NOT NULL,
    [FixedAssetEntryItemDetailId] INT             NOT NULL,
    [UnitValue]                   DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]               DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]               NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                    DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]          NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]               DECIMAL (18, 2) NOT NULL,
    [TotalValue]                  DECIMAL (18, 2) NOT NULL,
    [RTFPercentage]               NUMERIC (5, 2)  CONSTRAINT [DF_FixedAssetEntryDevolutionDetail_RTFPercentage] DEFAULT ((0)) NOT NULL,
    [RTFValue]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetEntryDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryDevolution] FOREIGN KEY ([FixedAssetEntryDevolutionId]) REFERENCES [FixedAsset].[FixedAssetEntryDevolution] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryItem] FOREIGN KEY ([FixedAssetEntryItemId]) REFERENCES [FixedAsset].[FixedAssetEntryItem] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryItemDetail] FOREIGN KEY ([FixedAssetEntryItemDetailId]) REFERENCES [FixedAsset].[FixedAssetEntryItemDetail] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryDevolutionDetail] NOCHECK CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryDevolution];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryDevolutionDetail] NOCHECK CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryDevolutionDetail] NOCHECK CONSTRAINT [FK_FixedAssetEntryDevolutionDetail_FixedAssetEntryItemDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la retefuente que se cobrara al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje de la retefuente que se le aplica al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del descuento, se obtiene multiplicando el SubTotalValue por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del porcentaje del iva, se obtiene multiplicando el Subtotalvalue por el porcentaje del IVA', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del iva que se va a cobrar al Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Subtotal del detalle de la devolución. Es la multiplicacion de la cantidad por el valor unitario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor Unitario del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de detalle de elemento de entrada de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryItemDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de elemento de entrada de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la devolucion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';

