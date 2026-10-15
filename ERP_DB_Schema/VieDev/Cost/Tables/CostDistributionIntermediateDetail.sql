CREATE TABLE [Cost].[CostDistributionIntermediateDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionIntermediateId] INT             NOT NULL,
    [ProductionCenterId]         INT             NOT NULL,
    [Proportion]                 NUMERIC (5, 2)  NOT NULL,
    [Value]                      DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionIntermediateDetail_Value] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostDistributionIntermediateDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionIntermediateDetail_CostDistributionIntermediate] FOREIGN KEY ([DistributionIntermediateId]) REFERENCES [Cost].[CostDistributionIntermediate] ([Id]),
    CONSTRAINT [FK_CostDistributionIntermediateDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionIntermediateDetail] NOCHECK CONSTRAINT [FK_CostDistributionIntermediateDetail_CostDistributionIntermediate];


GO
ALTER TABLE [Cost].[CostDistributionIntermediateDetail] NOCHECK CONSTRAINT [FK_CostDistributionIntermediateDetail_CostProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Value';

