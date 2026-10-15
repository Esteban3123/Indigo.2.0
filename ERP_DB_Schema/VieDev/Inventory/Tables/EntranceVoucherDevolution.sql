CREATE TABLE [Inventory].[EntranceVoucherDevolution] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)    NOT NULL,
    [OperatingUnitId]      INT             NOT NULL,
    [DocumentDate]         DATETIME        NOT NULL,
    [WarehouseId]          INT             NOT NULL,
    [EntranceVoucherId]    INT             NOT NULL,
    [Description]          VARCHAR (MAX)   NULL,
    [FreightValue]         DECIMAL (18, 2) NOT NULL,
    [FreightIVAPercentage] NUMERIC (5, 2)  NOT NULL,
    [FreightIVAValue]      DECIMAL (18, 2) NOT NULL,
    [Value]                DECIMAL (18, 2) NOT NULL,
    [ValueDiscount]        DECIMAL (18, 2) NOT NULL,
    [ValueTax]             DECIMAL (18, 2) NOT NULL,
    [WithholdingTax]       DECIMAL (18, 2) NOT NULL,
    [WithholdingICA]       DECIMAL (18, 2) NOT NULL,
    [RetentionSource]      DECIMAL (18, 2) NOT NULL,
    [RetentionOther]       DECIMAL (18, 2) NOT NULL,
    [DeductionOther]       DECIMAL (18, 2) NOT NULL,
    [DistrictTax]          DECIMAL (18, 2) NOT NULL,
    [TotalValue]           DECIMAL (18, 2) NOT NULL,
    [PaymentNoteId]        INT             NULL,
    [Status]               TINYINT         NOT NULL,
    [CreationUser]         VARCHAR (20)    NOT NULL,
    [CreationDate]         DATETIME        NOT NULL,
    [ModificationUser]     VARCHAR (20)    NULL,
    [ModificationDate]     DATETIME        NULL,
    [ConfirmationUser]     VARCHAR (20)    NULL,
    [ConfirmationDate]     DATETIME        NULL,
    [AnnulmentUser]        VARCHAR (20)    NULL,
    [AnnulmentDate]        DATETIME        NULL,
    [TimeStamp]            ROWVERSION      NOT NULL,
    CONSTRAINT [PK_EntranceVoucherDevolution__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntranceVoucherDevolution_EntranceVoucher] FOREIGN KEY ([EntranceVoucherId]) REFERENCES [Inventory].[EntranceVoucher] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolution_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolution_PaymentNotes] FOREIGN KEY ([PaymentNoteId]) REFERENCES [Payments].[PaymentNotes] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolution_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[EntranceVoucherDevolution] NOCHECK CONSTRAINT [FK_EntranceVoucherDevolution_EntranceVoucher];


GO
ALTER TABLE [Inventory].[EntranceVoucherDevolution] NOCHECK CONSTRAINT [FK_EntranceVoucherDevolution_OperatingUnit];


GO
ALTER TABLE [Inventory].[EntranceVoucherDevolution] NOCHECK CONSTRAINT [FK_EntranceVoucherDevolution_PaymentNotes];


GO
ALTER TABLE [Inventory].[EntranceVoucherDevolution] NOCHECK CONSTRAINT [FK_EntranceVoucherDevolution_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el valor de los descuentos distritales del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolution', @level2type = N'COLUMN', @level2name = N'DistrictTax';

