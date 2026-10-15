CREATE TABLE [Authorization].[AuthorizationPortfolioCareCenter] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationPortfolioId] INT          NOT NULL,
    [CareCenterCode]           VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_AuthorizationPortfolioCareCenter__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationPortfolioCareCenter_AuthorizationPortfolio] FOREIGN KEY ([AuthorizationPortfolioId]) REFERENCES [Authorization].[AuthorizationPortfolio] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationPortfolioCareCenter] NOCHECK CONSTRAINT [FK_AuthorizationPortfolioCareCenter_AuthorizationPortfolio];

