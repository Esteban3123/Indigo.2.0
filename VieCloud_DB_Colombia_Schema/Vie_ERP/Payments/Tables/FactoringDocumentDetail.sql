CREATE TABLE [Payments].[FactoringDocumentDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFactoringDocument]      INT             NOT NULL,
    [IdAccountPayable]         INT             NOT NULL,
    [PreviousDate]             DATE            NOT NULL,
    [NewDate]                  DATE            NOT NULL,
    [PreviousBalance]          DECIMAL (18, 2) NOT NULL,
    [NewBalance]               DECIMAL (18, 2) NOT NULL,
    [IdCashFlowConceptExpense] INT             NOT NULL,
    [IdSupplier]               INT             NOT NULL,
    [IdDistributionLines]      INT             NOT NULL,
    [IdCashFlowConceptIncome]  INT             NOT NULL,
    [IdSupplierBefore]         INT             CONSTRAINT [DF__Factoring__IdSup__798E732A] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FactoringDocumentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FactoringDocumentDetail_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_FactoringDocumentDetail_CashFlowConceptExpense] FOREIGN KEY ([IdCashFlowConceptExpense]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_FactoringDocumentDetail_CashFlowConceptIncome] FOREIGN KEY ([IdCashFlowConceptIncome]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_FactoringDocumentDetail_DistributionLines] FOREIGN KEY ([IdDistributionLines]) REFERENCES [Common].[DistributionLines] ([Id]),
    CONSTRAINT [FK_FactoringDocumentDetail_FactoringDocument] FOREIGN KEY ([IdFactoringDocument]) REFERENCES [Payments].[FactoringDocument] ([Id]),
    CONSTRAINT [FK_FactoringDocumentDetail_Supplier] FOREIGN KEY ([IdSupplier]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_FactoringDocumentDetail_SupplierBefore] FOREIGN KEY ([IdSupplierBefore]) REFERENCES [Common].[Supplier] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor anterior en la operación de factoring; referencia a Common.Supplier, permite rastrear cambios de proveedores en la cesión de deudas.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdSupplierBefore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor anterior', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdSupplierBefore';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdSupplierBefore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de flujo de caja de ingreso; referencia a Treasury.CashFlowConcept, clasificación contable del ingreso generado por la operación de factoring.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowConceptIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de flujo de caja del ingreso', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowConceptIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowConceptIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la línea de distribución contable; referencia a Common.DistributionLines, vinculación a la distribución presupuestaria o analítica de la operación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdDistributionLines';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la linea de distribución', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdDistributionLines';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdDistributionLines';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del proveedor (acreedor cedente); referencia a Common.Supplier, entidad que transfiere la deuda en operación de factoring.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdSupplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdSupplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdSupplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de flujo de caja de egreso; referencia a Treasury.CashFlowConcept, clasificación contable del gasto o costo de la operación de factoring.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowConceptExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de flujo de caja del egreso', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowConceptExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowConceptExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo nuevo de la cuenta por pagar tras la operación de factoring; valor decimal (18,2), refleja el estado actualizado de la obligación después de la cesión o novación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'NewBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica el nuevo saldo de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'NewBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'NewBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo anterior de la cuenta por pagar antes de la operación de factoring; valor decimal (18,2), monto original de la deuda a transferir.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'PreviousBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica el saldo anterior de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'PreviousBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'PreviousBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nueva fecha de vencimiento o procesamiento de la cuenta por pagar; tipo DATE, fecha actualizada tras novación o renegociación en factoring.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'NewDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica la nueva fecha de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'NewDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'NewDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha anterior o fecha original de vencimiento de la cuenta por pagar; tipo DATE, referencia temporal del compromiso antes de la operación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'PreviousDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica la fecha anterior de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'PreviousDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'PreviousDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por pagar (obligación crediticia); referencia a Payments.AccountPayable, deuda objeto de la operación de factoring.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del documento de factoring; referencia a Payments.FactoringDocument, vinculación al registro maestro de la operación de cesión de deuda.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdFactoringDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento factoring', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdFactoringDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdFactoringDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador incremental (IDENTITY) del detalle de factoring; tipo INT, clave primaria para cada línea de la operación de factoring.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id incremental del detalle', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los documentos de factoring (cesión de cartera o anticipos sobre cuentas por pagar a proveedores). Registra los cambios de fecha y saldo en cada cuenta por pagar involucrada en una operación de factoring, junto con los conceptos de flujo de caja y los proveedores relacionados antes y después de la operación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail';
