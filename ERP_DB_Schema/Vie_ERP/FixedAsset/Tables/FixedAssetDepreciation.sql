CREATE TABLE [FixedAsset].[FixedAssetDepreciation] (
    [Id]              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId] INT            NOT NULL,
    [Code]            VARCHAR (20)   NOT NULL,
    [ClosingMonth]    INT            NOT NULL,
    [ClosingYear]     INT            NOT NULL,
    [ClosingDate]     DATE           NOT NULL,
    [Observation]     VARCHAR (3000) NULL,
    [Status]          TINYINT        NOT NULL,
    [TimeStamp]       ROWVERSION     NOT NULL,
    CONSTRAINT [PK_FixedAssetDepreciation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetDepreciation_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetDepreciation] NOCHECK CONSTRAINT [FK_FixedAssetDepreciation_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciation', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado de la Depreciacion   1 - Registrado  2 - Confirmado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciation', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha del cierre, este se compone del ultimo dia del mes y el año que se esta cerrando', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciation', @level2type = N'COLUMN', @level2name = N'ClosingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el año del cierre', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciation', @level2type = N'COLUMN', @level2name = N'ClosingYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el mes del cierre', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciation', @level2type = N'COLUMN', @level2name = N'ClosingMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la depreciacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciation', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Unidad Operativa ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciation', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciation', @level2type = N'COLUMN', @level2name = N'Id';

