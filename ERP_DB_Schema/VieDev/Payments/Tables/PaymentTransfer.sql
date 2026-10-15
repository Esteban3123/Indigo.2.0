CREATE TABLE [Payments].[PaymentTransfer] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [DocumentDate]     DATETIME      NOT NULL,
    [ThirdPartyId]     INT           NOT NULL,
    [SupplierId]       INT           NOT NULL,
    [OperatingUnitId]  INT           NOT NULL,
    [TransferType]     TINYINT       NOT NULL,
    [AdvancePaymentId] INT           NOT NULL,
    [MainAccountId]    INT           NOT NULL,
    [CostCenterId]     INT           NULL,
    [Observations]     VARCHAR (500) NOT NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [ConfirmationUser] VARCHAR (20)  NULL,
    [ConfirmationDate] DATETIME      NULL,
    [AnnulmentUser]    VARCHAR (20)  NULL,
    [AnnulmentDate]    DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_PaymentTransfer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentTransfer_AdvancePayments] FOREIGN KEY ([AdvancePaymentId]) REFERENCES [Payments].[AdvancePayments] ([Id]),
    CONSTRAINT [FK_PaymentTransfer_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PaymentTransfer_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PaymentTransfer_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_PaymentTransfer_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_PaymentTransfer_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payments].[PaymentTransfer] NOCHECK CONSTRAINT [FK_PaymentTransfer_AdvancePayments];


GO
ALTER TABLE [Payments].[PaymentTransfer] NOCHECK CONSTRAINT [FK_PaymentTransfer_CostCenter];


GO
ALTER TABLE [Payments].[PaymentTransfer] NOCHECK CONSTRAINT [FK_PaymentTransfer_MainAccounts];


GO
ALTER TABLE [Payments].[PaymentTransfer] NOCHECK CONSTRAINT [FK_PaymentTransfer_OperatingUnit];


GO
ALTER TABLE [Payments].[PaymentTransfer] NOCHECK CONSTRAINT [FK_PaymentTransfer_Supplier];


GO
ALTER TABLE [Payments].[PaymentTransfer] NOCHECK CONSTRAINT [FK_PaymentTransfer_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del traslado  1 - Mismo Proveedor  2 - Diferente Proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentTransfer', @level2type = N'COLUMN', @level2name = N'TransferType';

