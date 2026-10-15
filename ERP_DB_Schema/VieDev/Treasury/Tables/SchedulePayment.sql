CREATE TABLE [Treasury].[SchedulePayment] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20) NOT NULL,
    [ScheduledDate]       DATE         NOT NULL,
    [EntityBankAccountId] INT          NULL,
    [CostCenterId]        INT          NULL,
    [PaymentMethod]       TINYINT      NULL,
    [CheckId]             INT          NULL,
    [NumberNote]          VARCHAR (50) NULL,
    [TaxByMil]            BIT          CONSTRAINT [DF_SchedulePayment_TaxByMil] DEFAULT ((0)) NULL,
    [OperativeUnitId]     INT          NOT NULL,
    [Status]              TINYINT      NOT NULL,
    [CreationUser]        VARCHAR (20) NOT NULL,
    [CreationDate]        DATETIME     NOT NULL,
    [ModificationUser]    VARCHAR (20) NULL,
    [ModificationDate]    DATETIME     NULL,
    [ConfirmationUser]    VARCHAR (20) NULL,
    [ConfirmationDate]    DATETIME     NULL,
    [AnnulmentUser]       VARCHAR (20) NULL,
    [AnnulmentDate]       DATETIME     NULL,
    [PartialPaymentUser]  VARCHAR (20) NULL,
    [PartialPaymentDate]  DATETIME     NULL,
    [FullPaymentUser]     VARCHAR (20) NULL,
    [FullPaymentDate]     DATETIME     NULL,
    [TimeStamp]           ROWVERSION   NOT NULL,
    [PaymentDate]         DATETIME     CONSTRAINT [DF__ScheduleP__Payme__6A8139C4] DEFAULT ('2022-01-01') NOT NULL,
    CONSTRAINT [PK_SchedulePayment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SchedulePayment_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_SchedulePayment_EntityBankAccounts] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_SchedulePayment_OperatingUnit] FOREIGN KEY ([OperativeUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Treasury].[SchedulePayment] NOCHECK CONSTRAINT [FK_SchedulePayment_CostCenter];


GO
ALTER TABLE [Treasury].[SchedulePayment] NOCHECK CONSTRAINT [FK_SchedulePayment_EntityBankAccounts];


GO
ALTER TABLE [Treasury].[SchedulePayment] NOCHECK CONSTRAINT [FK_SchedulePayment_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la programacion de pagos  1 - Registrado  2 - Confirmado  3 - Anulado  4 - Pagado Parcial  5 - Pagado Total', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SchedulePayment', @level2type = N'COLUMN', @level2name = N'Status';

