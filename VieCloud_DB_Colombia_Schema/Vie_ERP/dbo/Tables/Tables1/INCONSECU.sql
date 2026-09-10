CREATE TABLE [dbo].[INCONSECU] (
    [IDCONSECU] CHAR (8)     NOT NULL,
    [CONSECDES] CHAR (80)    NOT NULL,
    [CONNUMACT] DECIMAL (18) NOT NULL,
    CONSTRAINT [PK_INCONSECU] PRIMARY KEY CLUSTERED ([IDCONSECU] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo actual, valor numérico decimal que registra el último consecutivo asignado en la secuencia de numeración para documentos, facturación, ingresos o procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU', @level2type = N'COLUMN', @level2name = N'CONNUMACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero consecutivo actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU', @level2type = N'COLUMN', @level2name = N'CONNUMACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU', @level2type = N'COLUMN', @level2name = N'CONNUMACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del consecutivo, texto que identifica el tipo o propósito de la secuencia numerada (ej: facturación, RIPS, recetas, ingresos, atenciones).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU', @level2type = N'COLUMN', @level2name = N'CONSECDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descricion del consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU', @level2type = N'COLUMN', @level2name = N'CONSECDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU', @level2type = N'COLUMN', @level2name = N'CONSECDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo, identificador único alfanumérico que clasifica y referencia cada tipo de secuencia de numeración utilizada en el sistema de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU', @level2type = N'COLUMN', @level2name = N'IDCONSECU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU', @level2type = N'COLUMN', @level2name = N'IDCONSECU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU', @level2type = N'COLUMN', @level2name = N'IDCONSECU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de consecutivos del sistema: guarda cada secuencia numérica utilizada en el ERP/EHR (por ejemplo numeración de ingresos, facturas, órdenes, etc.), su descripción y el valor actual del contador para garantizar unicidad en los identificadores generados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU';
