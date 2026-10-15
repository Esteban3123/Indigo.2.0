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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si aplica a que aplica la Nota:  0. Factura  1. Anticipo  2.Reversión CXP  3.Reversión Amortizaciones', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'IndicatesBillAdvance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado   1. Registrado   2. Confirmado   3. Anulado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentNotes', @level2type = N'COLUMN', @level2name = N'Status';

