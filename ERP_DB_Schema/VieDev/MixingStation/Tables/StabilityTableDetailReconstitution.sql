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

