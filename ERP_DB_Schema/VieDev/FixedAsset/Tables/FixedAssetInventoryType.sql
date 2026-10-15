CREATE TABLE [FixedAsset].[FixedAssetInventoryType] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (50) NOT NULL,
    [Type]             TINYINT      NOT NULL,
    [Status]           BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_InventoryType_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Inventario  1 - Equipo Biomédico  2 - Dispositivo Médico  3 - Equipo Industrial  4 - Construcciones y Edificaciones  5 - Moviliario  6 - Equipos de Oficina  7 - Muebles de Oficina  8 - Terrenos  9 - Equipo de transporte  10 - Otros    ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInventoryType', @level2type = N'COLUMN', @level2name = N'Type';

