CREATE TABLE [Common].[DistributionLinesICARetention] (
    [Id]                      INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionLineId]      INT NOT NULL,
    [OperatingUnitId]         INT NOT NULL,
    [AccountPayableConceptId] INT NOT NULL,
    CONSTRAINT [PK_DistributionLinesICARetention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionLinesICARetention_AccountPayableConcepts] FOREIGN KEY ([AccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_DistributionLinesICARetention_DistributionLines] FOREIGN KEY ([DistributionLineId]) REFERENCES [Common].[DistributionLines] ([Id]),
    CONSTRAINT [FK_DistributionLinesICARetention_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Common].[DistributionLinesICARetention] NOCHECK CONSTRAINT [FK_DistributionLinesICARetention_AccountPayableConcepts];


GO
ALTER TABLE [Common].[DistributionLinesICARetention] NOCHECK CONSTRAINT [FK_DistributionLinesICARetention_DistributionLines];


GO
ALTER TABLE [Common].[DistributionLinesICARetention] NOCHECK CONSTRAINT [FK_DistributionLinesICARetention_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de Pagos, Solo pueden ser conceptos de tipo de Retencion y de tipos especificos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la linea de distribucion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'DistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'DistributionLinesICARetention', @level2type = N'COLUMN', @level2name = N'Id';

