CREATE TABLE [Inventory].[MedicalFormulaDetailDeferred] (
    [Id]                       INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MedicalFormulaDetailId]   INT      NOT NULL,
    [FirstDeliveryDate]        DATETIME NOT NULL,
    [DeliveryQuantityDeferred] INT      NOT NULL,
    [Periodicity]              INT      NOT NULL,
    [Number]                   INT      NOT NULL,
    [DeliveryDate]             DATETIME NOT NULL,
    [DeliveryQuantity]         INT      NOT NULL,
    [PendingQuantity]          INT      NOT NULL,
    CONSTRAINT [PK_MedicalFormulaDetailDeferred] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFormulaDetailDeferred_MedicalFormulaDetailDeferred] FOREIGN KEY ([MedicalFormulaDetailId]) REFERENCES [Inventory].[MedicalFormulaDetail] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MedicalFormulaDetailDeferred]
    ON [Inventory].[MedicalFormulaDetailDeferred]([MedicalFormulaDetailId] ASC, [Number] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente de dispensación (INT). Disminuye con cada entrega; inicia igual a DeliveryQuantity. Rastrea unidades restantes por entregar en el diferido de medicamento/producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'PendingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad pendiente, cada vez que se realiza la dispensación este valor disminuye porque empieza con el mismo valor de DeliveryQuantity', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'PendingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'PendingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad a entregar en esta cuota (INT). Se calcula a partir de DeliveryQuantityDeferred. Representa las unidades del medicamento/producto en cada dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'DeliveryQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad a entregar, se calcula con DeliveryQuantityDeferred', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'DeliveryQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'DeliveryQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha programada de entrega/dispensación (DATETIME). Se calcula sumando la FirstDeliveryDate más múltiplos de Periodicity. Define cuándo se entrega cada cuota del diferido.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'DeliveryDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de entrega, se calcula con la primera fecha de entrega y la periodicidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'DeliveryDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'DeliveryDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial identificador del diferido (INT). Ordena las cuotas de entrega del medicamento (1, 2, 3...). Usado para rastrear qué número de dispensación corresponde.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero que identifica el diferido', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Periodicidad en días entre entregas (INT). Define el intervalo temporal para cada cuota. Ej: 30 días = entrega mensual; 7 días = semanal.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'Periodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el tiempo en dias con la cual se va a entregar cada producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'Periodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'Periodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad por cuota del diferido (INT). Unidades de medicamento/producto a entregar en cada dispensación programada del diferido.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'DeliveryQuantityDeferred';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad que se va a entregar por cada diferido', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'DeliveryQuantityDeferred';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'DeliveryQuantityDeferred';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera fecha de entrega del diferido (DATETIME). Fecha base; resto de entregas se calculan sumando múltiplos de Periodicity. Punto de partida del cronograma.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'FirstDeliveryDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primera fecha de entrega. Con base a esta se calculan las otros diferidos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'FirstDeliveryDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'FirstDeliveryDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de fórmula médica (INT, FK). Referencia al producto/medicamento en la receta. Vincula el diferido con línea específica de la prescripción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la formula medica, en el detalle es donde van los productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro (INT IDENTITY, PK). Clave primaria de la tabla MedicalFormulaDetailDeferred.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el cronograma de entregas diferidas (fraccionadas) de medicamentos en una fórmula médica, indicando cuántas unidades se entregan en cada fecha según la periodicidad pactada y cuántas quedan pendientes por despachar.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred';
