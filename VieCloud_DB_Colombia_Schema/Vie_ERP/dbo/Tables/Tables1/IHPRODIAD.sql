CREATE TABLE [dbo].[IHPRODIAD] (
    [CODCONCEC]   INT           NOT NULL,
    [CODDIAGNO]   CHAR (4)      NOT NULL,
    [TIPDIAGNOS]  CHAR (1)      NOT NULL,
    [OBSERVACIO]  VARCHAR (MAX) NULL,
    [CODPRODUC]   CHAR (20)     NOT NULL,
    [CODPRODUCHC] CHAR (20)     NULL,
    CONSTRAINT [PK_IHPRODIAD] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODDIAGNO] ASC),
    CONSTRAINT [FK_IHPRODIAD_IHLISTPRO] FOREIGN KEY ([CODPRODUCHC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto en catálogo IHLISTPRO, referencia FK a tabla de lista de productos, identificador único del medicamento/insumo vinculado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODPRODUCHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del producto relacionado con el campo CODPRODUC de la tabla IHLISTPRO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODPRODUCHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODPRODUCHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del producto, identificador único del medicamento, insumo o dispositivo médico en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas del diagnóstico agregadas a la ficha técnica del producto, notas sobre indicaciones, contraindicaciones o reacciones adversas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del diagnostico agregado a la ficha tecnica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de diagnóstico o reacción: 1=Indicaciones, 2=Contraindicaciones, 3=Precauciones, 4=Reacciones Adversas; clasificación de relación producto-diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'TIPDIAGNOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Diagnostico  1= Indicaciones  2= Contraindicaciones  3= Precauciones  4= Reacciones Adversas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'TIPDIAGNOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'TIPDIAGNOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico, identificador único del código CIE-10 u otra clasificación diagnóstica relacionada con el producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo de cabecera de la relación producto-diagnóstico, identificador del registro padre en tabla de diagnóstico de productos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo el consecutivo de la cabecera de la tabla de diagnostico de productos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre productos o servicios de salud y diagnósticos (CIE-10) asociados a una concepción o concepto de cobertura. Permite vincular qué diagnósticos justifican o están permitidos para un producto o servicio determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD';
