CREATE TABLE [Billing].[AccountPayableNumberingAuthorization] (
    [Id]                       INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountPayableId]         INT         NOT NULL,
    [NumberingAuthorizationId] INT         NOT NULL,
    [InvoicePrefix]            VARCHAR (6) NULL,
    [Consecutive]              BIGINT      NOT NULL,
    CONSTRAINT [PK_AccountPayableNumberingAuthorization__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableId] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_NumberingAuthorization_Id] FOREIGN KEY ([NumberingAuthorizationId]) REFERENCES [Billing].[NumberingAuthorization] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo numérico secuencial del documento soporte (factura, recibo, comprobante de pago). Identificador de secuencia para numeración de documentos contables.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del documento soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prefijo alfanumérico (máx. 6 caracteres) del documento soporte; utilizado en facturación, recibos y comprobantes para diferencias numéricas por autorización o centro de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'InvoicePrefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prefijo del documento soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'InvoicePrefix';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'InvoicePrefix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la autorización de numeración de documentos soporte (FK a Billing.NumberingAuthorization); referencia a permisos DIAN, resolutions, o autorizaciones de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'NumberingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento soporte', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'NumberingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'NumberingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por pagar asociada (FK a Payments.AccountPayable); vincula el documento soporte a la obligación financiera, factura o gasto por cancelar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asignación; clave primaria (Identity) que relaciona consecutivos y prefijos con cuentas por pagar y autorizaciones de numeración.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de autorizaciones de numeración asociadas a cuentas por pagar, que controla el prefijo y consecutivo de facturas o documentos de cobro emitidos dentro del proceso de facturación y cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'AccountPayableNumberingAuthorization';
