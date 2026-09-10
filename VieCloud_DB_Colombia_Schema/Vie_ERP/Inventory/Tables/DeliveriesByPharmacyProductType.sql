CREATE TABLE [Inventory].[DeliveriesByPharmacyProductType] (
    [Id]                  INT          IDENTITY (1, 1) NOT NULL,
    [HCFARMEPDId]         INT          NOT NULL,
    [ProductType]         BIT          NOT NULL,
    [Quantity]            INT          NOT NULL,
    [ConcentrationByUnit] VARCHAR (20) NOT NULL,
    [TotalWeight]         VARCHAR (30) NOT NULL,
    CONSTRAINT [PK_DeliveriesByPharmacyProductType__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeliveriesByPharmacyProductType_HCFARMEPD] FOREIGN KEY ([HCFARMEPDId]) REFERENCES [dbo].[HCFARMEPD] ([ID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso total acumulado del producto (resultado de Cantidad × Concentración por unidad); VARCHAR(30) almacena valor con unidades (gramos, mg, etc.).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'TotalWeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso Total (Cantidades * Concentracion)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'TotalWeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'TotalWeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración, dosis o potencia por cada unidad individual del medicamento (ej: mg/mL, mg/comprimido); VARCHAR(20) para flexibilidad de formato.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'ConcentrationByUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentracion por cada unidad de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'ConcentrationByUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'ConcentrationByUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica de unidades del producto farmacéutico seleccionado, distribuido o entregado en esta transacción de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de producto seleccionado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación binaria del producto farmacéutico: 0/False = Producto Transformado (magistral/preparado en farmacia); 1/True = Producto Comercial (industrial/marca registrada).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'ProductType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de producto: False o 0 = Producto Transformado
																					True o 1 = Producto Comercial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'ProductType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'ProductType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia HCFARMEPD; identifica la solicitud, encargo o movimiento de farmacia desde el cual se originó la entrega o distribución de medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'HCFARMEPDId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla HCFARMEPD desde donde se realizo la solicitud a farmacia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'HCFARMEPDId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'HCFARMEPDId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de entrega de producto farmacéutico por tipo en la farmacia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de entregas de farmacia desglosadas por tipo de producto, indicando cantidades dispensadas, concentración por unidad y peso total de cada entrega asociada a un registro de farmacia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType';
