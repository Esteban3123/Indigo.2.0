CREATE TABLE [Contract].[RateManualValidityDetail] (
    [Id]                   INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RateManualValidityId] INT      NOT NULL,
    [RateManualId]         INT      NOT NULL,
    [InitialDate]          DATETIME NOT NULL,
    [EndDate]              DATETIME NOT NULL,
    CONSTRAINT [PK_RateManualValidityDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RateManualValidityDetail_RateManual] FOREIGN KEY ([RateManualId]) REFERENCES [Contract].[RateManual] ([Id]),
    CONSTRAINT [FK_RateManualValidityDetail_RateManualValidity] FOREIGN KEY ([RateManualValidityId]) REFERENCES [Contract].[RateManualValidity] ([Id])
);


GO
ALTER TABLE [Contract].[RateManualValidityDetail] NOCHECK CONSTRAINT [FK_RateManualValidityDetail_RateManual];


GO
ALTER TABLE [Contract].[RateManualValidityDetail] NOCHECK CONSTRAINT [FK_RateManualValidityDetail_RateManualValidity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualValidityDetail', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualValidityDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Manual tarifa', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualValidityDetail', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Vigencia del manual de tarifa', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualValidityDetail', @level2type = N'COLUMN', @level2name = N'RateManualValidityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManualValidityDetail', @level2type = N'COLUMN', @level2name = N'Id';

