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
ALTER TABLE [Treasury].[VoucherTransactionDetails] NOCHECK CONSTRAINT [FK_VoucherTransactionD_CostCenter];


GO
ALTER TABLE [Treasury].[VoucherTransactionDetails] NOCHECK CONSTRAINT [FK_VoucherTransactionD_ExpenseConcept];


GO
ALTER TABLE [Treasury].[VoucherTransactionDetails] NOCHECK CONSTRAINT [FK_VoucherTransactionD_MainAccount];


GO
ALTER TABLE [Treasury].[VoucherTransactionDetails] NOCHECK CONSTRAINT [FK_VoucherTransactionD_Retention];


GO
ALTER TABLE [Treasury].[VoucherTransactionDetails] NOCHECK CONSTRAINT [FK_VoucherTransactionD_ThirdParty];


GO
ALTER TABLE [Treasury].[VoucherTransactionDetails] NOCHECK CONSTRAINT [FK_VoucherTransactionD_VoucherTransaction];


GO
ALTER TABLE [Treasury].[VoucherTransactionDetails] NOCHECK CONSTRAINT [FK_VoucherTransactionDetails_CashRegisters];


GO
ALTER TABLE [Treasury].[VoucherTransactionDetails] NOCHECK CONSTRAINT [FK_VoucherTransactionDetails_IdCashFlowConcept];


GO
ALTER TABLE [Treasury].[VoucherTransactionDetails] NOCHECK CONSTRAINT [FK_VoucherTransactionDetails_IdGeneralLedgerIVA];


GO
ALTER TABLE [Treasury].[VoucherTransactionDetails] NOCHECK CONSTRAINT [FK_VoucherTransactionDetails_SupplierBankAccount];


GO
ALTER TABLE [Treasury].[VoucherTransactionDetails] NOCHECK CONSTRAINT [FK_VoucherTransactionDetails_VoucherTransactionDetails];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica como se va a llevar el Registro del IVA  1 - IVA al Costo  2 - IVA Descontable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta bancaria del proveedor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionDetails', @level2type = N'COLUMN', @level2name = N'SupplierBankAccountId';

