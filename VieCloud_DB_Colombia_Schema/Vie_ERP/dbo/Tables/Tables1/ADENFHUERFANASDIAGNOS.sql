CREATE TABLE [dbo].[ADENFHUERFANASDIAGNOS] (
    [ID]               INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDADENFHUERFANAS] INT      NOT NULL,
    [CODDIAGNO]        CHAR (4) NOT NULL,
    CONSTRAINT [PK_ADENFHUERFANASDIAGNOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADENFHUERFANASDIAGNOS_ADENFHUERFANAS] FOREIGN KEY ([IDADENFHUERFANAS]) REFERENCES [dbo].[ADENFHUERFANAS] ([ID]),
    CONSTRAINT [FK_ADENFHUERFANASDIAGNOS_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[ADENFHUERFANASDIAGNOS] NOCHECK CONSTRAINT [FK_ADENFHUERFANASDIAGNOS_INDIAGNOS];




GO



GO
ALTER TABLE [dbo].[ADENFHUERFANASDIAGNOS] NOCHECK CONSTRAINT [FK_ADENFHUERFANASDIAGNOS_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (CIE-10, CUPS u otra clasificación); referencia a tabla INDIAGNOS; permite vincular diagnósticos clínicos a enfermedades huérfanas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda elcodigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la enfermedad huérfana; relación con tabla ADENFHUERFANAS; vincula cada diagnóstico a su registro de enfermedad rara o de baja prevalencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de las enfermedades huerfanas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la relación diagnóstico-enfermedad huérfana; clave primaria; consecutivo autoincrementable para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los ingresos o atenciones de enfermedades huérfanas con sus diagnósticos CIE-10 asociados. Permite registrar uno o varios diagnósticos para cada caso de enfermedad huérfana o rara.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS';
