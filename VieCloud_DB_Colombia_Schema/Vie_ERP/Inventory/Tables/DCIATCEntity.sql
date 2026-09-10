CREATE TABLE [Inventory].[DCIATCEntity] (
    [Id]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdDCI]       INT NOT NULL,
    [IdATCEntity] INT NOT NULL,
    CONSTRAINT [PK_DCIATCEntity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DCIATCEntity_ATCEntity] FOREIGN KEY ([IdATCEntity]) REFERENCES [Inventory].[ATCEntity] ([Id]),
    CONSTRAINT [FK_DCIATCEntity_DCI] FOREIGN KEY ([IdDCI]) REFERENCES [Inventory].[DCI] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Entidad ATC (Anatomical Therapeutic Chemical Classification), clave foránea que referencia la clasificación anatómica, terapéutica y química del medicamento o sustancia farmacéutica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity', @level2type = N'COLUMN', @level2name = N'IdATCEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Entidad ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity', @level2type = N'COLUMN', @level2name = N'IdATCEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity', @level2type = N'COLUMN', @level2name = N'IdATCEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del DCI (Denominación Común Internacional), clave foránea que referencia el principio activo, ingrediente farmacológico o sustancia activa del medicamento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity', @level2type = N'COLUMN', @level2name = N'IdDCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity', @level2type = N'COLUMN', @level2name = N'IdDCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity', @level2type = N'COLUMN', @level2name = N'IdDCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) de la relación entre DCI y Entidad ATC, clave primaria de la tabla de asociación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los principios activos (DCI, Denominación Común Internacional) con las entidades o categorías del sistema de clasificación ATC (Anatómica, Terapéutica, Química). Permite saber a qué grupo terapéutico pertenece cada principio activo de medicamento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity';
