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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado de la Depreciacion   1 - Registrado  2 - Confirmado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetDepreciation', @level2type = N'COLUMN', @level2name = N'Status';

