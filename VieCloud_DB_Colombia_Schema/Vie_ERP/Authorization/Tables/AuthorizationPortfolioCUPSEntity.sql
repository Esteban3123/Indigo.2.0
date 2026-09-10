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



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AuthorizationPortfolioCUPSEntity]
    ON [Authorization].[AuthorizationPortfolioCUPSEntity]([AuthorizationPortfolioId] ASC, [CUPSEntityId] ASC, [ContractDescriptionId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la descripción del contrato vinculada al código CUPS; referencia a la especificación comercial y de cobertura del procedimiento, servicio o producto autorizado (INT, FK → Contract.ContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la Descripción relacionada al CUPS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del CUPS (Código Único de Procedimientos en Salud); referencia a la entidad que define el procedimiento, servicio, producto o tecnología en salud autorizado (INT, FK → Contract.CUPSEntity)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del CUPS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de autorización; agrupa autorizaciones por categoría de cobertura, producto o plan (INT, FK → Authorization.AuthorizationGroup)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Grupo de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del portafolio de autorización; agrupa procedimientos y servicios autorizados bajo una política o línea de cobertura contractual (INT, FK → Authorization.AuthorizationPortfolio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del portafolio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de relación entre portafolio de autorización, grupo de autorización, CUPS y descripción contractual (INT, PK IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los portafolios de autorización con los códigos CUPS (procedimientos y servicios de salud) permitidos por entidad, agrupados por grupo de autorización y opcionalmente vinculados a una descripción de contrato. Define qué servicios o procedimientos pueden ser autorizados dentro de cada portafolio para una entidad pagadora.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioCUPSEntity';
