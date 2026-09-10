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



GO
CREATE NONCLUSTERED INDEX [IDX_CampaignDetailUsers_CampaignDetailId_UserRole]
    ON [MixingStation].[CampaignDetailUsers]([CampaignDetailId] ASC, [UserRole] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rol funcional asignado al usuario en la campaña: 1=QF Calidad (Químico Farmacéutico Calidad), 2=QF Producción (Químico Farmacéutico Producción), 3=Auxiliar de Central de Mezclas, 4=Director técnico; define permisos y responsabilidades en mezclas/preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'UserRole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rol asignado al usuario   1=QF Calidad  2=QF Producción   3=Auxiliar de Central de Mezclas   4=Director técnico', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'UserRole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'UserRole';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario de seguridad (VARCHAR 50); código único del usuario autenticado en el sistema de seguridad, equivalente a login/usuario del profesional de salud o operario.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario de seguridad del sistema que tiene permiso/acceso; usuario autenticado del ERP/EHR con credenciales de control de acceso.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario que tiene permiso , el usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de campaña padre (FK → CampaignDetail.Id); referencia a la cabecera/encabezado de campañas de producción en Central de Mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de campañas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asignación de usuario a detalle de campaña (INT IDENTITY, clave primaria).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuarios o personas asignadas a cada detalle de campaña de comunicación o marketing. Relaciona los participantes, sus roles y códigos internos con cada campaña específica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailUsers';
