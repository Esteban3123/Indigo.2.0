CREATE TABLE [Portfolio].[OpeningBalanceCircularZeroThirty] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [InvoiceNumber]      VARCHAR (20)    NOT NULL,
    [TotalValuePayments] NUMERIC (18, 2) NOT NULL,
    [TotalGlosaValue]    NUMERIC (18, 2) NOT NULL,
    [CreationUser]       VARCHAR (20)    NOT NULL,
    CONSTRAINT [PK_OpeningBalanceCircularZeroThirty] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [InvoiceNumber_UNIQUE] UNIQUE NONCLUSTERED ([InvoiceNumber] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de saldo inicial (VARCHAR 20). Auditoría de entrada de datos en el sistema.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total de glosas (impugnaciones/rechazos) por factura. NUMERIC(18,2). Cartera en disputa o ajuste.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'TotalGlosaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'total glosa', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'TotalGlosaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'TotalGlosaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total de pagos realizados por factura. NUMERIC(18,2). Capital cobrado o liquidado en Circular 030.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'TotalValuePayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total pagos', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'TotalValuePayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'TotalValuePayments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de factura o liquidación (VARCHAR 20, UNIQUE). Equivalente a comprobante, recibo o documento de cobro RIPS.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY 1,1). Clave primaria de la tabla de saldos iniciales.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de saldos iniciales (apertura) para la Circular 030 de la Superintendencia. Registra facturas/liquidaciones con totales de pagos y glosas en cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla de saldos iniciales para la circular 030', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'OpeningBalanceCircularZeroThirty';

