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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18) a devolver por retención o deducción en comprobante de entrada; importe que se reintegra al proveedor o acreedor.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se le va a devolver a la deduccion o a la retencion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de descuento: 1=Retención (descuento obligatorio por ley), 2=Deducción (descuento comercial o contractual); TINYINT.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del registro  1 - Retención  2 - Deducción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la deducción o retención original en tabla EntranceVoucherOtherDeduction; vincula a la deducción matriz que se devuelve.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'EntranceVoucherOtherDeductionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de otra deducción de comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'EntranceVoucherOtherDeductionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'EntranceVoucherOtherDeductionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del comprobante de devolución de entrada (recepción/ingreso); relaciona con EntranceVoucherDevolution para rastrear la devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del comprobante de devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de este registro de deducción o retención devuelta en el comprobante de entrada; clave primaria.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuentos u otras deducciones aplicadas a las devoluciones de comprobantes de entrada (vales de entrada devueltos) en el módulo de inventario. Relaciona cada devolución de vale de entrada con el tipo y valor de la deducción adicional correspondiente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionOtherDeduction';
