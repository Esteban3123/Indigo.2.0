CREATE TABLE [Contract].[MarketingUnitCups] (
    [Id]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MarketingUnitId] INT NULL,
    [CupsEntityId]    INT NULL,
    CONSTRAINT [PK_MarketingUnitCups__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MarketingUnitCups_CupsEntity] FOREIGN KEY ([CupsEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_MarketingUnitCups_MarketingUnit] FOREIGN KEY ([MarketingUnitId]) REFERENCES [Contract].[MarketingUnit] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_MarketingUnitCups__MarketingUnitId__CupsEntityId]
    ON [Contract].[MarketingUnitCups]([MarketingUnitId] ASC, [CupsEntityId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad CUPS (Código Único de Procedimientos en Salud); referencia a procedimiento, servicio o prestación de salud registrada en el catálogo CUPS del contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups', @level2type = N'COLUMN', @level2name = N'CupsEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups', @level2type = N'COLUMN', @level2name = N'CupsEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups', @level2type = N'COLUMN', @level2name = N'CupsEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de mercadeo o centro de atención asociado; vinculación entre la unidad funcional y los códigos CUPS que ofrece.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups', @level2type = N'COLUMN', @level2name = N'MarketingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de mercadeo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups', @level2type = N'COLUMN', @level2name = N'MarketingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups', @level2type = N'COLUMN', @level2name = N'MarketingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria de la tabla MarketingUnitCups; identificador único de la relación entre unidad de mercadeo y entidad CUPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona unidades de mercadeo o comerciales con los servicios CUPS (códigos de procedimientos y servicios de salud) habilitados o asociados a cada una. Permite definir qué servicios pueden ofrecerse o facturarse bajo cada unidad comercial o de contratación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups';
