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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del anticipo que se creo cuando se confirmo el documento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor del nuevo anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el centro de costo con el que se va a crear el nuevo anticipo, este solo se solicita si la cuenta contable maneja centro de costo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta contable del nuevo anticipo, la cual se sugiere del anticipo de la cabecera pero el usuario la puede cambiar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cliente del nuevo anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la nota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'PortfolioNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de distribucion', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDistribution', @level2type = N'COLUMN', @level2name = N'Id';

