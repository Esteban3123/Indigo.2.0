CREATE TABLE [MixingStation].[CampaignReports] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId] INT           NOT NULL,
    [EntityId]         INT           NOT NULL,
    [EntityName]       VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_CampaignReports] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignReports_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de campaña (FK a CampaignDetail.Id). Referencia única a cada línea de configuración, parámetro o actividad específica dentro de una campaña de atención, promoción o gestión sanitaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignReports', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de campaña', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignReports', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignReports', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de entidades asociadas a los reportes de campañas de comunicación o marketing. Vincula cada detalle de campaña con las entidades (clientes, pacientes, grupos) que participaron o fueron impactadas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignReports';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignReports';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de reporte de campaña.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignReports', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignReports', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad relacionada con la campaña (por ejemplo, paciente, cliente o grupo).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignReports', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignReports', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad vinculada a la campaña, como el nombre del paciente, cliente o grupo destinatario.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignReports', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignReports', @level2type = N'COLUMN', @level2name = N'EntityName';
