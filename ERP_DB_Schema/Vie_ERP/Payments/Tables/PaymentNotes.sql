CREATE TABLE [Payments].[PaymentNotes] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (20)  NOT NULL,
    [NoteDate]                    DATETIME      NOT NULL,
    [Status]                      TINYINT       NOT NULL,
    [IdSupplier]                  INT           NOT NULL,
    [IdSupplierDistributionLines] INT           NULL,
    [IdCostCenter]                INT           NULL,
    [Comment]                     VARCHAR (MAX) NULL,
    [Nature]                      TINYINT       NOT NULL,
    [Reinstatement]               BIT           NOT NULL,
    [IdVoucherTransaction]        INT           NULL,
    [IndicatesBillAdvance]        TINYINT       NOT NULL,
    [CancelCheck]                 BIT           NULL,
    [BudgetInterface]             BIT           NOT NULL,
    [IdOperatingUnit]             INT           NOT NULL,
    [EntityId]                    INT           NULL,
    [EntityCode]                  VARCHAR (20)  NULL,
    [EntityName]                  VARCHAR (250) NULL,
    [CreationUser]                VARCHAR (20)  NOT NULL,
    [CreationDate]                DATETIME      NOT NULL,
    [ModificationUser]            VARCHAR (20)  NULL,
    [ModificationDate]            DATETIME      NULL,
    [ConfirmationUser]            VARCHAR (20)  NULL,
    [ConfirmationDate]            DATETIME      NULL,
    [AnnulmentUser]               VARCHAR (20)  NULL,
    [AnnulmentDate]               DATETIME      NULL,
    [TimeStamp]                   ROWVERSION    NOT NULL,
    [IdAccountPayable]            INT           NULL,
    [AllowDiscountPromptPayment]  BIT           CONSTRAINT [DF__PaymentNo__Allow__1887FB96] DEFAULT ((0)) NOT NULL,
    [AffectBaseToDiscount]        BIT           CONSTRAINT [DF__PaymentNo__Affec__197C1FCF] DEFAULT ((0)) NOT NULL,
    [CurrencyId]                  INT           CONSTRAINT [DF__PaymentNo__Curre__586354E0] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_PaymentNotes__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentNotes_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PaymentNotes_IdAccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_PaymentNotes_SuppliersDistributionLines] FOREIGN KEY ([IdSupplierDistributionLines]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_PaymentsNote_Supplier] FOREIGN KEY ([IdSupplier]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_PaymentsNote_VoucherTransaction] FOREIGN KEY ([IdVoucherTransaction]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);


GO
ALTER TABLE [Payments].[PaymentNotes] NOCHECK CONSTRAINT [FK_PaymentNotes_Currency];


GO
ALTER TABLE [Payments].[PaymentNotes] NOCHECK CONSTRAINT [FK_PaymentNotes_IdAccountPayable];


GO
ALTER TABLE [Payments].[PaymentNotes] NOCHECK CONSTRAINT [FK_PaymentNotes_SuppliersDistributionLines];


GO
ALTER TABLE [Payments].[PaymentNotes] NOCHECK CONSTRAINT [FK_PaymentsNote_Supplier];


GO
ALTER TABLE [Payments].[PaymentNotes] NOCHECK CONSTRAINT [FK_PaymentsNote_VoucherTransaction];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PaymentNotes__Code]
    ON [Payments].[PaymentNotes]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Moneda de la nota deb/cred', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta base para descuento pronto pago', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'AffectBaseToDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite postular facturas para descuento pronto pago', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'AllowDiscountPromptPayment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si debe realizar interfaz presupuestal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'BudgetInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si debe anular el cheque', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'CancelCheck';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si aplica a que aplica la Nota:  0. Factura  1. Anticipo  2.Reversión CXP  3.Reversión Amortizaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'IndicatesBillAdvance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'IdVoucherTransaction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que indica si la nota es de reintegro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'Reinstatement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la nota(Debito=1, Credito=2)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo, siempre y cuando la cuenta de la linea requiera centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Linea de Distribucion asociada al Proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'IdSupplierDistributionLines';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'IdSupplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado   1. Registrado   2. Confirmado   3. Anulado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'NoteDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'Id';

