CREATE TABLE [Portfolio].[AccountReceivableDocumentDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountReceivableDocumentId] INT             NOT NULL,
    [AccountReceivableConceptId]  INT             NOT NULL,
    [MainAccountId]               INT             NOT NULL,
    [ThirdPartyId]                INT             NULL,
    [CostCenterId]                INT             NULL,
    [Nature]                      TINYINT         NOT NULL,
    [Value]                       NUMERIC (18, 2) CONSTRAINT [DF_AccountReceivableDocumentDetail_Value] DEFAULT ((0)) NOT NULL,
    [Observation]                 VARCHAR (500)   NULL,
    CONSTRAINT [PK_AccountReceivableDocumentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivableDocumentDetail_AccountReceivableConcept] FOREIGN KEY ([AccountReceivableConceptId]) REFERENCES [Portfolio].[AccountReceivableConcept] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocumentDetail_AccountReceivableDocument] FOREIGN KEY ([AccountReceivableDocumentId]) REFERENCES [Portfolio].[AccountReceivableDocument] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocumentDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocumentDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountReceivableDocumentDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[AccountReceivableDocumentDetail] NOCHECK CONSTRAINT [FK_AccountReceivableDocumentDetail_AccountReceivableConcept];


GO
ALTER TABLE [Portfolio].[AccountReceivableDocumentDetail] NOCHECK CONSTRAINT [FK_AccountReceivableDocumentDetail_AccountReceivableDocument];


GO
ALTER TABLE [Portfolio].[AccountReceivableDocumentDetail] NOCHECK CONSTRAINT [FK_AccountReceivableDocumentDetail_CostCenter];


GO
ALTER TABLE [Portfolio].[AccountReceivableDocumentDetail] NOCHECK CONSTRAINT [FK_AccountReceivableDocumentDetail_MainAccounts];


GO
ALTER TABLE [Portfolio].[AccountReceivableDocumentDetail] NOCHECK CONSTRAINT [FK_AccountReceivableDocumentDetail_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la naturaleza del registro  1 - Debito  2 - Credito', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableDocumentDetail', @level2type = N'COLUMN', @level2name = N'Nature';

