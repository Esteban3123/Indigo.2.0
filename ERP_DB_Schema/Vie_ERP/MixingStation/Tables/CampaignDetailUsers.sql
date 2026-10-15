CREATE TABLE [MixingStation].[CampaignDetailUsers] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId] INT          NOT NULL,
    [UserId]           INT          NOT NULL,
    [UserCode]         VARCHAR (50) NOT NULL,
    [UserRole]         INT          NOT NULL,
    CONSTRAINT [PK_CampaignDetailUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignDetailUsers_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id])
);


GO
ALTER TABLE [MixingStation].[CampaignDetailUsers] NOCHECK CONSTRAINT [FK_CampaignDetailUsers_CampaignDetail];


GO
CREATE NONCLUSTERED INDEX [IDX_CampaignDetailUsers_CampaignDetailId_UserRole]
    ON [MixingStation].[CampaignDetailUsers]([CampaignDetailId] ASC, [UserRole] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rol asignado al usuario   1=QF Calidad  2=QF Producción   3=Auxiliar de Central de Mezclas   4=Director técnico', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'UserRole';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de campañas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'Id';

