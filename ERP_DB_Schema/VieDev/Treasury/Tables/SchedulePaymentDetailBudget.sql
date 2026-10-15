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

