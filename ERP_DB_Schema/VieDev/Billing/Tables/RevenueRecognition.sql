CREATE TABLE [Billing].[RevenueRecognition] (
    [Id]                               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]                      INT          NOT NULL,
    [JournalVoucherId]                 INT          NULL,
    [JournalVoucherTypeId]             INT          NOT NULL,
    [JournalVoucherConsecutive]        BIGINT       NULL,
    [OperativeUnitId]                  INT          CONSTRAINT [DF_RevenueRecognition_OperativeUnitId] DEFAULT ((14)) NOT NULL,
    [TotalCareGroup]                   DECIMAL (18) CONSTRAINT [DF_RevenueRecognition_TotalCareGroup] DEFAULT ((0)) NOT NULL,
    [VoucherDate]                      DATETIME     NOT NULL,
    [JournalVoucherReverseId]          INT          NULL,
    [JournalVoucherTypeReverseId]      INT          NULL,
    [JournalVoucherReverseConsecutive] BIGINT       NULL,
    [VoucherDateReverse]               DATETIME     NULL,
    [State]                            TINYINT      CONSTRAINT [DF_RevenueRecognition_State] DEFAULT ((1)) NOT NULL,
    [CreationUser]                     VARCHAR (20) NOT NULL,
    [CreationDate]                     DATETIME     NOT NULL,
    CONSTRAINT [PK_RevenueRecognition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RevenueRecognition_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_RevenueRecognition_JournalVouchers] FOREIGN KEY ([JournalVoucherId]) REFERENCES [GeneralLedger].[JournalVouchers] ([Id]),
    CONSTRAINT [FK_RevenueRecognition_JournalVouchers1] FOREIGN KEY ([JournalVoucherReverseId]) REFERENCES [GeneralLedger].[JournalVouchers] ([Id]),
    CONSTRAINT [FK_RevenueRecognition_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_RevenueRecognition_JournalVoucherTypes1] FOREIGN KEY ([JournalVoucherTypeReverseId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_RevenueRecognition_OperatingUnit] FOREIGN KEY ([OperativeUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Billing].[RevenueRecognition] NOCHECK CONSTRAINT [FK_RevenueRecognition_CareGroup];


GO
ALTER TABLE [Billing].[RevenueRecognition] NOCHECK CONSTRAINT [FK_RevenueRecognition_JournalVouchers];


GO
ALTER TABLE [Billing].[RevenueRecognition] NOCHECK CONSTRAINT [FK_RevenueRecognition_JournalVouchers1];


GO
ALTER TABLE [Billing].[RevenueRecognition] NOCHECK CONSTRAINT [FK_RevenueRecognition_JournalVoucherTypes];


GO
ALTER TABLE [Billing].[RevenueRecognition] NOCHECK CONSTRAINT [FK_RevenueRecognition_JournalVoucherTypes1];


GO
ALTER TABLE [Billing].[RevenueRecognition] NOCHECK CONSTRAINT [FK_RevenueRecognition_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1 - Reconocido  2 - Reversado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'State';

