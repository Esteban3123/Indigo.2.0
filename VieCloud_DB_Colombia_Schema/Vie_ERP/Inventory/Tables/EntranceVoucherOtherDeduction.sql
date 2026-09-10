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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor pendiente o excedente (NUMERIC 18,0) de la deducción/retención no procesada; saldo restante tras pagos o liquidaciones parciales (default=0).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'ValueOutstanding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor excepcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'ValueOutstanding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'ValueOutstanding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18,0) de la deducción o retención aplicada al comprobante de entrada; monto total del descuento, retención o ajuste.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la deduccion o de la retencion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del registro: 1=Retención (descuento retenido), 2=Deducción (ajuste o descuento aplicado); TINYINT que especifica el tipo de operación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del registro  1 - Retención  2 - Deducción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la deducción o retención aplicada (FK a Inventory.OtherWithholdingDeduction); código del concepto de descuento, retención o ajuste.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'OtherWithholdingDeductionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la deduccion o de la retencion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'OtherWithholdingDeductionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'OtherWithholdingDeductionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del comprobante de entrada de inventario (FK a Inventory.EntranceVoucher); referencia al recibo o documento de ingreso de mercancía.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'EntranceVoucherId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de deducción/retención en comprobante de entrada de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de deducciones y retenciones adicionales aplicadas a los comprobantes de entrada de inventario (vales de ingreso). Permite asociar múltiples descuentos o retenciones a un mismo ingreso de mercancía, controlando el valor descontado y el saldo pendiente por deducir.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherOtherDeduction';
