CREATE TABLE [Inventory].[RelatedSupplieMedicine] (
    [Id]       INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ATCId]    INT     NOT NULL,
    [ItemType] TINYINT NOT NULL,
    [SourceId] INT     NOT NULL,
    CONSTRAINT [PK_RelatedSupplieMedicine] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RelatedSupplieMedicine_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_RelatedSupplieMedicine_ATCId]
    ON [Inventory].[RelatedSupplieMedicine]([ATCId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del insumo o medicamento relacionado. Referencia a la fuente (insumo farmacéutico o principio activo medicamento) según ItemType. Clave foránea hacia tabla de insumos o medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'SourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de insumo o id de medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'SourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'SourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ítem relacionado: 1=Insumo (suministro, material, dispositivo médico), 2=Medicamento (fármaco, principio activo). TINYINT para clasificar naturaleza del SourceId.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'ItemType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de item: 1|Insumo, 2|Medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'ItemType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de clasificación ATC (Anatomical Therapeutic Chemical). Clave foránea que referencia [Inventory].[ATC]. Código de categorización farmacéutica internacional para medicamentos y sustancias activas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria (PK) Identity. Identificador único de la relación entre ATC, insumo y medicamento en tabla RelatedSupplieMedicine. INT auto-incremental.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona proveedores con medicamentos según la clasificación ATC (Anatomical Therapeutic Chemical). Permite identificar qué proveedor suministra cada tipo de medicamento o insumo registrado en el inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplieMedicine';
