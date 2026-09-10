CREATE TABLE [HumanTalent].[Pv_TestPeriodExam_StrengthsAndWeaknesses] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TestPeriodExamId] INT          NOT NULL,
    [Strengths]        VARCHAR (50) NULL,
    [Weaknesses]       VARCHAR (50) NULL,
    [CreationUser]     VARCHAR (50) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (50) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_Pv_TestPeriodExam_StrengthsAndWeaknesses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Pv_TestPeriodExam_StrengthsAndWeaknesses_TestPeriodExam] FOREIGN KEY ([TestPeriodExamId]) REFERENCES [HumanTalent].[TestPeriodExam] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de fortalezas y debilidades. Tipo: DATETIME. Registra cuándo se actualizó por última vez la evaluación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro. Tipo: VARCHAR(50). Nombre o código del usuario que editó fortalezas y debilidades.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de evaluación. Tipo: DATETIME. Marca cuándo se registraron inicialmente fortalezas y debilidades.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro inicial. Tipo: VARCHAR(50). Nombre o código del usuario que registró la evaluación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Creación de los Usuarios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Debilidades identificadas en el examen del período de prueba. Tipo: VARCHAR(50). Áreas de mejora o competencias deficitarias del evaluado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'Weaknesses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Debilidades', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'Weaknesses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'Weaknesses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fortalezas identificadas en el examen del período de prueba. Tipo: VARCHAR(50). Competencias desarrolladas o capacidades sobresalientes del evaluado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'Strengths';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fortalezas', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'Strengths';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'Strengths';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del examen de período de prueba asociado. Tipo: INT. FK a TestPeriodExam(Id). Vincula fortalezas y debilidades a su evaluación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'TestPeriodExamId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de examen del período de prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'TestPeriodExamId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'TestPeriodExamId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la tabla Pv_TestPeriodExam_StrengthsAndWeaknesses. Tipo: INT IDENTITY. Clave primaria, registra cada evaluación de fortalezas y debilidades.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fortalezas y debilidades registradas por período de examen en el módulo de Talento Humano. Almacena los resultados cualitativos de evaluaciones o pruebas aplicadas a empleados o candidatos, indicando qué aspectos positivos y áreas de mejora se identificaron en cada prueba.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses';
