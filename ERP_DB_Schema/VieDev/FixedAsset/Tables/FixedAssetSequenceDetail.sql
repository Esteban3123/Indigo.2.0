CREATE TABLE [FixedAsset].[FixedAssetSequenceDetail] (
    [Id]                    INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseFixedAssetC] INT    NOT NULL,
    [IdSequense]            INT    NOT NULL,
    [IdOperatingUnit]       INT    NULL,
    [Next]                  BIGINT CONSTRAINT [DF_SequenseFixedAssetD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseFixedAssetD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseFixedAssetD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseFixedAssetD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseFixedAssetD_SequenseFixedAssetC] FOREIGN KEY ([IdSequenseFixedAssetC]) REFERENCES [FixedAsset].[FixedAssetSequence] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseFixedAssetD_OperatingUnit];


GO
ALTER TABLE [FixedAsset].[FixedAssetSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseFixedAssetD_Sequense];


GO
ALTER TABLE [FixedAsset].[FixedAssetSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseFixedAssetD_SequenseFixedAssetC];

