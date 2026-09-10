CREATE TABLE [Treasury].[CashReceiptDetails] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCashReceipt]                 INT             NOT NULL,
    [IdThirdParty]                  INT             NOT NULL,
    [IdMainAccount]                 INT             NOT NULL,
    [IdCostCenter]                  INT             NULL,
    [Nature]                        TINYINT         NOT NULL,
    [IdCashReceiptConcept]          INT             NOT NULL,
    [CashReceiptConceptAffectation] TINYINT         NOT NULL,
    [Value]                         DECIMAL (18, 2) NOT NULL,
    [IdRetentionConcept]            INT             NULL,
    [PercentageRetention]           DECIMAL (5, 2)  NULL,
    [BillingValue]                  DECIMAL (18, 2) NULL,
    [BaseValue]                     DECIMAL (18, 2) NULL,
    [CardNumber]                    VARCHAR (30)    NULL,
    [Detail]                        VARCHAR (MAX)   NULL,
    [IdCashFlowConcept]             INT             NULL,
    [CurrencyId]                    INT             NULL,
    [TRM]                           NUMERIC (20, 5) NULL,
    [ValueInCurrencyHeader]         NUMERIC (20, 5) CONSTRAINT [DF__CashRecei__Value__026E9D45] DEFAULT ((0)) NOT NULL,
    [ThirdPartyBeneficiaryId]       INT             NULL,
    CONSTRAINT [PK_CashReceiptDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptDetails_CashReceiptConcepts] FOREIGN KEY ([IdCashReceiptConcept]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_CashReceipts] FOREIGN KEY ([IdCashReceipt]) REFERENCES [Treasury].[CashReceipts] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_IdCashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_MainAccounts] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_RetentionConcepts] FOREIGN KEY ([IdRetentionConcept]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_CashReceiptDetails_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);

GO
CREATE NONCLUSTERED INDEX [IX_CashReceiptDetails_IdCashReceipt]
    ON [Treasury].[CashReceiptDetails] ([IdCashReceipt] ASC);




GO



GO



GO



GO



GO



GO





GO



GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero beneficiario (proveedor, paciente, acreedor). Condicional según tipo de concepto de recibo de caja. FK a [Common].[ThirdParty].', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'ThirdPartyBeneficiaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica el tercero beneficiario, este dato depende del tipo de concepto de recibo de caja seleccionado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'ThirdPartyBeneficiaryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'ThirdPartyBeneficiaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la línea convertido a moneda de cabecera del recibo. NUMERIC(20,5). Facilita análisis en moneda local consolidada.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor convertido a la moneda de la cabecera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa representativa del mercado (TRM) vigente al momento de conversión de moneda extranjera. NUMERIC(20,5). Usado para cálculo de ValueInCurrencyHeader.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'TRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor del trm de la moneda al que fue convertido el valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'TRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'TRM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda de la transacción de caja. FK a [Common].[Currency]. Permite multicurrencia en recibos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de flujo de efectivo. FK a [Treasury].[CashFlowConcept]. Clasifica impacto en tesorería (ingreso/egreso).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o detalle que genera automáticamente el sistema hacia el asiento contable. VARCHAR(MAX). Documenta razón de movimiento para auditoría.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el detalle o descripcion que se debe ir al comprobante contable, por lo general este campo se calcula de forma automatica por el sistema', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de tarjeta de crédito/débito usado en el pago. VARCHAR(30). Sensitivo: datos PII enmascarados en consultas.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'CardNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de la tarjeta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'CardNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'CardNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base imponible sobre el cual se calcula el porcentaje de retención. DECIMAL(18,2). Generalmente sin descuentos previos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor base al que se le aplica el porcentaje de la retencion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de la factura antes de descuentos, retenciones e IVA. DECIMAL(18,2). Base para cálculo de afectaciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la factura sin descuentos, retenciones e IVA', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención aplicado (ej: 2%, 3%). DECIMAL(5,2). Determina monto retenido sobre BaseValue.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'PercentageRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'porcentaje de la retencion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'PercentageRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'PercentageRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención. FK a [GeneralLedger].[RetentionConcepts]. Clasifica tipo: impuesto, garantía, descuento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del concepto de retencion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto neto de la línea del recibo de caja. DECIMAL(18,2). Valor principal contabilizado en cuenta.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de afectación: 1=Ninguno, 2=Cancelación/Abonos CxC, 3=Reintegro Anticipos Proveedores. TINYINT. Define impacto en cuentas por cobrar/pagar.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'CashReceiptConceptAffectation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afectación   1- Ninguno   2- Cancelacion / Abonos Facturas CxC  3- Reintegro de Anticipos a Proveedores', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'CashReceiptConceptAffectation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'CashReceiptConceptAffectation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto del recibo de caja. FK a [Treasury].[CashReceiptConcepts]. Determina naturaleza y tratamiento contable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id concepto recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza contable: 1=Débito, 2=Crédito. TINYINT. Define impacto en ecuación contable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza contable: 1 = Débito, 2 = Crédito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo. FK a [Payroll].[CostCenter]. Opcional. Departamento/área responsable del movimiento.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal. FK a [GeneralLedger].[MainAccounts]. Donde se registra el movimiento en libro mayor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cuenta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (proveedor, cliente, acreedor, paciente). FK a [Common].[ThirdParty]. Contrapartida de la transacción.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera del recibo de caja. FK a [Treasury].[CashReceipts]. Agrupa múltiples líneas de detalle.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCashReceipt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCashReceipt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCashReceipt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle del recibo de caja. INT IDENTITY. Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de líneas de un recibo de caja: cada fila representa un concepto de ingreso o afectación contable dentro de un recibo de caja, indicando el tercero, la cuenta contable, el centro de costos, el valor, la retención aplicada y la moneda.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails';
