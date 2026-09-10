CREATE TABLE [Billing].[InvoiceTransfer] (
    [Id]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PreviousInvoiceId] INT NOT NULL,
    [NewInvoiceId]      INT NOT NULL,
    CONSTRAINT [PK_InvoiceTransfer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceTransfer_Invoice] FOREIGN KEY ([PreviousInvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_InvoiceTransfer_Invoice1] FOREIGN KEY ([NewInvoiceId]) REFERENCES [Billing].[Invoice] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la factura nueva o destino en transferencia de facturación; referencia a Billing.Invoice; usado en glosas, reemisiones o ajustes de factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer', @level2type = N'COLUMN', @level2name = N'NewInvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de factura nueva', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer', @level2type = N'COLUMN', @level2name = N'NewInvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer', @level2type = N'COLUMN', @level2name = N'NewInvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la factura anterior u origen en transferencia de facturación; referencia a Billing.Invoice; rastrea factura original antes de cambio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer', @level2type = N'COLUMN', @level2name = N'PreviousInvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de factura anterior', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer', @level2type = N'COLUMN', @level2name = N'PreviousInvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer', @level2type = N'COLUMN', @level2name = N'PreviousInvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincremental (INT IDENTITY) y clave primaria de registro de transferencia de factura; auditoría de movimientos y cambios en facturación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de traslados o reasignaciones de facturas: vincula una factura original con la nueva factura que la reemplaza, permitiendo rastrear el historial de transferencias de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceTransfer';
