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
ALTER TABLE [dbo].[ADPA3047E] NOCHECK CONSTRAINT [FK_ADPA3047E_ADCENATEN];


GO
CREATE NONCLUSTERED INDEX [IX_ADPA3047E__CODCENATE__CODENTIDA__SERDEFENV]
    ON [dbo].[ADPA3047E]([CODCENATE] ASC, [CODENTIDA] ASC, [SERDEFENV] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Common.GETDATE() en que se realizó ultimo envio exitoso de email de reporte triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'DateSucessfulMailGeneration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha redondeada en que se realizó ultimo envio exitoso de email de reporte triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'DateLastExecution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Setea fecha de inicio de reporte cuando la Azure Function la detecta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'DateFirstExecution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Setea si se requiere el reporte de la escala 5 del triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Setea si se requiere el reporte de la escala 4 del triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Setea si se requiere el reporte de la escala 3 del triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Setea si se requiere el reporte de la escala 2 del triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Setea si se requiere el reporte de la escala 1 del triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ScaleTriage1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Setea el intervalo en horas de cuánto se va a disparar el servicio de envio de email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'IntervalOfTimeToSendEmails';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Setea si se va a realizar impresión de escala de triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'TriageStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Email de la entidad (Este campo es obsoleto, no se debe implementar!!!)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ENTIEMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Realizar Envío de Correo a la dirección territorial de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'ENVCORENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempos para el segundo re envio del correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'SEGREENVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempos para el primer re envio del correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'PRIREENVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'autonumérico de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Intrucciones para el Reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INSTREPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'URL Formulario Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'URLFORWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extension Numero Telefonico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'EXTTELLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicativo Numero Telefonico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INDTELLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Telefonico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'NUMTELLLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio que se utilizara por defecto para el reporte de los Formatos - Res 3047  1: FAX   2: E-Mail  3. Intercambio electronico  4: Llamada Telefonica  5: Formulario Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'SERDEFENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contraseña del Usuario Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'PASWORWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'CODUSUWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio Web para Intercambio Electronico de datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'URLSERWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Extension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'NUMEXTTEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Telefonico Fax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'NUMTELCEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicativo del Numero Telefonico Fax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'INDNUMTEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Centron de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'CODCENATE';

