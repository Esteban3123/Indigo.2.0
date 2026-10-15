CREATE TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetailPart] (
    [Id]                            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionEntranceItemDetailId] INT          NOT NULL,
    [PartAccesoriesConsumiblesId]   INT          NOT NULL,
    [DepreciatePart]                BIT          NOT NULL,
    [Value]                         NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_FixedAssetPartAccesoriesConsumiblesInputRemission] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPartAccesoriesConsumiblesInputRemission_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([PartAccesoriesConsumiblesId]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id]),
    CONSTRAINT [FK_PartAccesoriesConsumiblesInputRemission_InputRemissionEquipment] FOREIGN KEY ([RemissionEntranceItemDetailId]) REFERENCES [FixedAsset].[FixedAssetRemissionEntranceItemDetail] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetailPart] NOCHECK CONSTRAINT [FK_FixedAssetPartAccesoriesConsumiblesInputRemission_FixedAssetPartsAccesoriesConsumables];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetailPart] NOCHECK CONSTRAINT [FK_PartAccesoriesConsumiblesInputRemission_InputRemissionEquipment];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor de la parte o componente, este campo solo se llena si el campo DepreciatePart esta en 1', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la parte se debe depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'DepreciatePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la parte', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'PartAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de detalle del elemento de entrada de remisión', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'RemissionEntranceItemDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'Id';

