CREATE TABLE [FixedAsset].[FixedAssetPartsAccesoriesConsumables] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (50) NOT NULL,
    [AllowDepreciate]  BIT          CONSTRAINT [DF_FixedAssetPartsAccesoriesConsumables_AllowDepreciate] DEFAULT ((1)) NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_PartsAccesoriesConsumables] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si permite depreciar    Si permite depreciar se debe de llenar obligatoriamente el detalle de esta tabla como minimo con el libro oficial', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPartsAccesoriesConsumables', @level2type = N'COLUMN', @level2name = N'AllowDepreciate';

