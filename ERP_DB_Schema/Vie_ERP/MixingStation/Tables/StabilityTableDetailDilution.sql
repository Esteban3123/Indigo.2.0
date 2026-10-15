CREATE TABLE [MixingStation].[StabilityTableDetailDilution] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [StabilityTableDetailId] INT             NOT NULL,
    [ATCId]                  INT             NOT NULL,
    [ConcentrationMaximum]   DECIMAL (18, 2) NOT NULL,
    [ConcentrationMinimum]   DECIMAL (18, 2) NOT NULL,
    [PhotoProtection]        BIT             NOT NULL,
    [InfusionTime]           INT             NOT NULL,
    [BibliographicReference] VARCHAR (300)   NULL,
    [Container]              VARCHAR (20)    NULL,
    [HourStability]          INT             CONSTRAINT [DF_StabilityTableDetailDilution_HourStability] DEFAULT ((0)) NOT NULL,
    [StorageTemperatureId]   INT             NULL,
    CONSTRAINT [PK_StabilityTableDetailDilution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StabilityTableDetailDilution_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_StabilityTableDetailDilution_StabilityTableDetail] FOREIGN KEY ([StabilityTableDetailId]) REFERENCES [MixingStation].[StabilityTableDetail] ([Id]),
    CONSTRAINT [FK_StabilityTableDetailDilution_StorageTemperature] FOREIGN KEY ([StorageTemperatureId]) REFERENCES [Inventory].[StorageTemperature] ([Id])
);


GO
ALTER TABLE [MixingStation].[StabilityTableDetailDilution] NOCHECK CONSTRAINT [FK_StabilityTableDetailDilution_ATC];


GO
ALTER TABLE [MixingStation].[StabilityTableDetailDilution] NOCHECK CONSTRAINT [FK_StabilityTableDetailDilution_StabilityTableDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estabilidad en horas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'HourStability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Envase', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'Container';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia bibliografica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'BibliographicReference';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de infusión en min', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'InfusionTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Foto protección', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'PhotoProtection';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentración minima', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'ConcentrationMinimum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentración maxima', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'ConcentrationMaximum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle de tipo reconstituyente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'StabilityTableDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilution', @level2type = N'COLUMN', @level2name = N'Id';

