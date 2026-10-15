CREATE TABLE [Cost].[CostDirectDistributionSecondary] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20)    NOT NULL,
    [DistributionSecondaryId] INT             NOT NULL,
    [Description]             VARCHAR (300)   NOT NULL,
    [Year]                    INT             NOT NULL,
    [Month]                   INT             NOT NULL,
    [CostEstimationId]        INT             NOT NULL,
    [Status]                  TINYINT         NOT NULL,
    [CreationUser]            VARCHAR (20)    NOT NULL,
    [CreationDate]            DATETIME        NOT NULL,
    [ModificationUser]        VARCHAR (20)    NULL,
    [ModificationDate]        DATETIME        NULL,
    [ConfirmUser]             VARCHAR (20)    NULL,
    [ConfirmDate]             DATETIME        NULL,
    [AnnulmentUser]           VARCHAR (20)    NULL,
    [AnnulmentDate]           DATETIME        NULL,
    [TimeStamp]               ROWVERSION      NOT NULL,
    [Value]                   DECIMAL (18, 2) CONSTRAINT [DF_CostDirectDistributionSecondary_Value] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostDirectDistributionSecondary__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDirectDistributionSecondary_CostDistributionDirectCost] FOREIGN KEY ([CostEstimationId]) REFERENCES [Cost].[CostEstimationNative] ([Id]),
    CONSTRAINT [FK_CostDirectDistributionSecondary_CostDistributionSecondary] FOREIGN KEY ([DistributionSecondaryId]) REFERENCES [Cost].[CostDistributionSecondary] ([Id])
);


GO
ALTER TABLE [Cost].[CostDirectDistributionSecondary] NOCHECK CONSTRAINT [FK_CostDirectDistributionSecondary_CostDistributionDirectCost];


GO
ALTER TABLE [Cost].[CostDirectDistributionSecondary] NOCHECK CONSTRAINT [FK_CostDirectDistributionSecondary_CostDistributionSecondary];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Estado del Documento  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDirectDistributionSecondary', @level2type = N'COLUMN', @level2name = N'Status';

