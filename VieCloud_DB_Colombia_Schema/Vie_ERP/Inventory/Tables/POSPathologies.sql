CREATE TABLE [Inventory].[POSPathologies] (
    [Id]           INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductId]    INT     NULL,
    [DiagnosticId] INT     NOT NULL,
    [MedicamentId] INT     NULL,
    [MinimumAge]   TINYINT NULL,
    [MaximumAge]   TINYINT NULL,
    [AgeMeasure]   TINYINT NULL,
    CONSTRAINT [PK_POSPathologies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_POSPathologies_ATC] FOREIGN KEY ([MedicamentId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_POSPathologies_Diagnostic] FOREIGN KEY ([DiagnosticId]) REFERENCES [Inventory].[Diagnostic] ([Id]),
    CONSTRAINT [FK_POSPathologies_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);




GO



GO



GO



GO
-- Índice filtrado para optimizar consultas por ProductId (usado en ViewListRevenueControl)
CREATE NONCLUSTERED INDEX [IX_POSPathologies_ProductId]
ON [Inventory].[POSPathologies] ([ProductId] ASC, [Id] ASC)
WHERE [ProductId] IS NOT NULL;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida de edad (TINYINT): 1=Años, 2=Meses, 3=Días. Define la escala temporal para interpretar MinimumAge y MaximumAge en restricciones pediátricas o geriátricas del POS.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'AgeMeasure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de edad: 1:Años, 2:Meses, 3:Dias', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'AgeMeasure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'AgeMeasure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida (TINYINT, 0-255) para la cobertura de este medicamento o producto en el diagnóstico. Límite superior de rango etario, puede ser nulo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad máxima', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima permitida (TINYINT, 0-255) para la cobertura de este medicamento o producto en el diagnóstico. Límite inferior de rango etario, puede ser nulo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'MinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad mínima', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'MinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'MinimumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del medicamento en catálogo ATC (INT, FK → Inventory.ATC). Se completa cuando el registro proviene del proceso ATC. Nulo si el origen es desde ProductId. Equivalente a código farmacoterapéutico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'MedicamentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del medicamento(Antes ATC), este campo se llena siempre y cuando el proceso venga desde ATC, si el proceso viene de productos este campo no se llena y se llena el campo ProductId', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'MedicamentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'MedicamentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del diagnóstico (INT, FK → Inventory.Diagnostic). Referencia obligatoria al código de diagnóstico, patología o enfermedad asociada al POS. Campo requerido siempre.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'DiagnosticId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'DiagnosticId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'DiagnosticId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto de inventario (INT, FK → Inventory.InventoryProduct). Se completa cuando el registro proviene del proceso de productos. Nulo si el origen es desde MedicamentId/ATC. Referencia a producto del inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto, este valor va nulo ya que se agrega funcionalidad para manejo de pos en atc, cuando el proceso viene desde atc no se llena este campo sino que se llena MedicamentId', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de patología en el Plan Obligatorio de Salud (POS). Clave primaria con identidad auto-incremental.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de patologias', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre productos o medicamentos del punto de venta (POS) y las patologías o diagnósticos asociados, incluyendo restricciones de edad mínima y máxima para su uso o dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies';
