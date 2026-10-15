CREATE TABLE [FixedAsset].[DeteriorationIndications] (
    [Id]               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)   NOT NULL,
    [Name]             VARCHAR (100)  NOT NULL,
    [Rate]             DECIMAL (5, 2) NOT NULL,
    [Status]           BIT            NOT NULL,
    [CreationUser]     VARCHAR (20)   NOT NULL,
    [CreationDate]     DATETIME       NOT NULL,
    [ModificationUser] VARCHAR (20)   NULL,
    [ModificationDate] DATETIME       NULL,
    [TimeStamp]        ROWVERSION     NOT NULL,
    CONSTRAINT [PK_DeteriorationIndication] PRIMARY KEY CLUSTERED ([Id] ASC)
);

