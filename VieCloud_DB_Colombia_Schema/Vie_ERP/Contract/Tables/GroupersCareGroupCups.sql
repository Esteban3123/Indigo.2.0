CREATE TABLE [Contract].[GroupersCareGroupCups] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupersCareGroupId] INT NOT NULL,
    [CUPSEntityId]        INT NOT NULL,
    CONSTRAINT [PK_GroupersCareGroupCups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersCareGroupCups_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_GroupersCareGroupCups_GroupersCareGroup] FOREIGN KEY ([GroupersCareGroupId]) REFERENCES [Contract].[GroupersCareGroup] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad CUPS (Código Único de Procedimientos en Salud); referencia a procedimiento, servicio o prestación catalogada en el nomenclador CUPS para facturación y RIPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de entidad de CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención (Grouperrs); agrupa procedimientos y servicios de salud para gestión de facturas, glosas y contratación con aseguradoras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups', @level2type = N'COLUMN', @level2name = N'GroupersCareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del grupo de atención', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups', @level2type = N'COLUMN', @level2name = N'GroupersCareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups', @level2type = N'COLUMN', @level2name = N'GroupersCareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autonumérica (INT IDENTITY) que identifica unívocamente cada asociación entre un grupo de atención y un procedimiento CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los grupos de atención (agrupadores de cuidado) del contrato con los códigos de servicio CUPS asignados a cada grupo. Permite definir qué procedimientos o servicios CUPS pertenecen a cada agrupador de cuidado dentro de la contratación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroupCups';
