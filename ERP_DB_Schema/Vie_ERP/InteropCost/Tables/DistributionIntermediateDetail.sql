CREATE TABLE [InteropCost].[DistributionIntermediateDetail] (
    [Id]                         INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionIntermediateId] INT            NOT NULL,
    [ProductionCenterId]         INT            NOT NULL,
    [Proportion]                 NUMERIC (5, 2) NOT NULL,
    CONSTRAINT [PK_DistributionIntermediateDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionIntermediateDetail_DistributionIntermediate] FOREIGN KEY ([DistributionIntermediateId]) REFERENCES [InteropCost].[DistributionIntermediate] ([Id]),
    CONSTRAINT [FK_DistributionIntermediateDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionIntermediateDetail] NOCHECK CONSTRAINT [FK_DistributionIntermediateDetail_DistributionIntermediate];


GO
ALTER TABLE [InteropCost].[DistributionIntermediateDetail] NOCHECK CONSTRAINT [FK_DistributionIntermediateDetail_ProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de proporcion que se va a distribuir', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Proportion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la distribucion intermedia', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'DistributionIntermediateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Id';

