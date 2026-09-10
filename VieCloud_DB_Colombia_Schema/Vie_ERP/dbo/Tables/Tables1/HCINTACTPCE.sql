CREATE TABLE [dbo].[HCINTACTPCE] (
    [IDINTACT]     INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODINTPCE]    INT         NOT NULL,
    [CODACTPCE]    INT         NOT NULL,
    [CODPLANCENF]  VARCHAR (3) NOT NULL,
    [CODVALORAPCE] INT         NOT NULL,
    [CODDIAGENF]   VARCHAR (5) NOT NULL,
    [IDHCCLASENF]  INT         NULL,
    CONSTRAINT [PK_HCINTACTPCE] PRIMARY KEY CLUSTERED ([IDINTACT] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCINTACTPCE]
    ON [dbo].[HCINTACTPCE]([CODPLANCENF] ASC, [CODVALORAPCE] ASC, [IDHCCLASENF] ASC, [CODDIAGENF] ASC, [CODINTPCE] ASC, [CODACTPCE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la clase de enfermería, clasificación o tipología de la intervención de cuidado en el plan de enfermería (FK referencial, INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la clase', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico de enfermería, diagnóstico del paciente registrado en la historia clínica enfermería (VARCHAR 5, ej: CIE-10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código dominio, clasificador de valor o dominio funcional asignado a la actividad PCE en enfermería (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código dominio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código plan de enfermería, identificador del plan de atención o cuidado enfermero del paciente (VARCHAR 3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código plan de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la actividad PCE, identificador de actividad, procedimiento o intervención de cuidado de enfermería (INT, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODACTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la actividad PCE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODACTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODACTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico intervención, identificador del diagnóstico enfermero relacionado con la intervención o actividad PCE (INT, FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODINTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código diagnóstico intervención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODINTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODINTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo, PK auto-incremental de la intersección diagnóstico-intervención-actividad de enfermería (INT IDENTITY).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'IDINTACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'IDINTACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'IDINTACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los valores de las actividades de enfermería aplicadas a cada intervención de cuidado del paciente, asociando el plan de cuidados, el diagnóstico de enfermería y la clasificación de enfermería correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE';
