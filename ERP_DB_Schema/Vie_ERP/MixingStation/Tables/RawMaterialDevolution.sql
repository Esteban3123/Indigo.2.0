CREATE TABLE [MixingStation].[RawMaterialDevolution] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [DocumentDate]     DATE          NOT NULL,
    [CampaignDetailId] INT           NOT NULL,
    [Detail]           VARCHAR (500) NOT NULL,
    [State]            TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [AnnulmentUser]    VARCHAR (20)  NULL,
    [AnnulmentDate]    DATETIME      NULL,
    [ConfirmationUser] VARCHAR (20)  NULL,
    [ConfirmationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_RawMaterialDevolution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RawMaterialDevolution_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [IX_RawMaterialDevolution] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [MixingStation].[RawMaterialDevolution] NOCHECK CONSTRAINT [FK_RawMaterialDevolution_CampaignDetail];

