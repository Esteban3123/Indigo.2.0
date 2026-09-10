CREATE TABLE [dbo].[HCHOMIPRESTDM] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODMIPRES]   VARCHAR (3)   NOT NULL,
    [DESCRIPCION] VARCHAR (100) NOT NULL,
    [ESTADO]      BIT           NOT NULL,
    CONSTRAINT [PK__HCHOMIPRESTDM] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro en MIPRES (activo/inactivo). Bit que indica si el tipo de dispositivo médico está habilitado en el sistema de información de prestaciones en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de registro en mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de dispositivo médico MIPRES. Nombre o detalle del dispositivo para identificación en solicitudes, prescripciones y reportes de información de prestaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de tipo de dispositivo médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de dispositivo médico MIPRES. Identificador único de 3 caracteres que clasifica el dispositivo médico conforme a la información de prestaciones en salud (MIPRES).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM', @level2type = N'COLUMN', @level2name = N'CODMIPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de tipo de dispositivo médico mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM', @level2type = N'COLUMN', @level2name = N'CODMIPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM', @level2type = N'COLUMN', @level2name = N'CODMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) de la tabla HCHOMIPRESTDM. Valor incremental que registra cada tipo de dispositivo médico MIPRES en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador  de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos o modalidades de prescripción MIPRES (plataforma del Ministerio de Salud para prescripción de tecnologías no incluidas en el plan de beneficios). Permite clasificar los diferentes tipos de prescripción habilitados en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESTDM';
