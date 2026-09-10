CREATE TABLE [dbo].[HCPCEINTACT] (
    [IDINTACT]       INT IDENTITY (1, 1) NOT NULL,
    [IDDIAGINT]      INT NOT NULL,
    [CODACTPCE]      INT NOT NULL,
    [IDHCPCECONTROL] INT NOT NULL,
    CONSTRAINT [PK_HCPCEINTACT] PRIMARY KEY CLUSTERED ([IDINTACT] ASC),
    CONSTRAINT [FK_HCPCEINTACT_HCACTIVIPCE] FOREIGN KEY ([CODACTPCE]) REFERENCES [dbo].[HCACTIVIPCE] ([CODACTPCE]),
    CONSTRAINT [FK_HCPCEINTACT_HCPCEDIAGINT] FOREIGN KEY ([IDDIAGINT]) REFERENCES [dbo].[HCPCEDIAGINT] ([IDDIAGINT])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCPCEINTACT_IDDIAGINT_IDHCPCECONTROL_CODACTPCE]
    ON [dbo].[HCPCEINTACT]([IDDIAGINT] ASC, [IDHCPCECONTROL] ASC)
    INCLUDE([CODACTPCE]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del control del plan de enfermería. Clave foránea que vincula intervenciones y actividades al registro de seguimiento y evaluación del plan de cuidados de enfermería del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el id del control del plan de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de actividades de enfermería. Referencia a catálogo de actividades/intervenciones de enfermería (tabla HCACTIVIPCE). Identifica la acción específica a realizar en el plan de cuidados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'CODACTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda codigo actividades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'CODACTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'CODACTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del diagnóstico de enfermería e intervenciones asociadas. Clave foránea que vincula la intervención al diagnóstico específico registrado en la historia clínica del paciente (tabla HCPCEDIAGINT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'IDDIAGINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id diagnostico intervesiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'IDDIAGINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'IDDIAGINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la relación intervención-actividad. Clave primaria que registra cada asociación entre un diagnóstico de enfermería y una actividad específica del plan de cuidados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'IDINTACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda elId intervensiones actividades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'IDINTACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT', @level2type = N'COLUMN', @level2name = N'IDINTACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de actividades de PCE (Plan de Cuidado Enfermería) asociadas a diagnósticos de intervención en la historia clínica. Vincula cada actividad de cuidado con su diagnóstico de intervención y el control de PCE correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEINTACT';
