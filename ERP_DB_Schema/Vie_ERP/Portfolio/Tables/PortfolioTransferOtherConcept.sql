CREATE TABLE [Portfolio].[PortfolioTransferOtherConcept] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioTransferId]    INT             NOT NULL,
    [PortfolioNoteConceptId] INT             NOT NULL,
    [MainAccountId]          INT             NOT NULL,
    [ThirdPartyId]           INT             NULL,
    [CostCenterId]           INT             NULL,
    [Nature]                 TINYINT         NOT NULL,
    [Value]                  NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_PortfolioTransferOtherConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioTransferOtherConcept_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioTransferOtherConcept_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioTransferOtherConcept_PortfolioNoteConcept] FOREIGN KEY ([PortfolioNoteConceptId]) REFERENCES [Portfolio].[PortfolioNoteConcept] ([Id]),
    CONSTRAINT [FK_PortfolioTransferOtherConcept_PortfolioTransfer] FOREIGN KEY ([PortfolioTransferId]) REFERENCES [Portfolio].[PortfolioTransfer] ([Id]),
    CONSTRAINT [FK_PortfolioTransferOtherConcept_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioTransferOtherConcept] NOCHECK CONSTRAINT [FK_PortfolioTransferOtherConcept_CostCenter];


GO
ALTER TABLE [Portfolio].[PortfolioTransferOtherConcept] NOCHECK CONSTRAINT [FK_PortfolioTransferOtherConcept_MainAccounts];


GO
ALTER TABLE [Portfolio].[PortfolioTransferOtherConcept] NOCHECK CONSTRAINT [FK_PortfolioTransferOtherConcept_PortfolioNoteConcept];


GO
ALTER TABLE [Portfolio].[PortfolioTransferOtherConcept] NOCHECK CONSTRAINT [FK_PortfolioTransferOtherConcept_PortfolioTransfer];


GO
ALTER TABLE [Portfolio].[PortfolioTransferOtherConcept] NOCHECK CONSTRAINT [FK_PortfolioTransferOtherConcept_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor del concepto', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Naturaleza  1 - Debito   2 - Credito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del centro de costo, este campo solo se llena si la cuenta contable maneja centro de costo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de nota o traslado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'PortfolioNoteConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del traslado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'PortfolioTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferOtherConcept', @level2type = N'COLUMN', @level2name = N'Id';

