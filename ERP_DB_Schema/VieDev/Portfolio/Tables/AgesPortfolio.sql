CREATE TABLE [Portfolio].[AgesPortfolio] (
    [Id]                      INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SettingPortfolioId]      INT            NOT NULL,
    [Name]                    VARCHAR (100)  NOT NULL,
    [InitialRange]            INT            NOT NULL,
    [EndRange]                INT            NOT NULL,
    [DeteriorationPercentage] NUMERIC (5, 2) CONSTRAINT [DF_AgesPortfolio_DegradationPercentage] DEFAULT ((0)) NOT NULL,
    [ProvisionPercentage]     NUMERIC (5, 2) CONSTRAINT [DF_AgesPortfolio_DeteriorationPercentage1] DEFAULT ((0)) NOT NULL,
    [Color]                   INT            NOT NULL,
    [CreationUser]            VARCHAR (20)   NOT NULL,
    [CreationDate]            DATETIME       NOT NULL,
    [ModificationUser]        VARCHAR (20)   NULL,
    [ModificationDate]        DATETIME       NULL,
    [TimeStamp]               ROWVERSION     NOT NULL,
    CONSTRAINT [PK_AgesPortfolio] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AgesPortfolio_SettingPortfolio] FOREIGN KEY ([SettingPortfolioId]) REFERENCES [Portfolio].[SettingPortfolio] ([Id])
);


GO
ALTER TABLE [Portfolio].[AgesPortfolio] NOCHECK CONSTRAINT [FK_AgesPortfolio_SettingPortfolio];

