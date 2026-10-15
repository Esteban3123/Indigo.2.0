CREATE TABLE [Portfolio].[PortfolioNoteDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioNoteId]        INT             NOT NULL,
    [PortfolioNoteConceptId] INT             NOT NULL,
    [MainAccountId]          INT             NOT NULL,
    [ThirdPartyId]           INT             NOT NULL,
    [CostCenterId]           INT             NULL,
    [Nature]                 TINYINT         NOT NULL,
    [Value]                  NUMERIC (20, 2) NOT NULL,
    [RetentionConceptId]     INT             NULL,
    [BaseValue]              DECIMAL (20, 2) NULL,
    [Percentage]             NUMERIC (5, 2)  NULL,
    [Observations]           VARCHAR (3000)  NULL,
    [IdGeneralLedgerIVA]     INT             NULL,
    [IvaRate]                DECIMAL (18, 2) NULL,
    [TotalConcept]           DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_PortfolioNoteDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioNoteDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDetail_GeneralLedgerIVA] FOREIGN KEY ([IdGeneralLedgerIVA]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDetail_PortfolioNote] FOREIGN KEY ([PortfolioNoteId]) REFERENCES [Portfolio].[PortfolioNote] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDetail_PortfolioNoteConcept] FOREIGN KEY ([PortfolioNoteConceptId]) REFERENCES [Portfolio].[PortfolioNoteConcept] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDetail_RetentionConcepts] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_PortfolioNoteDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioNoteDetail] NOCHECK CONSTRAINT [FK_PortfolioNoteDetail_CostCenter];


GO
ALTER TABLE [Portfolio].[PortfolioNoteDetail] NOCHECK CONSTRAINT [FK_PortfolioNoteDetail_GeneralLedgerIVA];


GO
ALTER TABLE [Portfolio].[PortfolioNoteDetail] NOCHECK CONSTRAINT [FK_PortfolioNoteDetail_MainAccounts];


GO
ALTER TABLE [Portfolio].[PortfolioNoteDetail] NOCHECK CONSTRAINT [FK_PortfolioNoteDetail_PortfolioNote];


GO
ALTER TABLE [Portfolio].[PortfolioNoteDetail] NOCHECK CONSTRAINT [FK_PortfolioNoteDetail_PortfolioNoteConcept];


GO
ALTER TABLE [Portfolio].[PortfolioNoteDetail] NOCHECK CONSTRAINT [FK_PortfolioNoteDetail_RetentionConcepts];


GO
ALTER TABLE [Portfolio].[PortfolioNoteDetail] NOCHECK CONSTRAINT [FK_PortfolioNoteDetail_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor resultante de la suma del campo Base más valor IVA', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDetail', @level2type = N'COLUMN', @level2name = N'TotalConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El valor aplicado al valor base por la tarifa de IVA', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDetail', @level2type = N'COLUMN', @level2name = N'IvaRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relaciona la tarifa del IVA', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteDetail', @level2type = N'COLUMN', @level2name = N'IdGeneralLedgerIVA';

