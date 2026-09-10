CREATE TABLE [Treasury].[VoucherTransactionDetails] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVoucherTransaction]  INT             NOT NULL,
    [IdEntityBankAccount]   INT             NULL,
    [CashRegisterId]        INT             NULL,
    [IdThirdParty]          INT             NULL,
    [IdExpenseConcept]      INT             NULL,
    [IdMainAccount]         INT             NOT NULL,
    [Nature]                TINYINT         NOT NULL,
    [IdCostCenter]          INT             NULL,
    [Value]                 DECIMAL (18, 5) NULL,
    [IdRetentionConcept]    INT             NULL,
    [BaseValue]             DECIMAL (18, 2) NULL,
    [BillingValue]          DECIMAL (18, 2) NULL,
    [PercentRetention]      DECIMAL (5, 2)  NULL,
    [Detail]                VARCHAR (MAX)   NULL,
    [Observation]           VARCHAR (MAX)   NULL,
    [IdCashFlowConcept]     INT             NULL,
    [discountableIVA]       BIT             NULL,
    [IdGeneralLedgerIVA]    INT             NULL,
    [ValueIVA]              DECIMAL (18, 2) NULL,
    [TotalConcept]          DECIMAL (18, 5) NULL,
    [SupplierBankAccountId] INT             NULL,
    [TaxRegistration]       TINYINT         NULL,
    [EconomicActivityId]    INT             NULL,
    CONSTRAINT [PK_VoucherTransactionD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_VoucherTransactionD_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_VoucherTransactionD_ExpenseConcept] FOREIGN KEY ([IdExpenseConcept]) REFERENCES [Treasury].[ExpenseConcepts] ([Id]),
    CONSTRAINT [FK_VoucherTransactionD_MainAccount] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_VoucherTransactionD_Retention] FOREIGN KEY ([IdRetentionConcept]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_VoucherTransactionD_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_VoucherTransactionD_VoucherTransaction] FOREIGN KEY ([IdVoucherTransaction]) REFERENCES [Treasury].[VoucherTransaction] ([Id]),
    CONSTRAINT [FK_VoucherTransactionDetails_CashRegisters] FOREIGN KEY ([CashRegisterId]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_VoucherTransactionDetails_IdCashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_VoucherTransactionDetails_IdGeneralLedgerIVA] FOREIGN KEY ([IdGeneralLedgerIVA]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_VoucherTransactionDetails_SupplierBankAccount] FOREIGN KEY ([SupplierBankAccountId]) REFERENCES [Common].[SupplierBankAccount] ([Id]),
    CONSTRAINT [FK_VoucherTransactionDetails_VoucherTransactionDetails] FOREIGN KEY ([Id]) REFERENCES [Treasury].[VoucherTransactionDetails] ([Id])
);




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
CREATE NONCLUSTERED INDEX [IX_VoucherTransactionDetails_IdVoucherTransaction]
    ON [Treasury].[VoucherTransactionDetails]([IdVoucherTransaction] ASC)
    INCLUDE([IdEntityBankAccount], [CashRegisterId], [IdThirdParty], [Value], [Detail]);


GO

-- ==============================================
-- -- Author:		
-- Create date: 
-- Description:	Se valida que la cuenta contable de cuentas bancarias ó cuenta contable de las cajas NO sea diferente a la que esta parametrizada en el momento.
-- ==============================================

