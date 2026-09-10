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
ALTER TABLE [MixingStation].[ProductionScheduleDetail] NOCHECK CONSTRAINT [FK_ProductionScheduleDetail_CampaignDetail];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de campaña de producción asociado; referencia a CampaignDetail.Id para vincular líneas específicas de la campaña', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la campaña asociada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera de programación de producción; referencia a ProductionSchedule.Id que agrupa los detalles del cronograma', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail', @level2type = N'COLUMN', @level2name = N'ProductionScheduleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail', @level2type = N'COLUMN', @level2name = N'ProductionScheduleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail', @level2type = N'COLUMN', @level2name = N'ProductionScheduleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, INT IDENTITY) del registro de detalle en la programación de producción de la estación de mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la programación de producción en la estación de mezcla. Relaciona cada línea de un cronograma de producción con el detalle de campaña correspondiente, permitiendo planificar qué lotes o mezclas se ejecutan en cada campaña.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionScheduleDetail';
