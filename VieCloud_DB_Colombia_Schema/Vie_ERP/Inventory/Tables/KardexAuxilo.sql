CREATE TABLE [Inventory].[KardexAuxilo] (
    [Id]                    INT NOT NULL,
    [PreviousAmountProduct] INT NOT NULL,
    [CantidaReal]           INT NOT NULL,
    CONSTRAINT [PK_KardexAuxilo] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad real de artículo de auxilio (medicamento, insumo, material médico) registrada en el movimiento de kardex; cantidad efectiva disponible o consumida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo', @level2type = N'COLUMN', @level2name = N'CantidaReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad real', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo', @level2type = N'COLUMN', @level2name = N'CantidaReal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo', @level2type = N'COLUMN', @level2name = N'CantidaReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad anterior del producto de auxilio antes del movimiento; saldo previo de inventario del medicamento, insumo o material médico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo', @level2type = N'COLUMN', @level2name = N'PreviousAmountProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad anterior del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo', @level2type = N'COLUMN', @level2name = N'PreviousAmountProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo', @level2type = N'COLUMN', @level2name = N'PreviousAmountProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT) de la transacción de kardex de auxilios; clave primaria de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro auxiliar del kardex de inventario que almacena los movimientos de productos, guardando la cantidad anterior y la cantidad real de cada ítem para el control y trazabilidad del stock.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo';
