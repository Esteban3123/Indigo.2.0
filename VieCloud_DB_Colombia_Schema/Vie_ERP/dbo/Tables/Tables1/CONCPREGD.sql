CREATE TABLE [dbo].[CONCPREGD] (
    [IDCONCRES] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONCPRE] TINYINT       NOT NULL,
    [CONCRESPT] VARCHAR (250) NOT NULL,
    [CONESTADO] BIT           NOT NULL,
    [CONCOMENT] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_CONCPREGD] PRIMARY KEY CLUSTERED ([IDCONCRES] ASC),
    CONSTRAINT [FK_CONCPREGD_CONCPREGC] FOREIGN KEY ([IDCONCPRE]) REFERENCES [dbo].[CONCPREGC] ([IDCONCPRE])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario adicional o notas complementarias sobre la respuesta, texto libre de hasta 2GB (VARCHAR MAX), opcional para justificaciones o aclaraciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'CONCOMENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'comentario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'CONCOMENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'CONCOMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo de la respuesta (BIT: 1=Activo, 0=Inactivo), indica si la opción de respuesta está vigente o deshabilitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'CONESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'CONESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'CONESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto o contenido de la respuesta a la pregunta de consentimiento, descripción de hasta 250 caracteres que el usuario verá como opción seleccionable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'CONCRESPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Texto de la respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'CONCRESPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'CONCRESPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación a pregunta padre (FK → CONCPREGC.IDCONCPRE), vincula esta respuesta a su pregunta de consentimiento asociada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'IDCONCPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación Pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'IDCONCPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'IDCONCPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de la respuesta de consentimiento (PK, INT IDENTITY), clave primaria para ubicar unívocamente cada opción de respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'IDCONCRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico Respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'IDCONCRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD', @level2type = N'COLUMN', @level2name = N'IDCONCRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuestas o puntos posibles de una pregunta de conciliación o encuesta clínica. Cada registro representa una opción de respuesta asociada a una pregunta específica, con su estado activo/inactivo y comentarios adicionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPREGD';
