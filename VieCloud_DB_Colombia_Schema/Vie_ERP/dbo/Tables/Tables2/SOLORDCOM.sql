CREATE TABLE [dbo].[SOLORDCOM] (
    [ORDCONSEC]  VARCHAR (50)   NOT NULL,
    [CODUSUARI]  CHAR (20)      NOT NULL,
    [ORDENFECH]  DATETIME       NOT NULL,
    [AUTOCOTI]   INT            NOT NULL,
    [ORDDESCUE]  NUMERIC (5, 2) NOT NULL,
    [ORDESTADO]  TINYINT        NULL,
    [ORDFACTURA] VARCHAR (50)   NULL,
    [ORDFECENT]  DATETIME       NULL,
    [ORDMOTPAR]  VARCHAR (50)   NULL,
    [ORDARCFAC]  VARCHAR (50)   NULL,
    [PROVAUTO]   NUMERIC (18)   NOT NULL,
    [CODCENATE]  CHAR (10)      NULL,
    CONSTRAINT [PK_SOLORDCOM_1] PRIMARY KEY CLUSTERED ([ORDCONSEC] ASC),
    CONSTRAINT [FK_SOLORDCOM_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_SOLORDCOM_SOLPROVEE] FOREIGN KEY ([PROVAUTO]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (unidad funcional, sede, IPS). FK a ADCENATEN. Identifica dónde se gestiona la orden de compra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) del proveedor. FK a SOLPROVEE. Referencia el proveedor asociado a la orden de compra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'PROVAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'PROVAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'PROVAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Archivo o referencia de factura de venta emitida por el proveedor. VARCHAR(50), puede vincular la orden con documentos de comprobante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDARCFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factura de venta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDARCFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDARCFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de la solicitud parcial o rechazo parcial de la orden. Justificación textual de entregas incompletas o modificaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDMOTPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el motivo de la solicitud parcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDMOTPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDMOTPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de entrega efectiva de la orden de compra. DATETIME, registro de cuándo se recibió el pedido en el centro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDFECENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDFECENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDFECENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura de la orden de compra emitida por el proveedor. VARCHAR(50), documento fiscal de la transacción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDFACTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la factura de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDFACTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDFACTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la orden de compra (TINYINT): pendiente, recibida, cancelada, parcial, etc. Seguimiento del ciclo de compra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el estado de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento aplicado a la orden de compra. NUMERIC(5,2), porcentaje o valor de reducción de precio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDDESCUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el descuento de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDDESCUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDDESCUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) de la orden de compra. INT, clave para rastrear cotizaciones y compras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión de la orden de compra. DATETIME, cuándo se generó la solicitud al proveedor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDENFECH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la fecha de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDENFECH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDENFECH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que creó o gestiona la orden. CHAR(20), profesional, administrativo o encargado de compras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único (PK) de la orden de compra. VARCHAR(50), identificador primario para auditoría y trazabilidad RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el consecutivo del orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes de compra o solicitudes de pedido a proveedores. Registra cada orden generada, su estado, descuentos aplicados, facturación asociada y el centro de atención que la origina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM';
