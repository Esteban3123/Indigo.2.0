CREATE TABLE [Authorization].[AuthorizationPortfolioCUPSEntity] (
    [Id]                       INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationPortfolioId] INT NOT NULL,
    [AuthorizationGroupId]     INT NOT NULL,
    [CUPSEntityId]             INT NOT NULL,
    [ContractDescriptionId]    INT NULL,
    CONSTRAINT [PK_AuthorizationPortfolioCUPSEntity__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationPortfolioCUPSEntity_AuthorizationGroup] FOREIGN KEY ([AuthorizationGroupId]) REFERENCES [Authorization].[AuthorizationGroup] ([Id]),
    CONSTRAINT [FK_AuthorizationPortfolioCUPSEntity_AuthorizationPortfolio] FOREIGN KEY ([AuthorizationPortfolioId]) REFERENCES [Authorization].[AuthorizationPortfolio] ([Id]),
    CONSTRAINT [FK_AuthorizationPortfolioCUPSEntity_ContractDescription] FOREIGN KEY ([ContractDescriptionId]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_AuthorizationPortfolioCUPSEntity_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationPortfolioCUPSEntity] NOCHECK CONSTRAINT [FK_AuthorizationPortfolioCUPSEntity_AuthorizationGroup];


GO
ALTER TABLE [Authorization].[AuthorizationPortfolioCUPSEntity] NOCHECK CONSTRAINT [FK_AuthorizationPortfolioCUPSEntity_AuthorizationPortfolio];


GO
ALTER TABLE [Authorization].[AuthorizationPortfolioCUPSEntity] NOCHECK CONSTRAINT [FK_AuthorizationPortfolioCUPSEntity_ContractDescription];


GO
ALTER TABLE [Authorization].[AuthorizationPortfolioCUPSEntity] NOCHECK CONSTRAINT [FK_AuthorizationPortfolioCUPSEntity_CUPSEntity];

