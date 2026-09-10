CREATE TABLE [MixingStation].[CMCenterAttention] (
    [Id]                  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdMixingStation]     INT       NOT NULL,
    [IdCenterAttention]   INT       NULL,
    [IdProductionLine]    INT       NOT NULL,
    [CodeCenterAttention] CHAR (10) CONSTRAINT [DF_CMCenterAttention_CodeCenterAttention] DEFAULT ('          ') NOT NULL,
    [StateCA]             BIT       NULL,
    CONSTRAINT [PK_CMCenterAttention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMCenterAttention_CMConfiguration] FOREIGN KEY ([IdMixingStation]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_CMCenterAttention_ProductionLine] FOREIGN KEY ([IdProductionLine]) REFERENCES [MixingStation].[ProductionLine] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_CMCenterAttention_IdProductionLine]
    ON [MixingStation].[CMCenterAttention]([IdProductionLine] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de Centro de Atención: 1=Activo, 0=Inactivo. Bit booleano que indica si la asociación estación-centro está habilitada para producción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'StateCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro: 1:Activo, 0:Inactivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'StateCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'StateCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención (CHAR 10). Identificador alfanumérico del centro/unidad funcional de atención vinculado. Búsqueda: código centro, unidad funcional, punto de atención.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'CodeCenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo centro de atención', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'CodeCenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'CodeCenterAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la Línea de Producción (FK a ProductionLine). Identificador numérico que referencia la línea de manufactura/procesamiento asociada. Búsqueda: línea producción, línea mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'IdProductionLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id línea de producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'IdProductionLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'IdProductionLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del Centro de Atención (INT, nullable). Identificador numérico del centro/unidad de atención vinculado. Búsqueda: centro atención, unidad funcional, punto servicio.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'IdCenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id centro de atención', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'IdCenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'IdCenterAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la Estación de Mezcla (FK a CMConfiguration). Identificador numérico de la estación/equipo de mezcla configurado. Búsqueda: estación mezcla, equipo mezcla, configuración estación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'IdMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de Estación de mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'IdMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'IdMixingStation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Autonumérico PRIMARY KEY (IDENTITY 1,1). Identificador único auto-incrementado que identifica cada asociación estación-centro-línea en la tabla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre estaciones de mezcla (mixing stations) y centros de atención habilitados para operar en cada línea de producción. Permite configurar qué centros de atención están asociados a cada estación de mezcla y línea de producción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterAttention';
