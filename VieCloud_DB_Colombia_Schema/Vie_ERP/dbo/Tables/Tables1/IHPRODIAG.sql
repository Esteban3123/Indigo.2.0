CREATE TABLE [dbo].[IHPRODIAG] (
    [AUTO]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPRODUC]   CHAR (20)     NOT NULL,
    [CODDIAGNO]   CHAR (4)      NOT NULL,
    [TIPDIAGNOS]  CHAR (1)      NOT NULL,
    [OBSERVACIO]  VARCHAR (MAX) NULL,
    [CODPRODUCHC] CHAR (20)     NULL,
    CONSTRAINT [PK_IHPRODIAG] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_IHPRODIAG_IHLISTPRO] FOREIGN KEY ([CODPRODUCHC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto relacionado (CHAR 20, FK a IHLISTPRO.CODPRODUC); referencia al producto maestro en tabla de listado de productos; permite normalizar y vincular con catálogo central de medicamentos e insumos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'CODPRODUCHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del producto relacionado con el campo CODPRODUC de la tabla IHLISTPRO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'CODPRODUCHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'CODPRODUCHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas clínicas o comentarios adjuntos al diagnóstico en la ficha técnica del producto (VARCHAR MAX); información complementaria sobre indicación, contraindicación, precaución o reacción adversa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del diagnostico agregado a la ficha tecnica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de diagnóstico (CHAR 1): 1=Indicaciones, 2=Contraindicaciones, 3=Precauciones, 4=Reacciones Adversas; categoriza el tipo de relación clínica entre producto y diagnóstico en ficha técnica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'TIPDIAGNOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de diagnostico agregado  1-Indicaciones 2- Contraindicaciones 3 -Precauciones 4-Reacciones Adversas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'TIPDIAGNOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'TIPDIAGNOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (CHAR 4), clasificación clínica (CIE-10 u otro estándar) asociado al producto; diagnóstico, enfermedad, condición o indicación médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del producto (CHAR 20), identificador único del medicamento, dispositivo o insumo en el catálogo de productos del ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de la relación producto-diagnóstico en ficha técnica; clave primaria de tabla IHPRODIAG.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de ficha tecnica diagnosticos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre productos o servicios de salud y sus diagnósticos asociados (CIE-10). Permite indicar qué diagnósticos están vinculados a cada producto o procedimiento, incluyendo el tipo de diagnóstico y observaciones clínicas relevantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG';
