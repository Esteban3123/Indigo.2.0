CREATE TABLE [dbo].[AGESPACTI] (
    [CODESPECI] CHAR (3) NOT NULL,
    [CODACTMED] CHAR (3) NOT NULL,
    [VARCAC]    INT      NULL,
    CONSTRAINT [PK_AGESPACTI] PRIMARY KEY CLUSTERED ([CODESPECI] ASC, [CODACTMED] ASC),
    CONSTRAINT [FK_AGESPACTI_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGESPACTI_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO
ALTER TABLE [dbo].[AGESPACTI] NOCHECK CONSTRAINT [FK_AGESPACTI_AGACTIMED];




GO
ALTER TABLE [dbo].[AGESPACTI] NOCHECK CONSTRAINT [FK_AGESPACTI_AGACTIMED];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variable CAC (Cuenta de Alto Costo) asociada a la relación especialidad-actividad médica. Indica qué variable de reporte (114.1 a 114.4) aplica cuando la cita se marca como cumplida. Valores: 1=Var114.1, 2=Var114.2, 3=Var114.3, 4=Var114.4, 5=Var114.5, 6=Var114.6. Usado para RIPS y control de atención en patologías de alto costo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPACTI', @level2type = N'COLUMN', @level2name = N'VARCAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si la relación especialidad actividad responderá alguna variable de la CAC, esto cuando la cita del paciente se pasa a cumplida    1: Responde variable 114.1  2: Responde variable 114.2  3: Responde variable 114.3  4: Responde variable 114.4  5: Responde variable 114.5  6: Responde variable 114.6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPACTI', @level2type = N'COLUMN', @level2name = N'VARCAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPACTI', @level2type = N'COLUMN', @level2name = N'VARCAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Actividad Médica (3 caracteres). Identifica el tipo de procedimiento, consulta o intervención realizada en la atención al paciente. Referencia a catálogo de actividades del ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPACTI', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la actividad Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPACTI', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPACTI', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Especialidad (3 caracteres). Identifica la especialidad médica (ej: medicina general, cirugía, pediatría). Clave foránea a tabla INESPECIA. Usado para clasificar servicios de salud y profesionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPACTI', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPACTI', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPACTI', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre especialidades médicas y actividades o actos médicos permitidos para el agendamiento, incluyendo la variación o capacidad de citas asociada a cada combinación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPACTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGESPACTI';
