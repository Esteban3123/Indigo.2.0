CREATE TABLE [FixedAsset].[FixedAssetIngressEquipmentDetail] (
    [Id]                           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetIngressEquipment] INT          NOT NULL,
    [IdEquipment]                  INT          NOT NULL,
    [LicensePlate]                 VARCHAR (50) NOT NULL,
    [Serie]                        VARCHAR (50) NOT NULL,
    [IdReponsible]                 INT          NOT NULL,
    [IdFunctionalUnit]             INT          NOT NULL,
    [IdLocation]                   INT          NOT NULL,
    [AdquisitionDate]              DATE         NOT NULL,
    [Depreciate]                   BIT          NOT NULL,
    [ComponentDepreciate]          BIT          NULL,
    CONSTRAINT [PK_FixedAssetIngressEquipmentDetail_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de depreciación por componente (BIT, Sí/No). Determina si el equipo se deprecia de forma desglosada por sus componentes individuales en lugar de como activo único.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'ComponentDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deprecia por Componente (Si - No)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'ComponentDepreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'ComponentDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de depreciación del activo fijo (BIT, Sí/No). Marca si el equipo debe incluirse en cálculos de depreciación contable y fiscal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deprecia (Si - No)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de adquisición o compra del equipo (DATE). Punto de partida para cálculo de depreciación y vida útil del activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ubicación física del equipo (FK INT). Referencia a lugar o sede donde el equipo está instalado o almacenado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdLocation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Ubicación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdLocation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdLocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad funcional responsable (FK INT). Referencia al área, departamento o centro de costo que custodia el equipo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del responsable del equipo (FK INT). Referencia al profesional de la salud, administrador o custodio asignado al activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdReponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Reponsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdReponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdReponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de serie del equipo (VARCHAR 50). Identificador único del fabricante para trazabilidad y garantía del activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Serie', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Serie';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Serie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Placa o código patrimonial del equipo (VARCHAR 50). Identificador interno de la institución para control de inventario y activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'LicensePlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Placa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'LicensePlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'LicensePlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo o catálogo de equipo (FK INT). Referencia a la clasificación, marca y modelo del activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ingreso de activos fijos (FK INT). Referencia al documento o comprobante maestro de entrada del equipo al patrimonio.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFixedAssetIngressEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Equipo de entrada de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFixedAssetIngressEquipment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFixedAssetIngressEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY). Clave primaria para cada detalle de equipo ingresado al sistema de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de equipos incluidos en un ingreso o entrada de activos fijos. Registra la información individual de cada equipo dado de alta: placa, serie, responsable, ubicación, fecha de adquisición y si aplica depreciación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetIngressEquipmentDetail';
