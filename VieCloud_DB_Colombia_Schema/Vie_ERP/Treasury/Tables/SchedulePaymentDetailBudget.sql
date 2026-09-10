CREATE TABLE [Treasury].[SchedulePaymentDetailBudget] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchedulePaymentDetailId] INT             NOT NULL,
    [ObligationDetailId]      INT             NOT NULL,
    [Value]                   DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_SchedulePaymentDetailBudget__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SchedulePaymentDetailBudget_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id]),
    CONSTRAINT [FK_SchedulePaymentDetailBudget_SchedulePaymentDetail] FOREIGN KEY ([SchedulePaymentDetailId]) REFERENCES [Treasury].[SchedulePaymentDetail] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) asignado para pagar la obligación presupuestal en el detalle seleccionado; monto de pago, cuantía, importe financiero.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor con el cual se pagará la obligación en el detalle seleccionado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de obligación presupuestal vinculado; referencia a Budget.ObligationDetail para trazabilidad de gasto, compromiso, obligación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle de la obligacion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle del cronograma de pago de factura; referencia a Treasury.SchedulePaymentDetail que vincula el pago programado, desembolso, giro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'SchedulePaymentDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle de pago de factura', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'SchedulePaymentDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'SchedulePaymentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de asignación presupuestal en el cronograma de pago; clave primaria para auditoría y relación entre obligación y pago.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle presupuestal de los pagos programados: registra la distribución de cada ítem de un cronograma de pagos contra las obligaciones presupuestales correspondientes, indicando el valor asignado a cada obligación dentro del plan de pagos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget';
