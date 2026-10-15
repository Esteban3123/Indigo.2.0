CREATE TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioInitialBalanceId]  INT           NOT NULL,
    [AccountReceivableType]      TINYINT       NOT NULL,
    [ThirdPartyId]               INT           NOT NULL,
    [CustomerId]                 INT           NULL,
    [InvoiceNumber]              VARCHAR (20)  NOT NULL,
    [AccountReceivableDate]      DATETIME      NOT NULL,
    [Term]                       INT           NOT NULL,
    [ExpiredDate]                DATETIME      NOT NULL,
    [Observations]               VARCHAR (300) NOT NULL,
    [PortfolioStatus]            TINYINT       NOT NULL,
    [NumberShares]               INT           NOT NULL,
    [Value]                      NUMERIC (18)  NOT NULL,
    [Balance]                    NUMERIC (18)  CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_Balance] DEFAULT ((0)) NOT NULL,
    [CostCenterId]               INT           NULL,
    [InvoiceCategoryId]          INT           NULL,
    [AccountWithoutRadicateId]   INT           CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_AccountWithoutRadicateId] DEFAULT ((203)) NULL,
    [AccountRadicateId]          INT           CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_AccountRadicateId] DEFAULT ((203)) NULL,
    [AccountObjectionRemediedId] INT           CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_AccountObjectionRemediedId] DEFAULT ((203)) NULL,
    [AccountConciliationId]      INT           NULL,
    [AccountLegalCollectionId]   INT           NULL,
    [AccountDebtorOrder]         INT           NULL,
    [AccountCreditorOrder]       INT           NULL,
    [AffectBudget]               BIT           CONSTRAINT [DF_PortfolioInitialBalanceAccountReceivable_AffectBudget] DEFAULT ((0)) NOT NULL,
    [BudgetId]                   INT           NULL,
    [IsElectronicInvoice]        BIT           CONSTRAINT [DF__Portfolio__IsEle__088834A5] DEFAULT ((0)) NOT NULL,
    [CUFE]                       VARCHAR (250) NULL,
    CONSTRAINT [PK_PortfolioInitialBalanceAccountReceivable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioAccountReceivable_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_PortfolioAccountReceivable_PortfolioInitialBalance] FOREIGN KEY ([PortfolioInitialBalanceId]) REFERENCES [Portfolio].[PortfolioInitialBalance] ([Id]),
    CONSTRAINT [FK_PortfolioAccountReceivable_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_InvoiceCategories] FOREIGN KEY ([InvoiceCategoryId]) REFERENCES [Billing].[InvoiceCategories] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts] FOREIGN KEY ([AccountWithoutRadicateId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts1] FOREIGN KEY ([AccountRadicateId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts2] FOREIGN KEY ([AccountObjectionRemediedId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts3] FOREIGN KEY ([AccountConciliationId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts4] FOREIGN KEY ([AccountLegalCollectionId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts5] FOREIGN KEY ([AccountDebtorOrder]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts6] FOREIGN KEY ([AccountCreditorOrder]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioAccountReceivable_Customer];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioAccountReceivable_PortfolioInitialBalance];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioAccountReceivable_ThirdParty];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_Budget];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_CostCenter];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_InvoiceCategories];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts1];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts2];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts3];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts4];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts5];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivable] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivable_MainAccounts6];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CUFE de la factura adicionada por saldo inicial', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'CUFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que valida si la factura es electrónica, lo que implica la adición del CUFE correspondiente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'IsElectronicInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el grupo de atencion afecta presupuesto  1 - Si  0- No    Si esta como si, entonces cuando se vaya a radicar la factura este creara un reconocimiento automaticamente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de Cartera  Sin Radicar = 1  Radicada= 2  Radicada Entidad = 3,  Objetada =4  Contestada Radicada = 5  Aceptada =6  Certificada Parcial = 7  Certificada Total = 8  No Subsanable = 9  Difícil Recaudo =10  Factura Devuelta = 11  Glosa Ratificada =12  Radicación Tramite Objeción =13  Devolución Factura = 14  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivable', @level2type = N'COLUMN', @level2name = N'PortfolioStatus';

