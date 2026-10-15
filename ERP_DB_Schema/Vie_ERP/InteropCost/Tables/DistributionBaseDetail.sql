CREATE TABLE [InteropCost].[DistributionBaseDetail] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionBaseId] INT             NOT NULL,
    [ProductionCenterId] INT             NOT NULL,
    [MainAccountId]      INT             NOT NULL,
    [CostCenterId]       INT             NULL,
    [Quantity]           NUMERIC (18, 4) NOT NULL,
    CONSTRAINT [PK_DistributionBaseDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionBaseDetail_DistributionBase] FOREIGN KEY ([DistributionBaseId]) REFERENCES [InteropCost].[DistributionBase] ([Id]),
    CONSTRAINT [FK_DistributionBaseDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionBaseDetail] NOCHECK CONSTRAINT [FK_DistributionBaseDetail_DistributionBase];


GO
ALTER TABLE [InteropCost].[DistributionBaseDetail] NOCHECK CONSTRAINT [FK_DistributionBaseDetail_ProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad que se va a distribuir, Solo se llena este campo si el tipo de distribucion es calculada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo, solo se solicita si la cuenta maneja centro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion al cual se va a distribuir', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la base de distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'DistributionBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';

