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
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_CashReceiptConcepts];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_CashReceipts];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_CostCenter];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_Currency];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_IdCashFlowConcept];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_MainAccounts];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_RetentionConcepts];


GO
ALTER TABLE [Treasury].[CashReceiptDetails] NOCHECK CONSTRAINT [FK_CashReceiptDetails_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica el tercero beneficiario, este dato depende del tipo de concepto de recibo de caja seleccionado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'ThirdPartyBeneficiaryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor convertido a la moneda de la cabecera', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor del trm de la moneda al que fue convertido el valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'TRM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la moneda de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el detalle o descripcion que se debe ir al comprobante contable, por lo general este campo se calcula de forma automatica por el sistema', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de la tarjeta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'CardNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor base al que se le aplica el porcentaje de la retencion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la factura sin descuentos, retenciones e IVA', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'porcentaje de la retencion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'PercentageRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del concepto de retencion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afectación   1- Ninguno   2- Cancelacion / Abonos Facturas CxC  3- Reintegro de Anticipos a Proveedores', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'CashReceiptConceptAffectation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id concepto recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza contable: 1 = Débito, 2 = Crédito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cuenta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'IdCashReceipt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetails', @level2type = N'COLUMN', @level2name = N'Id';

