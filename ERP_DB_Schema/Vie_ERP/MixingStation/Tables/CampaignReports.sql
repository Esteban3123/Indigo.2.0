CREATE TABLE [MixingStation].[CampaignReports] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId] INT           NOT NULL,
    [EntityId]         INT           NOT NULL,
    [EntityName]       VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_CampaignReports] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignReports_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id])
);


GO
ALTER TABLE [MixingStation].[CampaignReports] NOCHECK CONSTRAINT [FK_CampaignReports_CampaignDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de campaña', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignReports', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';

