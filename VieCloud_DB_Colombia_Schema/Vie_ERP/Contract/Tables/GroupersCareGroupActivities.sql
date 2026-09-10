CREATE TABLE [Contract].[GroupersCareGroupActivities] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupersCareGroupId] INT           NOT NULL,
    [AGACTIMEDCode]       VARCHAR (5)   NOT NULL,
    [AGACTIMEDName]       VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_GroupersCareGroupActivities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersCareGroupActivities_GroupersCareGroup] FOREIGN KEY ([GroupersCareGroupId]) REFERENCES [Contract].[GroupersCareGroup] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la actividad clínica del grupo de atención (AGACTIMED); descripción de procedimiento, servicio o prestación de salud asociada al agrupador de cuidado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la actividad.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la actividad clínica (AGACTIMED, máx 5 caracteres); identificador estándar de procedimiento, servicio o prestación de salud en el agrupador.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la actividad.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'AGACTIMEDCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave foránea) del grupo de atención o CareGroup al cual pertenece la actividad; vincula con tabla Contract.GroupersCareGroup.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'GroupersCareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de los grupos de CareGroup.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'GroupersCareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'GroupersCareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de actividad en el agrupador de cuidado; clave primaria autoincremental.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividades o servicios médicos asociados a los grupos de atención dentro de un agrupador de contratos. Relaciona cada grupo de cuidado con los códigos y nombres de las actividades médicas que lo componen.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupActivities';
