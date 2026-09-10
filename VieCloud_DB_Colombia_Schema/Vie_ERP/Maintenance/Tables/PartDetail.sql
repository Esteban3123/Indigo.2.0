CREATE TABLE [Maintenance].[PartDetail] (
    [Id]              INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPart]          INT     NOT NULL,
    [IdEquipmentType] TINYINT NOT NULL,
    CONSTRAINT [PK_PartDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PartDetail_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [Maintenance].[EquipmentType] ([Id]),
    CONSTRAINT [FK_PartDetail_PartDetail] FOREIGN KEY ([IdPart]) REFERENCES [Maintenance].[Part] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de equipamiento o accesorio relacionado (FK a Maintenance.EquipmentType). Referencia la clasificación del equipo médico o dispositivo asociado a la parte. Tipo: TINYINT.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de Accesorio Relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del accesorio o pieza de repuesto relacionada (FK a Maintenance.Part). Vincula la parte específica del inventario de mantenimiento. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail', @level2type = N'COLUMN', @level2name = N'IdPart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Accesorio relacionado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail', @level2type = N'COLUMN', @level2name = N'IdPart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail', @level2type = N'COLUMN', @level2name = N'IdPart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autonumérica (IDENTITY) de la tabla de relación entre accesorios/partes y tipos de equipamiento. Identifica de forma única cada asociación parte-equipo. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de relacion de accesorios y equipos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las partes o componentes de mantenimiento con los tipos de equipos a los que pertenecen, permitiendo saber qué piezas corresponden a cada categoría de equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PartDetail';
