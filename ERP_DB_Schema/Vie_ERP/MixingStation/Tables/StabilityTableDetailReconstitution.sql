CREATE TABLE [MixingStation].[StabilityTableDetailReconstitution] (
    [Id]                     INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [StabilityTableDetailId] INT            NOT NULL,
    [ATCId]                  INT            NOT NULL,
    [Volume]                 DECIMAL (5, 2) NOT NULL,
    [HourStability]          INT            NOT NULL,
    [Observations]           VARCHAR (1000) NULL,
    [StorageTemperatureId]   INT            NULL,
    CONSTRAINT [PK_StabilityTableDetailReconstitution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StabilityTableDetailReconstitution_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_StabilityTableDetailReconstitution_StabilityTableDetail] FOREIGN KEY ([StabilityTableDetailId]) REFERENCES [MixingStation].[StabilityTableDetail] ([Id]),
    CONSTRAINT [FK_StabilityTableDetailReconstitution_StorageTemperature] FOREIGN KEY ([StorageTemperatureId]) REFERENCES [Inventory].[StorageTemperature] ([Id])
);


GO
ALTER TABLE [MixingStation].[StabilityTableDetailReconstitution] NOCHECK CONSTRAINT [FK_StabilityTableDetailReconstitution_ATC];


GO
ALTER TABLE [MixingStation].[StabilityTableDetailReconstitution] NOCHECK CONSTRAINT [FK_StabilityTableDetailReconstitution_StabilityTableDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estabilidad en horas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'HourStability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'Volume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'StabilityTableDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailReconstitution', @level2type = N'COLUMN', @level2name = N'Id';

