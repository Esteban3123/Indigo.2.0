CREATE TABLE [MixingStation].[Harnessed] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [QuantityRemainingId] INT             NOT NULL,
    [CampaignDetailId]    INT             NOT NULL,
    [Quantity]            DECIMAL (20, 6) NOT NULL,
    [CreationUser]        VARCHAR (20)    NOT NULL,
    [CreationDate]        DATETIME        NOT NULL,
    CONSTRAINT [PK_Harnessed] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Harnessed_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_Harnessed_QuantityRemaining] FOREIGN KEY ([QuantityRemainingId]) REFERENCES [MixingStation].[QuantityRemaining] ([Id])
);


GO
ALTER TABLE [MixingStation].[Harnessed] NOCHECK CONSTRAINT [FK_Harnessed_CampaignDetail];


GO
ALTER TABLE [MixingStation].[Harnessed] NOCHECK CONSTRAINT [FK_Harnessed_QuantityRemaining];

