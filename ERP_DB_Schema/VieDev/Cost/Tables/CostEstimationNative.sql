CREATE TABLE [Cost].[CostEstimationNative] (
    [Id]                                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                                  INT             NOT NULL,
    [Month]                                 INT             NOT NULL,
    [ProductionCenterId]                    INT             NOT NULL,
    [DirectCostDistribution]                DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_DirectCostDistribution] DEFAULT ((0)) NOT NULL,
    [AutoCostDistribution]                  DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_AutoCostDistribution] DEFAULT ((0)) NOT NULL,
    [ManPowerDistributionDirect]            DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_ManPowerDistributionDirect] DEFAULT ((0)) NOT NULL,
    [ManPowerDistributionInDirect]          DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_ManPowerDistributionInDirect] DEFAULT ((0)) NOT NULL,
    [FixedAssetDistribution]                DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_FixedAssetDistribution] DEFAULT ((0)) NOT NULL,
    [DispensingDistribution]                DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_DispensingDistribution] DEFAULT ((0)) NOT NULL,
    [TransferDistribution]                  DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_TransferDistribution] DEFAULT ((0)) NOT NULL,
    [InitialDistribution]                   DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_InitialDistribution] DEFAULT ((0)) NOT NULL,
    [IntermediateDistribution]              DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_IntermediateDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryDirectCostDistribution]       DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryDirectCostDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryAutoCostDistribution]         DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryAutoCostDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryManPowerDistributionDirect]   DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryManPowerDistributionDirect] DEFAULT ((0)) NOT NULL,
    [SecondaryManPowerDistributionInDirect] DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryManPowerDistributionInDirect] DEFAULT ((0)) NOT NULL,
    [SecondaryFixedAssetDistribution]       DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryFixedAssetDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryDispensingDistribution]       DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryDispensingDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryTransferDistribution]         DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryTransferDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryDistribution]                 DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_SecondaryDistribution] DEFAULT ((0)) NOT NULL,
    [CreationUser]                          VARCHAR (20)    NOT NULL,
    [CreationDate]                          DATETIME        NOT NULL,
    [ModificationUser]                      VARCHAR (20)    NULL,
    [ModificationDate]                      DATETIME        NULL,
    [TimeStamp]                             ROWVERSION      NOT NULL,
    [CostAccountingAdjustment]              DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_CostAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [ManPowerAccountingAdjustment]          DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_ManPowerAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [FixedAssetAccountingAdjustment]        DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_FixedAssetAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [DispensingAccountingAdjustment]        DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_DispensingAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [TransferAccountingAdjustment]          DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_TransferAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [TotalSales]                            DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_TotalSales] DEFAULT ((0)) NOT NULL,
    [TotalSalesSecondary]                   DECIMAL (18, 2) CONSTRAINT [DF_CostEstimationNative_TotalSalesSecondary] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostEstimationNativeNative] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion Inicial la cual es la suma de:  Distribucion de Gastos Directos  Distribucion de Mano de Obra  Distribucion de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostEstimationNative', @level2type = N'COLUMN', @level2name = N'InitialDistribution';

