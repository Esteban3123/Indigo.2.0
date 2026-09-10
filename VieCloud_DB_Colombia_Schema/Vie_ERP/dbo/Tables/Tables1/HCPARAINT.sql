CREATE TABLE [dbo].[HCPARAINT] (
    [CODCONCEC] INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10)       NOT NULL,
    [VALMEDESP] BIT             NOT NULL,
    [VALMEDGEN] BIT             NOT NULL,
    [CALFOLIOS] INT             NULL,
    [CALFDESDE] DECIMAL (18, 1) NULL,
    [CALFHASTA] DECIMAL (18, 1) NULL,
    CONSTRAINT [PK_HCPARAINT] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCPARAINT_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor final o máximo de la calificación, rango superior para evaluación académica de historias clínicas (DECIMAL 18,1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CALFHASTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Final de la Calificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CALFHASTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CALFHASTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor inicial o mínimo de la calificación, rango inferior para evaluación académica de historias clínicas (DECIMAL 18,1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CALFDESDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Inicial de la Calificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CALFDESDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CALFDESDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de calificación requerida para folios académicos: 1=Exigir calificación obligatoria, 2=Calificación opcional, 3=Ninguno (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CALFOLIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Calificacion Folios Academicos   1: Exigir Calificación  2: Calificación Opcional  3: Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CALFOLIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CALFOLIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano para validar/requerir calificación de historias clínicas académicas por médicos generales (BIT, 0=No, 1=Sí)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'VALMEDGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Validar Historias Clinicas Academicas - Medicos Generales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'VALMEDGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'VALMEDGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano para validar/requerir calificación de historias clínicas académicas por médicos especialistas (BIT, 0=No, 1=Sí)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'VALMEDESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Validar Historias Clinicas Academicas - Medicos Especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'VALMEDESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'VALMEDESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, unidad funcional o institución de salud (CHAR 10, FK ADCENATEN.CODCENATE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo identificador único de los parámetros internos de configuración (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de los parametros de Internos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración de historia clínica por centro de atención: define si se valida firma médica especialista o general, y los rangos de folios permitidos para la documentación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAINT';
