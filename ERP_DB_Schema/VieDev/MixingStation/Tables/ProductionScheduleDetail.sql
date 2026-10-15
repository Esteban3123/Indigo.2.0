CREATE TABLE [MixingStation].[ProductionScheduleDetail] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionScheduleId] INT NOT NULL,
    [CampaignDetailId]     INT NOT NULL,
    CONSTRAINT [PK_ProductionScheduleDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionScheduleDetail_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_ProductionScheduleDetail_ProductionSchedule] FOREIGN KEY ([ProductionScheduleId]) REFERENCES [MixingStation].[ProductionSchedule] ([Id])
);


GO
ALTER TABLE [MixingStation].[ProductionScheduleDetail] NOCHECK CONSTRAINT [FK_ProductionScheduleDetail_CampaignDetail];


GO
ALTER TABLE [MixingStation].[ProductionScheduleDetail] NOCHECK CONSTRAINT [FK_ProductionScheduleDetail_ProductionSchedule];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la campaña asociada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail', @level2type = N'COLUMN', @level2name = N'ProductionScheduleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';

