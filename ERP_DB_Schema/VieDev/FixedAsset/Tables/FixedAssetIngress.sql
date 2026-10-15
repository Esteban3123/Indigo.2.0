CREATE TABLE [FixedAsset].[FixedAssetIngress] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20)  NOT NULL,
    [IngressDate]                DATE          NOT NULL,
    [IdAdquisitionType]          INT           NOT NULL,
    [IdSupplierDistributionLine] INT           NOT NULL,
    [Description]                VARCHAR (100) NOT NULL,
    [IdBank]                     INT           NULL,
    [DocumentNumber]             VARCHAR (100) NOT NULL,
    [DocumentDate]               DATE          NOT NULL,
    [DaysLater]                  INT           NOT NULL,
    [FreightValue]               NUMERIC (18)  NOT NULL,
    [IdIvaFreight]               INT           NOT NULL,
    [RoundService]               INT           NOT NULL,
    [Status]                     TINYINT       NOT NULL,
    [CreationUser]               VARCHAR (20)  NOT NULL,
    [CreationDate]               DATETIME      NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    [ConfirmationUser]           VARCHAR (20)  NULL,
    [ConfirmationDate]           DATETIME      NULL,
    [AnnulmentUser]              VARCHAR (20)  NULL,
    [AnnulmentDate]              DATETIME      NULL,
    [TimeStamp]                  ROWVERSION    NOT NULL,
    CONSTRAINT [PK_FixedAssetIngress_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetIngress_Bank] FOREIGN KEY ([IdBank]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_FixedAssetIngress_GeneralLedgerIVA] FOREIGN KEY ([IdIvaFreight]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetIngress] NOCHECK CONSTRAINT [FK_FixedAssetIngress_Bank];


GO
ALTER TABLE [FixedAsset].[FixedAssetIngress] NOCHECK CONSTRAINT [FK_FixedAssetIngress_GeneralLedgerIVA];