CREATE TRIGGER [Treasury].[tgg_VoucherTransactionDetails_ValidateMainAccount]
   ON  [Treasury].[VoucherTransactionDetails]
   AFTER  INSERT,UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	
	IF EXISTS (
	SELECT  1
	FROM INSERTED vt
	JOIN Treasury.EntityBankAccounts eba ON eba.Id = vt.IdEntityBankAccount
	WHERE vt.IdMainAccount <> eba.IdMainAccount
	)
		BEGIN
		THROW 51000, 'Error generado por control desde trigger. La cuenta contable de la cuenta bancaria en el detalle no es igual a la que esta parametrizada.', 1
	END


	IF EXISTS (
	SELECT  1
	FROM INSERTED vt
	JOIN Treasury.CashRegisters cr ON  cr.id = vt.CashRegisterId
	WHERE vt.IdMainAccount <> cr.IdMainAccount
	)
		BEGIN
		THROW 51000, 'Error generado por control desde trigger. La cuenta contable de la caja en el detalle no es igual a la que esta parametrizada.', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el método de registro contable del IVA (TINYINT): 1 = IVA al Costo (no descontable), 2 = IVA Descontable (recuperable). Define tratamiento fiscal en libro mayor.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'TaxRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica como se va a llevar el Registro del IVA  1 - IVA al Costo  2 - IVA Descontable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'TaxRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta bancaria del proveedor/acreedor (FK a SupplierBankAccount). Usado para transferencias y pagos a terceros.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta bancaria del proveedor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del concepto (DECIMAL 18,5): suma de base, IVA y retenciones aplicables. Monto final contabilizado en el comprobante de egreso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'TotalConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor total del concepto.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'TotalConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'TotalConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del Impuesto al Valor Agregado (DECIMAL 18,2). Monto de IVA calculado sobre la base o concepto facturado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'ValueIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor del IVA.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'ValueIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'ValueIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del asiento de IVA en el libro mayor (FK a GeneralLedgerIVA). Referencia contable para trazabilidad tributaria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdGeneralLedgerIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del libro mayor del IVA.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdGeneralLedgerIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdGeneralLedgerIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): 1 = IVA descontable/recuperable, 0 = IVA no descontable. Determina si puede aplicarse crédito fiscal.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'discountableIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo booleano que especifica si maneja IVA descontable. 1 - Si, 0 - No.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'discountableIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'discountableIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de flujo de caja (FK a CashFlowConcept). Clasifica el movimiento para proyecciones y análisis de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de flujo de caja.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de observaciones detalladas (VARCHAR MAX) sobre el detalle del comprobante de egreso. Información adicional, glosas, referencias o justificaciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece información detallada sobre el detalle del comprobante de egreso .', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del concepto (VARCHAR MAX). Detalle del rubro, servicio, gasto o retención aplicada en la línea del comprobante.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'detalle del concepto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Detail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Detail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención aplicado (DECIMAL 5,2). Tasa usada para calcular el valor retenido sobre el monto del concepto.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'PercentRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es el valor del porcentaje con el cual se calcula el valor del concepto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'PercentRetention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'PercentRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor facturado o monto bruto del concepto (DECIMAL 18,2). Base para cálculo de impuestos y retenciones antes de ajustes.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor de la factura.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'BillingValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base del concepto (DECIMAL 18,2). Monto fundamental antes de aplicar IVA, descuentos o retenciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el valor base.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención (FK a RetentionConcepts). Referencia al tipo de retención aplicada (renta, ICA, IVA, etc.).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la retención', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdRetentionConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del concepto (DECIMAL 18,5). Monto monetary del rubro contabilizado en el detalle del comprobante de egreso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor del concepto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo/departamento (FK a CostCenter). Ubica el gasto en unidad funcional para contabilidad analítica.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza contable del concepto (TINYINT): indica si es débito/crédito, entrada/salida. Determina comportamiento en el asiento contable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'naturaleza del concepto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal (FK a MainAccounts). Código de cuenta en el catálogo del plan de cuentas.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdMainAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de egreso/gasto (FK a ExpenseConcepts). Clasifica el tipo de gasto: servicios, suministros, honorarios, etc.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdExpenseConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del concepto de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdExpenseConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdExpenseConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero/acreedor relacionado (FK a ThirdParty). Proveedor, contratista, empleado o entidad asociada al comprobante.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del tercero con que está relacionado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la caja/punto de pago (FK a CashRegisters). Lugar físico o lógico donde se registra la transacción de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'CashRegisterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'CashRegisterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'CashRegisterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta bancaria de la entidad (FK a EntityBankAccount). Cuenta origen/destino para transferencias entre bancos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la cuenta bancaria a hacer el traslado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del comprobante de egreso (FK a VoucherTransaction). Relación con el encabezado del comprobante contable y de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdVoucherTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'relación con la cabecera de comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdVoucherTransaction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'IdVoucherTransaction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (INT IDENTITY PK). Clave primaria autoincremental para cada línea/detalle del comprobante de egreso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líneas de detalle de cada comprobante contable en tesorería: registra el desglose por cuenta contable, tercero, centro de costos, concepto de gasto o retención, valores base, IVA y flujo de caja de cada movimiento de un voucher o comprobante de pago.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la actividad económica asociada a la línea del comprobante, usada para clasificar el movimiento según el código de actividad económica del tercero o la operación (útil para retenciones y reportes tributarios).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
