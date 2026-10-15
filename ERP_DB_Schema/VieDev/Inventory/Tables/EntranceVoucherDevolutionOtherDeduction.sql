CREATE TABLE [Inventory].[EntranceVoucherDevolutionOtherDeduction] (
    [Id]                              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EntranceVoucherDevolutionId]     INT          NOT NULL,
    [EntranceVoucherOtherDeductionId] INT          NOT NULL,
    [Type]                            TINYINT      NOT NULL,
    [Value]                           NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_EntranceVoucherDevolutionOtherDeduction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntranceVoucherDevolutionOtherDeduction_EntranceVoucherDevolution] FOREIGN KEY ([EntranceVoucherDevolutionId]) REFERENCES [Inventory].[EntranceVoucherDevolution] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolutionOtherDeduction_OtherWithholdingDeduction] FOREIGN KEY ([EntranceVoucherOtherDeductionId]) REFERENCES [Inventory].[EntranceVoucherOtherDeduction] ([Id])
);


GO
ALTER TABLE [Inventory].[EntranceVoucherDevolutionOtherDeduction] NOCHECK CONSTRAINT [FK_EntranceVoucherDevolutionOtherDeduction_EntranceVoucherDevolution];


GO
ALTER TABLE [Inventory].[EntranceVoucherDevolutionOtherDeduction] NOCHECK CONSTRAINT [FK_EntranceVoucherDevolutionOtherDeduction_OtherWithholdingDeduction];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del registro  1 - Retención  2 - Deducción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'Type';

