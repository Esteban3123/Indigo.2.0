CREATE TABLE [Portfolio].[PortfolioNoteDistribution] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioNoteId]    INT             NOT NULL,
    [CustomerId]         INT             NOT NULL,
    [MainAccountId]      INT             NOT NULL,
    [CostCenterId]       INT             NULL,
    [Value]              NUMERIC (18, 2) NOT NULL,
    [PortfolioAdvanceId] INT             NULL,
    CONSTRAINT [PK_PortfolioNoteDistribution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioNoteDistribution_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDistribution_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDistribution_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDistribution_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDistribution_PortfolioNote] FOREIGN KEY ([PortfolioNoteId]) REFERENCES [Portfolio].[PortfolioNote] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioNoteDistribution] NOCHECK CONSTRAINT [FK_PortfolioNoteDistribution_CostCenter];


GO
ALTER TABLE [Portfolio].[PortfolioNoteDistribution] NOCHECK CONSTRAINT [FK_PortfolioNoteDistribution_Customer];


GO
ALTER TABLE [Portfolio].[PortfolioNoteDistribution] NOCHECK CONSTRAINT [FK_PortfolioNoteDistribution_MainAccounts];


GO
ALTER TABLE [Portfolio].[PortfolioNoteDistribution] NOCHECK CONSTRAINT [FK_PortfolioNoteDistribution_PortfolioAdvance];


GO
ALTER TABLE [Portfolio].[PortfolioNoteDistribution] NOCHECK CONSTRAINT [FK_PortfolioNoteDistribution_PortfolioNote];


GO
-- =============================================
-- Author:		Giovanny Plazas Lozano
-- Create date: 06/09/2023
-- Description:	Trigger para relacionar el recibo de caja con el anticipo cuando viene de una distribucion de anticipos para obtener el metodo de pago
-- =============================================
CREATE TRIGGER [Portfolio].[TriggerNoteDistribution]
   ON [Portfolio].[PortfolioNoteDistribution]
   AFTER INSERT,UPDATE
AS 
BEGIN
	


			update pa set CashReceiptId = pas.CashReceiptId
			from INSERTED d
			inner join Portfolio.PortfolioNote pn WITH(NOLOCK) on pn.Id = d.PortfolioNoteId
			inner join Portfolio.PortfolioAdvance pas WITH(NOLOCK) on pas.Id = pn.PortfolioAdvanceId
			inner join Portfolio.PortfolioAdvance pa WITH(NOLOCK) on pa.Id = d.PortfolioAdvanceId

END