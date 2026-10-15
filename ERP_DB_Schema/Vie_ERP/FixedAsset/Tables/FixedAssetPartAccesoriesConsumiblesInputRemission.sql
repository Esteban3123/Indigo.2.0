CREATE TABLE [FixedAsset].[FixedAssetPartAccesoriesConsumiblesInputRemission] (
    [Id]                              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdInputRemissionEquipmentDetail] INT          NOT NULL,
    [IdEquipment]                     INT          NOT NULL,
    [IdPartAccesoriesConsumibles]     INT          NOT NULL,
    [DepreciatePart]                  BIT          NULL,
    [Value]                           NUMERIC (18) NULL,
    CONSTRAINT [PK_FixedAssetPartAccesoriesConsumiblesInputRemission_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la parte se debe depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'DepreciatePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Parte Accesorios Consumibles', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'IdPartAccesoriesConsumibles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la parte o accesorio', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'IdInputRemissionEquipmentDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'Id';

