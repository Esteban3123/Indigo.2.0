CREATE TABLE [Cost].[CostIntermediateDistributionBaseDetail] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IntermediateDistributionBaseId] INT             NOT NULL,
    [ProductionCenterId]             INT             NOT NULL,
    [Quantity]                       NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_CostIntermediateDistributionBaseDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostIntermediateDistributionBaseDetail_CostIntermediateDistributionBase] FOREIGN KEY ([IntermediateDistributionBaseId]) REFERENCES [Cost].[CostIntermediateDistributionBase] ([Id]),
    CONSTRAINT [FK_CostIntermediateDistributionBaseDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad que se va a distribuir, Solo se llena este campo si el tipo de distribucion es calculada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la base de distribucion intermedia', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';

