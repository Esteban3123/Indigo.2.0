CREATE TABLE [Common].[ISO4217] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [CodeAbbreviation] VARCHAR (3)  NOT NULL,
    [CurrencyName]     VARCHAR (25) NOT NULL,
    CONSTRAINT [PK_ISO4217__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la divisa o moneda (ej: Peso Colombiano, Dólar Estadounidense, Euro). Texto descriptivo de hasta 25 caracteres para identificar la moneda en reportes y facturas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217', @level2type = N'COLUMN', @level2name = N'CurrencyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la divisa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217', @level2type = N'COLUMN', @level2name = N'CurrencyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217', @level2type = N'COLUMN', @level2name = N'CurrencyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código ISO 4217 de 3 caracteres alfabéticos que identifica unívocamente la divisa (ej: COP, USD, EUR). Utilizado en RIPS, transacciones, cotizaciones y facturación electrónica.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217', @level2type = N'COLUMN', @level2name = N'CodeAbbreviation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de 3 caracteres de la divisa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217', @level2type = N'COLUMN', @level2name = N'CodeAbbreviation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217', @level2type = N'COLUMN', @level2name = N'CodeAbbreviation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la tabla ISO4217. Clave primaria para referencia interna en relaciones de monedas y conversiones de divisas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de monedas y divisas según el estándar internacional ISO 4217. Contiene los códigos y nombres oficiales de cada moneda (por ejemplo: COP para peso colombiano, USD para dólar americano, EUR para euro).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217';
