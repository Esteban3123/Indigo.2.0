CREATE TABLE [Treasury].[CashReceiptConcepts] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (20)  NOT NULL,
    [Name]                        VARCHAR (100) NOT NULL,
    [Affectation]                 TINYINT       CONSTRAINT [DF_CashReceiptConcept_AffectPortfolio] DEFAULT ((0)) NOT NULL,
    [AffectBudget]                BIT           CONSTRAINT [DF_CashReceiptConcept_AffectBudget] DEFAULT ((0)) NOT NULL,
    [BudgetId]                    INT           NULL,
    [Discount]                    BIT           CONSTRAINT [DF_CashReceiptConcept_Discount] DEFAULT ((0)) NOT NULL,
    [Nature]                      TINYINT       NOT NULL,
    [IdMainAccount]               INT           NOT NULL,
    [Status]                      BIT           CONSTRAINT [DF_CashReceiptConcept_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                VARCHAR (20)  NOT NULL,
    [CreationDate]                DATETIME      NOT NULL,
    [ModificationUser]            VARCHAR (20)  NULL,
    [ModificationDate]            DATETIME      NULL,
    [TimeStamp]                   ROWVERSION    NOT NULL,
    [AffectCashFlowConcept]       BIT           NULL,
    [IdCashFlowConcept]           INT           NULL,
    [MakeAssociateIncome]         BIT           NULL,
    [LinkThirdPartyBeneficiary]   BIT           DEFAULT ((0)) NOT NULL,
    [IsFixedAmountInvoiceAdvance] BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CashReceiptConcepts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptConcept_MainAccounts] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_CashReceiptConcepts_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_CashReceiptConcepts_IdCashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id])
);


GO
ALTER TABLE [Treasury].[CashReceiptConcepts] NOCHECK CONSTRAINT [FK_CashReceiptConcept_MainAccounts];


GO
ALTER TABLE [Treasury].[CashReceiptConcepts] NOCHECK CONSTRAINT [FK_CashReceiptConcepts_Budget];


GO
ALTER TABLE [Treasury].[CashReceiptConcepts] NOCHECK CONSTRAINT [FK_CashReceiptConcepts_IdCashFlowConcept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refleja si el concepto califica para anticipos de facturas monto fijo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptConcepts', @level2type = N'COLUMN', @level2name = N'IsFixedAmountInvoiceAdvance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refleja si el concepto permite relacionar tercero beneficiario', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptConcepts', @level2type = N'COLUMN', @level2name = N'LinkThirdPartyBeneficiary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptConcepts', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afectación   1- Ninguno   2- Cancelacion / Abonos Facturas CxC  3- Reintegro de Anticipos a Proveedores  4 - Reintegro de CxP', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptConcepts', @level2type = N'COLUMN', @level2name = N'Affectation';

