CREATE TABLE [dbo].[INTERDETA_HISTORICA] (
    [AUTO] INT NOT NULL,
    [CODCONCEC] INT NOT NULL,
    [AUTOLABOR] INT NOT NULL,
    [CODSERIPS] CHAR (20) NOT NULL,
    [NUMEFOLIO] CHAR (10) NULL,
    [ORDTIP] CHAR (3) NULL,
    [TIPOINTERFAZ] TINYINT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro histórico de la interfaz de órdenes de laboratorio e imágenes, que guarda el detalle de cada servicio solicitado y su integración con sistemas externos de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro (llave autogenerada).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del concepto o encabezado de la orden de laboratorio/imagen al que pertenece este detalle.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador automático del laboratorio o sistema externo asociado a la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio solicitado (CUPS), identifica el examen, procedimiento de laboratorio o imagen diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o consecutivo del documento/orden generado para el servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de orden médica (por ejemplo: laboratorio, imagen, patología); clasifica la naturaleza de la solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'ORDTIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'ORDTIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de interfaz utilizada para la integración con el sistema externo (identifica el canal o protocolo de comunicación).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'TIPOINTERFAZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERDETA_HISTORICA', @level2type = N'COLUMN', @level2name = N'TIPOINTERFAZ';
