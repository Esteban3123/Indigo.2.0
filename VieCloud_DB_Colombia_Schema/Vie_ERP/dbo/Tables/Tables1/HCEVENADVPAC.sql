CREATE TABLE [dbo].[HCEVENADVPAC] (
    [ID]              INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDVACUNAPLICADA] INT        NULL,
    [IDEVENADVERSO]   INT        NULL,
    [OBSEVENADVER]    CHAR (200) NULL,
    [FECHACREACION]   DATETIME   NULL,
    [USUCREACION]     CHAR (20)  NULL,
    CONSTRAINT [PK_HCEVENADVPAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCEVENADVPAC_HCEVENADV] FOREIGN KEY ([IDEVENADVERSO]) REFERENCES [dbo].[HCEVENADV] ([ID]),
    CONSTRAINT [FK_HCEVENADVPAC_HCVACADIPAC] FOREIGN KEY ([IDVACUNAPLICADA]) REFERENCES [dbo].[HCVACADIPAC] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación del registro; auditoría de quién generó la asociación evento adverso-vacuna. Tipo: CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'USUCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro; timestamp de cuándo se documentó el evento adverso de la vacuna aplicada. Tipo: DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas clínicas o descripción detallada del evento adverso post-vacuna (reacción, síntoma, complicación). Tipo: CHAR(200), PII sensible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'OBSEVENADVER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación Evento Adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'OBSEVENADVER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'OBSEVENADVER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del evento adverso asociado; referencia FK a tabla HCEVENADV. Busca: evento adverso, reacción vacuna, complicación post-inmunización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'IDEVENADVERSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Evento Adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'IDEVENADVERSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'IDEVENADVERSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la vacuna aplicada al paciente; referencia FK a tabla HCVACADIPAC. Busca: vacuna, inmunización, inoculación, dosis aplicada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'IDVACUNAPLICADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Vacuna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'IDVACUNAPLICADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'IDVACUNAPLICADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY); clave primaria de la relación evento adverso-vacuna. Consecutivo de registro. Tipo: INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de eventos adversos asociados a vacunas aplicadas a pacientes. Permite documentar reacciones o complicaciones ocurridas tras la administración de una vacuna, incluyendo observaciones clínicas y datos de auditoría de creación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEVENADVPAC';
