CREATE TABLE [MixingStation].[RawMaterialDevolutionDetail] (
    [Id]                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RawMaterialDevolutionId]    INT          NOT NULL,
    [CampaignDetailValidationId] INT          NOT NULL,
    [Quantity]                   INT          NOT NULL,
    [CreationUser]               VARCHAR (20) NOT NULL,
    [CreationDate]               DATETIME     NOT NULL,
    [ModificationUser]           VARCHAR (20) NULL,
    [ModificationDate]           DATETIME     NULL,
    CONSTRAINT [PK_RawMaterialDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RawMaterialDevolutionDetail_CampaignDetailValidation] FOREIGN KEY ([CampaignDetailValidationId]) REFERENCES [MixingStation].[CampaignDetailValidation] ([Id]),
    CONSTRAINT [FK_RawMaterialDevolutionDetail_RawMaterialDevolution] FOREIGN KEY ([RawMaterialDevolutionId]) REFERENCES [MixingStation].[RawMaterialDevolution] ([Id])
);


GO
ALTER TABLE [MixingStation].[RawMaterialDevolutionDetail] NOCHECK CONSTRAINT [FK_RawMaterialDevolutionDetail_CampaignDetailValidation];


GO
ALTER TABLE [MixingStation].[RawMaterialDevolutionDetail] NOCHECK CONSTRAINT [FK_RawMaterialDevolutionDetail_RawMaterialDevolution];

