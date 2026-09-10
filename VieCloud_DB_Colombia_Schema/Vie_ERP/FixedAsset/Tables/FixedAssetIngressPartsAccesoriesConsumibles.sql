CREATE TABLE [FixedAsset].[FixedAssetIngressPartsAccesoriesConsumibles] (
    [Id]                           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetIngressEquipment] INT          NOT NULL,
    [IdEquipment]                  INT          NOT NULL,
    [IdPartsAccesoriesConsumibles] INT          NOT NULL,
    [DepreciatePart]               BIT          NULL,
    [Value]                        NUMERIC (18) NULL,
    CONSTRAINT [PK_FixedAssetIngressPartsAccesoriesConsumibles_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetIngressPartsAccesoriesConsumibles_FixedAssetEquipment] FOREIGN KEY ([IdEquipment]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (NUMERIC 18) del repuesto, accesorio o consumible asociado al equipo; importe de depreciación o costo unitario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que determina si la parte, accesorio o consumible debe ser depreciado contablemente como activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'DepreciatePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parte a depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'DepreciatePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'DepreciatePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, FK) del repuesto, accesorio o consumible registrado en el catálogo de partes; referencia a la entidad de componentes', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdPartsAccesoriesConsumibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Parts Accesories Consumibles', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdPartsAccesoriesConsumibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdPartsAccesoriesConsumibles';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del equipo (INT, FK → FixedAssetItem) al cual pertenecen las partes, accesorios o consumibles; enlace con activo fijo principal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ingreso o adquisición de activos fijos (INT, FK); relación con el registro de entrada del equipo al inventario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdFixedAssetIngressEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Equipo de entrada de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdFixedAssetIngressEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'IdFixedAssetIngressEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumerado (INT IDENTITY) de cada asociación entre equipo e ingreso con sus partes, accesorios y consumibles', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de partes, accesorios y consumibles asociados al ingreso de equipos de activos fijos. Permite detallar los componentes que acompañan a un equipo al momento de su recepción o inventario, indicando si se deprecian y su valor.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressPartsAccesoriesConsumibles';
