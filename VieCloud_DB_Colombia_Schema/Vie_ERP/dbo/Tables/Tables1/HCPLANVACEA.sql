CREATE TABLE [dbo].[HCPLANVACEA] (
    [ID]          INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCEVENADVID] INT        NULL,
    [IDHCPLANVAC] INT        NOT NULL,
    [OBSVACEVEAD] CHAR (200) NULL,
    [FECCREACION] DATETIME   NOT NULL,
    [USUCREACION] CHAR (20)  NULL,
    CONSTRAINT [PK_HCPLANVACEA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPLANVACEA_HCEVENADVID] FOREIGN KEY ([HCEVENADVID]) REFERENCES [dbo].[HCEVENADV] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o profesional de salud que registró el evento adverso en la vacunación (CHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del evento adverso relacionado con vacunas (DATETIME, trazabilidad)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'FECCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'FECCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'FECCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas, síntomas o reacciones adversas asociadas al plan de vacunación (CHAR 200, notas médicas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'OBSVACEVEAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'OBSVACEVEAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'OBSVACEVEAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del plan de vacunación del paciente vinculado a este evento adverso (INT, FK indirecto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'IDHCPLANVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del evento adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'IDHCPLANVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'IDHCPLANVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del evento adverso en vacunación, referencia a tabla HCEVENADV (INT, FK, Identification_Ofuscado=evento_adverso_PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'HCEVENADVID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla plan de vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'HCEVENADVID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'HCEVENADVID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de evento adverso en vacunas (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de eventos adversos asociados a planes de vacunación del paciente. Guarda las observaciones y la trazabilidad de cada evento adverso ocurrido durante o después de la aplicación de una vacuna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVACEA';
