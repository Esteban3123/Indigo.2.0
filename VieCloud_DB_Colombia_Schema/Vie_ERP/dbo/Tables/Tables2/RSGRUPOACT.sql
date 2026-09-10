CREATE TABLE [dbo].[RSGRUPOACT] (
    [ID]        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODACTMED] CHAR (3) NOT NULL,
    [IDHCGRUPO] INT      NOT NULL,
    CONSTRAINT [PK_HCGRUPOACT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RSGRUPOACT_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_RSGRUPOACT_RSGRUPO] FOREIGN KEY ([IDHCGRUPO]) REFERENCES [dbo].[RSGRUPO] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de revisión por sistema en la historia clínica dinámica; clave foránea hacia la tabla RSGRUPO que agrupa actividades médicas por categoría sistémica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT', @level2type = N'COLUMN', @level2name = N'IDHCGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del Grupo de Revisión por sistema de la HC dinámica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT', @level2type = N'COLUMN', @level2name = N'IDHCGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT', @level2type = N'COLUMN', @level2name = N'IDHCGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tres caracteres de la actividad médica (procedimiento, consulta, examen o servicio); referencia a AGACTIMED que define el catálogo de actividades asistenciales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la asociación entre grupo de revisión y actividad médica en la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agrupa actividades médicas dentro de grupos de historia clínica. Permite asociar códigos de actividades o procedimientos médicos a un grupo específico de historia clínica para su clasificación y presentación en formularios clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSGRUPOACT';
