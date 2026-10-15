CREATE TABLE [FixedAsset].[FixedAssetEntryDevolution] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]      INT            NOT NULL,
    [Code]                 VARCHAR (20)   NOT NULL,
    [DocumentDate]         DATETIME       NOT NULL,
    [FixedAssetEntryId]    INT            NOT NULL,
    [Description]          VARCHAR (MAX)  NOT NULL,
    [FreightValue]         NUMERIC (20)   NOT NULL,
    [FreightIVAPercentage] NUMERIC (5, 2) NOT NULL,
    [FreightIVAValue]      NUMERIC (20)   NOT NULL,
    [Value]                NUMERIC (20)   NOT NULL,
    [ValueDiscount]        NUMERIC (20)   NOT NULL,
    [ValueTax]             NUMERIC (20)   NOT NULL,
    [WithholdingTax]       NUMERIC (20)   NOT NULL,
    [WithholdingICA]       NUMERIC (20)   NOT NULL,
    [RetentionSource]      NUMERIC (20)   NOT NULL,
    [RetentionOther]       NUMERIC (20)   NOT NULL,
    [DeductionOther]       NUMERIC (20)   NOT NULL,
    [TotalValue]           NUMERIC (20)   NOT NULL,
    [Status]               TINYINT        NOT NULL,
    [CreationUser]         VARCHAR (20)   NOT NULL,
    [CreationDate]         DATETIME       NOT NULL,
    [ModificationUser]     VARCHAR (20)   NULL,
    [ModificationDate]     DATETIME       NULL,
    [ConfirmationUser]     VARCHAR (20)   NULL,
    [ConfirmationDate]     DATETIME       NULL,
    [AnnulmentUser]        VARCHAR (20)   NULL,
    [AnnulmentDate]        DATETIME       NULL,
    [TimeStamp]            ROWVERSION     NOT NULL,
    CONSTRAINT [PK_FixedAssetEntryDevolution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryDevolution_FixedAssetEntry] FOREIGN KEY ([FixedAssetEntryId]) REFERENCES [FixedAsset].[FixedAssetEntry] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryDevolution_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryDevolution] NOCHECK CONSTRAINT [FK_FixedAssetEntryDevolution_FixedAssetEntry];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryDevolution] NOCHECK CONSTRAINT [FK_FixedAssetEntryDevolution_OperatingUnit];

