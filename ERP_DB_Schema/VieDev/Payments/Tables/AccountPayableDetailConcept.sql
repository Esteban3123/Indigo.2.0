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
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_CostCenter];


GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_GeneralLedgerIVA];


GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_PaymentsConcept];


GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_PUC];


GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_RetentionConcept];


GO
ALTER TABLE [Payments].[AccountPayableDetailConcept] NOCHECK CONSTRAINT [FK_AccountPayableDetailConcept_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IDX_AccountPayableDetailConcept_IdConceptAccountPayable]
    ON [Payments].[AccountPayableDetailConcept]([IdConceptAccountPayable] ASC)
    INCLUDE([IdAccountPayable], [IdAccount], [IdThirdParty], [IdCostCenter], [Nature], [BaseValue], [BillingValue], [Value], [IdRetentionConcept], [Percentage], [Detail], [DeferredCausation], [IsDirectCost]);


GO
CREATE NONCLUSTERED INDEX [IX_AccountPayableDetailConcept_IdAccountPayable_IdRetentionConcept]
    ON [Payments].[AccountPayableDetailConcept]([IdAccountPayable] ASC, [IdRetentionConcept] ASC)
    INCLUDE([IdAccount], [BaseValue], [RateIva], [IvaValue]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total del concepto', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'TotalConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del IVA', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa del IVA', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableDetailConcept', @level2type = N'COLUMN', @level2name = N'RateIva';

