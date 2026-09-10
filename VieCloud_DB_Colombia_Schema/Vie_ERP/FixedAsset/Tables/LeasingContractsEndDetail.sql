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



GO



GO



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor histórico o costo original del activo fijo en el momento de adquisición o reconocimiento inicial del contrato de arrendamiento. Decimal(18,2), base para cálculo de depreciación.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor historico', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor residual o valor restante del activo fijo después de aplicar la depreciación acumulada. Decimal(18,2), representa el valor contable neto al finalizar el contrato de arrendamiento.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'ResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor restante cuando se deprecia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'ResidualValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'ResidualValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de depreciación acumulada o gasto de depreciación del período del activo fijo. Decimal(18,2), diferencia entre valor histórico y valor residual.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'DepreciatedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (1=Sí, 0=No) que determina si el activo fijo asociado al contrato de arrendamiento debe depreciar contablemente. BIT, control de política contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si(1) o no(0) deprecia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cuenta contable principal de Ledger General donde se registra el movimiento contable del activo fijo al finalizar el arrendamiento. INT, referencia a GeneralLedger.MainAccounts.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia el id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del activo fijo físico o bien tangible asociado al detalle de finalización del contrato de arrendamiento. INT, referencia a FixedAsset.FixedAssetPhysicalAsset.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia el id de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la finalización del contrato de arrendamiento (leasing) al cual pertenece este detalle de activo. INT, referencia a FixedAsset.LeasingContractsEnd.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'LeasingContractsEndId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referencia el id de finalización de contratos de arrendamiento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'LeasingContractsEndId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'LeasingContractsEndId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, IDENTITY) del registro de detalle de finalización de contrato de arrendamiento. INT, clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del cierre o finalización de contratos de leasing: registra, por cada activo físico involucrado en el cierre, los valores contables asociados como el valor depreciado, el valor residual y el valor histórico, junto con la cuenta contable principal y si el activo continúa siendo depreciable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'LeasingContractsEndDetail';
