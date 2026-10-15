CREATE TABLE [Treasury].[SchedulePaymentDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SupplierId]            INT             NOT NULL,
    [ThirdPartyId]          INT             NOT NULL,
    [SchedulePaymentId]     INT             NOT NULL,
    [DistributionLineId]    INT             NOT NULL,
    [ExpenseConceptId]      INT             NOT NULL,
    [MainAccountId]         INT             NOT NULL,
    [Nature]                TINYINT         NOT NULL,
    [AccountPayableId]      INT             NOT NULL,
    [AccountPayableShareId] INT             NOT NULL,
    [AmountPaid]            DECIMAL (18, 2) NOT NULL,
    [AmountPercent]         DECIMAL (5, 2)  NOT NULL,
    [PaymentConceptId]      INT             NOT NULL,
    [Paid]                  BIT             NOT NULL,
    [Description]           VARCHAR (300)   NULL,
    [VoucherTransactionId]  INT             NULL,
    [GeneratedVoucher]      BIT             CONSTRAINT [DF_SchedulePaymentDetail_GeneratedVoucher] DEFAULT ((0)) NOT NULL,
    [DiscountValue]         DECIMAL (18, 2) CONSTRAINT [DF__ScheduleP__Disco__76E710A9] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SchedulePaymentDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SchedulePaymentDetail_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_AccountPayableShares] FOREIGN KEY ([AccountPayableShareId]) REFERENCES [Payments].[AccountPayableShares] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_DistributionLines] FOREIGN KEY ([DistributionLineId]) REFERENCES [Common].[DistributionLines] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_ExpenseConcepts] FOREIGN KEY ([ExpenseConceptId]) REFERENCES [Treasury].[ExpenseConcepts] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_SchedulePayment] FOREIGN KEY ([SchedulePaymentId]) REFERENCES [Treasury].[SchedulePayment] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_TreasuryPaymentConcepts] FOREIGN KEY ([PaymentConceptId]) REFERENCES [Treasury].[TreasuryPaymentConcepts] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetail_VoucherTransaction] FOREIGN KEY ([VoucherTransactionId]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_AccountPayable];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_AccountPayableShares];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_DistributionLines];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_ExpenseConcepts];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_MainAccounts];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_SchedulePayment];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_Supplier];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_ThirdParty];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_TreasuryPaymentConcepts];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetail] NOCHECK CONSTRAINT [FK_SchedulePaymentDetail_VoucherTransaction];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SchedulePaymentDetail__SchedulePaymentId__AccountPayableId]
    ON [Treasury].[SchedulePaymentDetail]([SchedulePaymentId] ASC, [AccountPayableId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descuento pronto pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo especifica si ya se genero el comprobante de egreso con este detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'GeneratedVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del comprobante de egreso que se genero con este detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Describe el problema del porque no se puedo pagar la factura', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el registro ya fue pagado y por consiguiente se ha generado comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Paid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del concepto de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'PaymentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Porcentaje', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AmountPercent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor pagado a la cuota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AmountPaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de las coutas de las facturas', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableShareId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la factura de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza del concepto de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la cuenta contable asociada a la a la linea de distribucion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del concepto de egreso asociada a la linea de distribucion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'ExpenseConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la line a de distribucion asociada al proveedor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'DistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la programacion de pagos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'SchedulePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del tercero relacionado a la factura', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'proveedor asociado a la factura', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Id';

