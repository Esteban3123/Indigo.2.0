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


GO
CREATE NONCLUSTERED INDEX [IX_LeasingContractsEndDetail_MainAccountId]
    ON [FixedAsset].[LeasingContractsEndDetail]([MainAccountId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LeasingContractsEndDetail_LeasingContractsEndId]
    ON [FixedAsset].[LeasingContractsEndDetail]([LeasingContractsEndId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LeasingContractsEndDetail_PhysicalAssetId]
    ON [FixedAsset].[LeasingContractsEndDetail]([PhysicalAssetId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor historico', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor restante cuando se deprecia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si(1) o no(0) deprecia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia el id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia el id de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia el id de finalización de contratos de arrendamiento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'LeasingContractsEndId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'Id';

