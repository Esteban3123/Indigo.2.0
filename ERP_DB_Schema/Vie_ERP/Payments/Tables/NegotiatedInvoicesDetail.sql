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
ALTER TABLE [Payments].[NegotiatedInvoicesDetail] NOCHECK CONSTRAINT [FK_NegotiatedInvoicesDetail_NegotiatedInvoice];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1:Pending, 2: OK, 3: Error', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Negociación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'NegociationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de las notas aplicadas. Si son varias se separa por el /', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'NotesApplied';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de barras del documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'BarCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor a pagar al proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'ValueIssuer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor total del descuento del confirming', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DiscountConfirmingTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor total del documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'TotalAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de desembolso al proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DisbursementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de pago con extensión. Fecha en que el comprador debe pagar al fondeador', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'PaymentDateWithExtension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del fondeador', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'AssigneeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit del fondeador', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'AssigneeTaxNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del comprador', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DebtorName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit del comprador.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'DebtorTaxNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'IssuerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'IssuerTaxNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valores:  0 : CONFIRMING, 1: CASH_DISCOUNT', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de la factura', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de lote del documento. Es un UUID de 36 caracteres. Todas las facturas del lote llevan el mismo codehash. Es único por lote', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'CodeHash';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de las Facturas negociadas', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'IdNegotiatedInvoices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de las verificaciones de negociaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'NegotiatedInvoicesDetail', @level2type = N'COLUMN', @level2name = N'Id';

