CREATE TABLE [FixedAsset].[FixedAssetSequence] (
    [Id]         INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]     VARCHAR (5) NOT NULL,
    [IsManual]   BIT         CONSTRAINT [DF_FixedAssetSequence_IdManual] DEFAULT ((1)) NOT NULL,
    [Scope]      VARCHAR (2) CONSTRAINT [DF_SequenseFixedAssetC_Scope] DEFAULT ('O') NOT NULL,
    [Sequential] BIT         CONSTRAINT [DF_SequenseFixedAssetC_Sequential] DEFAULT ((0)) NOT NULL,
    [Rate]       TINYINT     CONSTRAINT [DF_SequenseFixedAssetC_Rate] DEFAULT ((3)) NOT NULL,
    CONSTRAINT [PK_SequenseFixedAssetC] PRIMARY KEY CLUSTERED ([Id] ASC)
);

