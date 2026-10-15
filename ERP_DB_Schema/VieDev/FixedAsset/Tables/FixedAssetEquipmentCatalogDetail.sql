CREATE TABLE [FixedAsset].[FixedAssetEquipmentCatalogDetail] (
    [Id]                                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentCatalog]                  INT NOT NULL,
    [IdAccountingStructure]               INT NOT NULL,
    [IdLoanSpendAccountingAccount]        INT NOT NULL,
    [IdLoanLeasingSpendAccountingAccount] INT NOT NULL,
    CONSTRAINT [PK_FixedAssetEquipmentCatalogDetail] PRIMARY KEY CLUSTERED ([Id] ASC)
);

