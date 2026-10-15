CREATE TABLE [FixedAsset].[FixedAssetLocationType] (
    [Id]            INT           NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [OrderLocation] INT           NOT NULL,
    [State]         BIT           NOT NULL,
    CONSTRAINT [PK_FixedAssetLocationType] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del tipo de ubicacion  1-Activo   0-Inactivo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetLocationType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el orden de la ubicacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetLocationType', @level2type = N'COLUMN', @level2name = N'OrderLocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del tipo de ubicacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetLocationType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetLocationType', @level2type = N'COLUMN', @level2name = N'Id';

