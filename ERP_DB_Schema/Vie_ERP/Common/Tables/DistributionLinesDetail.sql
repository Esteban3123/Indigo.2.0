CREATE TABLE [Common].[DistributionLinesDetail] (
    [Id]                      INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionLineId]      INT     NOT NULL,
    [AccountPayableConceptId] INT     NOT NULL,
    [ConceptType]             TINYINT NOT NULL,
    CONSTRAINT [PK_DistributionLinesDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionLinesDetail_AccountPayableConcepts] FOREIGN KEY ([AccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_DistributionLinesDetail_DistributionLines] FOREIGN KEY ([DistributionLineId]) REFERENCES [Common].[DistributionLines] ([Id])
);


GO
ALTER TABLE [Common].[DistributionLinesDetail] NOCHECK CONSTRAINT [FK_DistributionLinesDetail_AccountPayableConcepts];


GO
ALTER TABLE [Common].[DistributionLinesDetail] NOCHECK CONSTRAINT [FK_DistributionLinesDetail_DistributionLines];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo del concepto  1 - Declarante  2 - No Declarante', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de Pagos, Solo pueden ser conceptos de tipo de Retencion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la linea de distribucion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'DistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de las listas de distribucion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesDetail', @level2type = N'COLUMN', @level2name = N'Id';

