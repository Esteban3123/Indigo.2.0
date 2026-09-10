CREATE TABLE [dbo].[ADPA3047E] (
    [CODCENATE]                   CHAR (10)    NOT NULL,
    [CODENTIDA]                   CHAR (9)     NOT NULL,
    [INDNUMTEL]                   CHAR (5)     NULL,
    [NUMTELCEN]                   CHAR (20)    NULL,
    [NUMEXTTEL]                   CHAR (6)     NULL,
    [URLSERWEB]                   CHAR (150)   NOT NULL,
    [CODUSUWEB]                   CHAR (50)    NOT NULL,
    [PASWORWEB]                   CHAR (50)    NOT NULL,
    [SERDEFENV]                   CHAR (1)     NOT NULL,
    [NUMTELLLA]                   CHAR (20)    NOT NULL,
    [INDTELLLA]                   CHAR (5)     NOT NULL,
    [EXTTELLLA]                   CHAR (6)     NOT NULL,
    [URLFORWEB]                   CHAR (150)   NOT NULL,
    [INSTREPOR]                   NCHAR (500)  NOT NULL,
    [INDAUDFOR]                   NUMERIC (18) NOT NULL,
    [AUTO]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PRIREENVI]                   INT          NULL,
    [SEGREENVI]                   INT          NULL,
    [ENVCORENT]                   BIT          NOT NULL,
    [ENTIEMAIL]                   CHAR (50)    NULL,
    [TriageStatus]                BIT          NULL,
    [IntervalOfTimeToSendEmails]  TINYINT      NULL,
    [ScaleTriage1]                BIT          NULL,
    [ScaleTriage2]                BIT          NULL,
    [ScaleTriage3]                BIT          NULL,
    [ScaleTriage4]                BIT          NULL,
    [ScaleTriage5]                BIT          NULL,
    [DateFirstExecution]          DATETIME     NULL,
    [DateLastExecution]           DATETIME     NULL,
    [DateSucessfulMailGeneration] DATETIME     NULL,
    CONSTRAINT [PK_ADPAR3047] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_ADPA3047E_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_ADPA3047E__CODCENATE__CODENTIDA__SERDEFENV]
    ON [dbo].[ADPA3047E]([CODCENATE] ASC, [CODENTIDA] ASC, [SERDEFENV] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último envío exitoso de correo electrónico con reporte de triage, capturada con GETDATE(); auditoría de envíos de reportes de urgencia/triaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'DateSucessfulMailGeneration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Common.GETDATE() en que se realizó ultimo envio exitoso de email de reporte triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'DateSucessfulMailGeneration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'DateSucessfulMailGeneration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) redondeada de la última ejecución del proceso de envío de reporte de triage; marca temporal de última gestión de triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'DateLastExecution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha redondeada en que se realizó ultimo envio exitoso de email de reporte triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'DateLastExecution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'DateLastExecution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de inicio del proceso de reporte de triage, detectada por la Azure Function; marca de arranque de servicio de triaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'DateFirstExecution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Setea fecha de inicio de reporte cuando la Azure Function la detecta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'DateFirstExecution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'DateFirstExecution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: si es 1, requiere generar y enviar reporte de escala 5 (nivel crítico/rojo) del triage; control de nivel máximo urgencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Setea si se requiere el reporte de la escala 5 del triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: si es 1, requiere generar y enviar reporte de escala 4 (nivel alto/naranja) del triage; control de urgencia alta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Setea si se requiere el reporte de la escala 4 del triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: si es 1, requiere generar y enviar reporte de escala 3 (nivel moderado/amarillo) del triage; control de urgencia moderada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Setea si se requiere el reporte de la escala 3 del triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: si es 1, requiere generar y enviar reporte de escala 2 (nivel bajo/verde) del triage; control de urgencia baja', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Setea si se requiere el reporte de la escala 2 del triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: si es 1, requiere generar y enviar reporte de escala 1 (nivel mínimo/azul) del triage; control de urgencia mínima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Setea si se requiere el reporte de la escala 1 del triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intervalo en horas (TINYINT) para ejecutar automáticamente el servicio de envío de correos de reporte; frecuencia de disparo de notificaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'IntervalOfTimeToSendEmails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Setea el intervalo en horas de cuánto se va a disparar el servicio de envio de email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'IntervalOfTimeToSendEmails';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'IntervalOfTimeToSendEmails';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: si es 1, habilita la impresión y generación de escala de triage; bandera de activación de reportes de triaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'TriageStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Setea si se va a realizar impresión de escala de triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'TriageStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'TriageStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'[OBSOLETO - NO IMPLEMENTAR] Correo electrónico de la entidad (Centro de Atención); campo deprecado, migrar a tabla de contactos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ENTIEMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Email de la entidad (Este campo es obsoleto, no se debe implementar!!!)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ENTIEMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ENTIEMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: si es 1, envía correo electrónico del reporte de triage a la Dirección Territorial de Salud; distribución de reportes a autoridad sanitaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ENVCORENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Realizar Envío de Correo a la dirección territorial de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ENVCORENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ENVCORENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intentos/tiempo (INT) para el segundo reenvío automático del correo de reporte; política de reintentos de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'SEGREENVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempos para el segundo re envio del correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'SEGREENVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'SEGREENVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intentos/tiempo (INT) para el primer reenvío automático del correo de reporte; política de primer reintento de envío', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'PRIREENVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempos para el primer re envio del correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'PRIREENVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'PRIREENVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY); clave primaria técnica de la tabla de configuración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'autonumérico de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría (NUMERIC 18); traza de cambios y responsable de la configuración del reporte de triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrucciones y configuración (NCHAR 500) para la generación del reporte de Resolución 3047; detalles de formato y contenido de envío', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INSTREPOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Intrucciones para el Reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INSTREPOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INSTREPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL (CHAR 150) del formulario web para capturas; enlace de integración con sistema de formularios electrónicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'URLFORWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'URL Formulario Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'URLFORWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'URLFORWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión telefónica (CHAR 6) del número de línea de llamadas; complemento de identificación de ramal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'EXTTELLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Extension Numero Telefonico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'EXTTELLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'EXTTELLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicativo/prefijo internacional (CHAR 5) del número de línea de llamadas; código país/región del teléfono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INDTELLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicativo Numero Telefonico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INDTELLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INDTELLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico (CHAR 20) de línea de llamadas para comunicación de reportes; teléfono principal de contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'NUMTELLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'NUMTELLLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'NUMTELLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio por defecto para envío de Resolución 3047 (CHAR 1): 1=FAX, 2=E-Mail, 3=Intercambio electrónico, 4=Llamada, 5=Formulario Web; canal de distribución de reportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'SERDEFENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio que se utilizara por defecto para el reporte de los Formatos - Res 3047  1: FAX   2: E-Mail  3. Intercambio electronico  4: Llamada Telefonica  5: Formulario Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'SERDEFENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'SERDEFENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña de autenticación (CHAR 50) para el usuario de servicio web; credencial PII-Ofuscado para intercambio electrónico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'PASWORWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contraseña del Usuario Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'PASWORWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'PASWORWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/usuario (CHAR 50) para autenticación en servicio web de intercambio electrónico; credencial de integración de datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'CODUSUWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'CODUSUWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'CODUSUWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL (CHAR 150) del servicio web para intercambio electrónico de reportes; endpoint de integración RIPS/triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'URLSERWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio Web para Intercambio Electronico de datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'URLSERWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'URLSERWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión telefónica (CHAR 6) del centro de atención; ramal complementario del número principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'NUMEXTTEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Extension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'NUMEXTTEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'NUMEXTTEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico/FAX (CHAR 20) del centro de atención; contacto principal por fax del centro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'NUMTELCEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico Fax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'NUMTELCEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'NUMTELCEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicativo/prefijo internacional (CHAR 5) del número telefónico de FAX; código país del teléfono del centro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INDNUMTEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicativo del Numero Telefonico Fax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INDNUMTEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INDNUMTEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador (CHAR 9) de la Entidad afiliadora/aseguradora (FK); referencia a INENTIDAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador (CHAR 10) del Centro de Atención/Unidad Funcional (FK); referencia a ADCENATEN para reportes de triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centron de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de conexión y parámetros de envío electrónico por centro de atención y entidad: almacena credenciales del servicio web, URLs, datos de contacto telefónico, instrucciones de reporte, configuración de reenvíos automáticos de correo y escalas de triage habilitadas para cada centro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E';
