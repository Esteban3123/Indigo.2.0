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


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AuthorizationPortfolioCUPSEntity]
    ON [Authorization].[AuthorizationPortfolioCUPSEntity]([AuthorizationPortfolioId] ASC, [CUPSEntityId] ASC, [ContractDescriptionId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la Descripción relacionada al CUPS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del CUPS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Grupo de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del portafolio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'Id';

