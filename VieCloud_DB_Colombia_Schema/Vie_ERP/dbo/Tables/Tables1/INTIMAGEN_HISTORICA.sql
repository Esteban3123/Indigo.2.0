CREATE TABLE [dbo].[INTIMAGEN_HISTORICA] (
    [AUTO] INT NOT NULL,
    [ORDEN_INDIGO] VARCHAR (20) NOT NULL,
    [CODSERIPS] VARCHAR (15) NOT NULL,
    [IMAGENLAB] VARBINARY (MAX) NOT NULL,
    [DESIMGLAB] VARCHAR (50) NULL,
    [FECREGSIS] DATETIME NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Repositorio histórico de imágenes de laboratorio o diagnóstico asociadas a órdenes médicas. Guarda el archivo de imagen junto con su descripción, el servicio solicitado y la fecha de registro en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico interno autogenerado, clave única del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de orden médica o solicitud en el sistema Indigo, identifica la orden de examen o procedimiento a la que pertenece la imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio de salud (CUPS) asociado a la imagen, identifica el tipo de examen, procedimiento de laboratorio o imagen diagnóstica solicitado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Archivo binario de la imagen de laboratorio o diagnóstico (radiografía, ecografía, resultado escaneado, etc.), almacenado como documento digital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA', @level2type = N'COLUMN', @level2name = N'IMAGENLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA', @level2type = N'COLUMN', @level2name = N'IMAGENLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre del archivo de imagen de laboratorio, indica qué representa o cómo se identifica la imagen adjunta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA', @level2type = N'COLUMN', @level2name = N'DESIMGLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA', @level2type = N'COLUMN', @level2name = N'DESIMGLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que la imagen fue registrada en el sistema, fecha de carga o ingreso del archivo al expediente digital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTIMAGEN_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
