CREATE TABLE [Portfolio].[PortfolioDeteriorationClassificationDetails] (
    [Id]                                     INT            IDENTITY (1, 1) NOT NULL,
    [PortfolioDeteriorationClassificationId] INT            NOT NULL,
    [AgesPortfolioId]                        INT            NULL,
    [RangeType]                              TINYINT        NOT NULL,
    [AgesPortfolioName]                      VARCHAR (100)  NOT NULL,
    [DeteriorationNiifPercent]               DECIMAL (5, 2) NOT NULL,
    [DeteriorationFiscalPercent]             DECIMAL (5, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioDeteriorationClassificationDetails_AgesPortfolio] FOREIGN KEY ([AgesPortfolioId]) REFERENCES [Portfolio].[AgesPortfolio] ([Id]),
    CONSTRAINT [FK_PortfolioDeteriorationClassificationDetails_Classification] FOREIGN KEY ([PortfolioDeteriorationClassificationId]) REFERENCES [Portfolio].[PortfolioDeteriorationClassification] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de deterioro fiscal (DECIMAL 5,2) según régimen tributario aplicado a la cartera en este rango de edad; valor para reserva y castigo fiscal', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'DeteriorationFiscalPercent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de deterioro aplicado al libro Fiscal', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'DeteriorationFiscalPercent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'DeteriorationFiscalPercent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de deterioro contable (DECIMAL 5,2) según norma NIIF aplicado a la cartera en este rango de edad; valor para reserva y castigo NIIF', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'DeteriorationNiifPercent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de deterioro aplicado al libro Niif', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'DeteriorationNiifPercent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'DeteriorationNiifPercent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 100) del rango de antigüedad de cartera; ej: ''''0-30 días'''', ''''31-60 días'''', ''''Menor a 30 días'''', ''''Mayor a 90 días''''', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'AgesPortfolioName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la edad de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'AgesPortfolioName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'AgesPortfolioName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de rango de edad de cartera (TINYINT): 0=Edad Intermedia, 1=Edad Mínima, 2=Edad Máxima; clasifica el tramo de antigüedad aplicable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'RangeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de edad: 0=Edad Intermedia, 1=Edad Mínima, 2=Edad Máxima', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'RangeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'RangeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK, nullable) de la edad/antigüedad de cartera relacionada; referencia a AgesPortfolio; NULL cuando RangeType es mínimo o máximo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'AgesPortfolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la edad de cartera relacionado (puede ser NULL para mínimos y máximos)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'AgesPortfolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'AgesPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la clasificación de deterioro de cartera padre; referencia a PortfolioDeteriorationClassification para agrupar detalles por tipo de deterioro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'PortfolioDeteriorationClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Clasificación de deterioro de cartera a la que pertenece el detalle', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'PortfolioDeteriorationClassificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'PortfolioDeteriorationClassificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del detalle de deterioro de cartera; clave primaria que representa cada registro de aplicación de porcentajes de deterioro contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de los porcentajes de aplicación de deterioro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las clasificaciones de deterioro de cartera, con los porcentajes de deterioro NIIF y fiscal aplicados a cada rango de edad de cartera. Permite definir las tasas de provisión por tramos de mora para el cálculo de deterioro contable y tributario.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioDeteriorationClassificationDetails';
