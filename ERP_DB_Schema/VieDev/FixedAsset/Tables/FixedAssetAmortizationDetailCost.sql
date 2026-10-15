CREATE TABLE [FixedAsset].[FixedAssetAmortizationDetailCost] (
    [Id]                             INT             IDENTITY (1, 1) NOT NULL,
    [FixedAssetAmortizationDetailId] INT             NOT NULL,
    [MainAccountId]                  INT             NOT NULL,
    [ThirdPartyId]                   INT             NOT NULL,
    [LocationId]                     INT             NOT NULL,
    [CostCenterId]                   INT             NOT NULL,
    [AmortizedDays]                  INT             NOT NULL,
    [AmortizedValue]                 DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetAmortizationDetailCost__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetAmortizationDetailCost_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_FixedAssetAmortizationDetailCost_FixedAssetAmortizationDetail] FOREIGN KEY ([FixedAssetAmortizationDetailId]) REFERENCES [FixedAsset].[FixedAssetAmortizationDetail] ([Id]),
    CONSTRAINT [FK_FixedAssetAmortizationDetailCost_FixedAssetLocation] FOREIGN KEY ([LocationId]) REFERENCES [FixedAsset].[FixedAssetLocation] ([Id]),
    CONSTRAINT [FK_FixedAssetAmortizationDetailCost_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetAmortizationDetailCost_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetAmortizationDetailCost] NOCHECK CONSTRAINT [FK_FixedAssetAmortizationDetailCost_CostCenter];


GO
ALTER TABLE [FixedAsset].[FixedAssetAmortizationDetailCost] NOCHECK CONSTRAINT [FK_FixedAssetAmortizationDetailCost_FixedAssetAmortizationDetail];


GO
ALTER TABLE [FixedAsset].[FixedAssetAmortizationDetailCost] NOCHECK CONSTRAINT [FK_FixedAssetAmortizationDetailCost_FixedAssetLocation];


GO
ALTER TABLE [FixedAsset].[FixedAssetAmortizationDetailCost] NOCHECK CONSTRAINT [FK_FixedAssetAmortizationDetailCost_MainAccounts];


GO
ALTER TABLE [FixedAsset].[FixedAssetAmortizationDetailCost] NOCHECK CONSTRAINT [FK_FixedAssetAmortizationDetailCost_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la amortización del mes en la cuenta contable / Centro de costo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetailCost', @level2type = N'COLUMN', @level2name = N'AmortizedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de dias amortizados', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetailCost', @level2type = N'COLUMN', @level2name = N'AmortizedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id centro de costos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetailCost', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Localización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetailCost', @level2type = N'COLUMN', @level2name = N'LocationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetailCost', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetailCost', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de detalle de amortización de activos fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetailCost', @level2type = N'COLUMN', @level2name = N'FixedAssetAmortizationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetAmortizationDetailCost', @level2type = N'COLUMN', @level2name = N'Id';

