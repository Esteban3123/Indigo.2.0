CREATE TABLE [Glasses].[GlassesOrderFormulaWithoutRefraction] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [IdGlassesOrderC] INT          NOT NULL,
    [Eye]             INT          NOT NULL,
    [Sphere]          VARCHAR (10) NOT NULL,
    [Cylinder]        VARCHAR (10) NOT NULL,
    [Axis]            VARCHAR (10) NOT NULL,
    [Adition]         VARCHAR (10) NOT NULL,
    [CODPRODUC]       CHAR (20)    NOT NULL,
    CONSTRAINT [PK_GlassesOrderFormulaWithoutRefraction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlassesOrderFormulaWithoutRefraction_GlassesOrderC] FOREIGN KEY ([IdGlassesOrderC]) REFERENCES [Glasses].[GlassesOrderC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto, dispositivo oftalmológico, lentes, gafas, armazón. VARCHAR(20), identificador único del producto.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto (En este caso dispositivos oftalmológicos)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Adición, potencia adicional para visión cercana en lentes progresivos o bifocales. VARCHAR(10), valor dióptrico.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Adition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo adicion', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Adition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Adition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eje, orientación del cilindro en grados (0-180°) para corrección del astigmatismo. VARCHAR(10), parámetro refractivo.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo eje', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Axis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cilindro, potencia de corrección del astigmatismo en dioptrías. VARCHAR(10), valor refractivo negativo o positivo.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo cilindro', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Cylinder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esfera, potencia de corrección principal para miopía o hipermetropía en dioptrías. VARCHAR(10), valor refractivo fundamental.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo esfera', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Sphere';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo: 1=derecho (OD/ojo derecho), 2=izquierdo (OI/ojo izquierdo). INT, indicador lateral de la prescripción.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo ojo: 1: derecho, 2:izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de gafas, clave foránea a GlassesOrderC. INT, referencia a la orden padre del pedido oftalmológico.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla GlassesOrdrC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líneas de fórmula óptica sin refracción asociadas a una orden de gafas. Guarda los valores de la prescripción (esfera, cilindro, eje y adición) por ojo, junto con el producto óptico asignado, para órdenes que no requieren examen de refracción previo.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno de cada línea de fórmula óptica sin refracción.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderFormulaWithoutRefraction', @level2type = N'COLUMN', @level2name = N'Id';
