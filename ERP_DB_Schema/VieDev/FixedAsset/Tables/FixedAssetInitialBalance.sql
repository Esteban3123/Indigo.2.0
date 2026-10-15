CREATE TABLE [FixedAsset].[FixedAssetInitialBalance] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]  INT           NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [DocumentDate]     DATE          NOT NULL,
    [Observations]     VARCHAR (300) NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [ConfirmationUser] VARCHAR (20)  NULL,
    [ConfirmationDate] DATETIME      NULL,
    [AnnulmentUser]    VARCHAR (20)  NULL,
    [AnnulmentDate]    DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_FixedAssetInitialBalance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetInitialBalance_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [UQ_FixedAssetInitialBalance__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalance] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalance_OperatingUnit];

