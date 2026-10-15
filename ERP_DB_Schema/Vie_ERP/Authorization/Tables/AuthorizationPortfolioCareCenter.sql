CREATE TABLE [Authorization].[AuthorizationPortfolioCareCenter] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationPortfolioId] INT          NOT NULL,
    [CareCenterCode]           VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_AuthorizationPortfolioCareCenter__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationPortfolioCareCenter_AuthorizationPortfolio] FOREIGN KEY ([AuthorizationPortfolioId]) REFERENCES [Authorization].[AuthorizationPortfolio] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationPortfolioCareCenter] NOCHECK CONSTRAINT [FK_AuthorizationPortfolioCareCenter_AuthorizationPortfolio];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del centro de atención', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCareCenter', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del portafolio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCareCenter', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCareCenter', @level2type = N'COLUMN', @level2name = N'Id';

