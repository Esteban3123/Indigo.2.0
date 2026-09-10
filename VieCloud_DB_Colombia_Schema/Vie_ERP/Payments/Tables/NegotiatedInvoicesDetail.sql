CREATE TABLE [Payments].[NegotiatedInvoicesDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdNegotiatedInvoices]     INT             NOT NULL,
    [CodeHash]                 VARCHAR (36)    NOT NULL,
    [InvoiceNumber]            VARCHAR (100)   NOT NULL,
    [Model]                    VARCHAR (50)    NOT NULL,
    [IssuerTaxNumber]          VARCHAR (25)    NOT NULL,
    [IssuerName]               VARCHAR (300)   NULL,
    [DebtorTaxNumber]          VARCHAR (25)    NOT NULL,
    [DebtorName]               VARCHAR (300)   NULL,
    [AssigneeTaxNumber]        VARCHAR (25)    NOT NULL,
    [AssigneeName]             VARCHAR (300)   NULL,
    [PaymentDateWithExtension] DATE            NOT NULL,
    [DisbursementDate]         DATE            NULL,
    [TotalAmount]              DECIMAL (18, 2) NULL,
    [DiscountConfirmingTotal]  DECIMAL (18, 2) NULL,
    [ValueIssuer]              DECIMAL (18, 2) NULL,
    [BarCode]                  VARCHAR (500)   NULL,
    [NotesApplied]             VARCHAR (300)   NULL,
    [NegociationDate]          DATE            NOT NULL,
    [Message]                  VARCHAR (500)   NULL,
    [Status]                   TINYINT         CONSTRAINT [DF__Negotiate__Statu__0F48AA1F] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_NegotiatedInvoicesDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NegotiatedInvoicesDetail_NegotiatedInvoice] FOREIGN KEY ([IdNegotiatedInvoices]) REFERENCES [Payments].[NegotiatedInvoices] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del detalle de negociación: 1=Pendiente, 2=Exitoso, 3=Error. Indica si la factura fue procesada correctamente en el ciclo de confirming o descuento en efectivo.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1:Pending, 2: OK, 3: Error', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de resultado o error del procesamiento de la factura negociada. Comunica detalles sobre el estado de validación o rechazo.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de negociación de la factura. Momento en que se registra la transacción de confirming o descuento en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'NegociationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Negociación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'NegociationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'NegociationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Números de notas o ajustes aplicados al documento, separados por barra inclinada (/). Referencia a créditos o débitos sobre la factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'NotesApplied';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de las notas aplicadas. Si son varias se separa por el /', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'NotesApplied';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'NotesApplied';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de barras de la factura negociada. Identificador visual para lectura automatizada del documento en procesos de cobro.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'BarCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de barras del documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'BarCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'BarCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor a pagar al proveedor (emisor) por la factura negociada. Monto neto después de descontos aplicables.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'ValueIssuer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor a pagar al proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'ValueIssuer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'ValueIssuer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del descuento aplicado en la operación de confirming o descuento en efectivo sobre el monto original.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DiscountConfirmingTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor total del descuento del confirming', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DiscountConfirmingTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DiscountConfirmingTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del documento de la factura. Monto original antes de aplicar descuentos o ajustes.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'TotalAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor total del documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'TotalAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'TotalAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de desembolso al proveedor (emisor). Cuando se efectúa el pago de los fondos hacia la cuenta del acreedor.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DisbursementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de desembolso al proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DisbursementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DisbursementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de pago del comprador (deudor) al fondeador/entidad de confirming. Plazo máximo concedido para el reembolso de fondos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'PaymentDateWithExtension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de pago con extensión. Fecha en que el comprador debe pagar al fondeador', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'PaymentDateWithExtension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'PaymentDateWithExtension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del fondeador o entidad cedente. Organización que adquiere la factura y financia la operación de confirming.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'AssigneeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del fondeador', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'AssigneeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'AssigneeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT o identificación tributaria del fondeador (PII_Ofuscado). Identificador único del acreedor que recibe la factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'AssigneeTaxNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit del fondeador', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'AssigneeTaxNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'AssigneeTaxNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del comprador o deudor. Empresa que debe pagar al fondeador en la fecha establecida.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DebtorName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del comprador', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DebtorName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DebtorName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT o identificación tributaria del comprador (PII_Ofuscado). Identificador único del obligado al pago.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DebtorTaxNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit del comprador.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DebtorTaxNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DebtorTaxNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del proveedor o emisor de la factura. Acreedor original que emite el documento de cobro.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'IssuerName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'IssuerName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'IssuerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT o identificación tributaria del proveedor (PII_Ofuscado). Identificador único del emisor de la factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'IssuerTaxNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'IssuerTaxNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'IssuerTaxNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modelo de negociación: 0=CONFIRMING (financiamiento de flujo), 1=CASH_DISCOUNT (descuento en efectivo). Define el tipo de operación de factoraje.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valores:  0 : CONFIRMING, 1: CASH_DISCOUNT', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Model';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura. Identificador del documento de venta emitido por el proveedor.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hash o UUID de lote de 36 caracteres. Identificador único por lote; todas las facturas del mismo lote comparten el mismo CodeHash.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'CodeHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de lote del documento. Es un UUID de 36 caracteres. Todas las facturas del lote llevan el mismo codehash. Es único por lote', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'CodeHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'CodeHash';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del registro padre en Payments.NegotiatedInvoices. Agrupa el detalle dentro de su negociación principal.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'IdNegotiatedInvoices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de las Facturas negociadas', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'IdNegotiatedInvoices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'IdNegotiatedInvoices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de detalle. Clave primaria que distingue cada línea de verificación de negociación de factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de las verificaciones de negociaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las facturas incluidas en negociaciones de pago (confirming/descuento de facturas). Cada registro representa una factura individual negociada, con su emisor, deudor, cesionario, montos, descuentos aplicados y fechas de pago o desembolso.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail';
