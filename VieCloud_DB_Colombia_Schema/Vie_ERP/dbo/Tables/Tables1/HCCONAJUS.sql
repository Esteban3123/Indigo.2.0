CREATE TABLE [dbo].[HCCONAJUS] (
    [CODCONAJU] CHAR (3)     NOT NULL,
    [DESCODAJU] VARCHAR (50) NOT NULL,
    [ESTADOCOD] BIT          NOT NULL,
    CONSTRAINT [PK_HCCONAJUS] PRIMARY KEY CLUSTERED ([CODCONAJU] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del concepto de ajuste (activo/inactivo). Indicador booleano que controla si el tipo de ajuste está habilitado para usar en glosas, facturas o movimientos contables del EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS', @level2type = N'COLUMN', @level2name = N'ESTADOCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del concepto de ajuste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS', @level2type = N'COLUMN', @level2name = N'ESTADOCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS', @level2type = N'COLUMN', @level2name = N'ESTADOCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del concepto de ajuste. Nombre o etiqueta del tipo de ajuste (ej: reintegro, descuento, sobrevalor, corrección). Texto de referencia para búsqueda y reportes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS', @level2type = N'COLUMN', @level2name = N'DESCODAJU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del concepto de ajuste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS', @level2type = N'COLUMN', @level2name = N'DESCODAJU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS', @level2type = N'COLUMN', @level2name = N'DESCODAJU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del concepto de ajuste. Identificador único de 3 caracteres que clasifica el tipo de ajuste a aplicar en facturas, glosas o reclamaciones. Clave primaria para búsqueda rápida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS', @level2type = N'COLUMN', @level2name = N'CODCONAJU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Concepto de Ajuste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS', @level2type = N'COLUMN', @level2name = N'CODCONAJU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS', @level2type = N'COLUMN', @level2name = N'CODCONAJU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de concepto de ajuste utilizados en historia clínica. Permite clasificar y gestionar los diferentes motivos o categorías de ajuste aplicables en los registros clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS';
