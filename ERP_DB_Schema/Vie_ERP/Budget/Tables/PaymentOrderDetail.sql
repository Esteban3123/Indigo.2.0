CREATE TABLE [Budget].[PaymentOrderDetail] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PaymentOrderId]          INT          NOT NULL,
    [ObligationDetailId]      INT          NOT NULL,
    [ExpiredDate]             DATETIME     NOT NULL,
    [InitialValue]            NUMERIC (18) NOT NULL,
    [DebitModificationValue]  NUMERIC (18) NOT NULL,
    [CreditModificationValue] NUMERIC (18) NOT NULL,
    [TotalPaymentOrder]       NUMERIC (18) NOT NULL,
    [ExecutedValue]           NUMERIC (18) NOT NULL,
    [Balance]                 NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_PaymentOrderDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Budget_PaymentOrderDetail_ValidateValues] CHECK ((([InitialValue]-[DebitModificationValue])+[CreditModificationValue])=[TotalPaymentOrder] AND ([TotalPaymentOrder]-[ExecutedValue])=[Balance] AND NOT ([TotalPaymentOrder]<(0) OR [ExecutedValue]<(0) OR [Balance]<(0))),
    CONSTRAINT [FK_PaymentOrderDetail_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id]),
    CONSTRAINT [FK_PaymentOrderDetail_PaymentOrder] FOREIGN KEY ([PaymentOrderId]) REFERENCES [Budget].[PaymentOrder] ([Id])
);


GO
ALTER TABLE [Budget].[PaymentOrderDetail] NOCHECK CONSTRAINT [CK_Budget_PaymentOrderDetail_ValidateValues];


GO
ALTER TABLE [Budget].[PaymentOrderDetail] NOCHECK CONSTRAINT [FK_PaymentOrderDetail_ObligationDetail];


GO
ALTER TABLE [Budget].[PaymentOrderDetail] NOCHECK CONSTRAINT [FK_PaymentOrderDetail_PaymentOrder];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor ejecutado del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orden total de pago', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalPaymentOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor modificacion credito', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Modificacion debito', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Obligacion se de la orden de pago', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cabecera de la orden de pago', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'PaymentOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';

