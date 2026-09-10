CREATE TABLE [FixedAsset].[FixedAssetReclassificationDetail] (
    [Id]                           INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetReclassificationId] INT     NOT NULL,
    [PhysicalAssetId]              INT     NOT NULL,
    [HasOutput]                    BIT     NOT NULL,
    [Status]                       BIT     NOT NULL,
    [OutputRefund]                 BIT     NOT NULL,
    [AdquisitionType]              TINYINT NOT NULL,
    [MainAccountId]                INT     NULL,
    CONSTRAINT [PK_FixedAssetReclassificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetReclassificationDetail_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetReclassificationDetail_FixedAssetReclassification] FOREIGN KEY ([FixedAssetReclassificationId]) REFERENCES [FixedAsset].[FixedAssetReclassification] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK nullable) de la cuenta contable principal (GL) del activo fijo antes de la reclasificación. Vincula al plan de cuentas para auditoría contable y conciliación de saldos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable del activo fijo antes de la reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TinyInt (1=Compra Directa, 3=Comodato, 4=Donación, 5=Traslado de Bienes, 6=Otro Concepto, 7=Leasing Financiero, 8=Comodato Tercerizado, 9=Renting Financiero, 10=Renting Operativo) que captura el origen o forma de adquisición del activo al momento de la reclasificación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Adquisicion que tenía al momento de realizar la reclasificación  1 - Compra Directa  3 - Comodato  4 - Donacion  5 - Traspaso de Bienes  6 - Otro Concepto  7 - Leasing Financiero  8 - Comodato Tercerizado  9 - Renting Financiero  10 - Renting Operativo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bit (0=No, 1=Sí) que especifica si el activo fijo tiene salida por devolución al momento de la reclasificación. Restricción: activos descargados no pueden ser reclasificados (saldos contables deben estar en cero).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'OutputRefund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Activo tiene Salida (Por devolución) al momento de realizar la reclasificación.    Nota: Un activo que ya ha salido no será reclasificado puesto que el estado de sus cuentas, tanto de activo como de depreciación deben estar en 0', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'OutputRefund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'OutputRefund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bit (1=Activo, 0=Inactivo) que especifica el estado operativo del activo fijo al momento de realizar la reclasificación contable o traslado entre centros de atención.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el estado del activo al momento de realizar la reclasificación  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bit (0=No, 1=Sí) que indica si el activo fijo tiene salida registrada al momento de la reclasificación. Activos ya descargados no se reclasifican (cuentas de activo y depreciación acumulada deben estar en cero).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'HasOutput';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Activo tiene Salida al momento de realizar la reclasificación.    Nota: Un activo que ya ha salido no será reclasificado puesto que el estado de sus cuentas, tanto de activo como de depreciación deben estar en 0', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'HasOutput';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'HasOutput';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del activo fijo físico siendo reclasificado. Referencia a inventario de bienes, equipo, propiedad o infraestructura.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Activo Fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cabecera/encabezado de reclasificación de activos fijos. Vincula al registro maestro de reclasificación contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetReclassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de Reclasificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetReclassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetReclassificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del detalle de reclasificación del activo fijo. Clave primaria en cascada con FixedAssetReclassificationId y PhysicalAssetId.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la reclasificación del artículo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de reclasificación de activos fijos: registra cada activo físico involucrado en un proceso de reclasificación, indicando si fue dado de baja o reintegrado, el tipo de adquisición y la cuenta contable principal asociada.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail';
