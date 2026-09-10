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
ALTER TABLE [Budget].[PaymentOrderDetail] NOCHECK CONSTRAINT [CK_Budget_PaymentOrderDetail_ValidateValues];


GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente de pago; diferencia entre orden total y valor ejecutado (NUMERIC 18, no negativo)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ejecutado del rubro; monto pagado o desembolsado del presupuesto (NUMERIC 18, no negativo)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor ejecutado del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden total de pago; valor neto tras modificaciones deudoras y acreedoras (NUMERIC 18, no negativo)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalPaymentOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden total de pago', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalPaymentOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'TotalPaymentOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor modificación crédito; ajuste positivo o aumento al rubro presupuestal (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor modificacion credito', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor modificación débito; ajuste negativo o disminución al rubro presupuestal (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Modificacion debito', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor inicial; monto base del rubro antes de modificaciones presupuestales (NUMERIC 18, no negativo)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento; límite temporal para ejecutar o comprometer el pago (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de detalle de obligación; FK que vincula al rubro u obligación específica del presupuesto', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Obligacion se de la orden de pago', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador cabecera de orden de pago; FK que vincula al registro padre/contenedor de la orden', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'PaymentOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cabecera de la orden de pago', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'PaymentOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'PaymentOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental; clave primaria de la tabla PaymentOrderDetail (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las órdenes de pago presupuestales: registra cada renglón de una orden de pago vinculado a un detalle de obligación, con sus valores iniciales, modificaciones por débito y crédito, valor total, monto ejecutado y saldo pendiente por pagar.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PaymentOrderDetail';
