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



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SchedulePaymentDetail__SchedulePaymentId__AccountPayableId]
    ON [Treasury].[SchedulePaymentDetail]([SchedulePaymentId] ASC, [AccountPayableId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2, default=0) del descuento por pronto pago, pago anticipado o bonificación aplicada a este detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descuento pronto pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, default=0): 1=comprobante de egreso ya generado; 0=aún no se ha registrado el movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'GeneratedVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo especifica si ya se genero el comprobante de egreso con este detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'GeneratedVoucher';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'GeneratedVoucher';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del comprobante de egreso, asiento contable o transacción de tesorería generado al pagar este detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante de egreso que se genero con este detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto (VARCHAR 300) que documenta la razón, motivo, glosa o impedimento por el cual no se pudo procesar el pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Describe el problema del porque no se puedo pagar la factura', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: 1=detalle ya pagado y con comprobante de egreso generado; 0=pendiente de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Paid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el registro ya fue pagado y por consiguiente se ha generado comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Paid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Paid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del concepto de pago (canon, factura, cuota, interés, etc.) asociado a este detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'PaymentConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del concepto de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'PaymentConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'PaymentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje (DECIMAL 5,2) del valor total de la factura o cuota que representa este pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AmountPercent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Porcentaje', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AmountPercent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AmountPercent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) efectivamente pagado en esta cuota o detalle del cronograma', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AmountPaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor pagado a la cuota', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AmountPaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AmountPaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cuota, dividendo o plazo específico de la factura que se está cancelando', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableShareId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de las coutas de las facturas', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableShareId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableShareId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la factura, obligación o cuenta por pagar matriz a la que se abona este detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la factura de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza (TINYINT: 0=débito, 1=crédito) del concepto de egreso, determina el sentido contable del pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza del concepto de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cuenta contable principal (Mayor) que registra el movimiento financiero del pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la cuenta contable asociada a la a la linea de distribucion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del concepto de egreso (gasto, partida contable) asignado a este detalle de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'ExpenseConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del concepto de egreso asociada a la linea de distribucion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'ExpenseConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'ExpenseConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la línea de distribución asociada al proveedor y su asignación presupuestal', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'DistributionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la line a de distribucion asociada al proveedor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'DistributionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'DistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera o maestro de programación de pagos que agrupa este detalle', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'SchedulePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la programacion de pagos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'SchedulePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'SchedulePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del tercero (persona natural o jurídica) relacionado a la factura y obligación de pago', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del tercero relacionado a la factura', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del proveedor, acreedor o beneficiario asociado a la factura y su pago programado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'proveedor asociado a la factura', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'SupplierId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de cada detalle de programación de pago en la tabla SchedulePaymentDetail', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada línea de pago dentro de un cronograma de pagos a proveedores o terceros. Registra los montos, conceptos contables, cuentas por pagar y el estado de pago de cada ítem programado en tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetail';
