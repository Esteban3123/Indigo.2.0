CREATE TABLE [Inventory].[EntranceVoucherOtherDeduction] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EntranceVoucherId]           INT          NOT NULL,
    [OtherWithholdingDeductionId] INT          NOT NULL,
    [Type]                        TINYINT      NOT NULL,
    [Value]                       NUMERIC (18) NOT NULL,
    [ValueOutstanding]            NUMERIC (18) CONSTRAINT [DF_EntranceVoucherOtherDeduction_ValueOutstanding] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EntranceVoucherOtherDeduction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntranceVoucherOtherDeduction_EntranceVoucher] FOREIGN KEY ([EntranceVoucherId]) REFERENCES [Inventory].[EntranceVoucher] ([Id]),
    CONSTRAINT [FK_EntranceVoucherOtherDeduction_OtherWithholdingDeduction] FOREIGN KEY ([OtherWithholdingDeductionId]) REFERENCES [Inventory].[OtherWithholdingDeduction] ([Id])
);


GO
ALTER TABLE [Inventory].[EntranceVoucherOtherDeduction] NOCHECK CONSTRAINT [FK_EntranceVoucherOtherDeduction_EntranceVoucher];


GO
ALTER TABLE [Inventory].[EntranceVoucherOtherDeduction] NOCHECK CONSTRAINT [FK_EntranceVoucherOtherDeduction_OtherWithholdingDeduction];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor excepcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'ValueOutstanding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la deduccion o de la retencion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del registro  1 - Retención  2 - Deducción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la deduccion o de la retencion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'OtherWithholdingDeductionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'Id';

