CREATE TABLE [FixedAsset].[LeasingContractsEndDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LeasingContractsEndId] INT             NOT NULL,
    [PhysicalAssetId]       INT             NOT NULL,
    [MainAccountId]         INT             NOT NULL,
    [Depreciate]            BIT             NOT NULL,
    [DepreciatedValue]      DECIMAL (18, 2) NOT NULL,
    [ResidualValue]         DECIMAL (18, 2) NOT NULL,
    [HistoricalValue]       DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_LeasingContractsEndDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LeasingContractsEndDetail_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_LeasingContractsEndDetail_LeasingContractsEnd] FOREIGN KEY ([LeasingContractsEndId]) REFERENCES [FixedAsset].[LeasingContractsEnd] ([Id]),
    CONSTRAINT [FK_LeasingContractsEndDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [FixedAsset].[LeasingContractsEndDetail] NOCHECK CONSTRAINT [FK_LeasingContractsEndDetail_FixedAssetPhysicalAsset];


GO
ALTER TABLE [FixedAsset].[LeasingContractsEndDetail] NOCHECK CONSTRAINT [FK_LeasingContractsEndDetail_LeasingContractsEnd];


GO
ALTER TABLE [FixedAsset].[LeasingContractsEndDetail] NOCHECK CONSTRAINT [FK_LeasingContractsEndDetail_MainAccounts];

