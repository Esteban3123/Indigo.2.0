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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario o precio unitario de la parte, accesorio o consumible en la remisión de entrada (NUMERIC 18, puede ser nulo)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que especifica si la parte, accesorio o consumible debe ser depreciado contablemente (BIT, puede ser nulo)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'DepreciatePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la parte se debe depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'DepreciatePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'DepreciatePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la parte, accesorio o consumible registrado en el inventario de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'IdPartAccesoriesConsumibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Parte Accesorios Consumibles', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'IdPartAccesoriesConsumibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'IdPartAccesoriesConsumibles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del equipo padre o contenedor al que pertenece la parte, accesorio o consumible', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del detalle de remisión de entrada de equipos que contiene esta parte, accesorio o consumible', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'IdInputRemissionEquipmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la parte o accesorio', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'IdInputRemissionEquipmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'IdInputRemissionEquipmentDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico primario (Identity 1,1) único de la tabla, clave primaria en cluster', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de partes, accesorios y consumibles incluidos en remisiones de entrada de equipos (activos fijos). Permite detallar qué componentes acompañan a cada equipo recibido, si se deprecian por separado y su valor individual.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartAccesoriesConsumiblesInputRemission';
