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
ALTER TABLE [Treasury].[SchedulePaymentDetailBudget] NOCHECK CONSTRAINT [FK_SchedulePaymentDetailBudget_ObligationDetail];


GO
ALTER TABLE [Treasury].[SchedulePaymentDetailBudget] NOCHECK CONSTRAINT [FK_SchedulePaymentDetailBudget_SchedulePaymentDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor con el cual se pagará la obligación en el detalle seleccionado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle de la obligacion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle de pago de factura', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'SchedulePaymentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePaymentDetailBudget', @level2type = N'COLUMN', @level2name = N'Id';

