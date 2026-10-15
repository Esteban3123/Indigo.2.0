CREATE TABLE [Portfolio].[ReportPortfolioByAge] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [ClosingDate]            DATE          NULL,
    [OperatingUnits]         VARCHAR (MAX) NULL,
    [PersonTypes]            VARCHAR (MAX) NULL,
    [ThirdParties]           VARCHAR (MAX) NULL,
    [DocumentTypes]          VARCHAR (MAX) NULL,
    [Status]                 VARCHAR (MAX) NULL,
    [CalculateAgeBy]         INT           NULL,
    [IncludeAdvance]         INT           NULL,
    [OrderBy]                INT           NULL,
    [GroupOrDetailByAccount] BIT           NULL,
    CONSTRAINT [PK_ReportPortfolioByAge] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano que especifica si se agrupa o detalla por cuenta. 1 - Si, 2 - No.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'GroupOrDetailByAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ordenar por', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'OrderBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Criterio si incluye avance', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'IncludeAdvance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Criterio del calculo de la edad', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'CalculateAgeBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'estados concatenados por comas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de documentos concatenados por comas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'DocumentTypes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Terceros concatenados por comas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'ThirdParties';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tipo de personas concatenadas por comas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'PersonTypes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidades operativas concatenadas por comas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'OperatingUnits';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de corte', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'ClosingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autonumerico', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'ReportPortfolioByAge', @level2type = N'COLUMN', @level2name = N'Id';

