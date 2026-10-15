CREATE TABLE [Cost].[CostDistributionSecondaryBaseDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionSecondaryBaseId] INT             NOT NULL,
    [ProductionCenterId]          INT             NOT NULL,
    [Quantity]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_DistributionSecondaryBaseDetail_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionSecondaryBaseDetail_CostDistributionSecondaryBase] FOREIGN KEY ([DistributionSecondaryBaseId]) REFERENCES [Cost].[CostDistributionSecondaryBase] ([Id]),
    CONSTRAINT [FK_DistributionSecondaryBaseDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionSecondaryBaseDetail] NOCHECK CONSTRAINT [FK_DistributionSecondaryBaseDetail_CostDistributionSecondaryBase];


GO
ALTER TABLE [Cost].[CostDistributionSecondaryBaseDetail] NOCHECK CONSTRAINT [FK_DistributionSecondaryBaseDetail_CostProductionCenter];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CostDistributionSecondaryBaseDetail]
    ON [Cost].[CostDistributionSecondaryBaseDetail]([DistributionSecondaryBaseId] ASC, [ProductionCenterId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad que se va a distribuir, Solo se llena este campo si el tipo de distribucion es calculada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la base de distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';

