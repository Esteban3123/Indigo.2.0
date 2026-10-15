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

