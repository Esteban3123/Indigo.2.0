CREATE TABLE [FixedAsset].[LeasingContractsEnd] (
    [Id]              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId] INT          NOT NULL,
    [DocumentDate]    DATETIME     NOT NULL,
    [CreationUser]    VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_LeasingContractsEnd] PRIMARY KEY CLUSTERED ([Id] ASC)
);

