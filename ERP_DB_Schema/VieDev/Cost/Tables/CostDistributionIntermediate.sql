CREATE TABLE [Cost].[CostDistributionIntermediate] (
    [Id]                                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                              VARCHAR (20)    NOT NULL,
    [IntermediateDistributionElementId] INT             NULL,
    [ProductionCenterId]                INT             NOT NULL,
    [Description]                       VARCHAR (300)   NOT NULL,
    [Year]                              INT             NOT NULL,
    [Month]                             INT             NOT NULL,
    [CostEstimationId]                  INT             NULL,
    [Status]                            TINYINT         NOT NULL,
    [CreationUser]                      VARCHAR (20)    NOT NULL,
    [CreationDate]                      DATETIME        NOT NULL,
    [ModificationUser]                  VARCHAR (20)    NULL,
    [ModificationDate]                  DATETIME        NULL,
    [ConfirmUser]                       VARCHAR (20)    NULL,
    [ConfirmDate]                       DATETIME        NULL,
    [AnnulmentUser]                     VARCHAR (20)    NULL,
    [AnnulmentDate]                     DATETIME        NULL,
    [TimeStamp]                         ROWVERSION      NOT NULL,
    [Value]                             DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionIntermediate_Value] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostDistributionIntermediate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionIntermediate_CostEstimationNative] FOREIGN KEY ([CostEstimationId]) REFERENCES [Cost].[CostEstimationNative] ([Id]),
    CONSTRAINT [FK_CostDistributionIntermediate_CostIntermediateDistribution] FOREIGN KEY ([IntermediateDistributionElementId]) REFERENCES [Cost].[CostIntermediateDistribution] ([Id]),
    CONSTRAINT [FK_CostDistributionIntermediate_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionIntermediate] NOCHECK CONSTRAINT [FK_CostDistributionIntermediate_CostProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor a distribuir cargado de la distribucion inicial de la estimacion ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediate', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediate', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediate', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediate', @level2type = N'COLUMN', @level2name = N'ConfirmDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario confirmación', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediate', @level2type = N'COLUMN', @level2name = N'ConfirmUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el Estado del Documento  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediate', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la estimación de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediate', @level2type = N'COLUMN', @level2name = N'CostEstimationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elemento de distribución intermedia', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediate', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionElementId';

