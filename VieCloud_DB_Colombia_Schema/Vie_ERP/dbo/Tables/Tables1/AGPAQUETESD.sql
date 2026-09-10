CREATE TABLE [dbo].[AGPAQUETESD] (
    [ID]               INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGPAQUETE]      INT       NOT NULL,
    [CODPRODUC]        CHAR (20) NOT NULL,
    [CANTIDAD]         INT       NOT NULL,
    [CANTIDADPROBABLE] INT       NOT NULL,
    [PROBABILIDADUSO]  INT       NOT NULL,
    [IdCostCenter]     INT       NULL,
    CONSTRAINT [PK_AGPAQUETESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGPAQUETESD_AGPAQUETES] FOREIGN KEY ([IDAGPAQUETE]) REFERENCES [dbo].[AGPAQUETES] ([ID]),
    CONSTRAINT [FK_AGPAQUETESD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_AGPAQUETESD_Payroll.CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo (FK a Payroll.CostCenter) al que se asigna el gasto del producto; utilizado para contabilización, análisis de costos por unidad funcional y presupuesto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el id asociado al centro de costo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje (0-100) de probabilidad de uso del producto en el procedimiento quirúrgico; permite cálculos de costos esperados y glosas preventivas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'PROBABILIDADUSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje Probabilidad de uso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'PROBABILIDADUSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'PROBABILIDADUSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad estimada o probable de uso real del producto en la ejecución del paquete quirúrgico; utilizada para proyecciones de consumo y optimización de inventario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'CANTIDADPROBABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Probable Uso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'CANTIDADPROBABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'CANTIDADPROBABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad solicitada o requerida del producto para el paquete quirúrgico; base para presupuestos, facturas y RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Solicitado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del producto, insumo, medicamento o servicio (FK a IHLISTPRO); identificador normalizado del catálogo de inventario/productos para facturación y control de existencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de los Productos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paquete quirúrgico padre (FK a AGPAQUETES); agrupa productos, materiales y servicios quirúrgicos asociados a procedimientos quirúrgicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de los Paquetes Quirurgicos que guardan en tabla AGPAQUETES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de cada línea de detalle del paquete quirúrgico; clave primaria de la tabla AGPAQUETESD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de productos o servicios que componen un paquete de atención (paquete de salud). Registra cada ítem del paquete con su cantidad definida, cantidad probable de uso y la probabilidad de que ese producto o servicio sea utilizado dentro del paquete.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGPAQUETESD';
