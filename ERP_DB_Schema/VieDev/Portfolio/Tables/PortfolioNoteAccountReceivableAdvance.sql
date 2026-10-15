CREATE TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioNoteId]               INT             NOT NULL,
    [AccountReceivableId]           INT             NULL,
    [AccountReceivableShareId]      INT             NULL,
    [MainAccountId]                 INT             NULL,
    [AccountReceivableAccountingId] INT             NULL,
    [PortfolioAdvanceId]            INT             NULL,
    [AdjusmentValue]                NUMERIC (20, 2) NOT NULL,
    [PercentageValue]               NUMERIC (5, 2)  NOT NULL,
    [PreviousBalance]               NUMERIC (20, 2) NOT NULL,
    [Balance]                       NUMERIC (20, 2) NOT NULL,
    [ConceptId]                     INT             NULL,
    CONSTRAINT [PK_PortfolioNoteAccountReceivableAdvance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_AccountReceivableAccounting] FOREIGN KEY ([AccountReceivableAccountingId]) REFERENCES [Portfolio].[AccountReceivableAccounting] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_AccountReceivableShare] FOREIGN KEY ([AccountReceivableShareId]) REFERENCES [Portfolio].[AccountReceivableShare] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_PortfolioNote] FOREIGN KEY ([PortfolioNoteId]) REFERENCES [Portfolio].[PortfolioNote] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_AccountReceivable];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_AccountReceivableAccounting];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_AccountReceivableShare];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_MainAccounts];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_PortfolioAdvance];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableAdvance] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableAdvance_PortfolioNote];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de la Nota relacionado con la factura electrónica. Dependiendo de la naturaleza, puede ser:  
    - Débito:  
      1. Intereses  
      2. Gastos por cobrar  
      3. Cambio de valor  
    - Crédito:  
      1. Devolución o no aceptación de partes del servicio  
      2. Anulación de factura electrónica  
      3. Rebaja total aplicada  
      4. Descuento total aplicado  
      5. Rescisión: nulidad por falta de requisitos  
      6. Otros  
	  7. Sin referencia a una factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableAdvance', @level2type = N'COLUMN', @level2name = N'ConceptId';

