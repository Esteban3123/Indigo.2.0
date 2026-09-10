CREATE TABLE [FixedAsset].[FixedAssetInitialBalanceItem] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetInitialBalanceId] INT             NOT NULL,
    [ItemId]                     INT             NOT NULL,
    [Serie]                      VARCHAR (50)    NOT NULL,
    [Plate]                      VARCHAR (50)    NOT NULL,
    [LocationId]                 INT             NOT NULL,
    [ResponsibleId]              INT             NOT NULL,
    [SupplierId]                 INT             NULL,
    [HistoricalValue]            DECIMAL (18, 2) NOT NULL,
    [FairValue]                  DECIMAL (18, 2) NOT NULL,
    [TrademarkId]                INT             NOT NULL,
    [Model]                      VARCHAR (100)   NOT NULL,
    [PolicyId]                   INT             NULL,
    [HandlesWarranty]            BIT             NOT NULL,
    [WarrantyExpirationDate]     DATE            NULL,
    [AdquisitionDate]            DATE            NOT NULL,
    [Depreciate]                 BIT             NOT NULL,
    [StatusAssetId]              INT             NOT NULL,
    [Status]                     BIT             NOT NULL,
    [AdquisitionType]            TINYINT         CONSTRAINT [DF_FixedAssetInitialBalanceItem_AdquisitionType] DEFAULT ((1)) NOT NULL,
    [ValidMinorAmount]           BIT             CONSTRAINT [DF__FixedAsse__Valid__2835EA28] DEFAULT ((1)) NULL,
    [Amortize]                   BIT             CONSTRAINT [DF_FixedAssetInitialBalanceItem_Amortize] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetInitialBalanceItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetInitialBalance] FOREIGN KEY ([FixedAssetInitialBalanceId]) REFERENCES [FixedAsset].[FixedAssetInitialBalance] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetItem] FOREIGN KEY ([ItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetLocation] FOREIGN KEY ([LocationId]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetPoliza] FOREIGN KEY ([PolicyId]) REFERENCES [FixedAsset].[FixedAssetPolicy] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetResponsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetStatusAsset] FOREIGN KEY ([StatusAssetId]) REFERENCES [FixedAsset].[FixedAssetStatusAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetTrademark] FOREIGN KEY ([TrademarkId]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItem_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItem] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetTrademark];




GO



GO



GO



GO



GO



GO



GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItem] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItem_FixedAssetTrademark];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de amortización del activo (BIT: 1=Sí amortiza, 0=No amortiza). Aplica a intangibles y otros activos no depreciables según norma contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Amortize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Amortiza | 1 = Si | 0 = No | ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Amortize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Amortize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación de cuantía menor (BIT: 1=Sí aplica, 0=No aplica). Determina si el activo cumple umbral mínimo de registro contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'ValidMinorAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valida menor Cuantía | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'ValidMinorAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'ValidMinorAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de adquisición del activo (TINYINT: 1=Compra directa, 3=Comodato, 4=Donación, 5=Traspaso de bienes, 6=Otro concepto, 7=Leasing financiero). Especifica origen y modalidad de incorporación del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'AdquisitionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Adquisición  Especirfica el Tipo de Adquisicion   1 - Compra Directa  3 - Comodato  4 - Donadacion  5 - Traspaso de Bienes  6 - Otro Concepto  7 - Leasing Financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'AdquisitionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'AdquisitionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operacional del activo (BIT: 1=Activo/En uso, 0=Inactivo/Dado de baja). Indica disponibilidad actual para operaciones.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el estado del activo  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de estado detallado del activo (FK a FixedAssetStatusAsset). Referencia tabla con estados como: disponible, en mantenimiento, obsoleto, desaparecido, etc.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'StatusAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del activo, Se diligencia con la información de la tabla FixedAssetStatusAsset', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'StatusAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'StatusAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de depreciación (BIT: 1=Sí deprecia, 0=No deprecia). Depende del atributo AllowDepreciate del artículo: si prohíbe depreciar fuerza a 0; si permite es opcional por usuario.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deprecia (1 - SI, 0 - NO)    Nota: este campo depende del AllowDeprecate que se encuentra en el articulo ya que si alla esta marcado como no permite depreciar aca tambien debe ir siempre 0, pero si alla esta marcado como que si permite depreciar entonces aca es opcional por el usuario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de adquisición del activo (DATE). Marca inicio del período para cálculo de depreciación y vida útil contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Fecha de Adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento de garantía del activo (DATE NULL). Solo se completa si HandlesWarranty=1; relevante para seguimiento de cobertura y reclamaciones.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de vencimiento de la garantia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'WarrantyExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de cobertura de garantía (BIT: 1=Sí tiene garantía, 0=Sin garantía). Activa la funcionalidad de seguimiento de vencimiento y reclamaciones.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'HandlesWarranty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el item maneja garantia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'HandlesWarranty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'HandlesWarranty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de póliza de seguro asociada (INT FK a FixedAssetPolicy, NULL si no aplica). Vincula activo a cobertura aseguradora.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'PolicyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de política', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'PolicyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'PolicyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modelo o referencia técnica del artículo (VARCHAR 100). Especifica variante exacta dentro de la marca para identificación y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modelo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de marca registrada (INT FK a FixedAssetTrademark). Referencia fabricante u origen del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'TrademarkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de marca registrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'TrademarkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'TrademarkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor razonable o justo del activo (DECIMAL 18,2). Valuación de mercado para propósitos de medición contable y revaluos periódicos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'FairValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor razonable del articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'FairValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'FairValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor histórico de adquisición del activo (DECIMAL 18,2). Costo original base para cálculo de depreciación acumulada.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor historico', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor (INT FK a Common.Supplier, NULL si adquisición sin proveedor). Trazabilidad de origen y gestión de relaciones comerciales.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del responsable del activo (INT FK a FixedAssetResponsible). Persona o área custodio del bien en centro de costo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'ResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Responsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'ResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'ResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ubicación física (INT FK a FixedAssetLocation). Localización del activo en la infraestructura: oficina, almacén, unidad funcional, etc.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'LocationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Localización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'LocationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'LocationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de placa o registro del activo (VARCHAR 50). Identificador único visible para control y seguimiento físico, típico en vehículos y bienes móviles.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Plate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Placa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Plate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Plate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de serie del artículo (VARCHAR 50). Identificación única del fabricante para trazabilidad, garantía y detección de duplicados.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la serie del articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Serie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del artículo/ítem (INT FK a FixedAssetItem). Referencia al catálogo maestro de bienes para consistencia y descripción completa.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'ItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'ItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'ItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del lote/balance inicial de activos fijos (INT FK a FixedAssetInitialBalance). Agrupa activos del saldo de apertura contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'FixedAssetInitialBalanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de saldo inicial de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'FixedAssetInitialBalanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'FixedAssetInitialBalanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incremental (INT IDENTITY). Clave primaria de la línea de detalle en balance inicial de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ítems individuales que componen un saldo inicial de activos fijos. Cada fila representa un bien (mueble, equipo, vehículo, etc.) registrado al momento de la apertura o migración del módulo de activos fijos, con su valor, ubicación, responsable y condiciones de depreciación o amortización.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItem';
