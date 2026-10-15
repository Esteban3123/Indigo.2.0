CREATE TABLE [FixedAsset].[ValorizationDevaluation] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [DocumentDate]         DATE          NOT NULL,
    [DocumentType]         TINYINT       NOT NULL,
    [Detail]               VARCHAR (100) NOT NULL,
    [PhysicalFixedAssetId] INT           NOT NULL,
    [ThirdPartyId]         INT           NULL,
    [CostCenterId]         INT           NULL,
    [AccountingAccountId]  INT           NULL,
    [RegisterValue]        NUMERIC (18)  NOT NULL,
    [AppraisedValue]       NUMERIC (18)  NOT NULL,
    [ResponsibleId]        INT           NOT NULL,
    [HistoricalCost]       NUMERIC (18)  NOT NULL,
    [Depreciation]         NUMERIC (18)  NOT NULL,
    [CumulatedAdjustment]  NUMERIC (18)  NOT NULL,
    [CarryingAmount]       NUMERIC (18)  NOT NULL,
    [Status]               TINYINT       NOT NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [AnnulmentUser]        VARCHAR (20)  NULL,
    [AnnulmentDate]        DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    CONSTRAINT [PK_ValorizationDevaluation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ValorizationDevaluation_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ValorizationDevaluation_FixedAssetResponsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [FixedAsset].[FixedAssetResponsible] ([Id]),
    CONSTRAINT [FK_ValorizationDevaluation_MainAccounts] FOREIGN KEY ([AccountingAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ValorizationDevaluation_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [FixedAsset].[ValorizationDevaluation] NOCHECK CONSTRAINT [FK_ValorizationDevaluation_CostCenter];


GO
ALTER TABLE [FixedAsset].[ValorizationDevaluation] NOCHECK CONSTRAINT [FK_ValorizationDevaluation_FixedAssetResponsible];


GO
ALTER TABLE [FixedAsset].[ValorizationDevaluation] NOCHECK CONSTRAINT [FK_ValorizationDevaluation_MainAccounts];


GO
ALTER TABLE [FixedAsset].[ValorizationDevaluation] NOCHECK CONSTRAINT [FK_ValorizationDevaluation_ThirdParty];

