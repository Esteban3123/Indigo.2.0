CREATE TABLE [Glasses].[GlassesOrderObjectiveRefraction] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [IdGlassesOrderC] INT          NOT NULL,
    [TypeRefraction]  INT          NOT NULL,
    [Eye]             INT          NOT NULL,
    [Sphere]          VARCHAR (10) NOT NULL,
    [Cylinder]        VARCHAR (10) NOT NULL,
    [Axis]            VARCHAR (10) NOT NULL,
    [CODPRODUC]       CHAR (20)    NOT NULL,
    CONSTRAINT [PK_GlassesOrderObjectiveRefraction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlassesOrderObjectiveRefraction_GlassesOrderC] FOREIGN KEY ([IdGlassesOrderC]) REFERENCES [Glasses].[GlassesOrderC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto/dispositivo óptico asociado a la refracción objetiva. Identificador único del dispositivo de corrección visual (lentes, marcos, monturas) prescrito en la orden de gafas.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto asociado a la refracción (en este caso será el codigo de un dispositivo)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eje de la refracción objetiva en grados (0-180). Parámetro óptico que indica la orientación del cilindro en la corrección astigmática del ojo evaluado.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo eje', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Axis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cilindro de la refracción objetiva en dioptrías. Valor óptico que corrige el astigmatismo detectado en el examen refractivo objetivo del ojo.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo cilindro', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Cylinder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esfera de la refracción objetiva en dioptrías. Valor fundamental de la corrección óptica para miopía o hipermetropía en el examen refractivo objetivo.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo esfera', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Sphere';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ojo evaluado: 1=Ojo Derecho (OD), 2=Ojo Izquierdo (OI). Campo que especifica a cuál ojo pertenece la medición refractiva.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo ojo: 1: Derecho, 2:Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo/método de refracción objetiva: 1=Con cicloplejia (parálisis acomodativa), 2=Sin cicloplejia (refracción dinámica). Indica si la medición fue realizada bajo relajación medicamentosa o en estado natural.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'TypeRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo tipo de refraccion: 1: con cicloplejia, 2: sin cicloplejia (Aplica solo para esta tabla)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'TypeRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'TypeRefraction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con la orden de gafas (FK a [Glasses].[GlassesOrderC]). Referencia a la solicitud de corrección óptica/dispositivo visual que agrupa los parámetros refractivos de ambos ojos.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla GlassesOrderC', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'IdGlassesOrderC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de refracción objetiva de una orden de gafas. Guarda los valores ópticos medidos (esfera, cilindro, eje) por ojo y tipo de refracción, asociados a un producto óptico específico dentro de la orden.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de refracción objetiva.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'GlassesOrderObjectiveRefraction', @level2type = N'COLUMN', @level2name = N'Id';
