CREATE TABLE [dbo].[SOLDETORD] (
    [DETORDAUT] INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDCONSEC] VARCHAR (50)    NOT NULL,
    [PRODAUTON] INT             NOT NULL,
    [CANTCOMPR] INT             NOT NULL,
    [IVADETORD] NUMERIC (5, 2)  NOT NULL,
    [VALORNETO] NUMERIC (18, 2) NOT NULL,
    [DESCUENTO] NUMERIC (5, 2)  NOT NULL,
    [ORDCANREC] INT             NULL,
    CONSTRAINT [PK_SOLDETORD] PRIMARY KEY CLUSTERED ([DETORDAUT] ASC),
    CONSTRAINT [FK_SOLDETORD_SOLORDCOM] FOREIGN KEY ([ORDCONSEC]) REFERENCES [dbo].[SOLORDCOM] ([ORDCONSEC]),
    CONSTRAINT [FK_SOLDETORD_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades recibidas del producto en la orden de compra; registra el ingreso físico vs. lo solicitado (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'ORDCANREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la cantidad recibida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'ORDCANREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'ORDCANREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje o monto de descuento aplicado al detalle de la orden de compra; afecta el valor final facturado (NUMERIC 5,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'DESCUENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el descuento de la compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'DESCUENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'DESCUENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor neto total del renglón tras aplicar descuento e IVA; base para facturación y auditoría de compras (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'VALORNETO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el valor neto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'VALORNETO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'VALORNETO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de IVA (Impuesto al Valor Agregado) aplicado al detalle; requisito RIPS y normativa fiscal (NUMERIC 5,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'IVADETORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene autonumerico del iva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'IVADETORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'IVADETORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades compradas/solicitadas del producto en este renglón de orden (INT, obligatorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'CANTCOMPR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la cantidad comprada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'CANTCOMPR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'CANTCOMPR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del producto (FK a SOLPRODUC); vincula el artículo, medicamento o insumo a su maestro (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código consecutivo de la orden de compra (FK a SOLORDCOM); agrupa todos los detalles de una misma compra (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el consecutivo de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumeral) del detalle/renglón de la orden de compra; llave primaria para auditoría (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'DETORDAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'DETORDAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'DETORDAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las órdenes de solicitud de productos o servicios autorizados: registra cada ítem (producto/servicio) incluido en una orden, con sus cantidades, valores, descuentos e IVA aplicados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD';
