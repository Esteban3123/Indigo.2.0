CREATE TABLE [dbo].[HCONCOTOPO] (
    [ID]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADRE]     INT           NULL,
    [CODTOPOGRA]  VARCHAR (15)  NOT NULL,
    [DESCRIPCION] VARCHAR (100) NULL,
    CONSTRAINT [PK_HCONCOTOPOG] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCONCOTOPOG_HCONCOTOPOG] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[HCONCOTOPO] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del sitio topográfico (localización anatómica, región corporal o área de procedimiento); texto libre de hasta 100 caracteres para búsqueda semántica de anatomía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion topografico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del registro topográfico (identificador de localización anatómica, región o sitio de procedimiento); VARCHAR(15) para clasificación de topografía médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'CODTOPOGRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del registro topografico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'CODTOPOGRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'CODTOPOGRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia jerárquica a la tabla HCONCOTOPO (relación padre-hijo); permite estructura de topografías anidadas (ej: región > subregión > sitio específico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con el campo ID de la presente tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de cada registro topográfico; clave primaria para consultas de anatomía y procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo jerárquico de topografías o localizaciones anatómicas utilizadas en historia clínica para registrar la ubicación corporal de hallazgos, diagnósticos o procedimientos. Permite organizar los sitios anatómicos en niveles padre-hijo (por ejemplo: extremidad superior → brazo → codo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCONCOTOPO';
