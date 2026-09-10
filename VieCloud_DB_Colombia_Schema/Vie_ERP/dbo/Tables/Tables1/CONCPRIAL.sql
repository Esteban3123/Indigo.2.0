CREATE TABLE [dbo].[CONCPRIAL] (
    [IDCONCURS] TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMBRCONC] VARCHAR (250) NOT NULL,
    [FECINICON] DATETIME      NOT NULL,
    [FECLIMCON] DATETIME      NOT NULL,
    [FECPRECON] DATETIME      NOT NULL,
    [OBJETCONC] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_CONCPRIAL] PRIMARY KEY CLUSTERED ([IDCONCURS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Objetivos, propósito o descripción del concurso (VARCHAR MAX); detalles del fin, metas y criterios de evaluación de la convocatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'OBJETCONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Objetivos del concurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'OBJETCONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'OBJETCONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de entrega de premiación o resultados (DATETIME); momento en que se comunican ganadores o se otorgan reconocimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'FECPRECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Entrega Premiación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'FECPRECON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'FECPRECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de cierre o finalización del concurso (DATETIME); límite máximo para participación, inscripción o entrega de documentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'FECLIMCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final Del Concurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'FECLIMCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'FECLIMCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio del concurso (DATETIME); momento en que abre o comienza la recepción de participantes o propuestas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'FECINICON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial Del Concurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'FECINICON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'FECINICON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o título del concurso (VARCHAR 250); denominación de la convocatoria, concurso de méritos, selección o proceso competitivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'NOMBRCONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Del concurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'NOMBRCONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'NOMBRCONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (TINYINT) del concurso; clave primaria que referencia concursos o convocatorias internas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'IDCONCURS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico del concurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'IDCONCURS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL', @level2type = N'COLUMN', @level2name = N'IDCONCURS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de concursos o convocatorias institucionales, incluyendo sus fechas clave y el objeto o descripción de cada proceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONCPRIAL';
