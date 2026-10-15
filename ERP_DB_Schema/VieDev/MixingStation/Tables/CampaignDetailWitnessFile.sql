CREATE TABLE [MixingStation].[CampaignDetailWitnessFile] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [CampaignDetailId] INT           NOT NULL,
    [Name]             VARCHAR (500) NOT NULL,
    [Extension]        VARCHAR (10)  NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    CONSTRAINT [PK_CampaignDetailWitnessFile] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignDetailWitnessFile_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id])
);


GO
ALTER TABLE [MixingStation].[CampaignDetailWitnessFile] NOCHECK CONSTRAINT [FK_CampaignDetailWitnessFile_CampaignDetail];

