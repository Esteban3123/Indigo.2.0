CREATE TABLE [InteropCost].[DirectDistributionSecondary] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20)  NOT NULL,
    [DistributionSecondaryId] INT           NOT NULL,
    [Description]             VARCHAR (300) NOT NULL,
    [Year]                    INT           NOT NULL,
    [Month]                   INT           NOT NULL,
    [CostEstimationId]        INT           NOT NULL,
    [Status]                  TINYINT       NOT NULL,
    [CreationUser]            VARCHAR (20)  NOT NULL,
    [CreationDate]            DATETIME      NOT NULL,
    [ModificationUser]        VARCHAR (20)  NULL,
    [ModificationDate]        DATETIME      NULL,
    [ConfirmUser]             VARCHAR (20)  NULL,
    [ConfirmDate]             DATETIME      NULL,
    [AnnulmentUser]           VARCHAR (20)  NULL,
    [AnnulmentDate]           DATETIME      NULL,
    [TimeStamp]               ROWVERSION    NOT NULL,
    CONSTRAINT [PK_DirectDistributionSecondary] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DirectDistributionSecondary_DistributionDirectCost] FOREIGN KEY ([CostEstimationId]) REFERENCES [InteropCost].[CostEstimation] ([Id]),
    CONSTRAINT [FK_DirectDistributionSecondary_DistributionSecondary] FOREIGN KEY ([DistributionSecondaryId]) REFERENCES [InteropCost].[DistributionSecondary] ([Id])
);


GO
ALTER TABLE [InteropCost].[DirectDistributionSecondary] NOCHECK CONSTRAINT [FK_DirectDistributionSecondary_DistributionDirectCost];


GO
ALTER TABLE [InteropCost].[DirectDistributionSecondary] NOCHECK CONSTRAINT [FK_DirectDistributionSecondary_DistributionSecondary];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Estado del Documento  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DirectDistributionSecondary', @level2type = N'COLUMN', @level2name = N'Status';

