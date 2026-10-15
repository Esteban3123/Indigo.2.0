CREATE TABLE [InteropCost].[CostEstimation] (
    [Id]                                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                                  INT             NOT NULL,
    [Month]                                 INT             NOT NULL,
    [ProductionCenterId]                    INT             NOT NULL,
    [DirectCostDistribution]                DECIMAL (18, 2) NOT NULL,
    [AutoCostDistribution]                  DECIMAL (18, 2) NOT NULL,
    [CostAccountingAdjustment]              DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_AccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [ManPowerAccountingAdjustment]          DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_ManPowerAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [FixedAssetAccountingAdjustment]        DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_FixedAssetAccountingAdjustment] DEFAULT ((0)) NOT NULL,
    [ManPowerDistributionDirect]            DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_ManPowerDistributionDirect] DEFAULT ((0)) NOT NULL,
    [ManPowerDistributionInDirect]          DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_ManPowerDistributionInDirect] DEFAULT ((0)) NOT NULL,
    [FixedAssetDistribution]                DECIMAL (18, 2) NOT NULL,
    [DispensingDistribution]                DECIMAL (18, 2) NOT NULL,
    [TransferDistribution]                  DECIMAL (18, 2) NOT NULL,
    [InitialDistribution]                   DECIMAL (18, 2) NOT NULL,
    [IntermediateDistribution]              DECIMAL (18, 2) NOT NULL,
    [SecondaryDirectCostDistribution]       DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_SecondaryDirectCostDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryAutoCostDistribution]         DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_SecondaryAutoCostDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryManPowerDistributionDirect]   DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_ManPowerDistributionDirect1] DEFAULT ((0)) NOT NULL,
    [SecondaryManPowerDistributionInDirect] DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_ManPowerDistributionInDirect1] DEFAULT ((0)) NOT NULL,
    [SecondaryFixedAssetDistribution]       DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_SecondaryFixedAssetDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryDispensingDistribution]       DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_SecondaryDispensingDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryTransferDistribution]         DECIMAL (18, 2) CONSTRAINT [DF_CostEstimation_SecondaryTransferDistribution] DEFAULT ((0)) NOT NULL,
    [SecondaryDistribution]                 DECIMAL (18, 2) NOT NULL,
    [CreationUser]                          VARCHAR (20)    NOT NULL,
    [CreationDate]                          DATETIME        NOT NULL,
    [ModificationUser]                      VARCHAR (20)    NULL,
    [ModificationDate]                      DATETIME        NULL,
    [TimeStamp]                             ROWVERSION      NOT NULL,
    CONSTRAINT [PK_CostEstimation] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion Inicial la cual es la suma de:  Distribucion de Gastos Directos  Distribucion de Mano de Obra  Distribucion de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimation', @level2type = N'COLUMN', @level2name = N'InitialDistribution';

