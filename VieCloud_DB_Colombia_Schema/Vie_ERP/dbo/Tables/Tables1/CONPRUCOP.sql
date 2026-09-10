CREATE TABLE [dbo].[CONPRUCOP] (
    [IDCONPRCP] INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDGRUPUSC] TINYINT   NOT NULL,
    [CODUSUARI] CHAR (20) NOT NULL,
    [IDCONCPRE] TINYINT   NOT NULL,
    [CONVALRES] INT       NOT NULL,
    [CONFCHPR]  DATETIME  NOT NULL,
    CONSTRAINT [PK_CONPRUCOP] PRIMARY KEY CLUSTERED ([IDCONPRCP] ASC),
    CONSTRAINT [FK_CONPRUCOP_CONCPREGC] FOREIGN KEY ([IDCONCPRE]) REFERENCES [dbo].[CONCPREGC] ([IDCONCPRE]),
    CONSTRAINT [FK_CONPRUCOP_CONGRPUSC] FOREIGN KEY ([IDGRUPUSC]) REFERENCES [dbo].[CONGRPUSC] ([IDGRUPUSC])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de presentación de la pregunta. Tipo: DATETIME. Registra cuándo el usuario respondió o presentó la pregunta en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'CONFCHPR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha presentación pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'CONFCHPR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'CONFCHPR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación asignada por la respuesta proporcionada. Tipo: INT. Valor numérico que acumula puntos según la calidad o corrección de la respuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'CONVALRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntuación por la respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'CONVALRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'CONVALRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autoincremental) de la pregunta presentada. Tipo: TINYINT. Referencia a CONCPREGC. Clave foránea para vincular con el banco de preguntas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'IDCONCPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la pregunta presentada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'IDCONCPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'IDCONCPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que presenta o responde la pregunta. Tipo: CHAR(20). Identificación del profesional de salud o personal que interactúa con el cuestionario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que presenta la pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo del grupo de usuarios. Tipo: TINYINT. Referencia a CONGRPUSC. Clasifica el usuario dentro de un grupo funcional o de permisos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo código grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autoincremental) del registro de acumulación de puntos. Tipo: INT. Clave primaria que registra cada presentación de pregunta y puntuación asociada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'IDCONPRCP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico Acumulacion de puntos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'IDCONPRCP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'IDCONPRCP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Restricciones y permisos de acceso por usuario: define qué valores o rangos de información puede consultar o procesar cada usuario del sistema, según su grupo y concepto de restricción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP';
