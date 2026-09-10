CREATE TABLE [dbo].[INCUPSGRU] (
    [CODGRUIPS] CHAR (10)     NOT NULL,
    [DESGRUIPS] VARCHAR (100) NULL,
    [GRUPOIMAG] BIT           NOT NULL,
    CONSTRAINT [PK_INCUPSGRU] PRIMARY KEY CLUSTERED ([CODGRUIPS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que determina si el grupo de procedimientos/servicios es de tipo imagenología, radiología o diagnóstico por imagen (resonancia, tomografía, ecografía, radiografía). Facilita filtros en órdenes de imagen y clasificación de prestaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU', @level2type = N'COLUMN', @level2name = N'GRUPOIMAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me determina si el grupo es de tipo imagenologia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU', @level2type = N'COLUMN', @level2name = N'GRUPOIMAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU', @level2type = N'COLUMN', @level2name = N'GRUPOIMAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del grupo de procedimientos o servicios (VARCHAR 100). Nombre legible del agrupamiento para identificación en facturación, RIPS, órdenes médicas y consultas de disponibilidad de prestaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU', @level2type = N'COLUMN', @level2name = N'DESGRUIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU', @level2type = N'COLUMN', @level2name = N'DESGRUIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU', @level2type = N'COLUMN', @level2name = N'DESGRUIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del grupo de procedimientos o servicios (CHAR 10). Identificador en sistema de facturación, RIPS y órdenes. Agrupa procedimientos por categoría (imagenología, laboratorio, quirúrgico, etc.) para gestión de prestaciones en centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU', @level2type = N'COLUMN', @level2name = N'CODGRUIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Grupo del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU', @level2type = N'COLUMN', @level2name = N'CODGRUIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU', @level2type = N'COLUMN', @level2name = N'CODGRUIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupos o categorías de servicios CUPS/IPS utilizados para clasificar procedimientos y servicios de salud. Permite agrupar servicios en categorías como imágenes diagnósticas, laboratorio, procedimientos, entre otros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU';
