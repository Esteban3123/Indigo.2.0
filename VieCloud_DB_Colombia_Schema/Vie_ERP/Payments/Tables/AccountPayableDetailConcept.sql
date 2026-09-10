CREATE TABLE [Payments].[AccountPayableDetailConcept] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayable]        INT             NOT NULL,
    [IdConceptAccountPayable] INT             NOT NULL,
    [IdAccount]               INT             NOT NULL,
    [IdThirdParty]            INT             NOT NULL,
    [IdCostCenter]            INT             NULL,
    [Nature]                  TINYINT         NOT NULL,
    [BaseValue]               DECIMAL (18, 2) CONSTRAINT [DF_AccountPayableDetailConcept_BaseValue] DEFAULT ((0)) NOT NULL,
    [BillingValue]            DECIMAL (18, 2) NULL,
    [Value]                   DECIMAL (18, 2) NOT NULL,
    [IdRetentionConcept]      INT             NULL,
    [Percentage]              DECIMAL (6, 3)  NULL,
    [Detail]                  VARCHAR (500)   NULL,
    [DeferredCausation]       BIT             NULL,
    [IsDirectCost]            BIT             CONSTRAINT [DF_AccountPayableDetailConcept_IsDirectCost] DEFAULT ((0)) NOT NULL,
    [RateIva]                 INT             NULL,
    [IvaValue]                DECIMAL (18, 2) NULL,
    [TotalConcept]            DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_AccountsPayableD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableDetailConcept_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_AccountPayableDetailConcept_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountPayableDetailConcept_GeneralLedgerIVA] FOREIGN KEY ([RateIva]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_AccountPayableDetailConcept_PaymentsConcept] FOREIGN KEY ([IdConceptAccountPayable]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_AccountPayableDetailConcept_PUC] FOREIGN KEY ([IdAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountPayableDetailConcept_RetentionConcept] FOREIGN KEY ([IdRetentionConcept]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountPayableDetailConcept_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_AccountPayable];


GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_PaymentsConcept];


GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_PUC];




GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_AccountPayable];


GO



GO



GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_PaymentsConcept];


GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_PUC];


GO



GO





GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_AccountPayable];


GO



GO



GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_PaymentsConcept];


GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_PUC];


GO



GO



GO
CREATE NONCLUSTERED INDEX [IDX_AccountPayableDetailConcept_IdConceptAccountPayable]
    ON [Payments].[AccountPayableDetailConcept]([IdConceptAccountPayable] ASC)
    INCLUDE([IdAccountPayable], [IdAccount], [IdThirdParty], [IdCostCenter], [Nature], [BaseValue], [BillingValue], [Value], [IdRetentionConcept], [Percentage], [Detail], [DeferredCausation], [IsDirectCost]);


GO
ALTER INDEX [IDX_AccountPayableDetailConcept_IdConceptAccountPayable]
    ON [Payments].[AccountPayableDetailConcept] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_AccountPayableDetailConcept_IdAccountPayable_IdRetentionConcept]
    ON [Payments].[AccountPayableDetailConcept]([IdAccountPayable] ASC, [IdRetentionConcept] ASC)
    INCLUDE([IdAccount], [BaseValue], [RateIva], [IvaValue]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total del concepto (DECIMAL 18,2): suma de valor base, IVA y retenciones aplicadas al detalle de cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'TotalConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del concepto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'TotalConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'TotalConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del IVA (DECIMAL 18,2): impuesto al valor agregado calculado sobre el concepto, según tasa aplicable.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del IVA', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa del IVA (INT, FK→GeneralLedgerIVA): porcentaje de impuesto al valor agregado configurado en el plan de cuentas.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'RateIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tasa del IVA', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'RateIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'RateIva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de costo directo (BIT, default 0): especifica si el detalle fue agregado mediante distribución automática de elementos de costo.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IsDirectCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si el detalle fue agregado con la información de distribución de elementos del costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IsDirectCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IsDirectCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causación diferida (BIT): indica si el concepto aplica causación diferida (contabilización en período diferente al de origen).', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'DeferredCausation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el concepto maneja Causacion Diferida', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'DeferredCausation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'DeferredCausation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del concepto (VARCHAR 500): descripción o glosa adicional del concepto contable en la cuenta por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención (DECIMAL 6,3): tasa porcentual aplicada para descuentos, retenciones o aportes sobre el valor base.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de retencion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del concepto de retención (INT, FK→RetentionConcepts): referencia al concepto de retención, descuento o aporte aplicado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de retención', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor neto (DECIMAL 18,2): monto final del concepto después de aplicar ajustes y retenciones.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor facturado (DECIMAL 18,2, nullable): monto original registrado en factura o documento de origen del concepto.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor facturado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'BillingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base (DECIMAL 18,2, default 0): monto antes de impuestos, retenciones o descuentos en el detalle.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor base', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza contable (TINYINT): tipo de movimiento, Débito (1) o Crédito (2), que define impacto en cuenta.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza (Débito = 1, Crédito = 2)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del centro de costo (INT, FK→CostCenter, nullable): referencia a unidad funcional o centro de costo para imputación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero (INT, FK→ThirdParty): identificación del proveedor, contratista o entidad vinculada a la cuentas por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id cuenta contable (INT, FK→MainAccounts): referencia a cuenta del plan de cuentas (PUC) para registro contable.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id concepto de cuenta por pagar (INT, FK→AccountPayableConcepts): tipo de concepto configurado (honorarios, materiales, servicios, etc.).', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdConceptAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Concepto de cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdConceptAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdConceptAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id cuenta por pagar (INT, FK→AccountPayable): referencia al documento maestro de pasivo o factura por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del registro (INT, PK): identificador único del detalle de concepto en cuenta por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de conceptos contables asociados a cada cuenta por pagar: registra los ítems individuales (conceptos, valores, retenciones e IVA) que componen una obligación de pago a proveedores o terceros, permitiendo el desglose contable por cuenta, centro de costo y naturaleza del movimiento.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept';
