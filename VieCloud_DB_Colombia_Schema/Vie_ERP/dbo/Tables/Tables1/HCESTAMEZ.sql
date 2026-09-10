CREATE TABLE [dbo].[HCESTAMEZ] (
    [CODPRODUC] CHAR (20) NOT NULL,
    [CODPRODIL] CHAR (20) NOT NULL,
    [TIEESTMEZ] INT       NOT NULL,
    CONSTRAINT [PK_HCESTAMEZ] PRIMARY KEY CLUSTERED ([CODPRODUC] ASC, [CODPRODIL] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de estabilidad de la mezcla en minutos/horas. Período durante el cual la combinación de productos mantiene sus propiedades físico-químicas sin degradación. Tipo SQL: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ', @level2type = N'COLUMN', @level2name = N'TIEESTMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de Estabilidad de la Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ', @level2type = N'COLUMN', @level2name = N'TIEESTMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ', @level2type = N'COLUMN', @level2name = N'TIEESTMEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del producto diluyente usado en la mezcla. Identificador único del agente diluyente o vehículo que se mezcla con el producto principal. Tipo SQL: CHAR(20). Referencia a tabla de productos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ', @level2type = N'COLUMN', @level2name = N'CODPRODIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Producto Diluyente de la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ', @level2type = N'COLUMN', @level2name = N'CODPRODIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ', @level2type = N'COLUMN', @level2name = N'CODPRODIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del producto principal de la mezcla. Identificador único del fármaco, sustancia o compuesto base que se estabiliza. Tipo SQL: CHAR(20). Clave primaria compuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de tiempos de estabilidad de mezclas de productos farmacéuticos o de nutrición. Indica cuánto tiempo (en alguna unidad) una mezcla entre dos productos (producto base y producto diluido/componente) permanece estable una vez preparada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESTAMEZ';
