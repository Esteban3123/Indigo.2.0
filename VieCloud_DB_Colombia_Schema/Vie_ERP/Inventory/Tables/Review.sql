CREATE TABLE [Inventory].[Review] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [Description]            VARCHAR (MAX)   NULL,
    [EntityName]             VARCHAR (250)   NOT NULL,
    [EntityCode]             VARCHAR (20)    NOT NULL,
    [JournalVoucherType]     VARCHAR (200)   NULL,
    [Consecutive]            BIGINT          NULL,
    [JournalVoucherId]       INT             NOT NULL,
    [MainAccountId]          INT             NOT NULL,
    [MainAccount]            VARCHAR (200)   NOT NULL,
    [DocumentDate]           DATE            NULL,
    [AccountingDocumentDate] DATE            NULL,
    [DebitValue]             DECIMAL (20, 4) NOT NULL,
    [AccountingDebitValue]   DECIMAL (20, 4) NOT NULL,
    [CreditValue]            DECIMAL (20, 4) NOT NULL,
    [AccountingCreditValue]  DECIMAL (20, 4) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Revisión contable de movimientos de inventario. Registra los comprobantes de diario (débitos y créditos) asociados a entidades del inventario, con sus cuentas contables principales y fechas de documento para conciliación y auditoría.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de revisión contable de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o detalle del movimiento contable revisado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad (proveedor, almacén, centro de costo u otra) asociada al movimiento de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad relacionada con el movimiento; sirve para identificar el origen del registro.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comprobante de diario o voucher contable (por ejemplo: ingreso, egreso, ajuste, traslado).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'JournalVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'JournalVoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo del comprobante contable dentro del período o secuencia definida.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del comprobante de diario (voucher) asociado al movimiento contable.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno de la cuenta contable principal afectada por el movimiento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o código de la cuenta contable principal (plan de cuentas) afectada; cuenta mayor.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'MainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'MainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento fuente o del movimiento físico de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha contable oficial del documento; fecha con la que se registra en la contabilidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'AccountingDocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'AccountingDocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor debitado en moneda local por el movimiento de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor debitado en moneda contable o de reporte (puede diferir si hay conversión de divisa).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'AccountingDebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'AccountingDebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor acreditado en moneda local por el movimiento de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor acreditado en moneda contable o de reporte (puede diferir si hay conversión de divisa).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'AccountingCreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Review', @level2type = N'COLUMN', @level2name = N'AccountingCreditValue';
