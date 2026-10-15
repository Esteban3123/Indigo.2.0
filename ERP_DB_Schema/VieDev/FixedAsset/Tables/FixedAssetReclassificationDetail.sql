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
ALTER TABLE [FixedAsset].[FixedAssetReclassificationDetail] NOCHECK CONSTRAINT [FK_FixedAssetReclassificationDetail_FixedAssetPhysicalAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetReclassificationDetail] NOCHECK CONSTRAINT [FK_FixedAssetReclassificationDetail_FixedAssetReclassification];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Adquisicion que tenía al momento de realizar la reclasificación  1 - Compra Directa  3 - Comodato  4 - Donacion  5 - Traspaso de Bienes  6 - Otro Concepto  7 - Leasing Financiero  8 - Comodato Tercerizado  9 - Renting Financiero  10 - Renting Operativo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el Activo tiene Salida (Por devolución) al momento de realizar la reclasificación.    Nota: Un activo que ya ha salido no será reclasificado puesto que el estado de sus cuentas, tanto de activo como de depreciación deben estar en 0', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'OutputRefund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado del activo al momento de realizar la reclasificación  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el Activo tiene Salida al momento de realizar la reclasificación.    Nota: Un activo que ya ha salido no será reclasificado puesto que el estado de sus cuentas, tanto de activo como de depreciación deben estar en 0', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassificationDetail', @level2type = N'COLUMN', @level2name = N'HasOutput';

