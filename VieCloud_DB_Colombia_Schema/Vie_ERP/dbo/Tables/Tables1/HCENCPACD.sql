CREATE TABLE [dbo].[HCENCPACD] (
    [AUTO]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOHCENCPACC] INT           NOT NULL,
    [DESPREENC]     VARCHAR (500) NULL,
    [RESPREPAC]     VARCHAR (500) NULL,
    CONSTRAINT [PK_HCENCPACD] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta registrada a la pregunta formulada al paciente durante la encuesta de historia clínica; captura respuestas abiertas o cerradas del interrogatorio clínico (VARCHAR 500, sin PII directo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'RESPREPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la respuesta de la pregunta realizada al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'RESPREPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'RESPREPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o enunciado de la pregunta formulada al paciente en la encuesta; texto de la interrogante clínica asociada (VARCHAR 500)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'DESPREENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'DESPREENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'DESPREENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) de la cabecera de encuesta del paciente; vincula línea detalle a encuesta principal HCENCPACC (INT, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'AUTOHCENCPACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico Cabecera ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'AUTOHCENCPACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'AUTOHCENCPACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de cada línea detalle de respuesta en la encuesta del paciente; clave primaria de la tabla (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preguntas y respuestas de la encuesta de satisfacción del paciente durante la atención clínica. Registra cada ítem evaluado por el paciente junto con su respuesta, vinculado al encabezado de la encuesta correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENCPACD';
