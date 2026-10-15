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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se generó el reconocimiento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que creó', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1 - Reconocido  2 - Reversado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha del co provande de la reversión', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDateReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'consecutivode  comprobante contable generado de la reversion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante contable de la reversion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeReverseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del comprobante contable generado de la reversion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherReverseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de de la reversión', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'VoucherDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Total por el que se realiza el comprobante contable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'TotalCareGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa a la que corresponde el grupo de atencion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'OperativeUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del comprobante contable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de comprobante', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del comprobante contable que se genera', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'JournalVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo de atención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'llave autonumérica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognition', @level2type = N'COLUMN', @level2name = N'Id';

