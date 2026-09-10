CREATE TABLE [Inventory].[DrugActive] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [ParentDCIId] INT NOT NULL,
    [DCIId]       INT NULL,
    [ATCEntityId] INT NOT NULL,
    CONSTRAINT [PK_DrugActive] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DCI_DCI] FOREIGN KEY ([DCIId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [FK_DCI_ParentDCI] FOREIGN KEY ([ParentDCIId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [FK_DrugActive_ATCEntity] FOREIGN KEY ([ATCEntityId]) REFERENCES [Inventory].[ATCEntity] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la entidad ATC (Anatomical Therapeutic Chemical); FK a Inventory.ATCEntity. Clasifica el fármaco activo según nomenclatura farmacológica internacional.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del a entidad del ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'ATCEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del DCI (Denominación Común Internacional); FK a Inventory.DCI. Referencia el nombre genérico del principio activo del medicamento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'DCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'DCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'DCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación padre del DCI (Denominación Común Internacional); FK a Inventory.DCI. Vincula la jerarquía de principios activos farmacológicos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'ParentDCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificaccion Padre del DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'ParentDCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'ParentDCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (Identity INT) del registro en Inventory.DrugActive. Clave primaria para cada fármaco activo registrado en el inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de medicamentos activos (principios activos) disponibles en el inventario farmacéutico, vinculando cada fármaco con su clasificación ATC y su denominación común internacional (DCI) jerárquica (padre e hijo), utilizado para gestionar el catálogo de drogas activas en el sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive';
