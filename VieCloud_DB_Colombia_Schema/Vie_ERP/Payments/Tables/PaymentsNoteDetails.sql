CREATE TABLE [Payments].[PaymentsNoteDetails] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdPaymentsNote]               INT             NOT NULL,
    [IdAccountPayableConceptNotes] INT             NOT NULL,
    [IdAccount]                    INT             NOT NULL,
    [IdThirdParty]                 INT             NOT NULL,
    [IdCostCenter]                 INT             NULL,
    [BaseValue]                    DECIMAL (18, 2) NULL,
    [BillingValue]                 DECIMAL (18, 2) NULL,
    [Value]                        DECIMAL (18, 2) NOT NULL,
    [IdRetentionConcept]           INT             NULL,
    [Nature]                       TINYINT         NOT NULL,
    [Comments]                     VARCHAR (500)   NULL,
    [Percentage]                   DECIMAL (6, 3)  NULL,
    [DiscountableIVA]              BIT             NULL,
    [IdGeneralLedgerIVA]           INT             NULL,
    [IVAValue]                     DECIMAL (18, 2) NULL,
    [TotalConceptValue]            DECIMAL (18, 2) NULL,
    [TaxRegistration]              TINYINT         NULL,
    CONSTRAINT [PK_PaymentsNoteDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentsNoteDetail_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PaymentsNoteDetail_MainAccount] FOREIGN KEY ([IdAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PaymentsNoteDetail_PaymentsNote] FOREIGN KEY ([IdPaymentsNote]) REFERENCES [Payments].[PaymentNotes] ([Id]),
    CONSTRAINT [FK_PaymentsNoteDetail_RetentionConcept] FOREIGN KEY ([IdRetentionConcept]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_PaymentsNoteDetail_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_PaymentsNoteDetails_AccountPayableConceptNotes] FOREIGN KEY ([IdAccountPayableConceptNotes]) REFERENCES [Payments].[AccountPayableConceptNotes] ([Id]),
    CONSTRAINT [FK_PaymentsNoteDetails_IdGeneralLedgerIVA] FOREIGN KEY ([IdGeneralLedgerIVA]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro fiscal (TINYINT): clasificación tributaria o estado de registro para propósitos de retención y cumplimiento normativo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'TaxRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro fiscal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'TaxRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del concepto (DECIMAL 18,2): monto acumulado incluye base, IVA y descuentos aplicables al renglón contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'TotalConceptValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor total del concepto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'TotalConceptValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'TotalConceptValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del IVA según la tarifa (DECIMAL 18,2): impuesto al valor agregado calculado sobre la base imponible del detalle de pago', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IVAValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del IVA segun la tarifa', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IVAValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IVAValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la tarifa de IVA en tabla GeneralLedgerIVA (INT, FK): referencia a concepto impositivo, regla de cálculo y cuenta contable asociada', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdGeneralLedgerIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tarifa de IVA en la tabla GeneralLedgerIVA', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdGeneralLedgerIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdGeneralLedgerIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IVA Descontable (BIT): bandera 1=Sí, 0=No; indica si el impuesto puede utilizarse como crédito fiscal contra obligaciones tributarias', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'DiscountableIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IVA Descontable: 1-SI ; 0-No', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'DiscountableIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'DiscountableIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje (DECIMAL 6,3): tasa o proporción aplicada al cálculo de retención, IVA o descuento en el detalle de la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios (VARCHAR 500): notas o justificación libre del usuario sobre razones, ajustes o particularidades del renglón de pago', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza de la Nota (TINYINT): 1=Débito, 2=Crédito; indica el signo contable del movimiento (afecta cuentas por pagar/terceros)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza de la Nota 1. Debito 2. Credito', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del concepto de retención (INT, FK): referencia a tipo de descuento legal (impuesto renta, IVA, CREE, etc.) aplicado al pago', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de retencion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del detalle (DECIMAL 18,2): monto neto del renglón después de retenciones pero antes de considerar IVA descontable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del detalle', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor facturado (DECIMAL 18,2): importe original cobrado o facturado por el tercero antes de ajustes o descuentos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor facturado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base (DECIMAL 18,2): monto sobre el cual se calculan retenciones, IVA y otros conceptos en el detalle de pago', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor base', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del centro de costo (INT, FK nullable): asignación contable del gasto a unidad funcional, área o línea de negocio', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero (INT, FK): identificación única del proveedor, acreedor o beneficiario del pago (referencia a Common.ThirdParty)', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta contable (INT, FK): referencia a MainAccounts; cuenta donde se registra el movimiento contable del detalle', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del concepto de nota (INT, FK): referencia al tipo de línea contable (gasto, anticipo, ajuste) en la nota débito/crédito', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdAccountPayableConceptNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdAccountPayableConceptNotes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdAccountPayableConceptNotes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera nota débito/crédito (INT, FK): relación con PaymentNotes; agrupa detalles de un mismo documento de ajuste', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdPaymentsNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera notas debito/credito', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdPaymentsNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'IdPaymentsNote';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del registro (INT PK): identificador único de cada línea detalle en la nota de pago, clave primaria para auditoría y trazabilidad', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los conceptos contables asociados a notas de pago (notas débito o crédito sobre pagos a proveedores o terceros). Registra cada línea de una nota de pago con su cuenta contable, tercero, valor, retenciones, IVA y centro de costos correspondiente.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsNoteDetails';
