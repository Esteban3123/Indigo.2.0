CREATE TABLE [dbo].[INTIMAGEN] (
    [AUTO]         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO] VARCHAR (20)    NOT NULL,
    [CODSERIPS]    VARCHAR (15)    NOT NULL,
    [IMAGENLAB]    VARBINARY (MAX) NULL,
    [DESIMGLAB]    VARCHAR (50)    NULL,
    [FECREGSIS]    DATETIME        NULL,
    CONSTRAINT [PK_INTIMAGEN] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_INTIMAGEN__ORDEN_INDIGO__CODSERIPS__INC__DESIMGLAB__IMAGENLAB]
    ON [dbo].[INTIMAGEN]([ORDEN_INDIGO] ASC, [CODSERIPS] ASC)
    INCLUDE([DESIMGLAB], [IMAGENLAB]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la imagen en el sistema; timestamp de auditoría de ingreso del documento imagenológico al EHR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro en el Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción adicional, notas o comentarios clínicos complementarios de la imagen de laboratorio o estudio imagenológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'DESIMGLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Adicional de la Imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'DESIMGLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'DESIMGLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Archivo binario (VARBINARY MAX) que contiene la imagen médica, resultado de laboratorio o estudio imagenológico (radiografía, ecografía, TAC, RMN, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'IMAGENLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagen respectiva al resultado del laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'IMAGENLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'IMAGENLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS prestador; identificador del centro de atención o unidad funcional que generó el estudio imagenológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la orden: concatenación de código del paciente (identificación/cédula) con número de folio secuencial del estudio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden Indigo  Paciente concatenado con el numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable (IDENTITY); clave primaria técnica de la tabla para auditoría y relaciones internas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'AutoNumerico Identity', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena imágenes de resultados de laboratorio o exámenes diagnósticos asociadas a una orden, permitiendo adjuntar archivos gráficos (radiografías, reportes escaneados, imágenes de laboratorio) vinculados a un servicio o procedimiento específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN';
