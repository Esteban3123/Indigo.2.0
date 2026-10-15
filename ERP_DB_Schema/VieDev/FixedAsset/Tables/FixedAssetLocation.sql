CREATE TABLE [FixedAsset].[FixedAssetLocation] (
    [Id]               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParentId]         INT            NULL,
    [CityId]           INT            NULL,
    [Code]             VARCHAR (20)   NOT NULL,
    [Name]             VARCHAR (50)   NOT NULL,
    [FunctionalUnitId] INT            CONSTRAINT [DF_FixedAssetLocation_FunctionalUnitId] DEFAULT ((2)) NOT NULL,
    [Class]            TINYINT        NOT NULL,
    [LocationTypeId]   INT            NOT NULL,
    [RiskLevel]        CHAR (1)       NULL,
    [UseTime]          DECIMAL (3, 1) CONSTRAINT [DF_FixedAssetLocation_UseTime] DEFAULT ((8)) NOT NULL,
    [TimeStamp]        ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetLocation_FixedAssetLocationType] FOREIGN KEY ([LocationTypeId]) REFERENCES [FixedAsset].[FixedAssetLocationType] ([Id]),
    CONSTRAINT [FK_FixedAssetLocation_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_Location_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Location_Location] FOREIGN KEY ([ParentId]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetLocation] NOCHECK CONSTRAINT [FK_FixedAssetLocation_FixedAssetLocationType];


GO
ALTER TABLE [FixedAsset].[FixedAssetLocation] NOCHECK CONSTRAINT [FK_FixedAssetLocation_FunctionalUnit];


GO
ALTER TABLE [FixedAsset].[FixedAssetLocation] NOCHECK CONSTRAINT [FK_Location_City];


GO
ALTER TABLE [FixedAsset].[FixedAssetLocation] NOCHECK CONSTRAINT [FK_Location_Location];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tiempo de uso de los activos que estan en esa unidad  Min : 8  Max: 24', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetLocation', @level2type = N'COLUMN', @level2name = N'UseTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de riesgo de la ubicacion   1-Alto  2-Medio  3-Bajo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetLocation', @level2type = N'COLUMN', @level2name = N'RiskLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la clase de la ubicacion  1 - Administrativa  2 - Operativa  3 - Mantenimiento  4 - Almacen y Bodegaje    Nota: este clase se utiliza para saber cuando el activo debe o no depreciar con lo cual los tipos 1 y 2 deprecian y los 3 y 4 no deprecian', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetLocation', @level2type = N'COLUMN', @level2name = N'Class';

