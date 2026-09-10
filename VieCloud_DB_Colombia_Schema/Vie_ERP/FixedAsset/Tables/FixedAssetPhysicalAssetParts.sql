CREATE TABLE [FixedAsset].[FixedAssetPhysicalAssetParts] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PhysicalAssetId]             INT             NOT NULL,
    [PartAccesoriesConsumiblesId] INT             NOT NULL,
    [DepreciatePart]              BIT             NOT NULL,
    [HistoricalValue]             DECIMAL (18, 2) NOT NULL,
    [HasOutput]                   BIT             CONSTRAINT [DF_FixedAssetPhysicalAssetParts_HasOutput] DEFAULT ((0)) NOT NULL,
    [OutputDate]                  DATE            NULL,
    CONSTRAINT [PK_FixedAssetPhysicalAssetParts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPhysicalAssetParts_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([PartAccesoriesConsumiblesId]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAssetParts_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de salida, retiro o disposición final del activo fijo o componente. Tipo DATE. Campo nulo si el activo aún está en operación. Referencia FK a FixedAssetPhysicalAsset.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'OutputDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Fecha de salida del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'OutputDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'OutputDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que especifica si el activo fijo o componente ha sido dado de salida, retirado o dispuesto. Valor por defecto: 0 (sin salida). Búsqueda: desincorporación, retiro, baja de activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'HasOutput';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Activo tiene Salida', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'HasOutput';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'HasOutput';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor histórico o costo original de la parte, accesorio o consumible agregado al activo fijo. Tipo DECIMAL(18,2). Se registra solo si DepreciatePart=1 para cálculo de depreciación acumulada.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor de la parte o componente, este campo solo se llena si el campo DepreciatePart esta en 1', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que determina si la parte, accesorio o consumible debe incluirse en el cálculo de depreciación del activo fijo. Si=1, se deprecia; si=0, se excluye.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'DepreciatePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la parte se debe depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'DepreciatePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'DepreciatePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la parte, accesorio o consumible componente agregado al activo fijo. FK a FixedAssetPartsAccesoriesConsumables(Id). Búsqueda: componente, pieza, accesorio, consumible.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'PartAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la parte del activo agregado ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'PartAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'PartAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del activo fijo físico padre al que pertenecen las partes y accesorios. FK a FixedAssetPhysicalAsset(Id). Búsqueda: bien, propiedad, equipo, maquinaria.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del activo fijo ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) de la relación entre activo fijo y sus partes/accesorios. Clave primaria. Búsqueda: registro de componentes, descomposición de activos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de las partes del Activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Partes, accesorios y consumibles asociados a un activo físico (bien de uso). Registra si cada parte se deprecia de forma independiente, su valor histórico y si ya fue dado de baja con su fecha de salida.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts';
