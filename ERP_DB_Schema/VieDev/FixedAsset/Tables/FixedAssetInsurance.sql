CREATE TABLE [FixedAsset].[FixedAssetInsurance] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [IdThirdParty]     INT           NOT NULL,
    [IdPerson]         INT           NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [WebSite]          VARCHAR (100) NULL,
    [IdCity]           INT           NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_FixedAssetInsurance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Insurance_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Insurance_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_Insurance_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetInsurance] NOCHECK CONSTRAINT [FK_Insurance_City];


GO
ALTER TABLE [FixedAsset].[FixedAssetInsurance] NOCHECK CONSTRAINT [FK_Insurance_Person];


GO
ALTER TABLE [FixedAsset].[FixedAssetInsurance] NOCHECK CONSTRAINT [FK_Insurance_ThirdParty];

