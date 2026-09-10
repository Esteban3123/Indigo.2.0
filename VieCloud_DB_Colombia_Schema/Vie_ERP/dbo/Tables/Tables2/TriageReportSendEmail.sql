CREATE TABLE [dbo].[TriageReportSendEmail] (
    [TriageReportSendEmailId] NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [AUTO]                    INT            NOT NULL,
    [Email]                   VARCHAR (60)   NOT NULL,
    [Observations]            VARCHAR (2000) NULL,
    CONSTRAINT [PK_TriageReportSendEmail] PRIMARY KEY CLUSTERED ([TriageReportSendEmailId] ASC),
    CONSTRAINT [FK_ADPA3047E_AUTO] FOREIGN KEY ([AUTO]) REFERENCES [dbo].[ADPA3047E] ([AUTO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas o comentarios que se incluyen en el cuerpo del correo electrónico al enviar el reporte de triage; texto libre para contexto adicional de la notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones que quedan en el cuerpo del correo al que se le envía el reporte del triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correo electrónico (VARCHAR 60) destinataria para el envío automático del reporte de triage; contacto de notificación del proceso de clasificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Email al que se enviará el reporte de triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del autonumérico (FK) que referencia la tabla ADPA3047E (Admisión - Parámetros 3047 - Entidades); clave que vincula la configuración de la entidad de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del autonumérico de la tabla ADPA3047E (Admisión parámetros 3047 - Entidades)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (NUMERIC 18, PK Identity) que identifica el registro de configuración de envío de reporte de triage por correo; clave de cabecera de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'TriageReportSendEmailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera de email al que se envía la notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'TriageReportSendEmailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'TriageReportSendEmailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de destinatarios de correo electrónico para el envío automático de reportes de triage. Guarda las direcciones de email y observaciones asociadas a cada configuración de envío automático de informes de clasificación de urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail';
