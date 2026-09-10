CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrderEquipmentDetail] (
    [Id]                                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetPurchaseOrderEquipment] INT          NOT NULL,
    [IdEquipment]                        INT          NOT NULL,
    [LicensePlate]                       VARCHAR (50) NOT NULL,
    [Serie]                              VARCHAR (50) NOT NULL,
    [IdResponsible]                      INT          NOT NULL,
    [IdFunctionalUnit]                   INT          NOT NULL,
    [IdLocation]                         INT          NOT NULL,
    [AdquisitionDate]                    DATE         NOT NULL,
    [Depreciate]                         BIT          NOT NULL,
    [ComponentDepreciate]                BIT          NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrderEquipmentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipmentDetail_FixedAssetResponsible] FOREIGN KEY ([IdResponsible]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipmentDetail_FunctionalUnit] FOREIGN KEY ([IdFunctionalUnit]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipmentDetail_Location] FOREIGN KEY ([IdLocation]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de depreciación de componente (BIT, nullable). Define si el componente del equipo aplica depreciación contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'ComponentDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Componente depreciacón', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'ComponentDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'ComponentDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de depreciación (BIT). Define si el activo fijo equipo aplica depreciación contable en el período', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de adquisición del equipo (DATE). Fecha en que se compró, adquirió o recibió el activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ubicación del activo (INT, FK a FixedAsset.FixedAssetLocation). Sede, piso, departamento o zona donde está localizado el equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdLocation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id ubicación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdLocation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdLocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad funcional responsable (INT, FK a Payroll.FunctionalUnit). Centro de costo, departamento clínico o área operativa que custoda el activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Unidad funcional ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del responsable del activo (INT, FK a FixedAsset.FixedAssetResponsible). Persona o profesional de salud a cargo de la custodia y uso del equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID responsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de serie del equipo (VARCHAR 50). Identificador único del fabricante para trazabilidad y control del activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de serie', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Serie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Placa o código de identificación del activo (VARCHAR 50). Etiqueta interna para registro y control de inventario de bienes fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'LicensePlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Placa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'LicensePlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'LicensePlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo o catálogo de equipo (INT, FK). Referencia al catálogo maestro del equipamiento médico o no médico', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de detalle de orden de compra de activos fijos (INT, FK). Vincula este equipo a su orden de compra original en el ciclo de adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFixedAssetPurchaseOrderEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id. Equipo de orden de compra de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFixedAssetPurchaseOrderEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFixedAssetPurchaseOrderEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de detalle (INT IDENTITY). Clave primaria de la línea de equipo en la orden de compra de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de equipos registrados en órdenes de compra de activos fijos. Cada fila representa un equipo específico adquirido, con su placa, serie, responsable, ubicación y fecha de adquisición, permitiendo el seguimiento individual de activos para efectos de inventario y depreciación contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderEquipmentDetail';
