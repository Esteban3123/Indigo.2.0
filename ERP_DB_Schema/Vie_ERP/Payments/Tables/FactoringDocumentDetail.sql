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
ALTER TABLE [Payments].[FactoringDocumentDetail] NOCHECK CONSTRAINT [FK_FactoringDocumentDetail_AccountPayable];


GO
ALTER TABLE [Payments].[FactoringDocumentDetail] NOCHECK CONSTRAINT [FK_FactoringDocumentDetail_CashFlowConceptExpense];


GO
ALTER TABLE [Payments].[FactoringDocumentDetail] NOCHECK CONSTRAINT [FK_FactoringDocumentDetail_CashFlowConceptIncome];


GO
ALTER TABLE [Payments].[FactoringDocumentDetail] NOCHECK CONSTRAINT [FK_FactoringDocumentDetail_DistributionLines];


GO
ALTER TABLE [Payments].[FactoringDocumentDetail] NOCHECK CONSTRAINT [FK_FactoringDocumentDetail_FactoringDocument];


GO
ALTER TABLE [Payments].[FactoringDocumentDetail] NOCHECK CONSTRAINT [FK_FactoringDocumentDetail_Supplier];


GO
ALTER TABLE [Payments].[FactoringDocumentDetail] NOCHECK CONSTRAINT [FK_FactoringDocumentDetail_SupplierBefore];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor anterior', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdSupplierBefore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de flujo de caja del ingreso', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowConceptIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la linea de distribución', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdDistributionLines';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdSupplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de flujo de caja del egreso', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowConceptExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el nuevo saldo de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'NewBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el saldo anterior de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'PreviousBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica la nueva fecha de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'NewDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica la fecha anterior de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'PreviousDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento factoring', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'IdFactoringDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id incremental del detalle', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringDocumentDetail', @level2type = N'COLUMN', @level2name = N'Id';

