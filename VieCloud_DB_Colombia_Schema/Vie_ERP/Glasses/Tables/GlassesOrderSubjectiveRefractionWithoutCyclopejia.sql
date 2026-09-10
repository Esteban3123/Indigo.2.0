CREATE TABLE [Glasses].[GlassesOrderSubjectiveRefractionWithoutCyclopejia] (
    [Id]                   INT          IDENTITY (1, 1) NOT NULL,
    [IdGlassesOrderC]      INT          NOT NULL,
    [Eye]                  INT          NOT NULL,
    [Sphere]               VARCHAR (10) NOT NULL,
    [Cylinder]             VARCHAR (10) NOT NULL,
    [Axis]                 VARCHAR (10) NOT NULL,
    [Adition]              VARCHAR (20) NOT NULL,
    [FarVisualAcuity]      INT          NOT NULL,
    [ProximalVisualAcuity] INT          NOT NULL,
    [CODPRODUC]            CHAR (20)    NOT NULL,
    [Pinhole]              INT          NULL,
    CONSTRAINT [PK_GlassesOrderSubjectiveRefractionWithoutCyclopejia] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlassesOrderSubjectiveRefractionWithoutCyclopejia_IdGlassesOrderC] FOREIGN KEY ([IdGlassesOrderC]) REFERENCES [Glasses].[GlassesOrderC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto/dispositivo óptico asociado a la refracción subjetiva sin cicloplejía (lentes, montura, dispositivo oftálmico). VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto asociado a la refracción (en este caso será el codigo de un dispositivo)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual proximal/cercana (visión a corta distancia). INT. Medida en escala oftalmológica estándar.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'ProximalVisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo agudeza visual proxima', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'ProximalVisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'ProximalVisualAcuity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual lejana/distancia (visión a larga distancia). INT. Medida en escala oftalmológica estándar.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'FarVisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo agudeza visual lejana', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'FarVisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'FarVisualAcuity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Adición/poder adicional para visión próxima (bifocal/progresivo). VARCHAR(20). Diferencia dióptrica para lectura.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Adition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo adición', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Adition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Adition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eje/meridiano del cilindro (orientación astigmática). VARCHAR(10). Ángulo en grados (0-180°).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Eje', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Axis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cilindro/poder cilíndrico (corrección astigmatismo). VARCHAR(10). Valor dióptrico.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo cilindro', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Cylinder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esfera/poder esférico (corrección miopía/hipermetropía). VARCHAR(10). Valor dióptrico.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Esfera', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Sphere';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo examinado/refractado. INT. 1=Ojo Derecho (OD), 2=Ojo Izquierdo (OI).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo ojo: 1:Derecho, 2 :Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador FK de la orden/pedido de gafas (tabla GlassesOrderC). INT. Referencia a orden de prescripción óptica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla GlassesOrderC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos de la refracción subjetiva sin ciclopejia registrada en una orden de gafas, incluyendo los valores ópticos por ojo (esfera, cilindro, eje, adición) y la agudeza visual para lejos y cerca.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de refracción subjetiva sin ciclopejia.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de la agudeza visual con agujero estenopeico (pinhole), usado para evaluar si una baja visión es de origen refractivo o patológico.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Pinhole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithoutCyclopejia', @level2type = N'COLUMN', @level2name = N'Pinhole';
