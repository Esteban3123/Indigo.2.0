CREATE TABLE [Glasses].[GlassesOrderSubjectiveRefractionWithCyclopejia] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [IdGlassesOrderC] INT          NOT NULL,
    [Eye]             INT          NOT NULL,
    [Sphere]          VARCHAR (10) NOT NULL,
    [Cylinder]        VARCHAR (10) NOT NULL,
    [Axis]            VARCHAR (10) NOT NULL,
    [VisualAcuity]    INT          NOT NULL,
    [Adition]         VARCHAR (20) NOT NULL,
    [CODPRODUC]       CHAR (20)    NOT NULL,
    [Pinhole]         INT          NULL,
    CONSTRAINT [PK_GlassesOrderSubjectiveRefractionWithCyclopejia] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlassesOrderSubjectiveRefractionWithCyclopejia_GlassesOrderC] FOREIGN KEY ([IdGlassesOrderC]) REFERENCES [Glasses].[GlassesOrderC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba de pinhole (agujero estenopeico) con refracción subjetiva bajo cicloplejía; mejora agudeza si error refractivo presente (INT: 0=No, 1=Sí)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Pinhole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Refracción subjetiva con cicloplejía (pinhole)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Pinhole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Pinhole';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto/dispositivo de lentes (dispositivo médico) asociado a la refracción; referencia a catálogo de gafas/lentes', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto asociado a la refracción (en este caso será el codigo de un dispositivo)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Adición o poder adicional para visión próxima (bifocales/progresivos) en refracción con cicloplejía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Adition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo adicion', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Adition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Adition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual (INT, escala 0-100 o decimal 20/x) resultante tras refracción con cicloplejía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'VisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo agudeza visual', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'VisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'VisualAcuity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eje del cilindro (0-180 grados) en la refracción subjetiva con cicloplejía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo eje', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Axis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Potencia cilíndrica (astigmatismo en dioptrías) de la refracción subjetiva con cicloplejía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo cilindro', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Cylinder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Potencia esférica (graduación en dioptrías) medida en refracción subjetiva con cicloplejía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo esfera', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Sphere';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo evaluado: 1=Derecho (OD), 2=Izquierdo (OI); especifica lateralidad en refracción', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo ojo: 1: Derecho, 2:Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea que enlaza con la orden de gafas (GlassesOrderC); identifica el pedido de lentes asociado', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla GlassesOrderC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo (IDENTITY) de la refracción subjetiva con cicloplejía', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda los resultados de la refracción subjetiva con cicloplejia (dilatación pupilar) de órdenes de gafas, registrando los valores ópticos por ojo (esfera, cilindro, eje, agudeza visual y adición) necesarios para formular lentes correctivos.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderSubjectiveRefractionWithCyclopejia';
