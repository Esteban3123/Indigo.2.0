CREATE TABLE [dbo].[HCTABPARAD] (
    [ID]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCTABPARAC] INT          NOT NULL,
    [PARACLINICO]  VARCHAR (15) NOT NULL,
    [ORDEN]        INT          NOT NULL,
    CONSTRAINT [PK_HCTABPARAD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCTABPARAD_HCTABPARAC] FOREIGN KEY ([IDHCTABPARAC]) REFERENCES [dbo].[HCTABPARAC] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición numérica para ordenamiento y secuencia de paraclinicos en la visualización y procesamiento de resultados de exámenes, laboratorios e imágenes diagnósticas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'ORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Posicion para el ordenamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'ORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'ORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del tipo de paraclinico: examen de laboratorio, imagen diagnóstica, procedimiento paraclínico, estudio complementario, prueba diagnóstica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'PARACLINICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paraclinico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'PARACLINICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'PARACLINICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de FK referenciando tabla HCTABPARAC; llave foránea que vincula el detalle de paraclinicos (exámenes, laboratorios, imágenes) con su configuración maestro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'IDHCTABPARAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla HCTABPARAC (Tabla Paraclinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'IDHCTABPARAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'IDHCTABPARAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la tabla; clave primaria para identificar cada registro de detalle de paraclinicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de parámetros clínicos (paraclínicos) asociados a grupos o categorías de paraclínicos en historia clínica. Registra cada examen o prueba paraclínica vinculada a una categoría, con su orden de presentación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTABPARAD';
